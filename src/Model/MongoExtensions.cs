using MongoDB.Bson;

namespace MongoSharp.Model
{
    public static class MongoExtensions
    {
        private static readonly System.Text.RegularExpressions.Regex objectIdReplace = new System.Text.RegularExpressions.Regex(@"ObjectId\((.[a-f0-9]{24}.)\)", System.Text.RegularExpressions.RegexOptions.Compiled);
        /// <summary>
        /// deserializes this bson doc to a .net dynamic object
        /// </summary>
        /// <param name="bson">bson doc to convert to dynamic</param>
        /// http://stackoverflow.com/questions/10222472/is-there-mongodb-c-sharp-driver-support-system-dynamic-dynamicobject-in-net-4
        public static dynamic ToDynamic(this BsonDocument bson)
        {
            var json = objectIdReplace.Replace(bson.ToJson(), (s) => s.Groups[1].Value);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
        }

        public static BsonValue Create(this BsonType bsonType, object o)
        {
            BsonValue value = BsonNull.Value;

            try
            {
                switch (bsonType)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        value = BsonDouble.Create(o);
                        break;
                    case BsonType.String:
                        value = BsonString.Create(o);
                        break;
                    case BsonType.Document:
                        value = BsonDocument.Create(o);
                        break;
                    case BsonType.Array:
                        value = BsonArray.Create(o);
                        break;
                    case BsonType.Binary:
                        value = BsonBinaryData.Create(o);
                        break;
                    case BsonType.Undefined:
                        break;
                    case BsonType.ObjectId:
                        value = BsonObjectId.Create(o);
                        break;
                    case BsonType.Boolean:
                        value = BsonBoolean.Create(o);
                        break;
                    case BsonType.DateTime:
                        value = BsonDateTime.Create(o);
                        break;
                    case BsonType.Null:
                        value = BsonNull.Value;
                        break;
                    case BsonType.RegularExpression:
                        value = BsonRegularExpression.Create(o);
                        break;
                    case BsonType.JavaScript:
                        value = BsonJavaScript.Create(o);
                        break;
                    case BsonType.Symbol:
                        value = BsonSymbol.Create(o);
                        break;
                    case BsonType.JavaScriptWithScope:
                        value = BsonJavaScriptWithScope.Create(o);
                        break;
                    case BsonType.Int32:
                        value = BsonInt32.Create(o);
                        break;
                    case BsonType.Timestamp:
                        value = BsonTimestamp.Create(o);
                        break;
                    case BsonType.Int64:
                        value = BsonInt64.Create(o);
                        break;
                    case BsonType.MaxKey:
                        value = BsonValue.Create(o);
                        break;
                    case BsonType.MinKey:
                        value = BsonValue.Create(o);
                        break;
                }
            }
            catch
            {

            }            

            return value;
        }
    }
}
