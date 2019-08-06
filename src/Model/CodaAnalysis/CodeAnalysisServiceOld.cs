using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypeInfo = Microsoft.CodeAnalysis.TypeInfo;

// http://stackoverflow.com/questions/592139/is-it-possible-to-obtain-class-summary-at-runtime
// http://jolt.codeplex.com/wikipage?title=Jolt.XmlDocComments&referringTitle=Jolt
namespace MongoSharp.Model.CodaAnalysis
{        
    public class CodeAnalysisServiceOld
    {

        private INamespaceOrTypeSymbol GetContainer(TypeInfo typeInfo, SymbolInfo symbolInfo)
        {
            INamespaceOrTypeSymbol container = null;

            if (typeInfo.Type != null)
            {
                container = typeInfo.Type;
            }
            else if (symbolInfo.Symbol is INamespaceSymbol)
            {
                container = (INamespaceSymbol)symbolInfo.Symbol;
            }
            else if (symbolInfo.Symbol is IMethodSymbol)
            {
                ITypeSymbol returnType = ((IMethodSymbol)symbolInfo.Symbol).ReturnType;
                container = returnType;
            }
            else if (symbolInfo.Symbol is INamedTypeSymbol)
            {
                var namedType = ((INamedTypeSymbol)symbolInfo.Symbol);
                container = namedType.OriginalDefinition;
            }

            return container;
        }

        public List<string> GetCompletionListAtPosition(string source, int position)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var compilation = CSharpCompilation.Create("output", options: options)
                .AddSyntaxTrees(syntaxTree)
                .AddReferences(new MetadataFileReference(typeof(object).Assembly.Location),
                               new MetadataFileReference(typeof(MongoSharpTextWriter).Assembly.Location),
                               new MetadataFileReference(typeof(IEnumerable<int>).Assembly.Location),
                               new MetadataFileReference(typeof(IQueryable).Assembly.Location),
                               new MetadataFileReference(typeof(MongoDB.Bson.BsonDocument).Assembly.Location),
                               new MetadataFileReference(typeof(MongoDB.Driver.MongoCollection).Assembly.Location));

            var semanticModel = compilation.GetSemanticModel(syntaxTree);
            //position += 2;
            SyntaxToken token = syntaxTree.GetRoot().FindToken(position);
            SyntaxNode identifier = token.Parent;
            if (identifier is ArgumentListSyntax)
                identifier = identifier.Parent;
            
            TypeInfo typeInfo = semanticModel.GetTypeInfo(identifier);
            SymbolInfo symbolInfo = semanticModel.GetSymbolInfo(identifier);

            INamespaceOrTypeSymbol container = GetContainer(typeInfo, symbolInfo);
            ImmutableArray<ISymbol> symbols = semanticModel.LookupSymbols(position, container, includeReducedExtensionMethods: container is ITypeSymbol);
            
            var results = new List<string>();
            var sb = new StringBuilder();
            foreach (ISymbol symbol in symbols.Where(x => (x.DeclaredAccessibility == Accessibility.Public || x.DeclaredAccessibility == Accessibility.NotApplicable)
                /*&& !x.IsStatic*/))
            {
                sb.AppendLine(symbol.DeclaredAccessibility + " " + symbol.Kind + ": " + symbol.ToDisplayString());

                string symbolName = symbol.Name.Replace('\r', ' ').Replace('\n', ' ').Replace(" ", "");
                if (symbol is IMethodSymbol)
                {
                    if (((IMethodSymbol)symbol).IsGenericMethod)
                        symbolName += "<>";
                    if (((IMethodSymbol)symbol).IsExtensionMethod)
                        symbolName += "?0";
                    else
                        symbolName += "?1";
                }
                else if (symbol is IPropertySymbol)
                {
                    symbolName += "?2";
                }

                results.Add(symbolName);
            }
     
            results = results.Distinct().OrderBy(x => x).ToList();
            return results;
        }

        public List<string> GetMethodCompletionListAtPosition(string source, int position)
        {
            var methodOverloadList = new List<string>();

            try
            {
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
                var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
                var compilation = CSharpCompilation.Create("output", options: options)
                    .AddSyntaxTrees(syntaxTree)
                    .AddReferences(new MetadataFileReference(typeof(object).Assembly.Location),
                                   new MetadataFileReference(typeof(MongoSharpTextWriter).Assembly.Location),
                                   new MetadataFileReference(typeof(IEnumerable<int>).Assembly.Location),
                                   new MetadataFileReference(typeof(IQueryable).Assembly.Location),
                                   new MetadataFileReference(typeof(MongoDB.Bson.BsonDocument).Assembly.Location),
                                   new MetadataFileReference(typeof(MongoDB.Driver.MongoCollection).Assembly.Location));

                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                
                SyntaxToken token = syntaxTree.GetRoot().FindToken(position);
                SyntaxNode identifier = token.Parent;
                ArgumentListSyntax arglist=null;
                if (identifier is ArgumentListSyntax)
                {
                    arglist = identifier as ArgumentListSyntax;
                    identifier = identifier.Parent;
                }                   

                var symbolInfo = semanticModel.GetSymbolInfo(identifier);
                if (symbolInfo.CandidateReason == CandidateReason.OverloadResolutionFailure &&
                    symbolInfo.CandidateSymbols.Length > 0 && symbolInfo.CandidateSymbols[0].Kind == SymbolKind.Method)
                {
                    if(arglist != null)
                    {
                        List<ArgumentSyntax> args = arglist.Arguments.ToList();
                        var ex = args[0].Expression;
                    }

                    var methodSymbol = ((IMethodSymbol)symbolInfo.CandidateSymbols[0]);
                    methodOverloadList.AddRange(GetMethodOverloadList(methodSymbol));
                }
                else if (symbolInfo.Symbol is IMethodSymbol)
                {
                    var methodSymbol = ((IMethodSymbol)symbolInfo.Symbol);
                    methodOverloadList.AddRange(GetMethodOverloadList(methodSymbol));
                }
                else if(symbolInfo.Symbol is ITypeSymbol)
                {
                    var typeSymbol = symbolInfo.Symbol as ITypeSymbol;
                    methodOverloadList.AddRange(GetConstructorOverloadList(typeSymbol));
                }
            }
            catch(Exception e)
            {

            }

            return methodOverloadList;
        }

        private List<string> GetConstructorOverloadList(ITypeSymbol typeSymbol)
        {
           // IAssemblySymbol containingAssemblySymbol = typeSymbol.ContainingAssembly;
           // Assembly containingAssembly = new AssemblyLoader().GetAssembly(containingAssemblySymbol.Name);
            Type type;
            if (!new TypeHelper().TryFindType(typeSymbol.ToMetadataName(), out type))
                return new List<string>();

            ConstructorInfo[] ctors = type.GetConstructors();

            return ctors
                .ToList()
                .Select(x => x.ConstructorSignature() + "\r\n\r\n" + new XmlCommentsHelper().GetConstructorComments(x))
                .ToList();
        }

        private List<string> GetMethodOverloadList(IMethodSymbol methodSymbol)
        {
            var returnType = methodSymbol.ReturnType;

            string methodName = methodSymbol.Name;
            //IAssemblySymbol containingSymbol = methodSymbol.ContainingAssembly;
            //Assembly containingAssembly = new AssemblyLoader().GetAssembly(containingSymbol.Name);
            INamedTypeSymbol containtingTypeSymbol = methodSymbol.ContainingType;
            //Type methodContainingType = containingAssembly.GetType(containtingTypeSymbol.ToString());
            Type methodContainingType;
            new TypeHelper().TryFindType(containtingTypeSymbol.ToMetadataName(), out methodContainingType);
            MethodInfo[] methods = methodContainingType.GetMethods();

            return methods
                .ToList()
                .Where(x => x.Name == methodName)
                .Select(x => x.MethodSignature() + "\r\n\r\n" + new XmlCommentsHelper().GetMethodComments(x))
                .ToList();
        }

        public string GetCallTipTextAtPosition(string code, int position)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var compilation = CSharpCompilation.Create("output", options: options)
                .AddSyntaxTrees(syntaxTree)
                .AddReferences(new MetadataFileReference(typeof(object).Assembly.Location),
                               new MetadataFileReference(typeof(MongoSharpTextWriter).Assembly.Location),
                               new MetadataFileReference(typeof(IEnumerable<int>).Assembly.Location),
                               new MetadataFileReference(typeof(IQueryable).Assembly.Location),
                               new MetadataFileReference(typeof(MongoDB.Bson.BsonDocument).Assembly.Location),
                               new MetadataFileReference(typeof(MongoDB.Driver.MongoCollection).Assembly.Location));

            var semanticModel = compilation.GetSemanticModel(syntaxTree);

            SyntaxToken token = syntaxTree.GetRoot().FindToken(position);
            SyntaxNode identifier = token.Parent;
            SymbolInfo symbolInfo = semanticModel.GetSymbolInfo(identifier);
            Microsoft.CodeAnalysis.TypeInfo typeInfo = semanticModel.GetTypeInfo(identifier);

            ITypeSymbol typeSymbol = typeInfo.Type;
            if (typeSymbol == null && symbolInfo.Symbol != null && symbolInfo.Symbol is ITypeSymbol)
            {
                typeSymbol = (ITypeSymbol)symbolInfo.Symbol.OriginalDefinition;
            }

            var err = (from e in semanticModel.GetDiagnostics()
                       where (e.Severity == DiagnosticSeverity.Error || e.Severity == DiagnosticSeverity.Info) &&
                        position >= e.Location.SourceSpan.Start && position <= (e.Location.SourceSpan.Start + e.Location.SourceSpan.Length)
                       select e.ToString()).ToList();
            if (err.Any())
                return err[0];

            if (typeSymbol != null)
            {
                string xmlComments = new XmlCommentsHelper().GetTypeComments(typeSymbol);

                string symbolName = symbolInfo.Symbol == null ? "" : symbolInfo.Symbol.Name + "   ";
                string callTip = typeSymbol.ToString() + " " + symbolName + System.Environment.NewLine + "   " + xmlComments;
                callTip = callTip.TrimEnd(new[] { ' ', '\t', '\n' });
                return System.Environment.NewLine + "   " + callTip + "   " + System.Environment.NewLine;
            }
            else if (symbolInfo.Symbol is IMethodSymbol)
            {
                var method = ((IMethodSymbol)symbolInfo.Symbol);
                string xmlComments = new XmlCommentsHelper().GetMethodComments(method);

                string callTip = method.ReturnType.Name + " " + symbolInfo.Symbol.ToDisplayString() + System.Environment.NewLine + "   " + xmlComments;
                callTip = callTip.TrimEnd(new[] { ' ', '\t', '\n' });
                return System.Environment.NewLine + "   " + callTip + "   " + System.Environment.NewLine;
            }

            return null;
        }

        public List<Diagnostic> GetDiagnostics(string source)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var compilation = CSharpCompilation.Create("output", options: options)
                .AddSyntaxTrees(syntaxTree)
                .AddReferences(new MetadataFileReference(typeof(object).Assembly.Location),
                               new MetadataFileReference(typeof(MongoSharpTextWriter).Assembly.Location),
                               new MetadataFileReference(typeof(IEnumerable<int>).Assembly.Location),
                               new MetadataFileReference(typeof(IQueryable).Assembly.Location),
                               new MetadataFileReference(typeof(MongoDB.Bson.BsonDocument).Assembly.Location),
                               new MetadataFileReference(typeof(MongoDB.Driver.MongoCollection).Assembly.Location));

            var semanticModel = compilation.GetSemanticModel(syntaxTree);

            return (from e in semanticModel.GetDiagnostics()
                    select e).ToList();
        }

    }
}
