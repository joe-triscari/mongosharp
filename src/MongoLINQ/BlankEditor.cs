using ScintillaNET;
using WeifenLuo.WinFormsUI.Docking;

namespace MongoSharp
{
    public partial class BlankEditor : DockContent
    {
        public BlankEditor()
        {
            InitializeComponent();

            scintilla1.ConfigurationManager.Language = "mssql";
            scintilla1.Caret.Style = CaretStyle.Line;
        }

        public string Contents
        {
            set => scintilla1.Text = value;
        }

        private void closeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            
        }

        private void closeAllToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            EditorWindowManager.CloseAll();
        }
    }
}
