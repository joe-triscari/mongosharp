using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoSharp.Model
{
    public class MongoCodeGenerator
    {
        public List<string> GetProperties(MongoDatabaseInfo databaseInfo, string collectionName)
        {
            MongoCollectionModelInfo model = GetModel(databaseInfo, collectionName);
            if (string.IsNullOrWhiteSpace(model.ModelCode))
            {
                throw new Exception("Model does not exist");
            }

            var types = new MongoDynamicCodeRunner().CompileModelCode(model.ModelCode);
            var rootType = types.Find(t => t.Name == model.RootClassName);
            if (rootType == null)
            {
                throw new Exception($"{model.RootClassName} class not found in Model.");
            }

            List<PropertyData> properties = ObjectHelper.ToPropertyPaths(rootType);

            return properties.Select(x => x.ToString()).ToList();
        }

        public List<PropertyData> GetPropertiesPaths(MongoDatabaseInfo databaseInfo, string collectionName)
        {
            MongoCollectionModelInfo model = GetModel(databaseInfo, collectionName);
            if (string.IsNullOrWhiteSpace(model.ModelCode))
            {
                throw new Exception("Model does not exist");
            }

            var types = new MongoDynamicCodeRunner().CompileModelCode(model.ModelCode);
            var rootType = types.Find(t => t.Name == model.RootClassName);
            if (rootType == null)
            {
                throw new Exception($"{model.RootClassName} class not found in Model.");
            }

            List<PropertyData> properties = ObjectHelper.ToPropertyPaths(rootType);

            return properties;
        }

        private string GetModelCode(MongoDatabaseInfo databaseInfo, string collectionName)
        {
            MongoCollectionInfo collectionInfo = databaseInfo.GetCollectionInfo(collectionName);
            if (collectionInfo != null && collectionInfo.HasModel)
            {   // Model already exists for this collection.
                return collectionInfo.Models[0].ModelCode;
            }

            // Generate and save model code.
            var collection = databaseInfo.GetCollection(collectionName);
            var doc = collection.FindOne();
            if (doc == null)
                throw new Exception(
                    $"Collection '{collectionName}' is empty. Unable to determine schema from first document");

            var schemaInfo = MongoCollectionSchemaStore.GetSchemaDocument(databaseInfo, collectionName);
            var classes = new BsonDocumentConverter().ToCSharpClassDeclarations(schemaInfo.SchemaDocument);

            var sb = new StringBuilder();
            foreach (var classSyntax in classes)
                sb.AppendLine(classSyntax + "\r\n");

            string modelCode = sb.ToString();

            databaseInfo.SetCollectionModel(collectionName, modelCode, "Doc0", true, (int)schemaInfo.SampleSize, schemaInfo.SamplePercent);
            Settings.Instance.Save();

            return modelCode;
        }

        private MongoCollectionModelInfo GetModel(MongoDatabaseInfo databaseInfo, string collectionName)
        {
            MongoCollectionInfo collectionInfo = databaseInfo.GetCollectionInfo(collectionName);
            if (collectionInfo != null && collectionInfo.HasModel)
            {   // Model already exists for this collection.
                return collectionInfo.Models[0];
            }

            // Generate and save model code.
            var collection = databaseInfo.GetCollection(collectionName);
            var doc = collection.FindOne();
            if (doc == null)
                throw new Exception(
                    $"Collection '{collectionName}' is empty. Unable to determine schema from first document");

            var schemaInfo = MongoCollectionSchemaStore.GetSchemaDocument(databaseInfo, collectionName);
            var classes = new BsonDocumentConverter().ToCSharpClassDeclarations(schemaInfo.SchemaDocument);

            var sb = new StringBuilder();
            foreach (var classSyntax in classes)
                sb.AppendLine(classSyntax);

            string modelCode = sb.ToString();

            MongoCollectionModelInfo newModel = databaseInfo.SetCollectionModel(collectionName, modelCode, "Doc0", true, (int)schemaInfo.SampleSize, schemaInfo.SamplePercent);
            Settings.Instance.Save();

            return newModel;
        }

        public string GenerateMongoCode(MongoDatabaseInfo databaseInfo, string collectionName, string linqQuery, string mode, out int injectedCodeStartPos)
        {
            var collectionInfo = databaseInfo.GetCollectionInfo(collectionName);
            int additionalLength = 0;
            injectedCodeStartPos = 0;

            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using MongoDB.Bson;");
            sb.AppendLine("using MongoDB.Bson.IO;");
            sb.AppendLine("using MongoDB.Bson.Serialization.Attributes;");
            sb.AppendLine("using MongoDB.Driver;");
            sb.AppendLine("using MongoDB.Driver.Core;");
            sb.AppendLine("using MongoDB.Driver.Builders;");
            sb.AppendLine("using MongoDB.Driver.Linq;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using MongoSharp.Model;");
            sb.AppendLine("");
            sb.AppendLine("namespace MongoSharp.Query");
            sb.Append("{");
            sb.AppendLine("");

            if (mode != MongoSharpQueryMode.Json)
            {
                sb.AppendLine("\t#region Model");
                foreach (var conn in Settings.Instance.Connections)
                {
                    foreach (var dbInfo in conn.Databases)
                    {
                        foreach (var collInfo in dbInfo.Collections)
                        {
                            if (collInfo.HasModel && !string.IsNullOrWhiteSpace(collInfo.Namespace))
                            {
                                sb.AppendFormat("\tnamespace {0}\n\t{{\n", collInfo.Namespace);
                                sb.AppendLine(AddTabsToEachLine(GetModelCode(dbInfo, collInfo.Name), 2, false, "\n"));
                                sb.AppendLine("}");
                            }
                        }
                    }
                }
                sb.AppendLine("\t#endregion");
                sb.AppendLine("");
            }

            string modelType = mode == MongoSharpQueryMode.Json || !collectionInfo.Models.Any() ? "BsonDocument" : collectionInfo.Namespace + "." + collectionInfo.Models[0].RootClassName;

            sb.AppendLine("\tpublic class QueryExecutor");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tstatic List<QueryResult> _queryResults;");
            sb.AppendLine("");
            sb.AppendLine("\t\tpublic static List<QueryResult> RunQuery(System.IO.TextWriter textWriter)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\t_queryResults = new List<QueryResult>();");
            sb.AppendLine("\t\t\tif(textWriter != null) System.Console.SetOut(textWriter);");
            sb.AppendLine("");
            sb.AppendLine("\t\t\t#region Reserved Keywords");
            sb.AppendLine("\t\t\tvar client = new MongoClient(\"" + databaseInfo.Connection.GetConnectionString(databaseInfo.Name) + "\");");
            sb.AppendLine("\t\t\tvar server = client.GetServer();");
            sb.AppendLine("\t\t\tvar database = server.GetDatabase(\"" + databaseInfo.Name + "\");");
            sb.AppendLine("\t\t\tvar collection = database.GetCollection<" + modelType + ">(\"" + collectionName + "\");");
            sb.AppendLine("\t\t\t#endregion");
            sb.AppendLine("");

            sb.AppendLine("\t\t\t#region Injected Code");
            if (mode == MongoSharpQueryMode.CSharpStatements)
            {
                injectedCodeStartPos = sb.Length + additionalLength;
                sb.AppendLine(AddTabsToEachLine(linqQuery, 3, false));
            }
            else if(mode == MongoSharpQueryMode.CSharpQuery)
            {
                injectedCodeStartPos = sb.Length + "\t\t\tDump(".Length + additionalLength;
                sb.AppendFormat("\t\t\tDump({0});\r\n\r\n", AddTabsToEachLine(linqQuery,4, true));
            }
            else if(mode == MongoSharpQueryMode.Json)
            {                
                linqQuery = linqQuery.Replace("\"", "\\\"");
                linqQuery = linqQuery.Replace("\'", "\\\'");
                linqQuery = linqQuery.Replace("\r", "");
                linqQuery = linqQuery.Replace("\n", "");

                sb.AppendFormat("string json = \"{0}\";\r\n", linqQuery);
                sb.AppendLine("Dump(collection.Find(new QueryDocument(BsonDocument.Parse(json))));");
            }

            sb.AppendLine("\t\t\t#endregion");
            sb.AppendLine("");
            sb.AppendLine("\t\t\treturn _queryResults;");
            sb.AppendLine("\t\t}");            
            sb.AppendLine("");
            sb.AppendLine("\t\tstatic void Dump<T>(T result)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\t_queryResults.Add(QueryResult.ToQueryResult(result));");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t\tstatic void Print(string output)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tSystem.Console.WriteLine(output);");
            sb.AppendLine("\t\t}");

            sb.AppendLine("\t\tstatic MongoCollection<T> GetCollection<T>() where T : new()");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\treturn Helper.GetCollection<T>();");
            sb.AppendLine("\t\t}");

            sb.AppendLine("\t}");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private string AddTabsToEachLine(string text, int nbrOfTabs, bool skipFirst, string newLine = "\r\n")
        {
            return text;

            var lines = text.Split(new[] { newLine }, StringSplitOptions.None);
            var tabs = new string('\t', nbrOfTabs);

            var sb = new StringBuilder();
            bool isFirst = true;
            foreach (string line in lines)
            {
                if (isFirst && skipFirst)
                {
                    sb.AppendLine(line);
                }
                else
                {
                    sb.AppendLine(tabs + line);
                }
                isFirst = false;
            }

            return sb.ToString().TrimEnd('\r', '\n');
        }
    }
}
