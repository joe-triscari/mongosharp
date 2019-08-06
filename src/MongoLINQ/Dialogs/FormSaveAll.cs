using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MongoSharp
{
    public partial class FormSaveAll : Form
    {
        private readonly List<EditorWindow> _docsToSave;
        public bool WasCancelled { get; private set; }

        public FormSaveAll(List<EditorWindow> docsToSave)
        {
            _docsToSave = docsToSave;
            InitializeComponent();

            checkedListBoxSaveItems.DisplayMember = "SaveText";
        }

        private void FormSaveAll_Load(object sender, EventArgs e)
        {
            if (_docsToSave != null && _docsToSave.Any())
            {
                foreach (EditorWindow editorWindow in _docsToSave)
                {
                    checkedListBoxSaveItems.Items.Add(editorWindow);

                    for (int i = 0; i < checkedListBoxSaveItems.Items.Count; i++)
                        checkedListBoxSaveItems.SetItemChecked(i, true);
                }                
            }
            else
            {
                Close();
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            WasCancelled = false;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            WasCancelled = true;
            Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            var checkedDocs = checkedListBoxSaveItems.CheckedItems.Cast<EditorWindow>();
            foreach (EditorWindow doc in checkedDocs)
            {
                if (!EditorWindowManager.SaveDocument(doc))
                {
                    WasCancelled = true;
                    Close();
                    return;
                }
            }

            WasCancelled = false;
            Close();
        }        
    }
}
