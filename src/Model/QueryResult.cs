using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MongoDB.Bson;

namespace MongoSharp.Model
{
    public class QueryResult
    {
        private class MultiValueColumn { };

        public List<List<Object>> Results { get; set; }
        public List<PropertyData> Properties { get; set; }
        public Type Type { get; set; }
        public IEnumerable<object> RawResults { get; set; }

        public bool IsBsonDocuments { get; set; }

        public static QueryResult ToQueryResultFromList(IEnumerable<Object> results)
        {
            var queryResult = new QueryResult();
            List<object> list = results.ToList();

            if (list.Any())
            {
                var types = (from x in list
                             where x != null
                             group x by x.GetType() into grp
                             select grp.Key).ToList();

                object firstObj = null;
                if (types.Count < 2)
                {
                    firstObj = list.Find(x => x != null);
                }
                else
                {
                    firstObj = new MultiValueColumn();
                }

                if(firstObj == null)
                {
                    queryResult.Results = list.Select(result => new List<object>()).ToList();
                    queryResult.Properties = new List<PropertyData> { new PropertyData { Name = "Result", Type = typeof(string) } }; 
                }
                else if(firstObj.GetType() == typeof(MultiValueColumn))
                {
                    queryResult.Results = list.Select(result => new List<object> { result }).ToList();
                    queryResult.Properties = new List<PropertyData> { new PropertyData { Name = "Result", Type = typeof(MultiValueColumn) } };
                }
                else if (firstObj.GetType().IsSimpleType())
                {
                    queryResult.Results = list.Select(result => new List<object> { result == null ? "null" : result.ToString() }).ToList();
                    queryResult.Properties = new List<PropertyData> { new PropertyData { Name = "Result", Type = firstObj.GetType().BaseType == typeof(BsonValue) ? typeof(string) : firstObj.GetType() } }; 
                }
                else if (firstObj.GetType() == typeof (BsonDocument))
                {
                    queryResult.Results = new List<List<object>>();
                    foreach (BsonDocument bsonDoc in list)
                    {
                        var innerList = new List<object>();
                        foreach (BsonElement bsonEl in bsonDoc.Elements)
                        {
                            innerList.Add(bsonEl.Value);
                        }

                        queryResult.Results.Add(innerList);
                    }

                    queryResult.Properties = ((BsonDocument)firstObj).Elements.Select(x => new PropertyData
                        {
                            Name = x.Name,
                            Type = x.Value == null ? typeof(BsonValue) : x.Value.GetType()
                        }).ToList();
                }
                else
                {
                    queryResult.Results = new List<List<object>>();
                    
                    foreach (object obj in list)
                    {
                        var innerList = new List<object>();

                        var props = obj.GetType().GetProperties();
                        foreach (PropertyInfo prop in props)
                        {
                            innerList.Add(prop.GetValue(obj, null));
                        }
                        
                        queryResult.Results.Add(innerList);
                    }

                    queryResult.Properties = firstObj.GetType().GetProperties().Select(x => new PropertyData
                        {
                            Name = x.Name,
                            Type = x.PropertyType
                        }).ToList();
                    
                }
            }
            else
            {
                queryResult.Results = new List<List<object>>();                
            }

            return queryResult;
        }

        public static QueryResult ToQueryResult<T>(T result)
        {
            IEnumerable<object> list;
            bool isBsonDocuments = false;

            if (result == null)
                return null;

            if (result is IEnumerable && !(result is BsonDocument))
            {
                MethodInfo toList = typeof(Enumerable).GetMethod("ToList");

                Type[] argTypes = typeof (T).GetGenericArguments();
                MethodInfo genericMethod;
                if (argTypes.Length > 0)
                {
                    Type genericType = typeof (T).GetGenericArguments().First();
                    isBsonDocuments = genericType == typeof(BsonDocument);                    
                    genericMethod = toList.MakeGenericMethod(genericType);
                }
                else
                {
                    genericMethod = toList.MakeGenericMethod(typeof(object));
                }

                var enumerable = (IEnumerable) genericMethod.Invoke(null, new object[] {result});
                list = enumerable.Cast<object>();
                if(list.Any())
                {
                    isBsonDocuments = list.First() is BsonDocument;
                }
              
            }
            else
            {
                list = new List<object> {result};                
            }

            var qr= ToQueryResultFromList(list);
            qr.Type = typeof (T);
            qr.RawResults = list;
            qr.IsBsonDocuments = isBsonDocuments;

            return qr;
        }
    }
}
