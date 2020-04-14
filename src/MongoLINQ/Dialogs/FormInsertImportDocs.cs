using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;
using MongoSharp.Model;

namespace MongoSharp
{
    public partial class FormInsertImportDocs : Form
    {
        public MongoCollectionInfo CollectionInfo { get; set; }
        private bool _isValidating = false;

        public FormInsertImportDocs()
        {
            InitializeComponent();
        }

        private void FormInsertImportDocs_Load(object sender, EventArgs e)
        {
            Text = "Collection " + CollectionInfo.Name;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedIndex == 0)
            {
                string json = txtJson.Text;
                if(!string.IsNullOrWhiteSpace(json))
                {
                    bool isConverting = true;

                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        var document = BsonSerializer.Deserialize<BsonDocument>(json);
                        isConverting = false;
                        if (!_isValidating)
                        {
                            var mongoCollection = CollectionInfo.Database.GetCollection(CollectionInfo.Name);
                            mongoCollection.Insert(document, WriteConcern.Acknowledged);
                        }
                        else
                        {
                            txtJson.Text = document.ToJson(new JsonWriterSettings { Indent = true });
                            MessageBox.Show("Valid!", "Validation");
                        }
                    }
                    catch(Exception ex)
                    {
                        if(isConverting)
                        {
                            MessageBox.Show("Failed to convert json to Bson Document.\r\n\r\n" +
                                ex.Message, "Invalid JSON", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert document.\r\n\r\n" +
                                ex.Message, "Insert Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }                
            }
            else
            {
                string file = txtBoxImportFile.Text;
                if(string.IsNullOrWhiteSpace(file) || !File.Exists(file))
                {
                    MessageBox.Show("Please select a valid file.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int success=0, failed=0, total=0;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    string json = File.ReadAllText(file);
                    if(!string.IsNullOrWhiteSpace(json))
                    {
                        var bsonDocs = ConvertToBsonDocuments(ParseJson(json, checkBoxIsArray.Checked), cbxStopOnError.Checked && !_isValidating, out success, out failed);
                        total = bsonDocs.Count;
                        if (total > 0 && !_isValidating)
                        {
                            var mongoCollection = CollectionInfo.Database.GetCollection(CollectionInfo.Name);
                            mongoCollection.InsertBatch(bsonDocs, WriteConcern.Acknowledged);    
                        }
                    }
                    Cursor.Current = Cursors.Default;

                    if(!_isValidating)
                    {
                        MessageBox.Show(
                            $"{success} document(s) successfully imported.\r\n{failed} document(s) failed to import.\r\n{total - success - failed} document(s) were skipped.", "Import Complete");
                    }
                    else
                        MessageBox.Show("Valid!", "Validation");
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show("An error occurred while importing.\r\n\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
                
            }
        }

        private List<BsonDocument> ConvertToBsonDocuments(List<string> jsonDocs, bool stopOnError, out int successCount, out int failedCount)
        {
            successCount = 0;
            failedCount = 0;

            var bsonDocs = new List<BsonDocument>();
            foreach(var jsonDoc in jsonDocs)
            {
                try
                {
                    var document = BsonSerializer.Deserialize<BsonDocument>(jsonDoc);
                    bsonDocs.Add(document);

                    successCount++;
                }
                catch (Exception e)
                {
                    failedCount++;
                    if (_isValidating)
                        throw;
                    if (stopOnError)
                        return bsonDocs;
                }
            }

            return bsonDocs;
        }

        private List<string> ParseJson(string json, bool isArray)
        {
            if (isArray)
            {
                var bsonArray = BsonSerializer.Deserialize<BsonArray>(json);
                var docs = bsonArray.Values.ToList().ConvertAll(x => x.ToString());
                return docs;
            }

            var jsonDocs = new List<string>();
            string doc="";
            int openBracketCount=0;

            var chars = json.ToCharArray();
            for(int i=0; i < chars.Length; ++i)
            {
                char c = chars[i];
                if (c == '{')
                {
                    doc += c;
                    openBracketCount++;
                }                    
                else if( c == '}')
                {
                    doc += c;
                    openBracketCount--;
                    if(openBracketCount == 0)
                    {                        
                        jsonDocs.Add(doc);
                        doc = "";
                    }
                }
                else
                    doc += c;
            }

            return jsonDocs;
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            var dialogResult = openFileDialog1.ShowDialog();
            if(dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                txtBoxImportFile.Text = openFileDialog1.FileName;
            }
        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCreate.Text = tabControlMain.SelectedIndex == 0
                                ? "Insert"
                                : "Import";
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {            
            try
            {
                _isValidating = true;
                btnCreate_Click(btnValidate, e);
            }
            finally
            {
                _isValidating = false;
            }
        }
    }
}
