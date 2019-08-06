using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoSharp.Model;

namespace MongoSharp.TreeNodeTag
{
    public class TreeNodeTag
    {
        virtual public object GetData()
        {
            return this;
        }

        public bool IsLoaded { get; set; }
    }

    public class IndexNodeTag : TreeNodeTag
    {
        public MongoCollectionInfo MongoCollectionInfo { get; set; }
        public IndexInfo IndexInfo { get; set; }

        override public object GetData()
        {
            return this;
        }
    }

    public class CollectionNodeTag : TreeNodeTag
    {
        public CollectionNodeTag(MongoCollectionInfo col)
        {
            MongoCollectionInfo = col;
        }
        public MongoCollectionInfo MongoCollectionInfo { get; set; }

        override public object GetData()
        {
            return MongoCollectionInfo;
        }
    }

    public class DatabaseNodeTag : TreeNodeTag
    {
        public DatabaseNodeTag(MongoDatabaseInfo db)
        {
            MongoDatabaseInfo = db;
        }
        public MongoDatabaseInfo MongoDatabaseInfo { get; set; }

        override public object GetData()
        {
            return MongoDatabaseInfo;
        }
    }

    public class ConnectionNodeTag : TreeNodeTag
    {
        public ConnectionNodeTag(MongoConnectionInfo conn)
        {
            MongoConnectionInfo = conn;
        }
        public MongoConnectionInfo MongoConnectionInfo { get; set; }

        override public object GetData()
        {
            return MongoConnectionInfo;
        }
    }

    public class ModelNodeTag : TreeNodeTag {

        override public object GetData()
        {
            return this;
        }
    }

}
