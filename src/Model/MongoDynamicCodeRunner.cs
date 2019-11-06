using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace MongoSharp.Model
{
    public class MongoDynamicCodeRunner
    {        
        public List<QueryResult> CompileAndRun(string code, string mode, TextWriter textWriter)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var compilation = CSharpCompilation.Create("output", options: options)
                .AddSyntaxTrees(syntaxTree)
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                               MetadataReference.CreateFromFile(typeof(MongoSharpTextWriter).Assembly.Location),
                               MetadataReference.CreateFromFile(typeof(IEnumerable<int>).Assembly.Location),
                               MetadataReference.CreateFromFile(typeof(IQueryable).Assembly.Location),
                               MetadataReference.CreateFromFile(typeof(MongoDB.Bson.BsonDocument).Assembly.Location),
                               MetadataReference.CreateFromFile(typeof(MongoDB.Driver.MongoCollection).Assembly.Location),
                               MetadataReference.CreateFromFile(typeof(MongoDB.Driver.MongoClient).Assembly.Location)
                               );
        
            using (var stream = new MemoryStream())
            {
                EmitResult result = compilation.Emit(stream);

                if (result.Success)
                {
                    var assembly = Assembly.Load(stream.GetBuffer());
                    Module module = assembly.GetModules()[0];
                    if (module == null)
                        throw new Exception("Module is null");

                    Type mt = module.GetType("MongoSharp.Query.QueryExecutor");

                    if (mt == null)
                        throw new Exception("Type is null");

                    MethodInfo methInfo = mt.GetMethod("RunQuery");

                    if (methInfo == null)
                        throw new Exception("MethodInfo is null");

                    var results = (List<QueryResult>)methInfo.Invoke(null, new[] { textWriter });
                    return results;
                }
                else
                {
                    int lineNbr = GetCodeStartLineNumber(code) -1;
                    var errors = (from x in result.Diagnostics
                                  where x.Severity == DiagnosticSeverity.Error
                                  select x).ToList();
                    string text = errors.Aggregate("Compile error: ", (current, ce) => current + ("\r\n" + GetErrorText(ce, lineNbr)));
                    throw new Exception(text);
                }
            }
        }

        private int GetCodeStartLineNumber(string code)
        {
            var lines = code.Split('\n');

            for (int line = 0; line < lines.Length; line++)
            {
                if (lines[line].Contains("#region Injected Code"))
                    return line + 2;
            }

            return -1;
        }

        private string GetErrorText(Diagnostic d, int lineNbr)
        {
            return
                $"(Line {(d.Location.GetLineSpan().StartLinePosition.Line - lineNbr) + 1},{d.Location.GetLineSpan().StartLinePosition.Character}) {d.Id} : {d.GetMessage()}";
        }

        public List<Type> CompileModelCode(string code)
        {
            return CompileModelCode(code, "MongoSharp.Models");
        }

        public List<Type> CompileModelCode(string code, string theNamespace)
        {
            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using MongoDB.Bson;");
            sb.AppendLine("using MongoDB.Bson.Serialization.Attributes;");
            sb.AppendLine("using MongoDB.Driver;");
            sb.AppendLine("using MongoDB.Driver.Linq;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using MongoSharp.Model;");
            code = sb.ToString() + "namespace " + theNamespace + " {\r\n" + code + "\r\n}";

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
            
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var compilation = CSharpCompilation.Create("output", options: options)
                .AddSyntaxTrees(syntaxTree)
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                               MetadataReference.CreateFromFile(typeof(MongoSharpTextWriter).Assembly.Location),
                               MetadataReference.CreateFromFile(typeof(IEnumerable<int>).Assembly.Location),
                               MetadataReference.CreateFromFile(typeof(IQueryable).Assembly.Location),
                               MetadataReference.CreateFromFile(typeof(MongoDB.Bson.BsonDocument).Assembly.Location),
                               MetadataReference.CreateFromFile(typeof(MongoDB.Driver.MongoCollection).Assembly.Location),
                               MetadataReference.CreateFromFile(typeof(MongoDB.Driver.MongoClient).Assembly.Location)
                               );

            using (var stream = new MemoryStream())
            {
                EmitResult result = compilation.Emit(stream);

                if (result.Success)
                {
                    var assembly = Assembly.Load(stream.GetBuffer());
                    Module module = assembly.GetModules()[0];
                    if (module == null)
                        throw new Exception("Module is null");

                    List<Type> types = (from t in module.GetTypes()
                                        where t.IsClass && t.Namespace == theNamespace
                                        select t).ToList();
                    return types;
                }
                else
                {
                    var errors = (from x in result.Diagnostics
                                  where x.Severity == DiagnosticSeverity.Error
                                  select x).ToList();
                    string text = errors.Aggregate("Compile error: ", (current, ce) => current + ("\r\n" + ce.GetMessage()));
                    throw new Exception(text);
                }
            }
        }
    }
}
