using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using MongoSharp.Model;

namespace Model.Test
{
    [TestFixture]
    public class BsonDocumentBuilderTests
    {
        [Test]
        public void Test()
        {
            var client = new MongoClient("mongodb://accounting_:4PWMnQky@qgmongodata1:30100,qgmongodata2:30100");
            var server = client.GetServer();
            var db = server.GetDatabase("accounting");

            var collection = db.GetCollection("JournalImportDocs");
            List<BsonDocument> docs = collection.FindAll().ToList();
            var builder = new BsonDocumentBuilder();

            var aggregateDoc = builder.BuildDocument(docs);
        }

        [Test]
        public void TestBsonArray()
        {
            var ar = new BsonArray {new BsonInt32(200)};
            BsonValue el = ar[0];
            var b = el.IsBsonDocument;
            var json = el.ToString();
            var document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(json);

            var x = new Newtonsoft.Json.JsonToken();


            JObject o = JObject.Parse(String.Format("{{\"Array\": {0}}}", json));
            var jar = (JArray)o["Array"];
            json = jar[0].ToString();
        }
    }
}
