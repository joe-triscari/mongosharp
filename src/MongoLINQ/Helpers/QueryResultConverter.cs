using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using MongoSharp.Model;
using MongoDB.Bson;

namespace MongoSharp
{
    //http://stackoverflow.com/questions/17220959/c-sharp-mongo-queries-with-json-strings
    public class QueryResultConverter
    {
        public CustomDataTable ConvertBsonDocumentsToDataTable(IEnumerable<BsonDocument> bsonDocs)
        {
            var table = new CustomDataTable();           
            
            var columns = new List<DataColumn>();
            foreach(var bsonDoc in bsonDocs)
            {
                foreach(var el in bsonDoc.Elements)
                {
                    if(!columns.Exists(x => x.ColumnName == el.Name))
                    {
                        var column = new DataColumn
                                    {
                                        DataType = el.Value.IsBsonDocument || el.Value.IsBsonArray ? typeof(CustomDataTable) : typeof(string),
                                        ColumnName = el.Name,
                                        AllowDBNull = true
                                    };
                        table.Columns.Add(column);
                        columns.Add(column);
                    }
                }
            }

            foreach (var bsonDoc in bsonDocs)
            {
                DataRow dataRow = table.NewRow();

                foreach (var el in bsonDoc.Elements)
                {
                    string propName = el.Name;
                    if (el.Value.IsBsonNull)
                    {
                        dataRow[propName] = DBNull.Value;
                    }
                    else if (el.Value.IsBsonDocument || el.Value.IsBsonArray)
                    {
                        var placeHolderDataTable = new CustomDataTable
                                                {
                                                    IsLoaded = false,
                                                    OriginalObject = el.Value.IsBsonDocument ? el.Value.AsBsonDocument : (object)el.Value.AsBsonArray
                                                };                        
                        dataRow[propName] = placeHolderDataTable;
                    }
                    else
                    {
                        dataRow[propName] = el.Value;
                    }
                }

                table.Rows.Add(dataRow);
            }

            return table;
        }

        public CustomDataTable ConvertToDataTable(QueryResult queryResult)
        {
            var table = new CustomDataTable();

            if (queryResult.Results == null || !queryResult.Results.Any())
                return table;

            if (queryResult.IsBsonDocuments)
            {
                return ConvertBsonDocumentsToDataTable(queryResult.RawResults.Cast<BsonDocument>());
            }

            foreach (PropertyData prop in queryResult.Properties)
            {
                var column = new DataColumn
                    {
                        DataType = prop.GetUnderLyingType().IsSimpleType() ? prop.GetUnderLyingType() : typeof(CustomDataTable),
                        ColumnName = prop.Name,
                        AllowDBNull = true
                    };
                table.Columns.Add(column);
            }

            foreach (List<object> resultRow in queryResult.Results)
            {
                DataRow dataRow = table.NewRow();
                int columnIndex = 0;

                foreach (object obj in resultRow)
                {                    
                    string propName = queryResult.Properties[columnIndex++].Name;
                    if(obj == null)
                    {
                        dataRow[propName] = DBNull.Value;
                    }
                    else if (table.Columns[propName].DataType == typeof(CustomDataTable))
                    {
                        var placeHolderDataTable = new CustomDataTable {IsLoaded = false, OriginalObject = obj};
                        dataRow[propName] = placeHolderDataTable;
                    }
                    else
                    {
                        dataRow[propName] = obj;
                    }                    
                }

                table.Rows.Add(dataRow);
            }

            return table;
        }

        #region Convert QueryResult to TreeModels
        public List<ResultTreeNodeModel> ConvertToTreeNodeModels(QueryResult queryResult)
        {
            var nodes = new List<ResultTreeNodeModel>();
            int rowCounter = 0;

            if (queryResult.IsBsonDocuments)
            {
                foreach (BsonDocument bsonDoc in queryResult.RawResults)
                {
                    var node = new ResultTreeNodeModel
                                {
                                    Name = "(" + rowCounter++ + ") {..}",
                                    RawName = bsonDoc["_id"].ToString(),
                                    Value = "",
                                    Type = bsonDoc.BsonType.ToString(),
                                    BsonDocument = bsonDoc,
                                    IsValue = false,
                                    BsonUpdateQuery = bsonDoc["_id"].ToString()
                                };
                    node.Children.AddRange(ConvertBsonDocumentToTreeNodeModels(bsonDoc, node));
                    node.Children.ForEach(c => c.Parent = node);
                    nodes.Add(node);
                }
            }
            else
            {
                foreach (List<object> rowResults in queryResult.Results)
                {
                    var node = new ResultTreeNodeModel
                                {
                                    Name = "(" + rowCounter++ + ") {..}",
                                    Value = "",
                                    Type = queryResult.Type.ToString()
                                            .Replace("System.Linq.IQueryable`1[", "")
                                            .Replace("MongoSharp.Query.", "")
                                            .Replace("]", "")
                                };
                    node.Children.AddRange(ConvertToTreeNodeModels(rowResults, queryResult.Properties));

                    nodes.Add(node);
                }
            }

            return nodes;
        }

        private List<ResultTreeNodeModel> ConvertToTreeNodeModels(List<object> rowResult, List<PropertyData> properties)
        {
            var models = new List<ResultTreeNodeModel>();
            int columnIndex = 0;

            foreach (object obj in rowResult)
            {
                models.Add(ConvertToTreeNodeModel(obj, properties[columnIndex++]));
            }

            return models;
        }

        private ResultTreeNodeModel ConvertToTreeNodeModel(object obj, PropertyData propertyData)
        {

            if (propertyData.GetUnderLyingType().IsSimpleType() || obj == null)
            {
                return new ResultTreeNodeModel
                        {
                            Name = propertyData.Name,
                            Value = obj == null ? "null" : obj.ToString(),
                            Type = propertyData.FriendlyTypeName
                        };
            }

            if (obj is IEnumerable enumerable && !(enumerable is BsonDocument))
            {
                int count = enumerable.Cast<object>().Count();

                var rowDoc = new ResultTreeNodeModel
                {
                    Name = propertyData.Name + "[" + count + "]",
                    Value = null,
                    Type = propertyData.FriendlyTypeName
                };
                var innerQueryResult = QueryResult.ToQueryResult(obj);
                rowDoc.Children.AddRange(ConvertToTreeNodeModels(innerQueryResult));
                return rowDoc;
            }
            else
            {
                var rowDoc = new ResultTreeNodeModel
                {
                    Name = propertyData.Name + " {..}",
                    Value = null,
                    Type = propertyData.FriendlyTypeName
                };
                var innerQueryResult = QueryResult.ToQueryResult(obj);
                foreach (List<object> rowResults in innerQueryResult.Results)
                {
                    rowDoc.Children.AddRange(ConvertToTreeNodeModels(rowResults, innerQueryResult.Properties));
                }
                return rowDoc;
            }
        }

        #region BsonDocuments to TreeModels
        public List<ResultTreeNodeModel> ConvertBsonDocumentToTreeNodeModels(BsonDocument bsonDoc, ResultTreeNodeModel parentNode)
        {
            var list = new List<ResultTreeNodeModel>();
            ConvertBsonDocumentToTreeNodeModels(bsonDoc, parentNode, ref list);
            return list;
        }

        private ResultTreeNodeModel ConvertBsonValueToTreeNodeModel(BsonValue bsonValue, ResultTreeNodeModel parentNode, string name, int index)
        {
            var node = new ResultTreeNodeModel
                        {
                            Name = name,
                            RawName = name,
                            Type = bsonValue.BsonType.ToString(),
                            BsonType = bsonValue.BsonType,
                            IsValue = true,
                            Parent = parentNode,
                            BsonUpdateQuery = $"{parentNode.BsonUpdateQuery}.{index}"
                        };

            if(bsonValue.IsBsonNull)
            {
                node.Value = "null";
            }
            else if(bsonValue.IsBsonDocument)
            {
                var bsonDoc = bsonValue.AsBsonDocument;
                node.Name += " {..}";
                node.IsValue = false;
                node.BsonDocument = bsonDoc;
                var tmpList = node.Children;
                ConvertBsonDocumentToTreeNodeModels(bsonDoc, node, ref tmpList);
            }
            else
            {
                node.Value = bsonValue.ToString();
            }

            return node;
        }

        private void ConvertBsonDocumentToTreeNodeModels(BsonDocument bsonDoc, ResultTreeNodeModel parentNode, ref List<ResultTreeNodeModel> list)
        {
            foreach (BsonElement el in bsonDoc.Elements)
            {
                if (el.Value.IsBsonDocument)
                {
                    var innerBsonDoc = el.Value.AsBsonDocument;
                    var node = new ResultTreeNodeModel
                                    {
                                        Name = el.Name + " {..}",
                                        RawName = el.Name,
                                        Value = null,
                                        Type = el.Value.BsonType.ToString(),
                                        BsonType = el.Value.BsonType,
                                        BsonDocument = innerBsonDoc,
                                        IsValue = false,
                                        BsonUpdateQuery = parentNode.BsonUpdateQuery + "." + el.Name,
                                        Parent = parentNode
                                    };
                    list.Add(node);
                    var tmpList = node.Children;
                    ConvertBsonDocumentToTreeNodeModels(innerBsonDoc, node, ref tmpList);
                }
                else if(el.Value.IsBsonArray)
                {
                    var bsonArray = el.Value.AsBsonArray;
                    var node = new ResultTreeNodeModel
                                        {
                                            Name = el.Name + "[" + bsonArray.Count + "]",
                                            RawName = el.Name,
                                            Value = null,
                                            Type = bsonArray.BsonType.ToString(),
                                            BsonType = bsonArray.BsonType,
                                            IsValue = false,
                                            IsArray = true,
                                            BsonUpdateQuery = parentNode.BsonUpdateQuery + "." + el.Name,
                                            Parent = parentNode
                                        };
                    int row = 0;
                    foreach(BsonValue bsonValue in bsonArray)
                    {
                        node.Children.Add(ConvertBsonValueToTreeNodeModel(bsonValue, node, "(" + row + ")", row));
                        row += 1;
                    }
                    list.Add(node);
                }
                else
                {                    
                    list.Add(new ResultTreeNodeModel
                                    {
                                        Name = el.Name,
                                        RawName = el.Name,
                                        Value = el.Value.ToString(),
                                        Type = el.Value.BsonType.ToString(),
                                        BsonType = el.Value.BsonType,
                                        IsValue = true,
                                        BsonUpdateQuery = parentNode.BsonUpdateQuery + "." + el.Name,
                                        Parent = parentNode
                                    });
                }
            }
        }
        #endregion
        #endregion
    }
}
