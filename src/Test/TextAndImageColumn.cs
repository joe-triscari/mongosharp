using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Test
{
    //http://csharpuideveloper.blogspot.com/2010/09/how-to-insert-image-with-text-in-one.html

    public class TextAndImageColumn : DataGridViewTextBoxColumn
    {
        private Image imageValue;
        private Size imageSize;

        public TextAndImageColumn()
        {
            this.CellTemplate = new TextAndImageCell();            
        }

        public override object Clone()
        {
            TextAndImageColumn c = base.Clone() as TextAndImageColumn;
            c.imageValue = this.imageValue;
            c.imageSize = this.imageSize;
            return c;
        }

        public Image Image
        {
            get { return this.imageValue; }
            set
            {
                if (this.Image != value)
                {
                    this.imageValue = value;
                    this.imageSize = value.Size;

                    if (this.InheritedStyle != null)
                    {
                        Padding inheritedPadding = this.InheritedStyle.Padding;
                        this.DefaultCellStyle.Padding = new Padding(imageSize.Width,
                     inheritedPadding.Top, inheritedPadding.Right,
                     inheritedPadding.Bottom);
                    }
                }
            }
        }
        private TextAndImageCell TextAndImageCellTemplate
        {
            get { return this.CellTemplate as TextAndImageCell; }
        }
        internal Size ImageSize
        {
            get { return imageSize; }
        }
    }
}
