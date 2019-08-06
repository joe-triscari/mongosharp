using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoSharp.Model
{      
    [Serializable]
    public class MongoConnectionInfo
    {
        [XmlAttribute]
        public string Id { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string ServerString { get; set; }
        [XmlAttribute]
        public string Username { get; set; }
        [XmlAttribute]
        public string Password { get; set; }
        public List<MongoDatabaseInfo> Databases { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public string GetConnectionString(string databaseName)
        {
            if (String.IsNullOrWhiteSpace(databaseName))
            {
                return String.IsNullOrWhiteSpace(Username) ?
                            String.Format("mongodb://{0}", ServerString) :
                            String.Format("mongodb://{0}:{1}@{2}", Username, Password, ServerString);
            }            

            return String.IsNullOrWhiteSpace(Username) ?
                        String.Format("mongodb://{0}/{1}", ServerString, databaseName) :
                        String.Format("mongodb://{0}:{1}@{2}/{3}", Username, Password, ServerString, databaseName);
        }

        public bool IsValid(string databaseName)
        {
            try
            {
                MongoServer server = GetMongoServer(databaseName);                
                server.Connect();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public MongoServer GetMongoServer(string databaseName)
        {
            var settings = MongoClientSettings.FromUrl(MongoUrl.Create(GetConnectionString(databaseName)));
            var client = new MongoClient(settings);
            MongoServer server = client.GetServer();
            return server;
        }

        public MongoDatabaseInfo AddDatabase(string name)
        {
            var dbInfo = new MongoDatabaseInfo
                            {
                                Name = name,
                                Connection = this
                            };
            Databases.Add(dbInfo);
            return dbInfo;
        }
    }
}
