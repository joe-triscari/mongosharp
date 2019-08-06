using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;
using MongoSharp.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson.IO;

namespace MongoSharp.Dialogs
{
    public partial class FormEditJsonDocument : Form
    {
        public MongoCollection MongoCollection { get; set; }
        public BsonDocument BsonDocument { get; set; }

        public delegate void UpdateDocumentDelegate(string json);
        public UpdateDocumentDelegate UpdateDocument;

        public FormEditJsonDocument()
        {
            InitializeComponent();
            scintillaJson.SetLanguage("js");
            scintillaJson.ShowLineNumbers(Settings.Instance.Preferences.ShowEditorLineNumbers);
            scintillaJson.Caret.Style = CaretStyle.Line;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormEditJsonDocument_Load(object sender, EventArgs e)
        {
            scintillaJson.Text = BsonDocument.ToJson(new JsonWriterSettings { Indent = true });
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (UpdateDocument != null)
                    UpdateDocument(scintillaJson.Text);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
