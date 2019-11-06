using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoSharp.Model;
using MongoSharp.Dialogs;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace MongoSharp
{
    public partial class UserControlQueryResultsTree : UserControl, IUserControlQueryResult
    {
        private bool _isLoaded;
        private QueryResult _queryResult;
        private MongoCollectionInfo _mongoCollectionInfo;
        private ResultTreeNodeModel _currentModel;

        public class TextFilter : BrightIdeasSoftware.IModelFilter
        {
            public bool Filter(object modelObject)
            {
                return false;
            }
        }

        public UserControlQueryResultsTree()
        {
            InitializeComponent();
            treeListView1.UseFiltering = true;
            //treeListView1.ModelFilter = BrightIdeasSoftware.TextMatchFilter.Contains(treeListView1, "search");     
            treeListView1.CellEditStarting += treeListView1_CellEditStarting;
            treeListView1.CellEditFinishing += treeListView1_CellEditFinishing;
            treeListView1.CellToolTipShowing += treeListView1_CellToolTipShowing;
            treeListView1.CellRightClick += treeListView1_CellRightClick;
        }

        void treeListView1_CellRightClick(object sender, BrightIdeasSoftware.CellRightClickEventArgs e)
        {
            if (!IsBsonMode || e.Model == null)
                return;

            var rootNode = e.Model as ResultTreeNodeModel;
            if (rootNode == null)
                return;

            while (rootNode.Parent != null)
                rootNode = rootNode.Parent;
            
            _currentModel = rootNode;

            e.MenuStrip = cmsRightClick;
        }

        void treeListView1_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            e.Cancel = e.Column.Text != "Value";
            var x = e.RowObject as ResultTreeNodeModel;
            if ((x != null && !x.IsValue) || e.ListViewItem.Text == "_id")
            {
                e.Cancel = true;
            }
        }

        void treeListView1_CellToolTipShowing(object sender, BrightIdeasSoftware.ToolTipShowingEventArgs e)
        {
            var x = e.Item.RowObject as ResultTreeNodeModel;
            if(x != null && x.IsValue && !String.IsNullOrWhiteSpace(x.BsonUpdateQuery))
            {
                e.Text = x.BsonUpdateQuery;
            }
        }

        private IMongoQuery GetIdQuery(string id)
        {
            try
            {
                return Query.EQ("_id", new ObjectId(id));
            }
            catch (Exception)
            {
                return Query.EQ("_id", BsonValue.Create(id));
            }
        }

        void treeListView1_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (e.Cancel)
                return;
            if (e.NewValue.Equals(e.Value))
                return;

            var node = e.RowObject as ResultTreeNodeModel;
            if(node != null && node.IsValue)
            {
                var convertedValue = node.BsonType.Create(e.NewValue);
                if ((convertedValue == null || convertedValue == BsonNull.Value) && node.BsonType != BsonNull.Value)
                {
                    e.Cancel = true;
                    return;
                }

                try
                {
                    
                    var parts = node.BsonUpdateQuery.Split(new[] { '.' });
                    string _id = parts[0];
                    var collection = _mongoCollectionInfo.GetMongoCollection();
                    var query = GetIdQuery(_id);
                    var bDoc = collection.FindOneAs<BsonDocument>(query);
                    if (bDoc != null)
                    {                        
                        var u = node.BsonUpdateQuery.Substring(node.BsonUpdateQuery.IndexOf('.') + 1);
                        var update = MongoDB.Driver.Builders.Update.Set(u, convertedValue);

                        var args = new FindAndModifyArgs {Query = query, Update =  update};
                        collection.FindAndModify(args);

                        node.Value = e.NewValue.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Unexpected trouble finding document by id.", "Update Failed");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Failed to update document.\r\n\r\n" + ex.Message, "Update Error");
                }                
            }

        }

        private bool IsBsonMode
        {
            get => toolStripLabelEditModeEnabled.Visible;
            set => toolStripLabelEditModeEnabled.Visible = value;
        }

        public void OnSelected()
        {
            if (!_isLoaded)
            {
                Cursor.Current = Cursors.WaitCursor;

                List<ResultTreeNodeModel> models = new QueryResultConverter().ConvertToTreeNodeModels(_queryResult);
                treeListView1.SetObjects(models);

                if(_queryResult.IsBsonDocuments)
                {
                    treeListView1.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
                    IsBsonMode = true;
                }

                Cursor.Current = Cursors.Default;
                _isLoaded = true;
            }             
        }

        public void LoadResults(QueryResult queryResult, MongoCollectionInfo mongoCollectionInfo)
        {
            _queryResult = queryResult;
            _mongoCollectionInfo = mongoCollectionInfo;

            treeListView1.UseAlternatingBackColors = true;
            treeListView1.AlternateRowBackColor = Color.AliceBlue;
            treeListView1.GridLines = true;
            treeListView1.CanExpandGetter = model => ((ResultTreeNodeModel)model).Children.Count > 0;
            treeListView1.ChildrenGetter = model => ((ResultTreeNodeModel) model).Children;
            olvColumnKey.AspectGetter = x => ((ResultTreeNodeModel) x).Name;
            olvColumnValue.AspectGetter = x => ((ResultTreeNodeModel) x).Value;
            olvColumnType.AspectGetter = x => ((ResultTreeNodeModel) x).Type;                   
        }

        private void toolStripButtonExpandAll_Click(object sender, EventArgs e)
        {
            treeListView1.ExpandAll();            
        }

        private void toolStripButtonCollapseAll_Click(object sender, EventArgs e)
        {
            treeListView1.CollapseAll();
        
        }

        private void ExpandAll(ResultTreeNodeModel m)
        {
            if(m != null)
                treeListView1.Expand(m);

            foreach (var c in m.Children)
            {
                if (c.Children.Count > 0)
                    ExpandAll(c);
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(toolStripTextBox1.Text))
                treeListView1.ModelFilter = null;
            else
                treeListView1.ModelFilter = BrightIdeasSoftware.TextMatchFilter.Contains(treeListView1, toolStripTextBox1.Text);
        }

        private void expandSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = treeListView1.SelectedItem;
            if (item != null)
            {
                ExpandAll(item.RowObject as ResultTreeNodeModel);
            }   
        }

        private void collapseSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = treeListView1.SelectedItem;
            if (item != null)
                treeListView1.Collapse(item.RowObject);
        }

        private void toolStripButtonEditDocument_Click(object sender, EventArgs e)
        {
            var items = treeListView1.SelectedObjects;
            if(items.Count != 1)
            {
                MessageBox.Show("Please select a specific document to edit.");
                return;
            }

            var model = items[0] as ResultTreeNodeModel;
            if (model == null)
                return;

            while (!model.IsRoot)
                model = model.Parent;

            EditDocument(model);
        }        

        private void toolStripMenuItemEditValue_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItemEditDoc_Click(object sender, EventArgs e)
        {
            EditDocument(_currentModel);
        }

        private void EditDocument(ResultTreeNodeModel model)
        {
            var collection = _mongoCollectionInfo.GetMongoCollection();
            var form = new FormEditJsonDocument();
            form.UpdateDocument += json =>
                {
                    var bsonDoc = BsonDocument.Parse(json);
                    var result = collection.Save(bsonDoc, WriteConcern.Acknowledged);
                    //if (!result.Ok)
                    //    throw new Exception(result.ErrorMessage);

                    model.Children = new QueryResultConverter().ConvertBsonDocumentToTreeNodeModels(bsonDoc, model);
                    treeListView1.RefreshObject(model);
                };
            form.MongoCollection = collection;
            form.BsonDocument = model.BsonDocument;
            form.ShowDialog();
        }
    }
}
