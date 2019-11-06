using MongoDB.Driver;
using MongoSharp.Model;

namespace MongoSharp.TreeNodeTag
{
    public class TreeNodeTag
    {
        public virtual object GetData()
        {
            return this;
        }

        public bool IsLoaded { get; set; }
    }

    public class IndexNodeTag : TreeNodeTag
    {
        public MongoCollectionInfo MongoCollectionInfo { get; set; }
        public IndexInfo IndexInfo { get; set; }

        public override object GetData()
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

        public override object GetData()
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

        public override object GetData()
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

        public override object GetData()
        {
            return MongoConnectionInfo;
        }
    }

    public class ModelNodeTag : TreeNodeTag {

        public override object GetData()
        {
            return this;
        }
    }

}
