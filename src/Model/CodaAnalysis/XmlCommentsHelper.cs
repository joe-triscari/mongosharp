using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.Collections.Immutable;
using System.Xml;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using Jolt;
using MongoSharp.Model.CodaAnalysis;

namespace MongoSharp.Model.CodaAnalysis
{
    public class XmlCommentsHelper
    {
        private readonly static Dictionary<string, XmlDocCommentReader> _commentReaders = new Dictionary<string, XmlDocCommentReader>();

        public string GetMethodComments(IMethodSymbol methodSymbol)
        {
            string xmlComments = String.Empty;

            try
            {
                if (methodSymbol.ReceiverType != null && methodSymbol.ReceiverType.IsType)
                {
                    ITypeSymbol receiverType = methodSymbol.ReceiverType;
                    //var containingAssembly = new AssemblyLoader().GetAssembly(methodSymbol.ContainingAssembly.Name);
                    //Type myType1 = containingAssembly.GetType(receiverType.ToString());
                    Type myType1;
                    new TypeHelper().TryFindType(receiverType.ToMetadataName(), out myType1);
                    var containingAssembly = myType1.Assembly;

                    var parameterTypes = new List<Type>();
                    foreach (var p in methodSymbol.Parameters)
                    {
                        Assembly parmContainingAssembly = new AssemblyLoader().GetAssembly(p.Type.ContainingAssembly.Name);
                        Type parmType = parmContainingAssembly.GetType(p.Type.ToMetadataName());
                        parameterTypes.Add(parmType);
                    }

                    var reader = new XmlDocCommentReader(containingAssembly);

                    MethodInfo methodInfo = methodSymbol.IsGenericMethod ?
                        myType1.GetGenericMethod(methodSymbol.Name, parameterTypes.ToArray())
                        : myType1.GetMethod(methodSymbol.Name, parameterTypes.ToArray());
                    var test = reader.GetComments(methodInfo);

                    xmlComments = test.Element("summary").Value.TrimStart(new[] { ' ', '\t', '\n' }).Replace(System.Environment.NewLine, " ");
                    xmlComments = Regex.Replace(xmlComments, @"\s+", " ");
                }
            }
            catch(Exception e)
            {
                xmlComments = e.Message;
            }

            return xmlComments;
        }

        public string GetMethodComments(MethodInfo methodInfo)
        {
            string xmlComments = String.Empty;

            try
            {
                var reader = GetCommentReader(methodInfo.DeclaringType.Assembly);
                var commentElement = reader.GetComments(methodInfo);
                
                xmlComments = commentElement.Element("summary").Value.TrimStart(new[] { ' ', '\t', '\n' }).Replace(System.Environment.NewLine, " ");
                xmlComments = Regex.Replace(xmlComments, @"\s+", " ");

                var parameters = commentElement.Elements("param").ToList();
                foreach(var parameter in parameters)
                {
                    xmlComments += String.Format("\r\n{0}: {1}", parameter.Attribute("name").Value, parameter.Value);
                }
                
            }
            catch(Exception e)
            {
                xmlComments = e.Message;
            }

            return xmlComments;
        }

        public string GetConstructorComments(ConstructorInfo constructorInfo)
        {
            string xmlComments = String.Empty;

            try
            {
                var reader = GetCommentReader(constructorInfo.DeclaringType.Assembly);
                var commentElement = reader.GetComments(constructorInfo);

                xmlComments = commentElement.Element("summary").Value.TrimStart(new[] { ' ', '\t', '\n' }).Replace(System.Environment.NewLine, " ");
                xmlComments = Regex.Replace(xmlComments, @"\s+", " ");
            }
            catch (Exception e)
            {
                xmlComments = e.Message;
            }

            return xmlComments;
        }

        public string GetTypeComments(ITypeSymbol typeSymbol)
        {
            string xmlComments = String.Empty;

            try
            {   
                var containingAssembly = new AssemblyLoader().GetAssembly(typeSymbol.ContainingAssembly.Name);
                Type type = null;
                new TypeHelper().TryFindType(typeSymbol.ToMetadataName(), out type);

               // Type type = containingAssembly.GetType(typeSymbol.ToString());
                var reader = GetCommentReader(containingAssembly);
                var commentElement = reader.GetComments(type);
              
                xmlComments = commentElement.Element("summary").Value
                                .TrimStart(new[] { ' ', '\t', '\n' })
                                .Replace(System.Environment.NewLine, " ");

                xmlComments = Regex.Replace(xmlComments, @"\s+", " ");
            }
            catch (Exception e)
            {
                //summary = e.Message;
            }

            return xmlComments;
        }

        private static XmlDocCommentReader GetCommentReader(Assembly assembly)
        {
            XmlDocCommentReader reader;
            if(_commentReaders.TryGetValue(assembly.FullName, out reader))
                return reader;

            reader = new XmlDocCommentReader(assembly);
            _commentReaders.Add(assembly.FullName, reader);

            return reader;
        }
    }
}
