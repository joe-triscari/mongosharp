using System;
using System.Linq;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoSharp.Model;

namespace MongoSharp
{
    public partial class FormAddConnection : Form
    {

        public MongoConnectionInfo MongoConnectionInfo { get; set; }

        public FormAddConnection()
        {
            InitializeComponent();
        }

        private void FormAddConnection_Load(object sender, EventArgs e)
        {
            if (MongoConnectionInfo != null)
            {
                txtBoxName.Text = MongoConnectionInfo.Name;
                txtBoxName.ReadOnly = true;

                txtBoxServer.Text = MongoConnectionInfo.ServerString;
                txtBoxServer.ReadOnly = true;

                txtBoxUsername.Text = MongoConnectionInfo.Username;
                txtBoxUsername.ReadOnly = true;

                txtBoxPassword.Text = MongoConnectionInfo.Password;
                txtBoxPassword.ReadOnly = true;

                txtBoxDatabase.Text = MongoConnectionInfo.Databases.Aggregate("", (current, db) => current + (db.Name + ","));
                txtBoxDatabase.ReadOnly = true;

                btnSave.Visible = false;
                btnTest.Visible = false;

                labelTitle.Text = labelTitle.Text.Replace("Add ", "");
                this.Text = MongoConnectionInfo.Name;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (TestConnection())
            {
                MessageBox.Show("Success!");
            }
        }

        private bool TestConnection()
        {
            MongoClient client;
            MongoServer server;

            if (String.IsNullOrWhiteSpace(txtBoxServer.Text))
            {
                MessageBox.Show("Server missing", "Missing Parameters");
                return false;
            }
            if (String.IsNullOrWhiteSpace(txtBoxDatabase.Text))
            {
                MessageBox.Show("Database missing", "Missing Parameters");
                return false;
            }

            try
            {
                string databaseString = txtBoxDatabase.Text.Trim().TrimEnd(',');
                string connect;
                if (String.IsNullOrWhiteSpace(databaseString))
                {
                    connect = String.IsNullOrWhiteSpace(txtBoxUsername.Text)
                                  ? $"mongodb://{txtBoxServer.Text}"
                                  : $"mongodb://{txtBoxUsername.Text}:{txtBoxPassword.Text}@{txtBoxServer.Text}";
                }
                else
                {
                    connect = String.IsNullOrWhiteSpace(txtBoxUsername.Text) ? $"mongodb://{txtBoxServer.Text}/{databaseString}"
                        : $"mongodb://{txtBoxUsername.Text}:{txtBoxPassword.Text}@{txtBoxServer.Text}/{databaseString}";
                }                

                client = new MongoClient(connect);               
                server = client.GetServer();
                //var databaseNames = server.GetDatabaseNames();
                server.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to server.\r\n\r\n" + ex.Message, "Connection Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var databases = txtBoxDatabase.Text.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string database in databases)
            {
                try
                {
                    MongoDatabase db = server.GetDatabase(database);
                    db.GetCollectionNames();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to connect to database" + database + ".\r\n\r\n" + ex.Message,
                                    "Connection Failed",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        public Action<MongoConnectionInfo> OnAddConnection;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TestConnection())
            {
                if (String.IsNullOrWhiteSpace(txtBoxName.Text))
                {
                    MessageBox.Show("Please enter a connection name");
                    return;
                }

                if (Settings.Instance.Connections.Exists(c => c.Name.Equals(txtBoxName.Text)))
                {
                    MessageBox.Show("A connection with this name already exists");
                    return;
                }

                var newConnection = new MongoConnectionInfo
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = txtBoxName.Text,
                        ServerString = txtBoxServer.Text,
                        Username = txtBoxUsername.Text,
                        Password = txtBoxPassword.Text,
                        Databases =
                            txtBoxDatabase.Text.Split(new[] {','}).ToList().ConvertAll(db => new MongoDatabaseInfo
                                {
                                    Name = db
                                })
                    };
                foreach (var database in newConnection.Databases)
                {
                    database.Connection = newConnection;
                }
                Settings.Instance.Connections.Add(newConnection);
                Settings.Instance.Save();
                Close();

                if (OnAddConnection != null)
                    OnAddConnection(newConnection);
            }            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtBoxPassword.UseSystemPasswordChar = !checkBoxShowPassword.Checked;
        }
        
    }
}
