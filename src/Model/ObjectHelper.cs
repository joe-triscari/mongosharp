using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MongoSharp.Model
{
    static public class ObjectHelper
    {

        static public List<PropertyData> ToPropertyPaths(Type theType)
        {
            var propPaths = new List<PropertyData>();

            ToPropertyPaths(theType, "", ref propPaths);

            return propPaths;
        }

        static private void ToPropertyPaths(Type theType, string parentObject, ref List<PropertyData> propertyPaths)
        {
            var collections = new List<Type> { typeof(IEnumerable<>), typeof(IEnumerable) };

            PropertyInfo[] props = theType.GetProperties();
            foreach (var prop in props)
            {
                var propType = prop.PropertyType;
                if (propType.IsSimpleType() || propType.Name == "ObjectId" || propType.Name.StartsWith("Bson"))
                {
                    propertyPaths.Add(!String.IsNullOrWhiteSpace(parentObject)
                                          ? new PropertyData
                                              {
                                                  Path = parentObject + "." + prop.Name,
                                                  Type = propType
                                              }
                                          : new PropertyData { Path = prop.Name, Type = propType });
                }
                else if (propType != typeof(string) && propType.GetInterfaces().Any(i => collections.Any(c => i == c)))
                {
                    propType = propType.GetElementType();
                    if (propType.IsSimpleType() || propType.Name == "ObjectId" || propType.Name.StartsWith("Bson"))
                    {
                        propertyPaths.Add(!String.IsNullOrWhiteSpace(parentObject)
                                              ? new PropertyData
                                                  {
                                                      Path = parentObject + "." + prop.Name + "[]",
                                                      Type = propType
                                                  }
                                              : new PropertyData {Path = prop.Name + "[]", Type = propType});
                    }
                    else
                    {
                        ToPropertyPaths(propType,
                                        !String.IsNullOrWhiteSpace(parentObject)
                                            ? parentObject + "." + prop.Name + "[]"
                                            : prop.Name + "[]", ref propertyPaths);
                    }                    
                }
                else if (propType.IsClass)
                {
                    string name;
                    if (!String.IsNullOrWhiteSpace(parentObject))
                        name = parentObject + "." + prop.Name;
                    else
                        name = prop.Name;

                    ToPropertyPaths(propType, name, ref propertyPaths);      
                }
                else if (propType.IsArray)
                {
                    propType = propType.GetElementType();

                    propertyPaths.Add(!String.IsNullOrWhiteSpace(parentObject)
                                          ? new PropertyData
                                          {
                                              Path = parentObject + "." + prop.Name + "[]",
                                              Type = propType
                                          }
                                          : new PropertyData { Path = prop.Name + "[]", Type = propType });
                }
                else
                {
                    var sh = 0;

                }
            }
        }
    }
}
