using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace MongoSharp.Model
{
    public class CodeFormatter
    {
        public string FormatCode(string code, string mode)
        {
            if(mode == MongoSharpQueryMode.Json)
            {
                var bsonDoc = BsonDocument.Parse(code);
                return bsonDoc.ToJson(new JsonWriterSettings { Indent = true });
            }

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
            var workspace = new AdhocWorkspace();
            var formattedResult = Formatter.Format(syntaxTree.GetRoot(), workspace);

            var formattedCode = formattedResult.ToFullString();

            return formattedCode;
        }

        //public string FormatCodeRegion(string code, string region)
        //{
        //    string formattedCode = FormatCode(code);
        //    var sb = new StringBuilder();

        //    bool insideRegion = false;
        //    var lines = formattedCode.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        //    foreach (string line in lines)
        //    {
        //        bool startRegion = line.Contains(String.Format("#region {0}", region));
        //        bool endRegion = line.Contains("endregion");

        //        if (startRegion)
        //            insideRegion = true;
        //        else if (endRegion)
        //            insideRegion = false;
        //        else if (insideRegion)
        //            sb.Append(RemoveLeadingTabs(line,6) + '\n');                
        //    }

        //    return sb.ToString();
        //}

        private string RemoveLeadingTabs(string line, int tabCount)
        {
            var tabs = new string(' ', tabCount);

            int pos = line.IndexOf(tabs);
            if (pos == 0)
            {
                line = line.Substring(tabCount);
            }

            return line;
        }
    }
}
