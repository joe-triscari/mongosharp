using System.Collections.Generic;
using MongoDB.Bson;

namespace MongoSharp.Model
{
    public class ResultTreeNodeModel
    {
        public ResultTreeNodeModel()
        {
            Parent = null;
            Children = new List<ResultTreeNodeModel>();
        }

        public string Name { get; set; }
        public string RawName { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public BsonType BsonType { get; set; }
        public bool IsValue { get; set; }
        public bool IsArray { get; set; }

        public ResultTreeNodeModel Parent { get; set; }
        public List<ResultTreeNodeModel> Children { get; set; }
        public BsonDocument BsonDocument { get; set; }

        public bool IsRoot { get { return Parent == null; } }

        public string BsonUpdateQuery { get; set; }
    }
}
