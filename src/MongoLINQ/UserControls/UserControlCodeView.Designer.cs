namespace MongoSharp
{
    partial class UserControlCodeView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlCodeView));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.scintillaCodeView = new MongoSharp.StandardScintilla();
            this.imageListCodeComplete = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.scintillaCodeView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(731, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // scintillaCodeView
            // 
            this.scintillaCodeView.Caret.Style = ScintillaNET.CaretStyle.Invisible;
            this.scintillaCodeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaCodeView.Location = new System.Drawing.Point(0, 25);
            this.scintillaCodeView.Name = "scintillaCodeView";
            this.scintillaCodeView.Size = new System.Drawing.Size(731, 593);
            this.scintillaCodeView.TabIndex = 1;
            this.scintillaCodeView.DoubleClick += new System.EventHandler(this.scintillaCodeView_DoubleClick);
            // 
            // imageListCodeComplete
            // 
            this.imageListCodeComplete.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCodeComplete.ImageStream")));
            this.imageListCodeComplete.TransparentColor = System.Drawing.Color.Empty;
            this.imageListCodeComplete.Images.SetKeyName(0, "Method_pub_ext.png");
            this.imageListCodeComplete.Images.SetKeyName(1, "Method_pub.png");
            this.imageListCodeComplete.Images.SetKeyName(2, "Property_pub.png");
            // 
            // UserControlCodeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scintillaCodeView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "UserControlCodeView";
            this.Size = new System.Drawing.Size(731, 618);
            ((System.ComponentModel.ISupportInitialize)(this.scintillaCodeView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private StandardScintilla scintillaCodeView;
        private System.Windows.Forms.ImageList imageListCodeComplete;

    }
}
