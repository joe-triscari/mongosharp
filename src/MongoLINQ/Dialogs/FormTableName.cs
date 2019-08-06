using System;
using System.Windows.Forms;

namespace MongoSharp
{
    public partial class FormTableName : Form
    {
        public string TableName { get; set; }
        public bool Canceled { get; set; }

        public FormTableName()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Canceled = true;
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            TableName = textBoxTableName.Text;
            Canceled = false;
            Close();
        }

        private void textBoxTableName_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = !String.IsNullOrWhiteSpace(textBoxTableName.Text);
        }
    }
}
