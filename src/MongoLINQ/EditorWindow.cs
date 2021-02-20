using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using MongoSharp.Model;
using ScintillaNET;
using WeifenLuo.WinFormsUI.Docking;

namespace MongoSharp
{
    public partial class EditorWindow : DockContent
    {
        private List<PropertyData> _props;
        private BackgroundWorker _bgWorker = new BackgroundWorker { WorkerSupportsCancellation = true };

        public EditorWindow()
        {
            InitializeComponent();
            LoadConnections();
            toolStripComboBoxModes.SelectedIndex = 0;
            this.KeyPreview = true;

            try
            {
                scintillaLinqCode.BackColor = Settings.Instance.Preferences.EditorBackColor;
            } catch
            {
                scintillaLinqCode.BackColor = System.Drawing.Color.White;
            }
            
            scintillaLinqCode.SetLanguage(Settings.Instance.Preferences.EditorSyntaxLanguage);
            scintillaLinqCode.ShowLineNumbers(Settings.Instance.Preferences.ShowEditorLineNumbers);
            scintillaLinqCode.Caret.Style = CaretStyle.Line;
            scintillaLinqCode.AllowDrop = true;

            tabControlCollection.OnExecuteCode += tabControlCollection_OnExecuteCode;
            toolStripConnection.AllowDrop = true;
            toolStripConnection.DragEnter += toolStripConnection_DragEnter;
            toolStripConnection.DragDrop += toolStripConnection_DragDrop;

            scintillaLinqCode.CodePositionMapper = (text, pos) =>
                {
                    var mode = toolStripComboBoxModes.SelectedItem as string;
                    if (mode == MongoSharpQueryMode.Json)
                        return -1;

                    var connection = toolStripComboBoxConnection.SelectedItem as MongoConnectionInfo;
                    var database = toolStripComboBoxDatabase.SelectedItem as MongoDatabaseInfo;

                    if (toolStripComboBoxCollection.SelectedItem is string collection && connection != null && database != null)
                    {
                        new MongoCodeGenerator().GenerateMongoCode(database, collection, text, mode, out var injectedCodeStartPos);
                        return pos + injectedCodeStartPos;
                    }

                    return -1;
                };
            scintillaLinqCode.GetCode = () =>
                {
                    var mode = toolStripComboBoxModes.SelectedItem as string;
                    if (mode == MongoSharpQueryMode.Json)
                        return "";

                    var connection = toolStripComboBoxConnection.SelectedItem as MongoConnectionInfo;
                    var database = toolStripComboBoxDatabase.SelectedItem as MongoDatabaseInfo;
                    if (toolStripComboBoxCollection.SelectedItem is string collection && connection != null && database != null)
                    {
                        string linqQuery = scintillaLinqCode.Text;
                        string code = new MongoCodeGenerator().GenerateMongoCode(database, collection, linqQuery, mode, out _);
                        return code;
                    }

                    return "";
                };
        }

        #region Properties
        public bool IsNew { get; set; }
        public bool IsModified { get; set; }
        public bool IsLoading { get; set; }
        public string FileName { get; set; }
        public Scintilla ScintillaEditor => scintillaLinqCode;

        public bool CloseHandledByManager { get; set; 
        }
        public string SaveText => IsNew ? TabText.Replace("*", "") : FileName;

        public string EditorText
        {
            get => scintillaLinqCode.Text;
            set => scintillaLinqCode.Text = value;
        }
        #endregion        

        #region Events
        #region Auto Complete

        void scintillaLinqCode_CharAdded(object sender, CharAddedEventArgs e)
        {
            //const char C = '.';           
            //if (e.Ch == C)
            //{
            //    DoAutoComplete(-2);
            //}
        }
        #endregion

        private void EditorWindow_Load(object sender, EventArgs e)
        {
            WindowManager.Instance.ControlActivated(tabControlCollection.TabPages[0].Controls.OfType<Scintilla>().First());
        }

        void toolStripConnection_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        void toolStripConnection_DragDrop(object sender, DragEventArgs e)
        {            
            if (e.Data.GetDataPresent(typeof(MongoCollectionInfo)))
            {
                var mongoCollection = e.Data.GetData(typeof(MongoCollectionInfo)) as MongoCollectionInfo;
                toolStripComboBoxConnection.SelectedItem = mongoCollection.Database.Connection;
                toolStripComboBoxDatabase.SelectedItem = mongoCollection.Database;
                toolStripComboBoxCollection.SelectedItem = mongoCollection.Name;
            }
            else if (e.Data.GetDataPresent(typeof (MongoDatabaseInfo)))
            {
                var mongoDatabase = e.Data.GetData(typeof(MongoDatabaseInfo)) as MongoDatabaseInfo;
                toolStripComboBoxConnection.SelectedItem = mongoDatabase.Connection;
                toolStripComboBoxDatabase.SelectedItem = mongoDatabase;
                toolStripComboBoxCollection.SelectedItem = null;
            }
            else if (e.Data.GetDataPresent(typeof(MongoConnectionInfo)))
            {
                var mongoConnection = e.Data.GetData(typeof(MongoConnectionInfo)) as MongoConnectionInfo;
                toolStripComboBoxConnection.SelectedItem = mongoConnection;
                toolStripComboBoxDatabase.SelectedItem = null;
                toolStripComboBoxCollection.SelectedItem = null;
            }

        }

        private void tabControlCollection_OnExecuteCode(string code)
        {
            ExecuteCode(code, "Raw");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            EditorWindowManager.Close(this, true);
        }

        private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorWindowManager.CloseAllButThis(this);
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorWindowManager.CloseAll();
        }

        private void scintillaLinqCode_DocumentChange(object sender, NativeScintillaEventArgs e)
        {
            EditorWindowManager.OnEditorChanged(this);
        }

        private void scintillaLinqCode_Load(object sender, EventArgs e)
        {
            this.IsLoading = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ExecuteCode(GetQuery(), toolStripComboBoxModes.SelectedItem as string);
        }

        private void toolStripButtonNavigateToColl_Click(object sender, EventArgs e)
        {
            var database = toolStripComboBoxDatabase.SelectedItem as MongoDatabaseInfo;
            var collection = toolStripComboBoxCollection.SelectedItem as string;
            WindowManager.Instance.ConnectionBrowserWindow.SelectCollection(database.GetCollectionInfo(collection));
        }

        private void EditorWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
                ExecuteCode(GetQuery(), toolStripComboBoxModes.SelectedItem as string);

            if (e.Control && e.KeyCode == Keys.S)
                EditorWindowManager.SaveActive();            
        }
       
        /// <summary>
        /// Collection selected. Update Schema view.
        /// </summary>
        private void toolStripComboBoxCollection_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnDropdownChanged();
            toolStripButtonExecute.Enabled = true;
            if (toolStripComboBoxConnection.SelectedItem is MongoConnectionInfo connection)
            {
                if (toolStripComboBoxDatabase.SelectedItem is MongoDatabaseInfo database)
                {
                    if (toolStripComboBoxCollection.SelectedItem is string collection)
                    {

                        try
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            this.Refresh();

                            MongoCollectionInfo collectionInfo = database.GetCollectionInfo(collection);
                            if(!collectionInfo.HasModel && !Settings.Instance.Preferences.AllowAutoGeneratedModels)
                            {
                                toolStripComboBoxCollection.SelectedItem = null;
                                WindowManager.Instance.OutputWindow.AppendOutput(
                                    $"Model for collection '{collection}' has not been defined yet.");
                            }
                            else
                            {
                                _props = new MongoCodeGenerator().GetPropertiesPaths(database, collection);
                                WindowManager.Instance.SchemaWindow.Properties = _props.Select(p => p.ToString()).ToArray();
                                scintillaLinqCode.AutoComplete.List = _props.Select(p => p.Path).ToList();
                                WindowManager.Instance.ConnectionBrowserWindow.CollectionSelected(collectionInfo);
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            toolStripButtonExecute.Enabled = false;
                            WindowManager.Instance.SchemaWindow.Properties = new[] { ex.Message };
                            WindowManager.Instance.OutputWindow.Output = $"Error: {ex.Message}";
                        }
                        finally
                        {
                            Cursor.Current = Cursors.Default;
                        }

                    }
                }

            }
        }

        /// <summary>
        /// Connection selected
        /// </summary>
        private void toolStripComboBoxConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnDropdownChanged();

            toolStripComboBoxDatabase.Items.Clear();
            toolStripComboBoxDatabase.Text = "";
            toolStripComboBoxCollection.Items.Clear();
            toolStripComboBoxCollection.Text = "";
            WindowManager.Instance.SchemaWindow.Clear();

            if (toolStripComboBoxConnection.SelectedItem is MongoConnectionInfo connection)
            {
                var databases = connection.Databases;
                foreach (var database in databases)
                {
                    toolStripComboBoxDatabase.Items.Add(database);
                }
            }
        }

        /// <summary>
        /// Database selected
        /// </summary>
        private void toolStripComboBoxDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnDropdownChanged();
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                toolStripComboBoxCollection.Items.Clear();
                toolStripComboBoxCollection.Text = "";
                WindowManager.Instance.SchemaWindow.Clear();

                if (toolStripComboBoxConnection.SelectedItem is MongoConnectionInfo connection)
                {
                    if (toolStripComboBoxDatabase.SelectedItem is MongoDatabaseInfo database)
                    {
                        var collections = new MongoConnectionHelper().GetCollectionNames(database);
                        foreach (var collection in collections)
                        {
                            toolStripComboBoxCollection.Items.Add(collection);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WindowManager.Instance.OutputWindow.Output = $"Connection Error: {ex.Message}";
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void toolStripButtonViewCode_Click(object sender, EventArgs e)
        {
            string linqQuery = GetQuery();
            if (string.IsNullOrWhiteSpace(linqQuery))
                return;

            if (toolStripComboBoxConnection.SelectedItem is MongoConnectionInfo connection)
            {
                if (toolStripComboBoxDatabase.SelectedItem is MongoDatabaseInfo database)
                {
                    if (toolStripComboBoxCollection.SelectedItem is string collection)
                    {
                        try
                        {
                            Cursor.Current = Cursors.WaitCursor;

                            var collectionInfo = database.GetCollectionInfo(collection);
                            if (!collectionInfo.HasModel && !Settings.Instance.Preferences.AllowAutoGeneratedModels)
                                throw new Exception($"Model for collection '{collection}' has not been defined yet.");

                            int injectedCodeStartPos;
                            string code = new MongoCodeGenerator().GenerateMongoCode(database, collection, linqQuery,
                                                                                     toolStripComboBoxModes.SelectedItem as string,
                                                                                     out injectedCodeStartPos);
                            AddCodeViewTabPage(code);

                            WindowManager.Instance.ConnectionBrowserWindow.CollectionSelected(collectionInfo);
                        }
                        catch (Exception ex)
                        {
                            AddCodeViewTabPage(ex.Message);
                        }
                        finally
                        {
                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
            }
        }

        private void toolStripButtonFormatCode_Click(object sender, EventArgs e)
        {
            string linqQuery = GetQuery();
            if (string.IsNullOrWhiteSpace(linqQuery))
                return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                var mode = toolStripComboBoxModes.SelectedItem as string;
                scintillaLinqCode.Text = new CodeFormatter().FormatCode(linqQuery, mode);
            }
            catch (Exception ex)
            {
                WindowManager.Instance.OutputWindow.AppendOutput(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void EditorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseHandledByManager)
                return;

            if (e.CloseReason == CloseReason.MdiFormClosing)
                e.Cancel = true;
            else
                e.Cancel = !EditorWindowManager.Close(this, false);          
        }

        // Resize all combo boxes on the toolstrip.
        private void toolStripComboBoxConnection_DropDown(object sender, EventArgs e)
        {
            var cb = (ToolStripComboBox)sender;

            using (System.Drawing.Graphics graphics = CreateGraphics())
            {
                int maxWidth = 0;
                foreach (object obj in cb.Items)
                {
                    System.Drawing.SizeF area = graphics.MeasureString(obj.ToString(), cb.Font);
                    maxWidth = Math.Max((int)area.Width, maxWidth);
                }
                cb.DropDownWidth = maxWidth;
            }
        }
        #endregion

        #region Functions
        public MongoSharpFile ToMongoSharpFile()
        {
            return new MongoSharpFile
                        {
                            ConnectionName = toolStripComboBoxConnection.SelectedItem == null
                                                ? "" : ((MongoConnectionInfo)toolStripComboBoxConnection.SelectedItem).Name,
                            DatabaseName = toolStripComboBoxDatabase.SelectedItem == null
                                                ? "" : ((MongoDatabaseInfo)toolStripComboBoxDatabase.SelectedItem).Name,
                            CollectionName = toolStripComboBoxCollection.SelectedItem == null
                                                ? "" : ((string)toolStripComboBoxCollection.SelectedItem),
                            QueryMode = toolStripComboBoxModes.SelectedItem as string,
                            QueryText = EditorText
                        };
        }

        public void FromMongoSharpFile(MongoSharpFile file)
        {
            toolStripComboBoxModes.SelectedItem = file.QueryMode;
            EditorText = file.QueryText;
            SetCollection(file.ConnectionName, file.DatabaseName, file.CollectionName);           
        }

        public void InsertText(string text)
        {
            scintillaLinqCode.InsertText(text);
        }

        private void OnDropdownChanged()
        {
            toolStripButtonNavigateToColl.Enabled = toolStripComboBoxConnection.SelectedItem is MongoConnectionInfo connection && 
                                                    toolStripComboBoxDatabase.SelectedItem is MongoDatabaseInfo database && 
                                                    toolStripComboBoxCollection.SelectedItem is string collection;
        }

        public void SetCollection(string connectionName, string databaseName, string collectionName)
        {
            toolStripComboBoxConnection.SelectedItem =
                toolStripComboBoxConnection.Items.Cast<MongoConnectionInfo>()
                                           .ToList()
                                           .Find(x => x.Name == connectionName);
            toolStripComboBoxDatabase.SelectedItem =
                toolStripComboBoxDatabase.Items.Cast<MongoDatabaseInfo>()
                                         .ToList()
                                         .Find(x => x.Name == databaseName);
            toolStripComboBoxCollection.SelectedItem =
                toolStripComboBoxCollection.Items.Cast<string>()
                                           .ToList()
                                           .Find(x => x == collectionName);
        }

        private void LoadConnections()
        {
            toolStripComboBoxConnection.Items.Clear();
            foreach (var connection in Settings.Instance.Connections)
            {
                toolStripComboBoxConnection.Items.Add(connection);
            }
        }        

        private void ExecuteCode(string rawQuery, string mode)
        {
            if (toolStripButtonExecute.Text == "Cancel")
            {
                toolStripButtonExecute.Enabled = false;
               _bgWorker.CancelAsync();
                return;
            }

            if (string.IsNullOrWhiteSpace(rawQuery))
            {
                return;
            }

            if (toolStripComboBoxConnection.SelectedItem is MongoConnectionInfo connection)
            {
                if (toolStripComboBoxDatabase.SelectedItem is MongoDatabaseInfo database)
                {
                    if (toolStripComboBoxCollection.SelectedItem is string collection)
                    {
                        try
                        {                            
                            Cursor.Current = Cursors.WaitCursor;
                            if (Settings.Instance.Preferences.OutputClearOnExecute)
                            {
                                WindowManager.Instance.OutputWindow.Output = "";
                            }

                            var collectionInfo = database.GetCollectionInfo(collection);
                            if (!collectionInfo.HasModel && !Settings.Instance.Preferences.AllowAutoGeneratedModels)
                            {
                                throw new Exception($"Model for collection '{collection}' has not been defined yet.");
                            }

                            string executableQuery = mode == "Raw"
                                              ? rawQuery
                                              : new MongoCodeGenerator().GenerateMongoCode(database, collection, rawQuery, mode, out _);

                            WindowManager.Instance.ConnectionBrowserWindow.CollectionSelected(collectionInfo);

                            _bgWorker = new BackgroundWorker { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
                            _bgWorker.DoWork += (sender, e) =>
                                        {
                                            var bg = (BackgroundWorker)sender;                                      

                                            List<QueryResult> data = null;
                                            Exception threadException = null;
                                            var oThread = new Thread(delegate() {
                                                try
                                                {
                                                    data = new MongoDynamicCodeRunner().CompileAndRun((string)e.Argument, mode, new MongoSharpTextWriter(bg));
                                                } 
                                                catch(Exception ex)
                                                {
                                                    threadException = ex;
                                                }                                                
                                            });

                                            oThread.Start();
                                            while (!oThread.IsAlive)
                                            {
                                                Thread.Sleep(10);
                                            }
                                            while (oThread.IsAlive && !bg.CancellationPending)
                                            {
                                                Thread.Sleep(250);
                                            }

                                            if (threadException != null)
                                            {
                                                throw threadException;
                                            }
                                            
                                            if (bg.CancellationPending)
                                            {
                                                oThread.Abort();
                                                while (oThread.IsAlive)
                                                {
                                                    Thread.Sleep(100);
                                                }
                                                e.Cancel = true;
                                            }
                                            else
                                            {
                                                e.Result = data;
                                            }
                                        };
                            _bgWorker.RunWorkerCompleted += (sender, e) =>
                                        {
                                            if (e.Error != null)
                                            {
                                                Exception ex = e.Error;
                                                string err = ex.Message;
                                                while(ex.InnerException !=null)
                                                {
                                                    ex = ex.InnerException;
                                                    err += "\r\n" + ex.Message;
                                                }

                                                if (!err.Contains("Compile error"))
                                                {
                                                    err = "Check your Model for correct data types!\r\n\r\n" + err;
                                                }

                                                WindowManager.Instance.OutputWindow.AppendOutput(err);
                                            }                                                
                                            else if (e.Cancelled)
                                            {
                                                WindowManager.Instance.OutputWindow.AppendOutput("Operation cancelled");
                                            }
                                            else
                                            {
                                                var results = (List<QueryResult>)e.Result;
                                                foreach (QueryResult queryResult in results)
                                                {
                                                    AddResultsTabPage(queryResult);
                                                }
                                            }
                                            toolStripButtonExecute.Text = "Execute";
                                            toolStripButtonExecute.Image = MongoSharp.Properties.Resources.Execute;
                                            toolStripButtonExecute.Enabled = true;
                                        };
                            _bgWorker.ProgressChanged += (sender, e) => WindowManager.Instance.OutputWindow.AppendOutput(e.UserState == null ? "" : e.UserState.ToString());
                            _bgWorker.RunWorkerAsync(executableQuery);

                            toolStripButtonExecute.Text = "Cancel";
                            toolStripButtonExecute.Image = MongoSharp.Properties.Resources.Cancel20x20;
                        }
                        catch (Exception ex)
                        {
                            WindowManager.Instance.OutputWindow.Output = ex.Message;
                        }
                        finally
                        {
                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
            }
        }

        private void AddResultsTabPage(QueryResult queryResult)
        {
            var database = toolStripComboBoxDatabase.SelectedItem as MongoDatabaseInfo;
            var collection = toolStripComboBoxCollection.SelectedItem as string;
            var collectionInfo = database.GetCollectionInfo(collection);
 
            var newTabPage = new TabPage($"Results - {tabControlCollection.GetTabCountForText("Results") + 1}");

            var queryResultControl = new UserControlQueryResults { Dock = DockStyle.Fill, OnCreateTableFromResults = AddTableResultsTabPage };

            queryResultControl.LoadResults(queryResult, collectionInfo);
            newTabPage.Controls.Add(queryResultControl);
            tabControlCollection.TabPages.Add(newTabPage);
            newTabPage.Select();

            tabControlCollection.SelectTab(newTabPage);
            queryResultControl.PostLoadProcessing();
        }

        private void AddCodeViewTabPage(string code)
        {
            code = new CodeFormatter().FormatCode(code, MongoSharpQueryMode.CSharpStatements);
            var page = new TabPage($"Code - {tabControlCollection.GetTabCountForText("Code") + 1}");
            var results = new UserControlCodeView { Dock = DockStyle.Fill };
            page.Controls.Add(results);
            tabControlCollection.TabPages.Add(page);
            page.Select();

            var scintillaControl = (Scintilla)results.Controls[0];
            scintillaControl.SetLanguage("cs");
            scintillaControl.ShowLineNumbers(true);

            scintillaControl.Text = code;
            tabControlCollection.SelectTab(page);
        }

        private void AddTableResultsTabPage(string code)
        {
            var page = new TabPage($"Table - {tabControlCollection.GetTabCountForText("Table") + 1}");
            var results = new UserControlCodeView { Dock = DockStyle.Fill };
            page.Controls.Add(results);
            tabControlCollection.TabPages.Add(page);
            page.Select();

            var scintillaControl = (Scintilla)results.Controls[0];
            scintillaControl.SetLanguage("mssql");
            scintillaControl.ShowLineNumbers(true);

            scintillaControl.Text = code;
            tabControlCollection.SelectTab(page);
        }

        private string GetQuery()
        {
            string linqQuery = scintillaLinqCode.Selection.Text;
            if (string.IsNullOrWhiteSpace(linqQuery))
            {
                linqQuery = scintillaLinqCode.Text;
            }

            return linqQuery;
        }
        public string GetSelectedText()
        {
            return scintillaLinqCode.Selection.Text; 
        }

        public void GoToLine(int line)
        {
            tabControlCollection.SelectedIndex = 0;
            scintillaLinqCode.GoTo.Line(line-1);
            scintillaLinqCode.Focus();
        }
        #endregion                     

        private void EditorWindow_Activated(object sender, EventArgs e)
        {
            WindowManager.Instance.ActiveEditorWindow = this;
            WindowManager.Instance.EditorRibbonPanel.Enabled = true;
            WindowManager.Instance.EditorRibbonPanel.Visible = true;

            WindowManager.Instance.ClipBoardRibbonPanel.Enabled = true;
            WindowManager.Instance.ClipBoardRibbonPanel.Visible = true;
        }

        private void EditorWindow_Deactivate(object sender, EventArgs e)
        {
            WindowManager.Instance.ActiveEditorWindow = null;
            WindowManager.Instance.EditorRibbonPanel.Enabled = false;
            WindowManager.Instance.EditorRibbonPanel.Visible = true;

            WindowManager.Instance.ClipBoardRibbonPanel.Enabled = false;
            WindowManager.Instance.ClipBoardRibbonPanel.Visible = true;
        }

        private void scintillaLinqCode_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(MongoCollectionInfo)))
            {
                if (e.Data.GetData(typeof(MongoCollectionInfo)) is MongoCollectionInfo mongoCollection && mongoCollection.HasModel)
                {
                    var code =
                        $"var {mongoCollection.Name[0].ToString().ToLower() + mongoCollection.Name.Substring(1)} = GetCollection<{mongoCollection.Namespace + "." + mongoCollection.Models[0].RootClassName}>();";

                    //var pos = scintillaLinqCode.PositionFromPoint(e.X, e.Y);
                    scintillaLinqCode.InsertText(/*pos,*/ code);
                }                
            }
        }

        private void scintillaLinqCode_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }
    }
}
