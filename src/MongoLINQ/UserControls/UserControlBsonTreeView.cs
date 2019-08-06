using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MongoDB.Bson;

namespace MongoSharp
{
    public partial class UserControlBsonTreeView : UserControl
    {
        public UserControlBsonTreeView()
        {
            InitializeComponent();
        }

        public void LoadBsonDocument(BsonDocument bsonDocument)
        {
            treeListViewBson.SetObjects(LoadProperties(bsonDocument));
            treeListViewBson.ExpandAll();
           // treeListViewBson.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
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

        private void UserControlBsonTreeView_Load(object sender, EventArgs e)
        {
            treeListViewBson.CanExpandGetter = model => ((PropertyValue)model).Properties.Count > 0;
            treeListViewBson.ChildrenGetter = model => ((PropertyValue)model).Properties;
            this.olvColumnProperty.AspectGetter = x => ((PropertyValue)x).Name;
            this.olvColumnValue.AspectGetter = x => ((PropertyValue)x).Value;
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
                    var subProp = new PropertyValue { Name = el.Name };
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
    }
}
