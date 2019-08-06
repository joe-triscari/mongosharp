using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DockPanelTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var f4 = new frmEditor();
            f4.Show(dockPanel1, DockState.Document);

            f4.DockStateChanged += f4_DockStateChanged;
        }

        void f4_DockStateChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
