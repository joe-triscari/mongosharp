using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace MongoSharp.Model
{
    public class MongoConnectionHelper
    {
        public List<string> GetCollectionNames(MongoDatabaseInfo databaseInfo)
        {
            var client = new MongoClient(databaseInfo.Connection.GetConnectionString(databaseInfo.Name));
            var server = client.GetServer();
            var db = server.GetDatabase(databaseInfo.Name);

            return
                db.GetCollectionNames()
                  .Where(x => !x.StartsWith("system.", StringComparison.CurrentCultureIgnoreCase))
                  .ToList();
        }
    }
}
