using System;
using System.Windows.Forms;
using MongoSharp.Model;

namespace MongoSharp
{
    public partial class FormAddCodeSnippet : Form
    {
        public FormAddCodeSnippet()
        {
            InitializeComponent();
        }

        public CodeSnippet CodeSnippet { get; private set; }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text.Trim();
            if (String.IsNullOrWhiteSpace(name))
                return;

            if (Settings.Instance.CodeSnippets.Exists(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
            {
                MessageBox.Show("Snippet already exists with this name. Enter a new name.", "Duplicate Name");
                return;
            }

            CodeSnippet = new Model.CodeSnippet
                            {
                                Name = name,
                                Description = textBoxDescription.Text.Trim()
                            };
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            CodeSnippet = null;
            Close();
        }
    }
}
