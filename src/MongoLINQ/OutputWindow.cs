using System;
using MongoSharp.Model.Interface;
using WeifenLuo.WinFormsUI.Docking;
using MongoSharp.Model;

namespace MongoSharp
{    
    public partial class OutputWindow : DockContent, IOutputWindow
    {
        public OutputWindow()
        {
            InitializeComponent();
        }

        public string Output
        {
            get => this.txtOutput.Text;
            set
            {               
                this.txtOutput.Text = value;
                Show();
            }
        }

        public void AppendOutput(string text)
        {
            if(String.IsNullOrEmpty(text))
                return;

            string line=String.Empty;
            if (txtOutput.Text != String.Empty)
                line = Environment.NewLine;

            if(Settings.Instance.Preferences.OutputShowTimestamp)
            {
                string format = Settings.Instance.Preferences.OutputTimestampFormat;
                if (string.IsNullOrWhiteSpace(format))
                    format = "hh:mm:ss.ffff";

                line += "[" + DateTime.Now.ToString(format) + "] " + text;
            }
            else
                line += text;

            txtOutput.Text += line;
            txtOutput.SelectionStart = txtOutput.TextLength;
            txtOutput.ScrollToCaret();

            Show(this.DockPanel, DockState.DockBottom);
           // Show();
        }

        private void txtOutput_DoubleClick(object sender, EventArgs e)
        {
            int cursorPosition = txtOutput.SelectionStart;
            int lineIndex = txtOutput.GetLineFromCharIndex(cursorPosition);
            string lineText = txtOutput.Lines[lineIndex];

            if (lineText.StartsWith("(Line "))
            {
                lineText = lineText.Replace("(Line ", "");
                int pos = lineText.IndexOf(',');
                if (pos >= 0)
                {
                    lineText = lineText.Substring(0, pos);
                    if (WindowManager.Instance.ActiveEditorWindow != null)
                    {
                        WindowManager.Instance.ActiveEditorWindow.GoToLine(Convert.ToInt32(lineText));
                    }
                }
            }
        }
    }
}
