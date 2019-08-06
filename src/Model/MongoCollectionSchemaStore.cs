﻿using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

namespace MongoSharp.Model
{
    static public class MongoCollectionSchemaStore
    {
        private static readonly Dictionary<string, MongoCollectionSchema> _docs = new Dictionary<string, MongoCollectionSchema>();

        static public MongoCollectionSchema GetSchemaDocument(MongoDatabaseInfo databaseInfo, string collectionName)
        {
            return GetSchemaDocument(databaseInfo, collectionName, 10000);
        }

        static public MongoCollectionSchema GetSchemaDocument(MongoDatabaseInfo databaseInfo, string collectionName, int maxSampleSize)
        {
            string key = String.Format("{0}-{1}", databaseInfo.Connection.Name, collectionName);
            if (_docs.ContainsKey(key))
                return _docs[key];

            MongoCollection<BsonDocument> collection = databaseInfo.GetCollection(collectionName);
            long collectionSize = collection.Count();
            long sampleSize = Math.Min(collectionSize, maxSampleSize);

            var cursor = collection.FindAll();
            cursor.SetSortOrder(SortBy.Descending("_id"));
            cursor.SetLimit((int)sampleSize);
            
            var docs = cursor.ToList();
            var doc = new BsonDocumentBuilder().BuildDocument(docs);
            _docs.Add(key, new MongoCollectionSchema
                {
                    SchemaDocument = doc,
                    SampleSize = sampleSize,
                    CollectionSize = collectionSize
                });

            return _docs[key];
        }
    }
}
