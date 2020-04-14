using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace MongoSharp.Model
{
    public class BsonDocumentConverter
    {
        public List<string> ToCSharpClassDeclarations(BsonDocument bsonDocument)
        {
            var docs = new List<string> {""};
            ToCSharpClassDeclarations(bsonDocument, "Doc0", ref docs);

            return docs;
        }

        private void ToCSharpClassDeclarations(BsonDocument bsonDocument, string docName, ref List<string> docs)
        {
            string classDef = $"[BsonIgnoreExtraElements]\npublic class {docName}\n{{\n";

            foreach (BsonElement element in bsonDocument.Elements)
            {
                if (element.Value.IsBsonDocument)
                {
                    var innerDoc = element.Value.AsBsonDocument;                    
                    if (innerDoc != null && innerDoc.Elements.Any())
                    {
                        classDef += $"\tpublic {GetDocName(element.Name)} {element.Name} {{ get; set; }}\n";
                        ToCSharpClassDeclarations(element.Value.AsBsonDocument, GetDocName(element.Name), ref docs);
                    }
                }
                else if(element.Value.IsBsonArray)
                {
                    var ar = element.Value.AsBsonArray;
                    if (ar.Count > 0)
                    {
                        var first = ar.First();
                        if(!first.IsBsonDocument)
                        {
                            classDef += $"\tpublic {ToCSharpType(first)} [] {element.Name} {{ get; set; }}\n";
                        }
                        else
                        {
                            var document = first.AsBsonDocument;

                            classDef += $"\tpublic {GetDocName(element.Name)} [] {element.Name} {{ get; set; }}\n";
                            ToCSharpClassDeclarations(document, GetDocName(element.Name), ref docs);
                        }
                    }
                    else
                    {   // No elements, can't determine type.
                        classDef += $"\tpublic BsonValue [] {element.Name} {{ get; set; }}\n";
                    }
                }
                else if (element.Name != "ExtensionData")
                {
                    classDef += $"\tpublic {ToCSharpType(element.Value)} {element.Name} {{ get; set; }}\n";
                }
            }

            classDef += "}\n";

            docs.Add(classDef);
        }

        private string GetDocName(string name)
        {
            if (name.EndsWith("ies"))
                return name.Substring(0, name.Length - 3) + "y";

            if(name.EndsWith("s"))
                return name.Substring(0, name.Length - 1);

            return name;
        }

        public string ToCSharpType(BsonValue bsonValue)
        {
            if (bsonValue == null || bsonValue.IsBsonNull)
                return "BsonValue";
            
            if (bsonValue.IsObjectId)
                return "ObjectId";
            if (bsonValue.IsBoolean)
                return "bool";
            if (bsonValue.IsBsonDateTime || bsonValue.IsBsonTimestamp || bsonValue.IsValidDateTime)
                return "DateTime";
            if (bsonValue.IsDouble)
                return "double";
            if (bsonValue.IsInt32)
                return "int";
            if (bsonValue.IsInt64)
                return "long";
            if (bsonValue.IsNumeric)
                return "decimal";
            if (bsonValue.IsString)
                return "string";

            return "BsonValue";
        }

        public string ToCSharpType(JTokenType jTokenType)
        {

            if (jTokenType == JTokenType.Boolean)
                return "bool";
            if (jTokenType == JTokenType.Date)
                return "DateTime";
            if (jTokenType == JTokenType.TimeSpan)
                return "TimeSpan";
            if (jTokenType == JTokenType.Float)
                return "decimal";
            if (jTokenType == JTokenType.Integer)
                return "int";
            if (jTokenType == JTokenType.String)
                return "string";

            return "BsonValue";
        }

        public List<string> ToPropertyPaths(BsonDocument bsonDocument)
        {
            var sb = new StringBuilder();

            ToPropertyPaths(bsonDocument, "", ref sb);

            string propsStr = sb.ToString();
            var props = propsStr.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            return props.ToList();
        }        

        private void ToPropertyPaths(BsonDocument bsonDocument, string parentObject, ref StringBuilder sb)
        {
            foreach (BsonElement element in bsonDocument.Elements)
            {
                if (element.Value.IsBsonDocument)
                {
                    var innerDoc = element.Value.AsBsonDocument;

                    if (innerDoc != null && innerDoc.Elements.Any())
                    {
                        string name;
                        if (!string.IsNullOrWhiteSpace(parentObject))
                            name = parentObject + "." + element.Name;
                        else
                            name = element.Name;
                        ToPropertyPaths(element.Value.AsBsonDocument, name, ref sb);
                    }
                }
                else if (element.Value.IsBsonArray)
                {
                    JArray ar = GetJArray(element.Value);                    
                    if (ar.Count > 0)
                    {
                        var first = ar.First();
                        var type = first.Type;
                        if (type == JTokenType.Object)
                        {
                            string json = first.ToString();
                            var document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(json);

                            string name;
                            if (!string.IsNullOrWhiteSpace(parentObject))
                                name = parentObject + "." + element.Name + "[]";
                            else
                                name = element.Name + "[]";
                            ToPropertyPaths(document, name, ref sb);
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(parentObject))
                                sb.AppendLine(parentObject + "." + element.Name + "[]");
                            else
                                sb.AppendLine(element.Name + "[]");
                        }
                        
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(parentObject))
                            sb.AppendLine(parentObject + "." + element.Name + "[]");
                        else
                            sb.AppendLine(element.Name + "[]");
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(parentObject))
                        sb.AppendLine(parentObject + "." + element.Name);
                    else
                        sb.AppendLine(element.Name);
                }
            }
        }

        private JArray GetJArray(BsonValue bsonValue)
        {
            string json = bsonValue.ToString();
            JArray ar;
            try
            {
                ar = JsonConvert.DeserializeObject<JArray>(RemoveISODateFunctions(json),
                                                           new IsoDateTimeConverter());
            }
            catch (Exception)
            {
                json = bsonValue.ToJson();
                ar = JsonConvert.DeserializeObject<JArray>(RemoveISODateFunctions(json),
                                                           new IsoDateTimeConverter());
            }

            return ar;
        }

        private string RemoveISODateFunctions(string json)
        {
            int pos = json.IndexOf("ISODate(", StringComparison.CurrentCulture);
            while (pos >= 0)
            {
                var sb = new StringBuilder();
                bool inISODateFunc = false;
                char[] chars = json.ToArray();
                for (int i = 0; i < chars.Length; ++i)
                {
                    if (i == pos)
                    {
                        i += "ISODate(".Length - 1;
                        inISODateFunc = true;
                    }
                    else if (inISODateFunc && chars[i] == ')')
                    {
                        inISODateFunc = false;
                    }
                    else
                    {
                        sb.Append(chars[i]);
                    }
                }

                json = sb.ToString();
                pos = json.IndexOf("ISODate(", StringComparison.CurrentCulture);
            }

            return json;
        }
    }
}
