using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Web.Script.Serialization;

namespace MongoSharp.Model
{
//    class BsonValueConverter : JsonConverter
//    {

//        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//        {
//            serializer.Serialize(writer, value.ToString());

//        }

//        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//        {
//            throw new NotImplementedException();
//        }

//        public override bool CanConvert(Type objectType)
//        {
//            return typeof(ObjectId).IsAssignableFrom(objectType);
//            //return true;
//        }


//    }

    public static class Helper
    {
        public static string JoinBsonArray(BsonValue ba, string seperator)
        {
            return string.Join<BsonValue>(seperator, ba.AsBsonArray.ToArray());
        }

        public static string JoinBsonArray(BsonValue ba)
        {
            return JoinBsonArray(ba, ",");
        }

        public static int ArrayLength(BsonValue ba)
        {
            var ar = ba.AsBsonArray.ToArray();
            return ar.Length;
        }

        public static string ToJson(object o)
        {
            if (o == null)
                return "NULL";

            return new JavaScriptSerializer().Serialize(o);
        }

        public static MongoCollection<T> GetCollection<T>() where T : new()
        {
            try
            {
                var t = new T();
                var tt = t.GetType();
                var ns = tt.Namespace;

                MongoCollectionInfo collectionInfo = null;
                foreach (var conn in Settings.Instance.Connections)
                {
                    foreach (var dbInfo in conn.Databases)
                    {
                        foreach (var collInfo in dbInfo.Collections)
                        {
                            if ("MongoSharp.Query." + collInfo.Namespace == ns)
                            {
                                collectionInfo = collInfo;
                                break;
                            }
                        }
                    }
                }

                if (collectionInfo == null)
                {
                    throw new Exception("Collection not found.");
                }
                else
                {
                    var client = new MongoClient(collectionInfo.Database.Connection.GetConnectionString(collectionInfo.Database.Name));
                    var server = client.GetServer();
                    var database = server.GetDatabase(collectionInfo.Database.Name);
                    var collection = database.GetCollection<T>(collectionInfo.Name);

                    return collection;
                }
            }
            catch(Exception e)
            {
                throw new Exception($"Error getting collection for type '{typeof(T).ToString()}'.", e);
            }

        }
    }
}
