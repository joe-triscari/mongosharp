using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using MongoSharp.Model;
using WeifenLuo.WinFormsUI.Docking;

namespace MongoSharp
{
    public static class EditorWindowManager
    {
        private static readonly List<EditorWindow> _editorWindows = new List<EditorWindow>();

        public static void OpenNew()
        {            
            var doc = CreateNewDocument();
            doc.IsNew = true;
            _editorWindows.Add(doc);
            doc.Show(WindowManager.Instance.MainDockPanel, DockState.Document);
        }

        public static void OpenNew(string connectionName, string databaseName, string collectionName)
        {
            var doc = CreateNewDocument();
            doc.IsLoading = true;
            if (!string.IsNullOrWhiteSpace(collectionName))
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

        public static void SaveActive(bool saveAs = false)
        {
            SaveDocument(FindActiveDocument(), saveAs);
        }

        public static bool SaveAll(List<EditorWindow> modified)
        {            
            if (modified.Any())
            {
                var frmSaveAll = new FormSaveAll(modified) {StartPosition = FormStartPosition.CenterParent};
                frmSaveAll.ShowDialog(WindowManager.Instance.MainForm);
                return frmSaveAll.WasCancelled;
            }

            return false;
        }

        public static bool SaveAll()
        {
            return SaveAll(_editorWindows.FindAll(w => w.IsModified));
        }

        public static bool SaveDocument(EditorWindow editorWindow, bool saveAs=false)
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

        public static void Open()
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

        public static void Open(string fileName)
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

        public static bool SaveQuery(EditorWindow editorWindow)
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

        public static bool Close(EditorWindow editorWindow, bool callFormClose)
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

        public static bool CloseAllButThis(EditorWindow editorWindow)
        {
            return CloseWindows(from w in _editorWindows
                                where w != editorWindow
                                select w);
        }

        public static bool CloseAll()
        {
            return CloseWindows(_editorWindows);
        }

        private static bool CloseWindows(IEnumerable<EditorWindow> windows)
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

        public static void OnEditorChanged(EditorWindow doc)
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

        private static EditorWindow FindActiveDocument()
        {
            return WindowManager.Instance.MainDockPanel.Documents.FirstOrDefault(content => content.DockHandler.IsActivated) as EditorWindow;
        }

        private static IDockContent FindDocument(string text)
        {

            return WindowManager.Instance.MainDockPanel.Documents.FirstOrDefault(content => content.DockHandler.TabText.TrimEnd('*', ' ') == text);
        }

        private static EditorWindow CreateNewDocument()
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

        public static void SetEditorPreferences(IEditorPreferences preferences)
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
