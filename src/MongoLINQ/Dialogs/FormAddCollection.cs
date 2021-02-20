using System;
using System.Windows.Forms;

namespace MongoSharp
{
    public partial class FormAddCollection : Form
    {
        public string CollectionName { get; set; }
        public bool Canceled { get; set; }

        public FormAddCollection()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCollection.Text))
            {
                MessageBox.Show("Invalid Collection name");
                return;
            }

            CollectionName = textBoxCollection.Text;
            Canceled = false;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Canceled = true;
            Close();
        }
    }
}
