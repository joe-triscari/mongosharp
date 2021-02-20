using System;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoSharp.Model;

namespace MongoSharp
{
    public partial class UserControlResultsJson : UserControl, IUserControlQueryResult
    {
        private bool _isLoaded;
        private QueryResult _queryResult;

        public UserControlResultsJson()
        {
            InitializeComponent();
            scintilla1.ConfigurationManager.Language = "cs";
        }

        private string GetPrettyPrintedJson(BsonDocument bsonDoc)
        {
            return bsonDoc.ToJson(new JsonWriterSettings { Indent = true });
        }

        public void OnSelected()
        {
            if (!_isLoaded)
            {
                Cursor.Current = Cursors.WaitCursor;
                
                try
                {
                    int counter = 0;
                    var sb = new StringBuilder();
                    foreach (object rawResult in _queryResult.RawResults)
                    {
                        string json = rawResult is BsonDocument
                            ? GetPrettyPrintedJson((BsonDocument) rawResult)
                            : Newtonsoft.Json.JsonConvert.SerializeObject(rawResult, Formatting.Indented);
                        sb.AppendLine("/* " + counter++ + " */" + System.Environment.NewLine + json);
                    }

                    scintilla1.Text = sb.ToString();
                }
                catch (Exception e)
                {
                    scintilla1.Text = e.Message;
                }

                Cursor.Current = Cursors.Default;
                _isLoaded = true;
            }
        }

        public void LoadResults(QueryResult queryResult)
        {
            _queryResult = queryResult;            
        }
        
    }
}
