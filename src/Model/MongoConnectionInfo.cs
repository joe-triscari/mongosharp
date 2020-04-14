using System;
using System.Collections.Generic;
using System.Xml.Serialization;
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
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                return string.IsNullOrWhiteSpace(Username) ? $"mongodb://{ServerString}"
                    : $"mongodb://{Username}:{Password}@{ServerString}";
            }            

            return string.IsNullOrWhiteSpace(Username) ? $"mongodb://{ServerString}/{databaseName}"
                : $"mongodb://{Username}:{Password}@{ServerString}/{databaseName}";
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
