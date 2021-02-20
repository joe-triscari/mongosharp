using System;
using System.Collections.Generic;
using System.Xml.Serialization;
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
        [XmlAttribute]
        public string Namespace { get; set; }
        public List<MongoCollectionModelInfo> Models { get; set; }

        [XmlIgnore]
        public MongoDatabaseInfo Database { get; set; }

        [XmlIgnore]
        public bool HasModel => Models != null && Models.Count > 0;

        [XmlIgnore]
        public bool HasNamespace => !string.IsNullOrWhiteSpace(Namespace);

        [XmlIgnore]
        public string Path => $"{Database.Connection.Name}.{Database.Name}.{Name}";

        [XmlIgnore]
        public string DefaultNamespace =>
            $"{Database.Connection.Name.Replace(' ', '_')}.{Database.Name.Replace(' ', '_')}.{Name.Replace(' ', '_')}";

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
