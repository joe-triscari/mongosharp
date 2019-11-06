using System;
using MongoDB.Bson;

namespace MongoSharp.Model
{
    public class MongoCollectionSchema
    {
        public BsonDocument SchemaDocument { get; set; }
        public long CollectionSize { get; set; }
        public long SampleSize { get; set; }
        public decimal SamplePercent => Math.Round((SampleSize/(decimal) CollectionSize) * 100m, 0);
    }
}
