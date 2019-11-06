using System.Reflection;
using MongoDB.Bson;

namespace MongoSharp.Model
{
    public static class BsonElementExtensions
    {
        private static readonly FieldInfo BsonElementValueProperty = typeof(BsonElement).GetField("_value", BindingFlags.NonPublic | BindingFlags.Instance);
        public static void SetValue(this BsonElement bsonElement, BsonValue bsonValue)
        {
            BsonElementValueProperty.SetValue(bsonElement, bsonValue);
        }
    }
}