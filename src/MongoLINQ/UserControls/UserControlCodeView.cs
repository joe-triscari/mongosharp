using System;
using System.Windows.Forms;

namespace MongoSharp
{
    public partial class UserControlCodeView : UserControl
    {
        public UserControlCodeView()
        {
            InitializeComponent();
        }

        private void scintillaCodeView_DoubleClick(object sender, EventArgs e)
        {
            //int curpos = scintillaCodeView.CurrentPos;
            //string linqQuery = scintillaCodeView.Text;
            //var typeName = new MongoDynamicCodeRunner().GetTypeAtPostion(linqQuery, curpos);
            //WindowManager.Instance.OutputWindow.AppendOutput(typeName);
        }

    }
}
