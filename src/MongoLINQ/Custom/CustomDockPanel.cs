using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MongoSharp
{
    public class CustomDockPanel : WeifenLuo.WinFormsUI.Docking.DockPanel
    {
        public CustomDockPanel()
        {
           // this.SetStyle(ControlStyles.UserPaint, true);
            this.ResizeRedraw = true;
            //this.BackgroundImage = Image.FromFile(@"C:\DEV\icons\png\48x48\Cube.png");
            //BackgroundImageLayout = ImageLayout.Center;
            this.DockBackColor = Color.White;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            try
            {
                //using (var brush = new LinearGradientBrush(ClientRectangle, Color.White, Color.White,
                //                                           LinearGradientMode.BackwardDiagonal))
                //{
                //    e.Graphics.FillRectangle(brush, ClientRectangle);
                //}

                //using (var brush = new LinearGradientBrush(ClientRectangle, Color.White, Color.FromArgb(220, 229, 232),
                //                                           LinearGradientMode.BackwardDiagonal))
                //{
                //    e.Graphics.FillRectangle(brush, ClientRectangle);
                //}

                using (var brush = new LinearGradientBrush(ClientRectangle, Color.FromArgb(243, 243, 243),Color.FromArgb(222, 222, 222),
                                                           LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, ClientRectangle);
                }

            }
            catch
            {
            }

            try
            {
                var image = MongoSharp.Properties.Resources.Logo48x48;
                e.Graphics.DrawImage(image, this.Width - image.Width - 50, this.Height - 100);
            }
            catch {}
            
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            this.Invalidate();
            base.OnScroll(se);
        }
    }
}
