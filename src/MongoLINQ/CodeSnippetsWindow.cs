using System;
using System.Windows.Forms;
using MongoSharp.Model;

namespace MongoSharp
{
    public partial class CodeSnippetsWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public delegate void OnInsertCodeEventHandler(string code);
        public event OnInsertCodeEventHandler OnInsertCode;

        public CodeSnippetsWindow()
        {
            InitializeComponent();
        }

        private void CodeSnippetsWindow_Load(object sender, EventArgs e)
        {
            LoadCodeSnippets();
        }

        public void AddCodeSnippet(CodeSnippet snippet, bool isNew = true)
        {
            if (isNew)
            {
                if (Settings.Instance.CodeSnippets.Exists(x => x.Name.Equals(snippet.Name, StringComparison.InvariantCultureIgnoreCase)))
                    throw new Exception("Snippet with same name already exists.");
            }

            var newRowIndex = dataGridView1.Rows.Add();
            var nameColumn = (DataGridViewTextBoxCell)dataGridView1.Rows[newRowIndex].Cells[0];
            nameColumn.Value = snippet.Name;

            var descriptionColumn = (DataGridViewTextBoxCell)dataGridView1.Rows[newRowIndex].Cells[1];
            descriptionColumn.Value = snippet.Description;

            var codeColumn = (DataGridViewTextBoxCell)dataGridView1.Rows[newRowIndex].Cells[2];
            codeColumn.Value = snippet.Code;

            if(isNew)
            {
                Settings.Instance.CodeSnippets.Add(snippet);
                Settings.Instance.Save();
            }
        }

        public void LoadCodeSnippets()
        {
            dataGridView1.Rows.Clear();
            foreach (var snippet in Settings.Instance.CodeSnippets)
                AddCodeSnippet(snippet, false);
        }

        private void toolStripButtonRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
                return;

            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            if (rowIndex < 0)
                return;

            var nameColumn = (DataGridViewTextBoxCell)dataGridView1.Rows[rowIndex].Cells[0];
            string name = nameColumn.Value.ToString();

            dataGridView1.Rows.RemoveAt(rowIndex);

            Settings.Instance.CodeSnippets.RemoveAll(x => x.Name == name);
            Settings.Instance.Save();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            InsertCodeSnippet();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            InsertCodeSnippet();
        }   

        private void InsertCodeSnippet()
        {
            var row = dataGridView1.CurrentRow;
            if (row == null)
                return;

            var codeColumn = (DataGridViewTextBoxCell)dataGridView1.Rows[row.Index].Cells[2];
            string code = codeColumn.Value.ToString();

            if (OnInsertCode != null && !string.IsNullOrEmpty(code))
                OnInsertCode(code);
        }
    }
}
