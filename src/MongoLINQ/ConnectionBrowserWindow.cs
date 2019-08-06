﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoSharp.Model;
using WeifenLuo.WinFormsUI.Docking;
using MongoSharp.TreeNodeTag;

namespace MongoSharp
{
    public interface IConnectionBrowserWindow
    {
        void SelectCollection(MongoCollectionInfo connectionInfo);
        void CollectionSelected(MongoCollectionInfo collectionInfo);
    }

    public partial class ConnectionBrowserWindow : DockContent, IConnectionBrowserWindow
    {
        private const int IMG_CONNECT = 0;
        private const int IMG_DATABASE = 1;
        private const int IMG_COLLECTION = 2;
        private const int IMG_MODEL = 3;
        private const int IMG_FIELD = 4;
        private const int IMG_INDEX = 5;
        private const int IMG_COLLECTION_NO_MODEL = 6;

        public ConnectionBrowserWindow()
        {
            InitializeComponent();
            tvConnections.Indent = 8;
        }

        private TreeNode _previouslySelectedNode;

        private void ConnectionBrowserWindow_Load(object sender, System.EventArgs e)
        {
            foreach (MongoConnectionInfo connection in Settings.Instance.Connections)
            {
                var connectionNode = tvConnections.Nodes.Add(connection.Name, connection.Name, IMG_CONNECT, IMG_CONNECT);
                connectionNode.Tag = new ConnectionNodeTag(connection);
                connectionNode.Nodes.Add("placeholder");
                //AddConnection(connectionNode, connection);
            }
            if (tvConnections.Nodes.Count > 0) 
                tvConnections.Nodes[0].EnsureVisible();

            tvConnections.Refresh();
            tvConnections.ItemDrag += tvConnections_ItemDrag;
        }

        private void tvConnections_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var connectionTag = e.Node.Tag as ConnectionNodeTag;
            if (connectionTag != null && !connectionTag.IsLoaded)
            {
                Cursor.Current = Cursors.WaitCursor;
                e.Node.Nodes.Clear();
                connectionTag.IsLoaded = true;
                AddConnection(e.Node, connectionTag.MongoConnectionInfo);                
                Cursor.Current = Cursors.Default;
            }
        }

        void tvConnections_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var treeNode = e.Item as TreeNode;
            if (treeNode != null && treeNode.Tag != null &&
                (treeNode.Tag is CollectionNodeTag || treeNode.Tag is DatabaseNodeTag ||
                 treeNode.Tag is ConnectionNodeTag))
            {
                var collectionTag = treeNode.Tag as CollectionNodeTag;
                if (collectionTag != null && !collectionTag.MongoCollectionInfo.HasModel && !Settings.Instance.Preferences.AllowAutoGeneratedModels)
                    return;

                DoDragDrop((treeNode.Tag as MongoSharp.TreeNodeTag.TreeNodeTag).GetData(), DragDropEffects.Link);
            }
        }

        public void SelectCollection(MongoCollectionInfo collectionInfo)
        {
            if (_previouslySelectedNode != null)
            {
                _previouslySelectedNode.Collapse();

                // Just toggle expansion if same collection as last time.
                if (_previouslySelectedNode.Parent != null && _previouslySelectedNode.Parent.Tag is CollectionNodeTag)
                {
                    if ((_previouslySelectedNode.Parent.Tag as CollectionNodeTag).MongoCollectionInfo.Path == collectionInfo.Path)
                    {
                        _previouslySelectedNode = null;
                        return;
                    }                        
                }                    
            }

            TreeNode[] connNode = tvConnections.Nodes.Find(collectionInfo.Path, true);
            if (connNode.Length == 0)
            {
                connNode = tvConnections.Nodes.Find(collectionInfo.Database.Connection.Name, false);
                if (connNode.Length > 0)
                {
                    connNode[0].Nodes.Clear();
                    Cursor.Current = Cursors.WaitCursor;
                    AddConnection(connNode[0], (connNode[0].Tag as ConnectionNodeTag).MongoConnectionInfo);
                    connNode = tvConnections.Nodes.Find(collectionInfo.Path, true);
                    Cursor.Current = Cursors.Default;
                }
            }

            if (connNode.Length == 1 && connNode[0].Nodes.Count > 0)
            {
                TreeNode modelNode = connNode[0].Nodes[0];
                modelNode.Expand();
                tvConnections.SelectedNode = modelNode;
                _previouslySelectedNode = modelNode;
            }
        }

        public void CollectionSelected(MongoCollectionInfo collectionInfo)
        {
            if (collectionInfo == null) return;

            TreeNode[] nodes = tvConnections.Nodes.Find(collectionInfo.Path, true);
            if (nodes.Length == 1)
            {
                TreeNode collectionNode = nodes[0];
                collectionNode.ImageIndex = collectionInfo.HasModel ? IMG_COLLECTION : IMG_COLLECTION_NO_MODEL;
                collectionNode.SelectedImageIndex = collectionNode.ImageIndex;

                var modelNode = collectionNode.Nodes.Find("Model", false)[0];
                if(modelNode != null && modelNode.Nodes.Count==0 && collectionInfo.HasModel)
                {
                    var properties = new MongoCodeGenerator().GetProperties(collectionInfo.Database, collectionNode.Text);
                    foreach (var property in properties)
                    {
                        modelNode.Nodes.Add(property, property, 4, 4);
                    }
                    modelNode.Parent.Expand();
                    modelNode.Expand();
                }
            }
        }

        #region Create Tree Nodes
        private void AddConnection(TreeNode connectionNode, MongoConnectionInfo connection)
        {            
           // if (connection.IsValid())
           // {
                foreach (MongoDatabaseInfo database in connection.Databases)
                {
                    if (database.IsValid())
                    {
                        var databaseNode = connectionNode.Nodes.Add(database.Name, database.Name, IMG_DATABASE, IMG_DATABASE);
                        databaseNode.Tag = new DatabaseNodeTag(database);
                        AddDatabase(databaseNode, database);
                        databaseNode.Expand();
                    }
                }

                connectionNode.Expand();
           // }
        }

        private void AddDatabase(TreeNode databaseNode, MongoDatabaseInfo databaseInfo)
        {            
            if (databaseInfo.IsValid())
            {
                List<string> collections = new MongoConnectionHelper().GetCollectionNames(databaseInfo);
                foreach (string collection in collections)
                {
                    MongoCollectionInfo collectionInfo = databaseInfo.GetCollectionInfo(collection)
                                                         ?? databaseInfo.AddCollection(collection);

                    var collectionNode = databaseNode.Nodes.Add(collectionInfo.Path, collection, IMG_COLLECTION, IMG_COLLECTION);
                    AddCollection(collectionNode, databaseInfo, collection);
                }
            }
        }

        private void AddCollection(TreeNode collectionNode, MongoDatabaseInfo databaseInfo, string collection)
        {
            MongoCollectionInfo collectionInfo = databaseInfo.GetCollectionInfo(collection);

            collectionNode.Tag = new CollectionNodeTag(collectionInfo);
            try
            {
                collectionNode.ToolTipText = "Count: " + collectionInfo.GetMongoCollection().Count(); 
            } catch {}
            
            var modelNode = collectionNode.Nodes.Add("Model", "Model", IMG_MODEL, IMG_MODEL);
            modelNode.Tag = new ModelNodeTag();

            if (collectionInfo != null)
            {                
                if (collectionInfo.HasModel)
                {
                    try
                    {
                        var properties = new MongoCodeGenerator().GetProperties(databaseInfo, collection);
                        foreach (var property in properties)
                        {
                            modelNode.Nodes.Add(property, property, IMG_FIELD, IMG_FIELD);
                        }
                    } catch { }
                }
                else
                {
                    collectionNode.ImageIndex = IMG_COLLECTION_NO_MODEL;
                    collectionNode.SelectedImageIndex = IMG_COLLECTION_NO_MODEL;
                }

                var indexNode = collectionNode.Nodes.Add("Indexes", "Indexes", IMG_INDEX, IMG_INDEX);
                indexNode.Tag = "Index";
                AddIndexNodes(indexNode, collectionInfo);
            }
        }

        private void AddIndexNodes(TreeNode indexNode, MongoCollectionInfo collectionInfo)
        {
            indexNode.Nodes.Clear();
            GetIndexesResult indexes = collectionInfo.GetMongoCollection().GetIndexes();
            foreach (IndexInfo index in indexes)
            {
                var idxNode = indexNode.Nodes.Add(index.Name, index.Name, IMG_FIELD, IMG_FIELD);
                idxNode.Tag = new IndexNodeTag { IndexInfo = index, MongoCollectionInfo = collectionInfo };
            }
        }
        #endregion

        private void tvConnections_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tvConnections.SelectedNode = e.Node;
                if (e.Node.Tag is CollectionNodeTag)
                {
                    newEditorToolStripMenuItem.Visible = (e.Node.Tag as CollectionNodeTag).MongoCollectionInfo.HasModel || Settings.Instance.Preferences.AllowAutoGeneratedModels;
                    cmsCollectionNode.Show(tvConnections, new Point(e.X, e.Y));
                }
                else if (e.Node.Tag is ConnectionNodeTag)
                {
                    cmsConnection.Show(tvConnections, new Point(e.X, e.Y));
                }
                else if (e.Node.Tag is DatabaseNodeTag)
                {
                    cmsDatabaseNode.Show(tvConnections, new Point(e.X, e.Y));
                }
                else if (e.Node.Tag is IndexNodeTag)
                {
                    cmsIndex.Show(tvConnections, new Point(e.X, e.Y));
                }
                else if (e.Node.Tag is ModelNodeTag)
                {
                    cmsModel.Show(tvConnections, new Point(e.X, e.Y));
                }
            }
        }

        private void newEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var col = (tvConnections.SelectedNode.Tag as CollectionNodeTag).MongoCollectionInfo;
            EditorWindowManager.OpenNew(col.Database.Connection.Name, col.Database.Name, col.Name);
        }

        /// <summary>
        /// Perform action on node double-click
        /// </summary>
        private void tvConnections_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
        }

        /// <summary>
        /// Add new connection
        /// </summary>
        private void toolStripButtonAddConnection_Click(object sender, EventArgs e)
        {
            var frmAddConnection = new FormAddConnection
                {
                    OnAddConnection = (conn) =>
                        {
                            var connectionNode = tvConnections.Nodes.Add(conn.Name, conn.Name, 0, 0);
                            connectionNode.Tag = new ConnectionNodeTag(conn);

                            AddConnection(connectionNode, conn);
                        }
                };
            frmAddConnection.ShowDialog(this);
        }

        /// <summary>
        /// Remove connection
        /// </summary>
        private void removeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove this connection?", "Confirm",
                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var connToRemove = (tvConnections.SelectedNode.Tag as ConnectionNodeTag).MongoConnectionInfo;

                Settings.Instance.Connections.RemoveAll(x => x.Id == connToRemove.Id);
                Settings.Instance.Save();
                tvConnections.Nodes.Remove(tvConnections.SelectedNode);
            }
        }

        /// <summary>
        /// Open edit Model window.
        /// </summary>
        private void editModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var modelNode = tvConnections.SelectedNode;
            var collectionNode = modelNode.Parent;

            var dbInfo = (modelNode.Parent.Parent.Tag as DatabaseNodeTag).MongoDatabaseInfo;
            MongoCollectionInfo colInfo = dbInfo.Collections.Find(x => x.Name == collectionNode.Text);
            if (colInfo == null)
            {
                colInfo = new MongoCollectionInfo { Database = dbInfo, Name = collectionNode.Text };
                dbInfo.Collections.Add(colInfo);
            }

            var frSchema = new FormSchema {CollectionInfo = colInfo};
            frSchema.ShowDialog();

            // Update tree view if Model changed.
            if (frSchema.WasUpdated)
            {
                modelNode.Nodes.Clear();
                if (colInfo.HasModel)
                {
                    collectionNode.ImageIndex = IMG_COLLECTION;
                    collectionNode.SelectedImageIndex = IMG_COLLECTION;
                    collectionNode.Name = colInfo.Path;
                    var properties = new MongoCodeGenerator().GetProperties(dbInfo, collectionNode.Text);
                    foreach (var property in properties)
                    {
                        modelNode.Nodes.Add(property, property, 4, 4);
                    }
                }
                else
                {
                    collectionNode.ImageIndex = IMG_COLLECTION_NO_MODEL;
                    collectionNode.SelectedImageIndex = IMG_COLLECTION_NO_MODEL;
                }
            }
        }

        /// <summary>
        /// Refresh tree nodes
        /// </summary>
        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var node = tvConnections.SelectedNode;
            if (node.Tag is DatabaseNodeTag)
            {
                node.Nodes.Clear();
                AddDatabase(node, (node.Tag as DatabaseNodeTag).MongoDatabaseInfo);
            }
            if (node.Tag is ConnectionNodeTag)
            {
                node.Nodes.Clear();
                AddConnection(node, (node.Tag as ConnectionNodeTag).MongoConnectionInfo);
            }
            if(node.Tag is CollectionNodeTag)
            {
                node.Nodes.Clear();
                var col = (node.Tag as CollectionNodeTag).MongoCollectionInfo;
                AddCollection(node, col.Database, col.Name);
            }
            Cursor = Cursors.Default;
        }

        private void tvConnections_AfterSelect(object sender, TreeViewEventArgs e)
        {
            toolStripButtonRefresh.Enabled = e.Node.Tag is DatabaseNodeTag || e.Node.Tag is ConnectionNodeTag || e.Node.Tag is CollectionNodeTag;
        }

        /// <summary>
        /// Drop collection
        /// </summary>
        private void dropToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = tvConnections.SelectedNode;
            if (node.Tag is CollectionNodeTag)
            {
                var resp = MessageBox.Show("Are you sure you want to drop this collection?", "Confirm",
                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.Yes)
                {
                    try
                    {
                        var collectionInfo = (node.Tag as CollectionNodeTag).MongoCollectionInfo;
                        var mongoDatabase = collectionInfo.Database.GetMongoDatabase();
                        mongoDatabase.DropCollection(collectionInfo.Name);
                        tvConnections.Nodes.Remove(node);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Failed to drop collection", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void addDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = tvConnections.SelectedNode;
            if (node.Tag is ConnectionNodeTag)
            {
                var addDb = new FormAddDatabase {StartPosition = FormStartPosition.CenterScreen};
                addDb.ShowDialog();
                if(!addDb.Canceled)
                {
                    var connectionInfo = (node.Tag as ConnectionNodeTag).MongoConnectionInfo;
                    var mongoServer = connectionInfo.GetMongoServer("");
                    if(!mongoServer.DatabaseExists(addDb.DatabaseName))
                    {
                        var resp = MessageBox.Show(String.Format("Database '{0}' doesn't exist. Do you want to create it?", addDb.DatabaseName),
                                                    "Create Database?", 
                                                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (resp != DialogResult.Yes)
                            return;
                    }

                    try
                    {
                        mongoServer.GetDatabase(addDb.DatabaseName);
                        var dbInfo = connectionInfo.AddDatabase(addDb.DatabaseName);
                        var databaseNode = node.Nodes.Add(dbInfo.Name, dbInfo.Name, 1, 1);
                        databaseNode.Tag = new DatabaseNodeTag(dbInfo);
                        AddDatabase(databaseNode, dbInfo);
                        databaseNode.Expand();
                        Settings.Instance.Save();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Failed to add database.\r\n\r\n" + ex.Message, "Operation Failed", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }                
            }
        }

        private void insertImportDocumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmInsert = new FormInsertImportDocs
                            {
                                CollectionInfo = (tvConnections.SelectedNode.Tag as CollectionNodeTag).MongoCollectionInfo,
                                StartPosition = FormStartPosition.CenterScreen
                            };
            frmInsert.ShowDialog(this);
        }

        private void createCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = tvConnections.SelectedNode;
            if (node.Tag is DatabaseNodeTag)
            {
                var addCol = new FormAddCollection {StartPosition = FormStartPosition.CenterScreen};
                addCol.ShowDialog();
                if (!addCol.Canceled)
                {
                    var databaseInfo = (node.Tag as DatabaseNodeTag).MongoDatabaseInfo;
                    var mongoDb = databaseInfo.GetMongoDatabase();
                    if (mongoDb.CollectionExists(addCol.CollectionName))
                    {
                        MessageBox.Show(String.Format("A collection with the name '{0}' already exists.", addCol.CollectionName), "Duplicate");
                    }
                    else
                    {
                        try
                        {
                            var result = mongoDb.CreateCollection(addCol.CollectionName);
                            if(!result.Ok)
                                throw new Exception(result.ErrorMessage);

                            var collectionInfo = databaseInfo.AddCollection(addCol.CollectionName);                         
                            var collectionNode = node.Nodes.Add(collectionInfo.Path, addCol.CollectionName, 2, 2);

                            AddCollection(collectionNode, databaseInfo, addCol.CollectionName);
                            SortBranch(tvConnections, node);
                            Settings.Instance.Save();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Failed to create collection\r\n\r\n" + ex.Message, "Operation Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void SortBranch(TreeView treeView1, TreeNode parentNode)
        {
            TreeNode[] nodes;
            if (parentNode == null)
            {
                nodes = new TreeNode[treeView1.Nodes.Count];
                treeView1.Nodes.CopyTo(nodes, 0);
            }
            else
            {
                nodes = new TreeNode[parentNode.Nodes.Count];
                parentNode.Nodes.CopyTo(nodes, 0);
            }
            Array.Sort(nodes, new NodeSorter());
            treeView1.BeginUpdate();
            if (parentNode == null)
            {
                treeView1.Nodes.Clear();
                treeView1.Nodes.AddRange(nodes);
            }
            else
            {
                parentNode.Nodes.Clear();
                parentNode.Nodes.AddRange(nodes);
            }
            treeView1.EndUpdate();
        }

        public class NodeSorter : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                var tx = (TreeNode) x;
                var ty = (TreeNode) y;
                return tx.Name.CompareTo(ty.Name);
            }
        }

        private void cmsCollectionNode_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void createIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = tvConnections.SelectedNode;
            if (node == null || !(node.Tag is CollectionNodeTag))
                return;

            var frmAddIndex = new FormAddIndex
                                {
                                    StartPosition = FormStartPosition.CenterParent,
                                    MongoCollectionInfo = (node.Tag as CollectionNodeTag).MongoCollectionInfo
                                };

            frmAddIndex.ShowDialog();
            if(!frmAddIndex.WasCancelled)
            {
                var indexNode = node.Nodes.Find("Indexes",true);
                if (indexNode != null && indexNode.Length > 0)
                    AddIndexNodes(indexNode[0], (node.Tag as CollectionNodeTag).MongoCollectionInfo);
            }
        }

        private void indexPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = tvConnections.SelectedNode;
            if (node == null || !(node.Tag is IndexNodeTag))
                return;

            var frmIndex = new FormIndexProperties
                            {
                                StartPosition = FormStartPosition.CenterParent,
                                IndexNodeTag = node.Tag as IndexNodeTag
                            };

            frmIndex.ShowDialog();
        }

        private void dropIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = tvConnections.SelectedNode;
            if (node.Tag is IndexNodeTag)
            {
                var resp = MessageBox.Show("Are you sure you want to drop this index?", "Confirm",
                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.Yes)
                {
                    try
                    {
                        var indexNodeTag = node.Tag as IndexNodeTag;

                        var mongoCollection = indexNodeTag.MongoCollectionInfo.GetMongoCollection();
                        mongoCollection.DropIndexByName(node.Name);
                        tvConnections.Nodes.Remove(node);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Failed to drop index", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void editModelToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (tvConnections.SelectedNode == null) return;

            tvConnections.SelectedNode = tvConnections.SelectedNode.Nodes.Find("Model",false)[0];
            editModelToolStripMenuItem_Click(sender, e);
        }

        private void compactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvConnections.SelectedNode == null) return;

            if(MessageBox.Show("Are you sure you want to compact this collection?", "Confirm", 
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var mongoCollection = (tvConnections.SelectedNode.Tag as CollectionNodeTag).MongoCollectionInfo.GetMongoCollection();
                var mongoDatabase = mongoCollection.Database;
                
                var cmdResult = mongoDatabase.RunCommand(new CommandDocument("compact", mongoCollection.Name));
                if (!cmdResult.Ok)
                    throw new Exception(cmdResult.ErrorMessage);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Compacting Collection");
            }     
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        ///  Connection Properties.
        /// </summary>
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvConnections.SelectedNode == null) return;

            var frm = new FormAddConnection
                      {
                          StartPosition = FormStartPosition.CenterParent,
                          MongoConnectionInfo =(tvConnections.SelectedNode.Tag as ConnectionNodeTag).MongoConnectionInfo
                      };
            frm.ShowDialog();
        }

        private void databaseStatistics_Click(object sender, EventArgs e)
        {
            if (tvConnections.SelectedNode == null) return;

            var frm = new FormDatabaseStats
                      {
                          StartPosition = FormStartPosition.CenterParent
                      };
            if (tvConnections.SelectedNode.Tag is DatabaseNodeTag)
                frm.MongoDatabaseInfo = (tvConnections.SelectedNode.Tag as DatabaseNodeTag).MongoDatabaseInfo;
            else
                frm.MongoCollectionInfo = (tvConnections.SelectedNode.Tag as CollectionNodeTag).MongoCollectionInfo;

            frm.ShowDialog();
        }        
    }
}