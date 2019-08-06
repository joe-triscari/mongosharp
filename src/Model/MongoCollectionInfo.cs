using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoSharp.Model
{
    [Serializable]
    public class MongoCollectionInfo
    {
        public MongoCollectionInfo()
        {
            Models = new List<MongoCollectionModelInfo>();
        }
        [XmlAttribute]
        public string Name { get; set; }
        public List<MongoCollectionModelInfo> Models { get; set; }

        [XmlIgnore]
        public MongoDatabaseInfo Database { get; set; }

        [XmlIgnore]
        public bool HasModel { get { return Models != null && Models.Count > 0; } }

        [XmlIgnore]
        public string Path
        {
            get
            {
                return String.Format("{0}.{1}.{2}", this.Database.Connection.Name, this.Database.Name, this.Name);
            }
        }

        public MongoCollection GetMongoCollection()
        {
            return Database.GetCollection(Name);
        }

        public bool IsValid()
        {
            var mongoDatabase = Database.GetMongoDatabase();
            return mongoDatabase.CollectionExists(Name);
        }
    }    
}
