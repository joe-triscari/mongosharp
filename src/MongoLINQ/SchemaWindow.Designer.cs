namespace MongoSharp
{
    partial class SchemaWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaWindow));
            this.listBoxSchema = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxSchema
            // 
            this.listBoxSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSchema.FormattingEnabled = true;
            this.listBoxSchema.HorizontalScrollbar = true;
            this.listBoxSchema.Location = new System.Drawing.Point(0, 0);
            this.listBoxSchema.Name = "listBoxSchema";
            this.listBoxSchema.Size = new System.Drawing.Size(284, 262);
            this.listBoxSchema.TabIndex = 0;
            // 
            // SchemaWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.listBoxSchema);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SchemaWindow";
            this.Text = "Collection Schema";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxSchema;
    }
}