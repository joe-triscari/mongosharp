using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoSharp.Model;

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
