using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace MongoSharp
{
    public class TabControlExtended : TabControl
    {
        private Point _lastClickedPos;
        private readonly ContextMenuStrip _contextMenuStrip;

        public delegate void OnExecuteCodeEventHandler(string code);
        public event OnExecuteCodeEventHandler OnExecuteCode;

        public TabControlExtended()
        {
            _contextMenuStrip = new ContextMenuStrip();
            _contextMenuStrip.Items.Add("Close", null, ItemClose_Clicked);
            _contextMenuStrip.Items.Add("Close All", null, ItemCloseAll_Clicked);

            this.Selected += TabControlExtended_Selected;
        }

        void TabControlExtended_Selected(object sender, TabControlEventArgs e)
        {
            Control control;
            var scintilla = e.TabPage.Controls.OfType<ScintillaNET.Scintilla>();
            if (scintilla.Any())
                control = scintilla.First();
            else
                control = e.TabPage;

            WindowManager.Instance.ControlActivated(control);
        }

        private ContextMenuStrip GetContextMenuStrip(Point clickedPoint)
        {
            var contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("Close", null, ItemClose_Clicked);
            contextMenuStrip.Items.Add("Close All", null, ItemCloseAll_Clicked);

            var tabPage = GetClickedTabPage(clickedPoint);
            if (tabPage != null && tabPage.Text.StartsWith("Code"))
            {
                contextMenuStrip.Items.Add("-", null, null);
                contextMenuStrip.Items.Add("Execute Code", null, ItemExecuteCode_Clicked);
            }

            return contextMenuStrip;
        }

        private void ItemExecuteCode_Clicked(object sender, EventArgs e)
        {
            var tabPage = GetClickedTabPage(_lastClickedPos);
            if (tabPage != null)
            {
                var scintillaCode = tabPage.Controls[0].Controls[0];
                string code = scintillaCode.Text;
                if (!string.IsNullOrWhiteSpace(code) && OnExecuteCode != null)
                {
                    OnExecuteCode(code);
                }
            }
        }

        private TabPage GetClickedTabPage(Point clickedPoint)
        {
            for (int i = 0; i < TabCount; i++)
            {
                Rectangle rect = GetTabRect(i);

                if (rect.Contains(PointToClient(clickedPoint)))
                {
                    return TabPages[i];
                }
            }

            return null;
        }

        private void ItemClose_Clicked(object sender, EventArgs e)
        {
            var tabPage = GetClickedTabPage(_lastClickedPos);
            if (tabPage != null)
            {
                TabPages.Remove(tabPage);
            }
        }

        private void ItemCloseAll_Clicked(object sender, EventArgs e)
        {
            for (int i = TabCount - 1; i > 0; i--)
            {
                TabPages.RemoveAt(i);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (e.Button == MouseButtons.Right)
            {
                _lastClickedPos = Cursor.Position;

                Rectangle rect = this.GetTabRect(0);
                if (!rect.Contains(this.PointToClient(_lastClickedPos)))
                {
                    //_contextMenuStrip.Show(_lastClickedPos);
                    var cms = GetContextMenuStrip(_lastClickedPos);
                    if(cms != null) cms.Show(_lastClickedPos);
                }
                
            }
        }

        public int GetTabCountForText(string startsWith)
        {
            int count = 0;

            for (int i = 0; i < this.TabCount; i++)
            {
                if (this.TabPages[i].Text.StartsWith(startsWith))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
