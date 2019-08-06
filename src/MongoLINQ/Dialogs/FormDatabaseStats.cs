using System;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoSharp.Model;

namespace MongoSharp
{
    public partial class FormDatabaseStats : Form
    {
        public MongoDatabaseInfo MongoDatabaseInfo { get; set; }
        public MongoCollectionInfo MongoCollectionInfo { get; set; }

        public FormDatabaseStats()
        {
            InitializeComponent();
        }

        private void FormDatabaseStats_Load(object sender, EventArgs e)
        {
            try
            {
                CommandResult cmdResult = null;

                if (MongoDatabaseInfo != null)
                {
                    Text = "Database Statistics";
                    lblTitle.Text = "Stats: " + MongoDatabaseInfo.Name;
                    var mongoDatabase = MongoDatabaseInfo.GetMongoDatabase();

                    cmdResult = mongoDatabase.RunCommand(new CommandDocument("dbStats", 1));
                }
                else if (MongoCollectionInfo != null)
                {
                    Text = "Collection Statistics";
                    lblTitle.Text = "Stats: " + MongoCollectionInfo.Name;
                    var mongoDatabase = MongoCollectionInfo.Database.GetMongoDatabase();
                    cmdResult = mongoDatabase.RunCommand(new CommandDocument("collStats", MongoCollectionInfo.Name));
                }

                if (cmdResult != null)
                {
                    if (!cmdResult.Ok)
                        throw new Exception(cmdResult.ErrorMessage);

                    treeViewBson.LoadBsonDocument(cmdResult.Response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
