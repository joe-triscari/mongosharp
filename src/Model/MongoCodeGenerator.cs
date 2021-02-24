using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoSharp.Model.CodeGen;

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
            var classes = new BsonDocumentConverter().ToCSharpClassDeclarations(schemaInfo.SchemaDocument, collectionName);

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

            var model = new CodeGenModel
            {
                LinqQuery = linqQuery,
                MongoDatabaseInfo = databaseInfo,
                MongoCollectionInfo = collectionInfo,
                Mode = mode,
                ModelType = mode == MongoSharpQueryMode.Json || !collectionInfo.Models.Any()
                    ? "BsonDocument"
                    : collectionInfo.Namespace + "." + collectionInfo.Models[0].RootClassName
            };

            string code = new MongoCodeBehindGen(model).TransformText();

            injectedCodeStartPos = 0;
            var injectPos = code.IndexOf("#region Injected Code", StringComparison.CurrentCultureIgnoreCase);
            if (injectPos > 0)
            {
                injectedCodeStartPos = injectPos + "#region Injected Code".Length;
            }

            return code;
        }

    }
}
