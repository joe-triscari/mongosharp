using MongoDB.Bson;
using System.Web.Script.Serialization;

namespace MongoSharp.Model
{
    static public class Helper
    {
        static public string JoinBsonArray(BsonValue ba, string seperator)
        {

            return string.Join<BsonValue>(seperator, ba.AsBsonArray.ToArray());
        }

        static public string JoinBsonArray(BsonValue ba)
        {
            return JoinBsonArray(ba, ",");
        }

        static public int ArrayLength(BsonValue ba)
        {
            var ar = ba.AsBsonArray.ToArray();
            return ar.Length;
        }

        static public string ToJson(object o)
        {
            if (o == null)
                return "NULL";

            return new JavaScriptSerializer().Serialize(o);
        }
    }
}
