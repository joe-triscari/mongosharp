namespace MongoSharp
{
    partial class UserControlQueryResultsTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlQueryResultsTree));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExpandAll = new System.Windows.Forms.ToolStripSplitButton();
            this.expandSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonCollapseAll = new System.Windows.Forms.ToolStripSplitButton();
            this.collapseSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelFilter = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabelEditModeEnabled = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonEditDocument = new System.Windows.Forms.ToolStripButton();
            this.treeListView1 = new BrightIdeasSoftware.TreeListView();
            this.olvColumnKey = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cmsRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemEditDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEditValue = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.cmsRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExpandAll,
            this.toolStripButtonCollapseAll,
            this.toolStripSeparator1,
            this.toolStripLabelFilter,
            this.toolStripTextBox1,
            this.toolStripLabelEditModeEnabled,
            this.toolStripButtonEditDocument});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(597, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonExpandAll
            // 
            this.toolStripButtonExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonExpandAll.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expandSelectedToolStripMenuItem});
            this.toolStripButtonExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExpandAll.Image")));
            this.toolStripButtonExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExpandAll.Name = "toolStripButtonExpandAll";
            this.toolStripButtonExpandAll.Size = new System.Drawing.Size(78, 22);
            this.toolStripButtonExpandAll.Text = "Expand All";
            this.toolStripButtonExpandAll.ButtonClick += new System.EventHandler(this.toolStripButtonExpandAll_Click);
            // 
            // expandSelectedToolStripMenuItem
            // 
            this.expandSelectedToolStripMenuItem.Name = "expandSelectedToolStripMenuItem";
            this.expandSelectedToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.expandSelectedToolStripMenuItem.Text = "Expand Selected";
            this.expandSelectedToolStripMenuItem.Click += new System.EventHandler(this.expandSelectedToolStripMenuItem_Click);
            // 
            // toolStripButtonCollapseAll
            // 
            this.toolStripButtonCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCollapseAll.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collapseSelectedToolStripMenuItem});
            this.toolStripButtonCollapseAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCollapseAll.Image")));
            this.toolStripButtonCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCollapseAll.Name = "toolStripButtonCollapseAll";
            this.toolStripButtonCollapseAll.Size = new System.Drawing.Size(85, 22);
            this.toolStripButtonCollapseAll.Text = "Collapse All";
            this.toolStripButtonCollapseAll.ButtonClick += new System.EventHandler(this.toolStripButtonCollapseAll_Click);
            // 
            // collapseSelectedToolStripMenuItem
            // 
            this.collapseSelectedToolStripMenuItem.Name = "collapseSelectedToolStripMenuItem";
            this.collapseSelectedToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.collapseSelectedToolStripMenuItem.Text = "Collapse Selected";
            this.collapseSelectedToolStripMenuItem.Click += new System.EventHandler(this.collapseSelectedToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabelFilter
            // 
            this.toolStripLabelFilter.Name = "toolStripLabelFilter";
            this.toolStripLabelFilter.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabelFilter.Text = "Filter";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBox1.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // toolStripLabelEditModeEnabled
            // 
            this.toolStripLabelEditModeEnabled.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabelEditModeEnabled.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabelEditModeEnabled.ForeColor = System.Drawing.Color.Red;
            this.toolStripLabelEditModeEnabled.Name = "toolStripLabelEditModeEnabled";
            this.toolStripLabelEditModeEnabled.Size = new System.Drawing.Size(110, 22);
            this.toolStripLabelEditModeEnabled.Text = "Edit mode enabled";
            this.toolStripLabelEditModeEnabled.ToolTipText = "Double-click cell to edit. Changes are saved immediately.";
            this.toolStripLabelEditModeEnabled.Visible = false;
            // 
            // toolStripButtonEditDocument
            // 
            this.toolStripButtonEditDocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEditDocument.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditDocument.Image")));
            this.toolStripButtonEditDocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditDocument.Name = "toolStripButtonEditDocument";
            this.toolStripButtonEditDocument.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonEditDocument.Text = "Edit Document";
            this.toolStripButtonEditDocument.Click += new System.EventHandler(this.toolStripButtonEditDocument_Click);
            // 
            // treeListView1
            // 
            this.treeListView1.AllColumns.Add(this.olvColumnKey);
            this.treeListView1.AllColumns.Add(this.olvColumnValue);
            this.treeListView1.AllColumns.Add(this.olvColumnType);
            this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnKey,
            this.olvColumnValue,
            this.olvColumnType});
            this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView1.Location = new System.Drawing.Point(0, 25);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.OwnerDraw = true;
            this.treeListView1.ShowGroups = false;
            this.treeListView1.Size = new System.Drawing.Size(597, 412);
            this.treeListView1.TabIndex = 1;
            this.treeListView1.UseCompatibleStateImageBehavior = false;
            this.treeListView1.View = System.Windows.Forms.View.Details;
            this.treeListView1.VirtualMode = true;
            // 
            // olvColumnKey
            // 
            this.olvColumnKey.Text = "Key";
            this.olvColumnKey.Width = 350;
            // 
            // olvColumnValue
            // 
            this.olvColumnValue.Text = "Value";
            this.olvColumnValue.Width = 350;
            // 
            // olvColumnType
            // 
            this.olvColumnType.Text = "Type";
            this.olvColumnType.Width = 200;
            // 
            // cmsRightClick
            // 
            this.cmsRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemEditDoc,
            this.toolStripMenuItemEditValue});
            this.cmsRightClick.Name = "cmsRightClick";
            this.cmsRightClick.Size = new System.Drawing.Size(164, 70);
            // 
            // toolStripMenuItemEditDoc
            // 
            this.toolStripMenuItemEditDoc.Name = "toolStripMenuItemEditDoc";
            this.toolStripMenuItemEditDoc.Size = new System.Drawing.Size(163, 22);
            this.toolStripMenuItemEditDoc.Text = "Edit Document";
            this.toolStripMenuItemEditDoc.Click += new System.EventHandler(this.toolStripMenuItemEditDoc_Click);
            // 
            // toolStripMenuItemEditValue
            // 
            this.toolStripMenuItemEditValue.Name = "toolStripMenuItemEditValue";
            this.toolStripMenuItemEditValue.Size = new System.Drawing.Size(163, 22);
            this.toolStripMenuItemEditValue.Text = "Edit Value / Type";
            this.toolStripMenuItemEditValue.Click += new System.EventHandler(this.toolStripMenuItemEditValue_Click);
            // 
            // UserControlQueryResultsTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeListView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "UserControlQueryResultsTree";
            this.Size = new System.Drawing.Size(597, 437);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.cmsRightClick.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private BrightIdeasSoftware.TreeListView treeListView1;
        private BrightIdeasSoftware.OLVColumn olvColumnKey;
        private BrightIdeasSoftware.OLVColumn olvColumnValue;
        private BrightIdeasSoftware.OLVColumn olvColumnType;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelFilter;
        private System.Windows.Forms.ToolStripSplitButton toolStripButtonExpandAll;
        private System.Windows.Forms.ToolStripMenuItem expandSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton toolStripButtonCollapseAll;
        private System.Windows.Forms.ToolStripMenuItem collapseSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabelEditModeEnabled;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditDocument;
        private System.Windows.Forms.ContextMenuStrip cmsRightClick;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEditDoc;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEditValue;
    }
}
