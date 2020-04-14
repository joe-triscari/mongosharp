using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Jolt;
using System.Runtime.CompilerServices;

namespace MongoSharp.Model.CodeAnalysis
{
    public class XmlCommentsHelper
    {
        private static readonly Dictionary<string, XmlDocCommentReader> _commentReaders = new Dictionary<string, XmlDocCommentReader>();

        public string GetMethodComments(IMethodSymbol methodSymbol)
        {
            string xmlComments = string.Empty;

            try
            {
                if (methodSymbol.ReceiverType != null && methodSymbol.ReceiverType.IsType)
                {
                    ITypeSymbol receiverType = methodSymbol.ReceiverType;
                    //var containingAssembly = new AssemblyLoader().GetAssembly(methodSymbol.ContainingAssembly.Name);
                    //Type myType1 = containingAssembly.GetType(receiverType.ToString());
                    new TypeHelper().TryFindType(receiverType.ToMetadataName(), out var myType1);
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

                    if (methodSymbol.IsExtensionMethod && methodInfo == null)
                    {
                        var extenstionsMethods = (from type in containingAssembly.GetTypes()
                                                  where type.IsSealed && !type.IsGenericType && !type.IsNested
                                                  from method in type.GetMethods(BindingFlags.Static
                                                      | BindingFlags.Public | BindingFlags.NonPublic)
                                                  where method.IsDefined(typeof(ExtensionAttribute), false)
                                                  where method.GetParameters()[0].ParameterType.Name == methodSymbol.ReceiverType.BaseType.Name 
                                                    && methodSymbol.MetadataName == method.Name
                                                  select method).ToList();
                        methodInfo = extenstionsMethods.Any() ? extenstionsMethods.First() : null;
                    }
                    var test = reader.GetComments(methodInfo);

                    if (test != null)
                    {
                        xmlComments = test.Element("summary").Value.TrimStart(new[] { ' ', '\t', '\n' }).Replace(Environment.NewLine, " ");
                        xmlComments = Regex.Replace(xmlComments, @"\s+", " ");
                    }
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
            string xmlComments = string.Empty;

            try
            {
                var reader = GetCommentReader(methodInfo.DeclaringType.Assembly);
                var commentElement = reader.GetComments(methodInfo);
                
                xmlComments = commentElement.Element("summary").Value.TrimStart(new[] { ' ', '\t', '\n' }).Replace(Environment.NewLine, " ");
                xmlComments = Regex.Replace(xmlComments, @"\s+", " ");

                var parameters = commentElement.Elements("param").ToList();
                foreach(var parameter in parameters)
                {
                    xmlComments += $"\r\n{parameter.Attribute("name").Value}: {parameter.Value}";
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
            string xmlComments = string.Empty;

            try
            {
                var reader = GetCommentReader(constructorInfo.DeclaringType.Assembly);
                var commentElement = reader.GetComments(constructorInfo);

                xmlComments = commentElement.Element("summary").Value.TrimStart(new[] { ' ', '\t', '\n' }).Replace(Environment.NewLine, " ");
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
            string xmlComments = string.Empty;

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
                                .Replace(Environment.NewLine, " ");

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
            if(_commentReaders.TryGetValue(assembly.FullName, out var reader))
                return reader;

            reader = new XmlDocCommentReader(assembly);
            _commentReaders.Add(assembly.FullName, reader);

            return reader;
        }
    }
}
