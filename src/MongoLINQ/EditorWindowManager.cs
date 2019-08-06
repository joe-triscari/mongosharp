using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using MongoSharp.Model;
using MongoSharp.Model.Interface;
using WeifenLuo.WinFormsUI.Docking;

namespace MongoSharp
{
    static public class EditorWindowManager
    {
        static private readonly List<EditorWindow> _editorWindows = new List<EditorWindow>();

        static public void OpenNew()
        {            
            var doc = CreateNewDocument();
            doc.IsNew = true;
            _editorWindows.Add(doc);
            doc.Show(WindowManager.Instance.MainDockPanel, DockState.Document);
        }

        static public void OpenNew(string connectionName, string databaseName, string collectionName)
        {
            var doc = CreateNewDocument();
            doc.IsLoading = true;
            if (!String.IsNullOrWhiteSpace(collectionName))
                doc.EditorText = "(from x in collection.AsQueryable()" + Environment.NewLine + "select x).Take(200)";
            doc.IsNew = true;
            doc.SetCollection(connectionName, databaseName, collectionName);
            _editorWindows.Add(doc);
            doc.Show(WindowManager.Instance.MainDockPanel, DockState.Document);

            new Thread(o =>
            {
                Thread.Sleep(1000);
                doc.IsLoading = false;
            }).Start();
        }

        static public void SaveActive(bool saveAs = false)
        {
            SaveDocument(FindActiveDocument(), saveAs);
        }

        static public bool SaveAll(List<EditorWindow> modified)
        {            
            if (modified.Any())
            {
                var frmSaveAll = new FormSaveAll(modified) {StartPosition = FormStartPosition.CenterParent};
                frmSaveAll.ShowDialog(WindowManager.Instance.MainForm);
                return frmSaveAll.WasCancelled;
            }

            return false;
        }

        static public bool SaveAll()
        {
            return SaveAll(_editorWindows.FindAll(w => w.IsModified));
        }

        static public bool SaveDocument(EditorWindow editorWindow, bool saveAs=false)
        {
            bool wasSaved = false;

            if (editorWindow != null)
            {
                if (editorWindow.IsNew || saveAs)
                {
                    var saveDialog = new SaveFileDialog { Filter = "lnq|*.lnq", FileName = editorWindow.TabText.TrimEnd('*') };
                    saveDialog.FileOk += (sender, args) =>
                        {
                            var file = editorWindow.ToMongoSharpFile();
                            File.WriteAllText(saveDialog.FileName, file.ToSaveFormat());
                            editorWindow.IsNew = false;
                            editorWindow.FileName = saveDialog.FileName;
                            var fi = new FileInfo(saveDialog.FileName);
                            editorWindow.TabText = fi.Name;
                            wasSaved = true;
                            Settings.Instance.AddRecentlyUsed(saveDialog.FileName);
                        };
                    saveDialog.ShowDialog(WindowManager.Instance.MainForm);
                }
                else
                {
                    var file = editorWindow.ToMongoSharpFile();
                    File.WriteAllText(editorWindow.FileName, file.ToSaveFormat());
                    editorWindow.TabText = editorWindow.TabText.TrimEnd('*');
                    wasSaved = true;
                }

                editorWindow.IsModified = !wasSaved;
            }

            return wasSaved;
        }

        static public void Open()
        {
            var openDialog = new OpenFileDialog { Filter = "lnq|*.lnq", Multiselect = true};
            openDialog.FileOk += (sender, args) =>
                {
                    foreach (string fileName in openDialog.FileNames)
                    {
                        var exisingDoc = _editorWindows.Find(x => x.FileName != null && x.FileName.Equals(fileName, StringComparison.CurrentCultureIgnoreCase));
                        if (exisingDoc != null)
                        {
                            exisingDoc.Activate();
                            continue;
                        }

                        var doc = CreateNewDocument();
                        doc.IsLoading = true;
                        doc.IsNew = false;                        
                        doc.FileName = fileName;
                        doc.TabText = new FileInfo(doc.FileName).Name;
                        doc.FromMongoSharpFile(new MongoSharpFile(File.ReadAllText(doc.FileName)));
                        _editorWindows.Add(doc);
                        doc.Show(WindowManager.Instance.MainDockPanel, DockState.Document);
                        Settings.Instance.AddRecentlyUsed(fileName);

                        new Thread(o =>
                            {
                                Thread.Sleep(1000);
                                doc.IsLoading = false;
                            }).Start();
                    }
                };
            openDialog.ShowDialog(WindowManager.Instance.MainForm); 
        }

        static public void Open(string fileName)
        {
            var exisingDoc = _editorWindows.Find(x => x.FileName != null && x.FileName.Equals(fileName, StringComparison.CurrentCultureIgnoreCase));
            if (exisingDoc != null)
            {
                exisingDoc.Activate();
                return;
            }

            var doc = CreateNewDocument();
            doc.IsLoading = true;
            doc.IsNew = false;
            doc.FileName = fileName;
            doc.TabText = new FileInfo(doc.FileName).Name;
            doc.FromMongoSharpFile(new MongoSharpFile(File.ReadAllText(doc.FileName)));
            _editorWindows.Add(doc);
            doc.Show(WindowManager.Instance.MainDockPanel, DockState.Document);
            Settings.Instance.AddRecentlyUsed(fileName);
            new Thread(o =>
            {
                Thread.Sleep(1000);
                doc.IsLoading = false;
            }).Start();
        }

        static public bool SaveQuery(EditorWindow editorWindow)
        {
            if (editorWindow.IsModified)
            {
                var result = MessageBox.Show("Do you want to save changes to " + editorWindow.TabText.TrimEnd('*') + "?",
                                             "Save?",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                    return false;

                if (result == DialogResult.Yes)
                {
                    bool saved = SaveDocument(editorWindow);
                    if (!saved)
                        return false;
                }
            }

            return true;
        }

        static public bool Close(EditorWindow editorWindow, bool callFormClose)
        {
            if (!SaveQuery(editorWindow))
                return false;

            if (callFormClose)
            {
                editorWindow.CloseHandledByManager = true;
                editorWindow.Close();
            }
                
            
            _editorWindows.Remove(editorWindow);

            return true;
        }

        static public bool CloseAllButThis(EditorWindow editorWindow)
        {
            return CloseWindows(from w in _editorWindows
                                where w != editorWindow
                                select w);
        }

        static public bool CloseAll()
        {
            return CloseWindows(_editorWindows);
        }

        static private bool CloseWindows(IEnumerable<EditorWindow> windows)
        {
            bool shouldCancel = true;

            var modified = new List<EditorWindow>();
            foreach (EditorWindow editorWindow in windows.ToList())
            {
                if (editorWindow.IsModified)
                {
                    modified.Add(editorWindow);
                }
                else
                {
                    editorWindow.CloseHandledByManager = true;
                    editorWindow.Close();
                    _editorWindows.Remove(editorWindow);
                }
            }

            if (!SaveAll(modified))
            {
                modified.ForEach(w =>
                    {
                        w.CloseHandledByManager = true;
                        w.Close();
                        _editorWindows.Remove(w);
                    });
                shouldCancel = false;
            }

            return shouldCancel;
        }

        static public void OnEditorChanged(EditorWindow doc)
        {
            if(doc.IsLoading)
                return;
            
            if (!doc.IsModified)
            {
                doc.IsModified = true;
                string tabText = doc.TabText;
                if (!tabText.EndsWith("*"))
                {
                    doc.TabText = tabText + "*";
                }
            }
        }

        static private EditorWindow FindActiveDocument()
        {
            return WindowManager.Instance.MainDockPanel.Documents.FirstOrDefault(content => content.DockHandler.IsActivated) as EditorWindow;
        }

        static private IDockContent FindDocument(string text)
        {

            return WindowManager.Instance.MainDockPanel.Documents.FirstOrDefault(content => content.DockHandler.TabText.TrimEnd('*', ' ') == text);
        }

        static private EditorWindow CreateNewDocument()
        {
            var editor = new EditorWindow();

            int count = 1;
            string text = "Editor" + count;
            while (FindDocument(text) != null)
            {
                count++;
                text = "Editor" + count;
            }
            editor.TabText = text;
            return editor;
        }

        static public void SetEditorPreferences(IEditorPreferences preferences)
        {
            foreach(var w in _editorWindows)
            {
                w.ScintillaEditor.BackColor = preferences.EditorBackColor;
                w.ScintillaEditor.SetLanguage(preferences.EditorSyntaxLanguage);
                w.ScintillaEditor.ShowLineNumbers(preferences.ShowEditorLineNumbers);
            }
        }
    }
}
