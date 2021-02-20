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
namespace MongoSharp.Model.CodeAnalysis
{    
    
    public class MethodResult
    {
        public MethodInfo MethodInfo { get; set; }
    }

    public class ConstructorResult
    {
        public ConstructorInfo ConstructorInfo { get; set; }
    }

    public class SymbolResult
    {
        public SymbolResult()
        {
            Symbols = new List<ISymbol>();
            MethodInfos = new List<MethodResult>();
            ConstructorInfos = new List<ConstructorResult>();
        }

        public List<ISymbol> Symbols { get; set; }
        public List<MethodResult> MethodInfos { get; set; }
        public List<ConstructorResult> ConstructorInfos { get; set; } 

        public List<string> GetAutoCompleteList()
        {
            var results = new List<string>();
            var sb = new StringBuilder();
            foreach (ISymbol symbol in Symbols.Where(x => (x.DeclaredAccessibility == Accessibility.Public || x.DeclaredAccessibility == Accessibility.NotApplicable)
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

        public List<string> GetOverLoadList()
        {
            if (MethodInfos.Any())
            {
                return MethodInfos
                    .ToList()
                    .Select(x => x.MethodInfo.MethodSignature() + "\r\n\r\n" + new XmlCommentsHelper().GetMethodComments(x.MethodInfo))
                    .ToList();
            }

            return ConstructorInfos
                .ToList()
                .Select(x => x.ConstructorInfo.ConstructorSignature() + "\r\n\r\n" + new XmlCommentsHelper().GetConstructorComments(x.ConstructorInfo))
                .ToList();
        }
    }
    
    public class CodeAnalysisService
    {
        public SymbolResult LookupSymbols(string source, int pos, char c)
        {
            var result = new SymbolResult();

            var syntaxTree = CSharpSyntaxTree.ParseText(source);
            var semanticModel = GenerateSemanticModel(syntaxTree);

            if (c == '.')
            {
                SyntaxToken syntaxToken = syntaxTree.GetRoot().FindToken(pos - 2);
                SyntaxNode syntaxNode = syntaxToken.Parent;
                if (syntaxNode is ArgumentListSyntax)
                    syntaxNode = syntaxNode.Parent;

                TypeInfo typeInfo = semanticModel.GetTypeInfo(syntaxNode);
                SymbolInfo symbolInfo = semanticModel.GetSymbolInfo(syntaxNode);

                INamespaceOrTypeSymbol container = GetContainer(typeInfo, symbolInfo);
                ImmutableArray<ISymbol> symbols = semanticModel.LookupSymbols(pos - 2, container, includeReducedExtensionMethods: container is ITypeSymbol);
                result.Symbols.AddRange(symbols.ToList());
            }
            else if(c == '(')
            {
                SyntaxToken token = syntaxTree.GetRoot().FindToken(pos - 2);
                SyntaxNode identifier = token.Parent;
                ArgumentListSyntax arglist = null;
                if (identifier is ArgumentListSyntax)
                {
                    arglist = identifier as ArgumentListSyntax;
                    identifier = identifier.Parent;
                }

                var symbolInfo = semanticModel.GetSymbolInfo(identifier);
                if (symbolInfo.CandidateReason == CandidateReason.OverloadResolutionFailure &&
                    symbolInfo.CandidateSymbols.Length > 0 && symbolInfo.CandidateSymbols[0].Kind == SymbolKind.Method)
                {
                    if (arglist != null)
                    {
                        List<ArgumentSyntax> args = arglist.Arguments.ToList();
                        var ex = args[0].Expression;
                    }

                    var methodSymbol = ((IMethodSymbol)symbolInfo.CandidateSymbols[0]);
                    result.MethodInfos.AddRange(GetMethodInfos(methodSymbol).ConvertAll(x => new MethodResult { MethodInfo= x }));
                }
                else if (symbolInfo.Symbol is IMethodSymbol)
                {
                    var methodSymbol = ((IMethodSymbol)symbolInfo.Symbol);
                    result.MethodInfos.AddRange(GetMethodInfos(methodSymbol).ConvertAll(x => new MethodResult { MethodInfo = x }));
                }
                else if (symbolInfo.Symbol is ITypeSymbol)
                {
                    var typeSymbol = symbolInfo.Symbol as ITypeSymbol;
                    result.ConstructorInfos.AddRange(GetConstructorInfos(typeSymbol).ConvertAll(x => new ConstructorResult { ConstructorInfo = x }));
                }
            }
            else
            {

            }
            

            return result;
        }

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

        private List<ConstructorInfo> GetConstructorInfos(ITypeSymbol typeSymbol)
        {
            // IAssemblySymbol containingAssemblySymbol = typeSymbol.ContainingAssembly;
            // Assembly containingAssembly = new AssemblyLoader().GetAssembly(containingAssemblySymbol.Name);
            if (!new TypeHelper().TryFindType(typeSymbol.ToMetadataName(), out var type))
                return new List<ConstructorInfo>();

            ConstructorInfo[] ctors = type.GetConstructors();

            return ctors.ToList();
        }

        private List<MethodInfo> GetMethodInfos(IMethodSymbol methodSymbol)
        {
            var returnType = methodSymbol.ReturnType;

            string methodName = methodSymbol.Name;
            INamedTypeSymbol containingTypeSymbol = methodSymbol.ContainingType;
            new TypeHelper().TryFindType(containingTypeSymbol.ToMetadataName(), out var methodContainingType);

            if (methodContainingType == null)
                return new List<MethodInfo>();

            MethodInfo[] methods = methodContainingType.GetMethods();

            return methods.Where(x => x.Name == methodName).ToList();
        }

        public static CSharpCompilation CreateCompilation(SyntaxTree syntaxTree)
        {
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var compilation = CSharpCompilation.Create("output", options: options)
                .AddSyntaxTrees(syntaxTree)
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(MongoSharpTextWriter).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(IEnumerable<int>).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(IQueryable).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(MongoDB.Bson.BsonDocument).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(MongoDB.Driver.MongoCollection).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(MongoDB.Driver.WriteConcernResult).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(MongoDB.Driver.MongoClient).Assembly.Location)
                );

            return compilation;
        }

        public static SemanticModel GenerateSemanticModel(SyntaxTree syntaxTree)
        {
            var compilation = CreateCompilation(syntaxTree);
            var semanticModel = compilation.GetSemanticModel(syntaxTree);

            return semanticModel;
        }

        public string GetCallTipTextAtPosition(string code, int position)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var semanticModel = GenerateSemanticModel(syntaxTree);

            SyntaxToken token = syntaxTree.GetRoot().FindToken(position);
            SyntaxNode identifier = token.Parent;
            SymbolInfo symbolInfo = semanticModel.GetSymbolInfo(identifier);
            TypeInfo typeInfo = semanticModel.GetTypeInfo(identifier);

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
                string callTip = typeSymbol.ToString() + " " + symbolName + Environment.NewLine + "   " + xmlComments;
                callTip = callTip.TrimEnd(new[] { ' ', '\t', '\n' });
                return Environment.NewLine + "   " + callTip + "   " + Environment.NewLine;
            }
            else if (symbolInfo.Symbol is IMethodSymbol)
            {
                var method = ((IMethodSymbol)symbolInfo.Symbol);
                string xmlComments = new XmlCommentsHelper().GetMethodComments(method);

                string callTip = method.ReturnType.Name + " " + symbolInfo.Symbol.ToDisplayString() + Environment.NewLine + "   " + xmlComments;
                callTip = callTip.TrimEnd(new[] { ' ', '\t', '\n' });
                return Environment.NewLine + "   " + callTip + "   " + Environment.NewLine;
            }

            return null;
        }

        public List<Diagnostic> GetDiagnostics(string source)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(source);
            var semanticModel = GenerateSemanticModel(syntaxTree);

            return (from e in semanticModel.GetDiagnostics()
                    select e).ToList();
        }

    }
}
