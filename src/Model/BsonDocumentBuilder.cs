using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MongoDB.Bson;

namespace MongoSharp.Model
{
    public class BsonDocumentBuilder
    {
        private const int BATCH_SIZE = 500;

        private static readonly FieldInfo BsonElementValueProperty = typeof(BsonElement).GetField("_value", BindingFlags.NonPublic | BindingFlags.Instance);
        public BsonDocument BuildDocument(List<BsonDocument> bsonDocs)
        {
            if (bsonDocs.Count <= BATCH_SIZE)
            {
                var aggregateDoc = new BsonDocument();

                foreach (var bsonDoc in bsonDocs)
                {
                    CombineDocs(ref aggregateDoc, bsonDoc);
                }

                return aggregateDoc;
            }

            var batches = SplitList(bsonDocs);
            List<BsonDocument> rtn = batches.AsParallel().Select(BuildDocument).ToList();
            return BuildDocument(rtn);
        }

        private List<List<BsonDocument>> SplitList(List<BsonDocument> list)
        {
            var listOfLists = new List<List<BsonDocument>>();
            for (int i = 0; i < list.Count(); i += BATCH_SIZE)
            {
                listOfLists.Add(list.Skip(i).Take(BATCH_SIZE).ToList());
            }
            return listOfLists;
        }

        private void CombineDocs(ref BsonDocument aggregateDoc, BsonDocument bsonDoc)
        {
            var elements = aggregateDoc.Elements.ToList();

            foreach (BsonElement el in bsonDoc.Elements)
            {
               // BsonElement currentEl = aggregateDoc.GetElement(el.Name);

                object boxedCurrentEl = (object)elements.Find(e => e.Name == el.Name);
            
                if (el.Value.IsBsonDocument)
                {
                    bool shouldAdd = true;
                    BsonDocument subDoc;
                    
                    if (((BsonElement)boxedCurrentEl).Value == null)
                    {
                        subDoc = new BsonDocument(false);
                    }
                    else
                    {
                        if (((BsonElement)boxedCurrentEl).Value.IsBsonNull || !((BsonElement)boxedCurrentEl).Value.IsBsonDocument ||
                            ((BsonElement)boxedCurrentEl).Value.AsBsonDocument.ElementCount == 0)
                        {
                            aggregateDoc.RemoveElement(((BsonElement)boxedCurrentEl));
                            subDoc = new BsonDocument(false);
                        }
                        else
                        {
                            subDoc = ((BsonElement)boxedCurrentEl).Value.AsBsonDocument;
                            shouldAdd = false;
                        }
                    }
                   
                    CombineDocs(ref subDoc, el.Value.AsBsonDocument);
                    if(shouldAdd)
                        aggregateDoc.Add(new BsonElement(el.Name, subDoc));
                }
                else if (el.Value.IsBsonArray)
                {
                    BsonArray subArray;

                    if (((BsonElement)boxedCurrentEl).Value == null)
                    { // This element doesn't exist in the aggregate document yet.
                        subArray = CombineArray(el.Value.AsBsonArray);
                        aggregateDoc.Add(new BsonElement(el.Name, subArray));
                    }
                    else
                    {
                        if (((BsonElement)boxedCurrentEl).Value.IsBsonNull || !((BsonElement)boxedCurrentEl).Value.IsBsonArray ||
                            ((BsonElement)boxedCurrentEl).Value.AsBsonArray.Count == 0)
                        { // This element does exist in the aggregate document but is no good.
                            aggregateDoc.RemoveElement(((BsonElement)boxedCurrentEl));
                            subArray = CombineArray(el.Value.AsBsonArray);
                            aggregateDoc.Add(new BsonElement(el.Name, subArray));
                        }
                        else
                        { // The array already exists in the aggregate document.
                            var newBsonArray = el.Value.AsBsonArray;
                            if (newBsonArray.Count > 0)
                            {
                                var preCombinedArray = new BsonArray(((BsonElement)boxedCurrentEl).Value.AsBsonArray);
                                preCombinedArray.AddRange(newBsonArray);
                                var combinedArray = CombineArray(preCombinedArray);
                                //boxedCurrentEl.SetValue(combinedArray);
                                BsonElementValueProperty.SetValue(boxedCurrentEl, combinedArray);
                                //currentEl = new BsonElement(currentEl.Name, combinedArray);
                            }                           
                        }
                    }
                }
                else
                {
                    if (((BsonElement)boxedCurrentEl).Value != null)
                    {
                        if (((BsonElement)boxedCurrentEl).Value.IsBsonNull)
                        {
                            //currentEl.SetValue(el.Value.Clone());
                            BsonElementValueProperty.SetValue(boxedCurrentEl, el.Value.Clone());
                            //var test = ((BsonElement) boxedCurrentEl).Value;
                            //currentEl = new BsonElement(currentEl.Name ?? "", BsonNull.Value);
                        }
                        else if (IsTypeGreater(((BsonElement)boxedCurrentEl).Value, el.Value))
                        {
                            //currentEl.SetValue(el.Value.Clone());
                            BsonElementValueProperty.SetValue(boxedCurrentEl, el.Value.Clone());
                            //currentEl = new BsonElement(currentEl.Name, el.Value.Clone());
                        }
                    }
                    else
                    {
                        aggregateDoc.Add(el.Clone());
                    }
                }
            }
        }

        private bool IsTypeGreater(BsonValue lhs, BsonValue rhs)
        {
            if (lhs.IsInt32 && (rhs.IsInt64 || rhs.IsNumeric || rhs.IsDouble))
                return true;

            return false;
        }

        public BsonArray CombineArray(BsonArray bsonArray)
        {
            if (bsonArray == null || bsonArray.Count == 0)
                return new BsonArray();

            if (bsonArray[0].IsBsonDocument)
            {
                List<BsonDocument> bsonDocs = bsonArray.ToList().ConvertAll(x => x.AsBsonDocument);
                var aggregateDoc = BuildDocument(bsonDocs);
                return new BsonArray(new List<BsonDocument> {aggregateDoc});
            }

            foreach (BsonValue bsonValue in bsonArray)
            {
                if (!bsonValue.IsBsonNull)
                    return new BsonArray {bsonValue.Clone()};                       
            }

            return new BsonArray();
        }
    }
}
