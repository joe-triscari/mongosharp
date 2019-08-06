namespace MongoSharp
{
    partial class FormIndexProperties
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIndexProperties));
            this.treeListView1 = new BrightIdeasSoftware.TreeListView();
            this.olvColumnProperty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnOk = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chkbxUnique = new System.Windows.Forms.CheckBox();
            this.chkbxSparse = new System.Windows.Forms.CheckBox();
            this.chkbxBackground = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListView1
            // 
            this.treeListView1.AllColumns.Add(this.olvColumnProperty);
            this.treeListView1.AllColumns.Add(this.olvColumnValue);
            this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnProperty,
            this.olvColumnValue});
            this.treeListView1.Location = new System.Drawing.Point(0, 62);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.OwnerDraw = true;
            this.treeListView1.ShowGroups = false;
            this.treeListView1.Size = new System.Drawing.Size(377, 215);
            this.treeListView1.TabIndex = 0;
            this.treeListView1.UseCompatibleStateImageBehavior = false;
            this.treeListView1.View = System.Windows.Forms.View.Details;
            this.treeListView1.VirtualMode = true;
            // 
            // olvColumnProperty
            // 
            this.olvColumnProperty.CellPadding = null;
            this.olvColumnProperty.Text = "Property";
            this.olvColumnProperty.Width = 240;
            // 
            // olvColumnValue
            // 
            this.olvColumnValue.CellPadding = null;
            this.olvColumnValue.Text = "Value";
            this.olvColumnValue.Width = 300;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(293, 311);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(0, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(377, 54);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Index Properties";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MongoSharp.Properties.Resources.Properties32x32;
            this.pictureBox1.Location = new System.Drawing.Point(19, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 33);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // chkbxUnique
            // 
            this.chkbxUnique.AutoSize = true;
            this.chkbxUnique.Enabled = false;
            this.chkbxUnique.Location = new System.Drawing.Point(13, 284);
            this.chkbxUnique.Name = "chkbxUnique";
            this.chkbxUnique.Size = new System.Drawing.Size(60, 17);
            this.chkbxUnique.TabIndex = 5;
            this.chkbxUnique.Text = "Unique";
            this.chkbxUnique.UseVisualStyleBackColor = true;
            // 
            // chkbxSparse
            // 
            this.chkbxSparse.AutoSize = true;
            this.chkbxSparse.Enabled = false;
            this.chkbxSparse.Location = new System.Drawing.Point(13, 306);
            this.chkbxSparse.Name = "chkbxSparse";
            this.chkbxSparse.Size = new System.Drawing.Size(59, 17);
            this.chkbxSparse.TabIndex = 6;
            this.chkbxSparse.Text = "Sparse";
            this.chkbxSparse.UseVisualStyleBackColor = true;
            // 
            // chkbxBackground
            // 
            this.chkbxBackground.AutoSize = true;
            this.chkbxBackground.Enabled = false;
            this.chkbxBackground.Location = new System.Drawing.Point(78, 284);
            this.chkbxBackground.Name = "chkbxBackground";
            this.chkbxBackground.Size = new System.Drawing.Size(84, 17);
            this.chkbxBackground.TabIndex = 7;
            this.chkbxBackground.Text = "Background";
            this.chkbxBackground.UseVisualStyleBackColor = true;
            // 
            // FormIndexProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 345);
            this.Controls.Add(this.chkbxBackground);
            this.Controls.Add(this.chkbxSparse);
            this.Controls.Add(this.chkbxUnique);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.treeListView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormIndexProperties";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FormIndexProperties";
            this.Load += new System.EventHandler(this.FormIndexProperties_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.TreeListView treeListView1;
        private BrightIdeasSoftware.OLVColumn olvColumnProperty;
        private BrightIdeasSoftware.OLVColumn olvColumnValue;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox chkbxUnique;
        private System.Windows.Forms.CheckBox chkbxSparse;
        private System.Windows.Forms.CheckBox chkbxBackground;

    }
}