using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using ScintillaNET;
using Microsoft.CodeAnalysis;
using MongoSharp.Model;
using MongoSharp.Model.CodaAnalysis;

namespace MongoSharp
{
    public interface ICodeAnalysisProvider
    {

    }

    public class StandardScintilla : ScintillaNET.Scintilla
    {
        ToolStripItem _miUndo;
        ToolStripItem _miRedo;
        ToolStripItem _miCut;
        ToolStripItem _miCopy;
        ToolStripItem _miDelete;
        ToolStripItem _miSelectAll;
        ToolStripItem _miCreateCodeSnippet;
        ToolStripItem _miInsertCodeSnippet;
        ToolStripItem _miShowLineNumbers;
        private ImageList imageListCodeComplete;
        private System.ComponentModel.IContainer components;

        private static readonly Font _callTipFont = new Font(
                   new FontFamily("Segoe UI"),
                   12,
                   FontStyle.Regular,
                   GraphicsUnit.Pixel);
        private readonly Timer _diagnosticTimer = new Timer();

        public Func<string, int, int> CodePositionMapper = null;
        public Func<string> GetCode = null;

        public StandardScintilla()
            : base()
        {
            InitializeComponent();
           // Indicators[0].Style = IndicatorStyle.RoundBox;
           // Indicators[0].Color = Color.Green;
            this.SelectionChanged += StandardScintilla_SelectionChanged;
            this.CharAdded += StandardScintilla_CharAdded;
            this.KeyDown += StandardScintilla_KeyDown;

            this.AutoComplete.IsCaseSensitive = false;
            this.AutoComplete.MaxHeight = 25;
            this.AutoComplete.FillUpCharacters = "";
            // scintillaCodeView.AutoComplete.SingleLineAccept = true;
            this.AutoComplete.IsCaseSensitive = false;
            this.AutoComplete.RegisterImages(imageListCodeComplete, Color.FromArgb(255, 0, 255));

            this.NativeInterface.SetMouseDwellTime(250);
            this.DwellStart += scintillaCodeView_DwellStart;
            this.DwellEnd += scintillaCodeView_DwellEnd;

            _diagnosticTimer.Interval = 2000;
            _diagnosticTimer.Tick += _diagnosticTimer_Tick;
            _diagnosticTimer.Start();

            this.LostFocus += StandardScintilla_LostFocus;
            this.GotFocus += StandardScintilla_GotFocus;
        }

        void StandardScintilla_GotFocus(object sender, EventArgs e)
        {
            if (_diagnosticTimer != null)
                _diagnosticTimer.Start();
        }

        void StandardScintilla_LostFocus(object sender, EventArgs e)
        {
            if (_diagnosticTimer != null)
                _diagnosticTimer.Stop();
        }

        void _diagnosticTimer_Tick(object sender, EventArgs e)
        {
            if(this.IsDisposed)
            {
                _diagnosticTimer.Stop();
                return;
            }

            int offset = CodePositionMapper != null ? CodePositionMapper(this.Text, 0) : 0;
            string fullCode = GetCode == null ? this.Text : GetCode();
            if (fullCode == String.Empty)
                return;

            var diagnostics = new CodeAnalysisService().GetDiagnostics(fullCode);
            this.Indicators.Reset();
            foreach(var d in diagnostics)
            {
                int start = d.Location.SourceSpan.Start - offset;
                if (start < 0)
                    continue;

                int length = d.Location.SourceSpan.IsEmpty ? 1 : d.Location.SourceSpan.Length;
                var r = new Range(start, start + length, this);
                Indicator ind = null;

                if(d.Severity == DiagnosticSeverity.Error)
                {                    
                    ind = this.Indicators[0];
                    ind.Style = IndicatorStyle.Squiggle;
                    ind.Color = Color.Red;                    
                }
                else if(d.Severity == DiagnosticSeverity.Info)
                {
                    ind = this.Indicators[1];
                    ind.Style = IndicatorStyle.Plain;
                    ind.Color = Color.Green; 
                }

                if(ind != null)
                    r.SetIndicator(ind.Number);
            }
        }

        void StandardScintilla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.J)
            {
                DoAutoComplete('.');
            }
        }

        void StandardScintilla_CharAdded(object sender, CharAddedEventArgs e)
        {
            if (e.Ch == '.' || e.Ch == '(' || e.Ch == ',')
            {
                var t = new Timer { Interval = 10, Tag = sender };

                t.Tick += (obj, ev) =>
                          {
                              ((Timer)obj).Stop();
                              DoAutoComplete(e.Ch);
                          };
                t.Start();
            }
        }

        private void DoAutoComplete(char c)
        {
            const int OFFSET = 0; //2 (old code)

            string fullCode = GetCode == null ? this.Text : GetCode();
            if (fullCode == String.Empty)
                return;

            int curpos = CodePositionMapper != null
                ? CodePositionMapper(this.Text, this.CurrentPos - OFFSET)
                : this.CurrentPos - OFFSET;
            if (curpos < 0)
                return;

            #region NewCode
            SymbolResult result = new CodeAnalysisService().LookupSymbols(fullCode, curpos, c);
            var completionList = result.GetAutoCompleteList();
            if (completionList.Any())
            {
                this.AutoComplete.Show(5, completionList);
            }
            var overloadList = result.GetOverLoadList();
            if(overloadList.Any())
            {
                this.CallTip.BackColor = Color.FromArgb(231, 232, 236);
                this.CallTip.ForeColor = Color.Black;
                this.Styles[ScintillaNET.StylesCommon.CallTip].Font = _callTipFont;

                var ol = new ScintillaNET.OverloadList(overloadList.ToArray());
                this.CallTip.ShowOverload(ol); 
            }
            return;
            #endregion 

            //if (c == '.')
            //{
            //    List<string> symbols = new CodeAnalysisServiceOld().GetCompletionListAtPosition(fullCode, curpos);
            //    if (symbols.Any())
            //    {
            //        this.AutoComplete.Show(5, symbols);
            //    }
            //}
            //else if(c == '(' || c == ',')
            //{
            //    if (c == ',') curpos += 2;

            //    List<string> results = new CodeAnalysisServiceOld().GetMethodCompletionListAtPosition(fullCode, curpos);
            //    if(results.Any())
            //    {
            //        this.CallTip.BackColor = Color.FromArgb(231, 232, 236);
            //        this.CallTip.ForeColor = Color.Black;
            //        this.Styles[ScintillaNET.StylesCommon.CallTip].Font = _callTipFont;
                
            //        var ol = new ScintillaNET.OverloadList(results.ToArray());
            //        this.CallTip.ShowOverload(ol);                    
            //    }
            //}
        }

        void scintillaCodeView_DwellEnd(object sender, ScintillaNET.ScintillaMouseEventArgs e)
        {
            this.CallTip.Hide();
        }

        void scintillaCodeView_DwellStart(object sender, ScintillaNET.ScintillaMouseEventArgs e)
        {            
            var curpos = this.PositionFromPoint(e.X, e.Y);
            int mappedPos = CodePositionMapper != null ? CodePositionMapper(this.Text, curpos) : curpos;
            if (mappedPos < 0) return;

            string fullCode = GetCode == null ? this.Text : GetCode();

            if (mappedPos > fullCode.Length - 1)
            {
                this.CallTip.Hide();
                return;
            }

            var currentChar = fullCode[mappedPos];
            if (Char.IsWhiteSpace(currentChar))
            {
                this.CallTip.Hide();
                return;
            }

            var callTipText = new CodeAnalysisService().GetCallTipTextAtPosition(fullCode, mappedPos);
            if (!String.IsNullOrWhiteSpace(callTipText))
            {
                this.CallTip.BackColor = Color.FromArgb(231, 232, 236);
                this.CallTip.ForeColor = Color.Black;
                this.Styles[ScintillaNET.StylesCommon.CallTip].Font = _callTipFont;
                
                if(callTipText.Length > 150 && callTipText.IndexOf('\n') < 0)
                {
                    var chars = callTipText.ToCharArray();
                    for(int i = 149; i < chars.Length; i++)
                    {
                        if(Char.IsWhiteSpace(chars[i]))
                        {
                            chars[i] = '\n';
                            break;
                        }
                    }
                    callTipText = new String(chars);
                }
                this.CallTip.Show(callTipText, curpos);
            }
            else
            {
                this.CallTip.Hide();
            }
        }

        void StandardScintilla_SelectionChanged(object sender, EventArgs e)
        {            
            NativeInterface.IndicatorClearRange(0, Text.Length);

            if (String.IsNullOrWhiteSpace(Selection.Text))
                return;

            foreach (Range r in FindReplace.FindAll(Selection.Text))
                r.SetIndicator(0);

        }

        private void initContextMenu()
        {
            var cm = this.ContextMenuStrip = new ContextMenuStrip();

            this._miUndo = new ToolStripMenuItem("Undo", Properties.Resources.Undo, (s, ea) => this.UndoRedo.Undo());
            cm.Items.Add(this._miUndo);

            this._miRedo = new ToolStripMenuItem("Redo", Properties.Resources.Redo, (s, ea) => this.UndoRedo.Redo());
            cm.Items.Add(this._miRedo);

            cm.Items.Add(new ToolStripSeparator());

            this._miCut = new ToolStripMenuItem("Cut", Properties.Resources.Cut, (s, ea) => this.Clipboard.Cut());
            cm.Items.Add(_miCut);

            this._miCopy = new ToolStripMenuItem("Copy", Properties.Resources.Copy, (s, ea) => this.Clipboard.Copy());
            cm.Items.Add(_miCopy);

            cm.Items.Add(new ToolStripMenuItem("Paste", Properties.Resources.Paste, (s, ea) => this.Clipboard.Paste()));

            this._miDelete = new ToolStripMenuItem("Delete", Properties.Resources.ErasePng, (s, ea) => this.NativeInterface.ReplaceSel(""));
            cm.Items.Add(_miDelete);

            cm.Items.Add(new ToolStripSeparator());

            this._miSelectAll = new ToolStripMenuItem("Select All", null, (s, ea) => this.Selection.SelectAll());
            cm.Items.Add(_miSelectAll);

            cm.Items.Add(new ToolStripSeparator());

            var dropDown = new List<ToolStripItem>();
            foreach(var codeSnippet in Model.Settings.Instance.CodeSnippets)
            {
                var ddi = new ToolStripMenuItem(codeSnippet.Name, null, (s, ea) =>
                                                                        {
                                                                            WindowManager.Instance.ActiveEditorWindow
                                                                                         .InsertText(codeSnippet.Code);
                                                                        })
                          {
                              ToolTipText = !String.IsNullOrWhiteSpace(codeSnippet.Description)? codeSnippet.Description : codeSnippet.Name
                          };
                dropDown.Add(ddi);
            }
            this._miInsertCodeSnippet = new ToolStripMenuItem("Insert Code Snippet", Properties.Resources.CodeSnippet16x16, dropDown.ToArray());
            cm.Items.Add(this._miInsertCodeSnippet);

            this._miCreateCodeSnippet = new ToolStripMenuItem("Create Code Snippet", null, (s, ea) => WindowManager.Instance.MainForm.DoCreateCodeSnippet());

            cm.Items.Add(_miCreateCodeSnippet);

            cm.Items.Add(new ToolStripSeparator());

            _miShowLineNumbers = new ToolStripMenuItem("Show Line Numbers", null,
                (s, ea) =>
                {
                    var mi = s as ToolStripMenuItem;
                    if (mi.Checked)
                    {
                        Margins[0].Width = 0;
                        mi.CheckState = CheckState.Unchecked;
                    }
                    else
                    {
                        this.ShowLineNumbers(true);
                        mi.CheckState = CheckState.Checked;
                    }
                });
            ((ToolStripMenuItem) _miShowLineNumbers).Checked = this.IsShowingLineNumbers();
            cm.Items.Add(_miShowLineNumbers);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                initContextMenu();

                _miUndo.Enabled = this.UndoRedo.CanUndo;
                _miRedo.Enabled = this.UndoRedo.CanRedo;
                _miCut.Enabled = this.Clipboard.CanCut;
                _miCopy.Enabled = this.Clipboard.CanCopy;
                _miDelete.Enabled = this.Selection.Length > 0;
                _miSelectAll.Enabled = this.TextLength > 0 && this.TextLength != this.Selection.Length;
                _miCreateCodeSnippet.Enabled = this.Selection.Length > 0;
                _miInsertCodeSnippet.Enabled = WindowManager.Instance.ActiveEditorWindow != null;
            }
            else
                base.OnMouseDown(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StandardScintilla));
            this.imageListCodeComplete = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // imageListCodeComplete
            // 
            this.imageListCodeComplete.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCodeComplete.ImageStream")));
            this.imageListCodeComplete.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListCodeComplete.Images.SetKeyName(0, "Method_pub_ext.png");
            this.imageListCodeComplete.Images.SetKeyName(1, "Method_pub.png");
            this.imageListCodeComplete.Images.SetKeyName(2, "Property_pub.png");
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
