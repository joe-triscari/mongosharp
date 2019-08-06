using System;
using System.Windows.Forms;

namespace MongoSharp
{
    public partial class FormAddDatabase : Form
    {
        public string DatabaseName { get; set; }
        public bool Canceled { get; set; }

        public FormAddDatabase()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Canceled = true;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(textBoxDatabase.Text))
            {
                MessageBox.Show("Invalid Database name");
                return;
            }

            DatabaseName = textBoxDatabase.Text;
            Canceled = false;
            Close();
        }
    }
}
