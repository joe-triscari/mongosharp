using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace MongoSharp.Model
{
    public class BsonDocumentBuilder
    {
        private const int BATCH_SIZE = 500;

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

                BsonElement currentEl = elements.Find(e => e.Name == el.Name);
                if (el.Value.IsBsonDocument)
                {
                    bool shouldAdd = true;
                    BsonDocument subDoc;
                    
                    if (currentEl == null)
                    {
                        subDoc = new BsonDocument(false);
                    }
                    else
                    {
                        if (currentEl.Value.IsBsonNull || !currentEl.Value.IsBsonDocument ||
                            currentEl.Value.AsBsonDocument.ElementCount == 0)
                        {
                            aggregateDoc.RemoveElement(currentEl);
                            subDoc = new BsonDocument(false);
                        }
                        else
                        {
                            subDoc = currentEl.Value.AsBsonDocument;
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

                    if (currentEl == null)
                    { // This element doesn't exist in the aggregate document yet.
                        subArray = CombineArray(el.Value.AsBsonArray);
                        aggregateDoc.Add(new BsonElement(el.Name, subArray));
                    }
                    else
                    {
                        if (currentEl.Value.IsBsonNull || !currentEl.Value.IsBsonArray ||
                            currentEl.Value.AsBsonArray.Count == 0)
                        { // This element does exist in the aggregate document but is no good.
                            aggregateDoc.RemoveElement(currentEl);
                            subArray = CombineArray(el.Value.AsBsonArray);
                            aggregateDoc.Add(new BsonElement(el.Name, subArray));
                        }
                        else
                        { // The array already exists in the aggregate document.
                            var newBsonArray = el.Value.AsBsonArray;
                            if (newBsonArray.Count > 0)
                            {
                                var preCombinedArray = new BsonArray(currentEl.Value.AsBsonArray);
                                preCombinedArray.AddRange(newBsonArray);
                                var combinedArray = CombineArray(preCombinedArray);
                                currentEl.Value = combinedArray;
                            }                           
                        }
                    }
                }
                else
                {
                    if (currentEl != null)
                    {
                        if (currentEl.Value.IsBsonNull)
                        {
                            currentEl.Value = el.Value.Clone();
                        }
                        else if (IsTypeGreater(currentEl.Value, el.Value))
                        {
                            currentEl.Value = el.Value.Clone();
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
