namespace MongoSharp
{
    partial class UserControlBsonTreeView
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
            this.treeListViewBson = new BrightIdeasSoftware.TreeListView();
            this.olvColumnProperty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.treeListViewBson)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListViewBson
            // 
            this.treeListViewBson.AllColumns.Add(this.olvColumnProperty);
            this.treeListViewBson.AllColumns.Add(this.olvColumnValue);
            this.treeListViewBson.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnProperty,
            this.olvColumnValue});
            this.treeListViewBson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListViewBson.Location = new System.Drawing.Point(0, 0);
            this.treeListViewBson.Name = "treeListViewBson";
            this.treeListViewBson.OwnerDraw = true;
            this.treeListViewBson.ShowGroups = false;
            this.treeListViewBson.Size = new System.Drawing.Size(560, 488);
            this.treeListViewBson.TabIndex = 0;
            this.treeListViewBson.UseCompatibleStateImageBehavior = false;
            this.treeListViewBson.View = System.Windows.Forms.View.Details;
            this.treeListViewBson.VirtualMode = true;
            // 
            // olvColumnProperty
            // 
            this.olvColumnProperty.CellPadding = null;
            this.olvColumnProperty.Text = "Property";
            this.olvColumnProperty.Width = 225;
            // 
            // olvColumnValue
            // 
            this.olvColumnValue.CellPadding = null;
            this.olvColumnValue.Text = "Value";
            this.olvColumnValue.Width = 240;
            // 
            // UserControlBsonTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeListViewBson);
            this.Name = "UserControlBsonTreeView";
            this.Size = new System.Drawing.Size(560, 488);
            this.Load += new System.EventHandler(this.UserControlBsonTreeView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeListViewBson)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.TreeListView treeListViewBson;
        private BrightIdeasSoftware.OLVColumn olvColumnProperty;
        private BrightIdeasSoftware.OLVColumn olvColumnValue;
    }
}
