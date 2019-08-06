using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoSharp.TreeNodeTag;

namespace MongoSharp
{
    public partial class FormIndexProperties : Form
    {
        public FormIndexProperties()
        {
            InitializeComponent();
        }

        public IndexNodeTag IndexNodeTag { get; set; }

        private void FormIndexProperties_Load(object sender, EventArgs e)
        {
            treeListView1.CanExpandGetter = model => ((PropertyValue)model).Properties.Count > 0;
            treeListView1.ChildrenGetter = model => ((PropertyValue) model).Properties;
            this.olvColumnProperty.AspectGetter = x => ((PropertyValue) x).Name;
            this.olvColumnValue.AspectGetter = x => ((PropertyValue) x).Value;

            chkbxUnique.Checked = IndexNodeTag.IndexInfo.IsUnique;
            chkbxSparse.Checked = IndexNodeTag.IndexInfo.IsSparse;
            chkbxBackground.Checked = IndexNodeTag.IndexInfo.IsBackground;

            // We also need to tell OLV what objects to display as root nodes
            treeListView1.SetObjects(LoadProperties(IndexNodeTag.IndexInfo.RawDocument));
            treeListView1.ExpandAll();
            treeListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private List<PropertyValue> LoadProperties(BsonDocument bsonDoc)
        {
            var list = new List<PropertyValue>();
            AddBsonDoc(bsonDoc, ref list);
            return list;
        }

        private void AddBsonDoc(BsonDocument bsonDoc, ref List<PropertyValue> list)
        {
            foreach (BsonElement el in bsonDoc.Elements)
            {
                if (el.Value.IsBsonDocument)
                {
                    var subProp = new PropertyValue { Name = el.Name};
                    list.Add(subProp);
                    var tmpList = subProp.Properties;
                    AddBsonDoc(el.Value.AsBsonDocument, ref tmpList);
                }
                else
                {
                    list.Add(new PropertyValue
                    {
                        Name = el.Name,
                        Value = el.Value.ToString()
                    });
                }
            }
        }

        private class PropertyValue
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public List<PropertyValue> Properties { get; set; }
            public PropertyValue()
            {
                Properties = new List<PropertyValue>();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
