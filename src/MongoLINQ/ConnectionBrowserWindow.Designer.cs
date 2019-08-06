namespace MongoSharp
{
    partial class ConnectionBrowserWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionBrowserWindow));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddConnection = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.tvConnections = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cmsCollectionNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editModelToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.insertImportDocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsConnection = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsModel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDatabaseNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsIndex = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dropIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.cmsCollectionNode.SuspendLayout();
            this.cmsConnection.SuspendLayout();
            this.cmsModel.SuspendLayout();
            this.cmsDatabaseNode.SuspendLayout();
            this.cmsIndex.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddConnection,
            this.toolStripButtonRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(242, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonAddConnection
            // 
            this.toolStripButtonAddConnection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddConnection.Image")));
            this.toolStripButtonAddConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddConnection.Name = "toolStripButtonAddConnection";
            this.toolStripButtonAddConnection.Size = new System.Drawing.Size(114, 22);
            this.toolStripButtonAddConnection.Text = "Add Connection";
            this.toolStripButtonAddConnection.Click += new System.EventHandler(this.toolStripButtonAddConnection_Click);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.Enabled = false;
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(66, 22);
            this.toolStripButtonRefresh.Text = "Refresh";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // tvConnections
            // 
            this.tvConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvConnections.ImageIndex = 0;
            this.tvConnections.ImageList = this.imageList1;
            this.tvConnections.Location = new System.Drawing.Point(0, 25);
            this.tvConnections.Name = "tvConnections";
            this.tvConnections.SelectedImageIndex = 0;
            this.tvConnections.ShowNodeToolTips = true;
            this.tvConnections.Size = new System.Drawing.Size(242, 494);
            this.tvConnections.TabIndex = 1;
            this.tvConnections.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvConnections_BeforeExpand);
            this.tvConnections.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvConnections_AfterSelect);
            this.tvConnections.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvConnections_NodeMouseClick);
            this.tvConnections.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvConnections_NodeMouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Database.ico");
            this.imageList1.Images.SetKeyName(1, "Database (1).ico");
            this.imageList1.Images.SetKeyName(2, "collection.ico");
            this.imageList1.Images.SetKeyName(3, "CSFile.ico");
            this.imageList1.Images.SetKeyName(4, "Icon_294.ico");
            this.imageList1.Images.SetKeyName(5, "key-icon.ico");
            this.imageList1.Images.SetKeyName(6, "collection_no_model.ico");
            // 
            // cmsCollectionNode
            // 
            this.cmsCollectionNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newEditorToolStripMenuItem,
            this.editModelToolStripMenuItem2,
            this.insertImportDocumentsToolStripMenuItem,
            this.dropToolStripMenuItem,
            this.createIndexToolStripMenuItem,
            this.compactToolStripMenuItem,
            this.statisticsToolStripMenuItem1});
            this.cmsCollectionNode.Name = "cmsCollectionNode";
            this.cmsCollectionNode.Size = new System.Drawing.Size(218, 158);
            this.cmsCollectionNode.Opening += new System.ComponentModel.CancelEventHandler(this.cmsCollectionNode_Opening);
            // 
            // newEditorToolStripMenuItem
            // 
            this.newEditorToolStripMenuItem.Name = "newEditorToolStripMenuItem";
            this.newEditorToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.newEditorToolStripMenuItem.Text = "New Editor...";
            this.newEditorToolStripMenuItem.Click += new System.EventHandler(this.newEditorToolStripMenuItem_Click);
            // 
            // editModelToolStripMenuItem2
            // 
            this.editModelToolStripMenuItem2.Name = "editModelToolStripMenuItem2";
            this.editModelToolStripMenuItem2.Size = new System.Drawing.Size(217, 22);
            this.editModelToolStripMenuItem2.Text = "Edit Model...";
            this.editModelToolStripMenuItem2.Click += new System.EventHandler(this.editModelToolStripMenuItem2_Click);
            // 
            // insertImportDocumentsToolStripMenuItem
            // 
            this.insertImportDocumentsToolStripMenuItem.Name = "insertImportDocumentsToolStripMenuItem";
            this.insertImportDocumentsToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.insertImportDocumentsToolStripMenuItem.Text = "Insert/Import Documents...";
            this.insertImportDocumentsToolStripMenuItem.Click += new System.EventHandler(this.insertImportDocumentsToolStripMenuItem_Click);
            // 
            // dropToolStripMenuItem
            // 
            this.dropToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dropToolStripMenuItem.Image")));
            this.dropToolStripMenuItem.Name = "dropToolStripMenuItem";
            this.dropToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.dropToolStripMenuItem.Text = "Drop";
            this.dropToolStripMenuItem.ToolTipText = "Drop Collection";
            this.dropToolStripMenuItem.Click += new System.EventHandler(this.dropToolStripMenuItem_Click);
            // 
            // createIndexToolStripMenuItem
            // 
            this.createIndexToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createIndexToolStripMenuItem.Image")));
            this.createIndexToolStripMenuItem.Name = "createIndexToolStripMenuItem";
            this.createIndexToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.createIndexToolStripMenuItem.Text = "Create Index...";
            this.createIndexToolStripMenuItem.Click += new System.EventHandler(this.createIndexToolStripMenuItem_Click);
            // 
            // compactToolStripMenuItem
            // 
            this.compactToolStripMenuItem.Name = "compactToolStripMenuItem";
            this.compactToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.compactToolStripMenuItem.Text = "Compact";
            this.compactToolStripMenuItem.Click += new System.EventHandler(this.compactToolStripMenuItem_Click);
            // 
            // statisticsToolStripMenuItem1
            // 
            this.statisticsToolStripMenuItem1.Name = "statisticsToolStripMenuItem1";
            this.statisticsToolStripMenuItem1.Size = new System.Drawing.Size(217, 22);
            this.statisticsToolStripMenuItem1.Text = "Statistics...";
            this.statisticsToolStripMenuItem1.Click += new System.EventHandler(this.databaseStatistics_Click);
            // 
            // cmsConnection
            // 
            this.cmsConnection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDatabaseToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.cmsConnection.Name = "cmsConnection";
            this.cmsConnection.Size = new System.Drawing.Size(157, 70);
            // 
            // addDatabaseToolStripMenuItem
            // 
            this.addDatabaseToolStripMenuItem.Name = "addDatabaseToolStripMenuItem";
            this.addDatabaseToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.addDatabaseToolStripMenuItem.Text = "Add Database...";
            this.addDatabaseToolStripMenuItem.Click += new System.EventHandler(this.addDatabaseToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.propertiesToolStripMenuItem.Text = "Properties...";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // cmsModel
            // 
            this.cmsModel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editModelToolStripMenuItem});
            this.cmsModel.Name = "cmsModel";
            this.cmsModel.Size = new System.Drawing.Size(141, 26);
            // 
            // editModelToolStripMenuItem
            // 
            this.editModelToolStripMenuItem.Name = "editModelToolStripMenuItem";
            this.editModelToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.editModelToolStripMenuItem.Text = "Edit Model...";
            this.editModelToolStripMenuItem.Click += new System.EventHandler(this.editModelToolStripMenuItem_Click);
            // 
            // cmsDatabaseNode
            // 
            this.cmsDatabaseNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createCollectionToolStripMenuItem,
            this.statisticsToolStripMenuItem});
            this.cmsDatabaseNode.Name = "cmsDatabaseNode";
            this.cmsDatabaseNode.Size = new System.Drawing.Size(175, 48);
            // 
            // createCollectionToolStripMenuItem
            // 
            this.createCollectionToolStripMenuItem.Name = "createCollectionToolStripMenuItem";
            this.createCollectionToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.createCollectionToolStripMenuItem.Text = "Create Collection...";
            this.createCollectionToolStripMenuItem.Click += new System.EventHandler(this.createCollectionToolStripMenuItem_Click);
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.statisticsToolStripMenuItem.Text = "Statistics...";
            this.statisticsToolStripMenuItem.Click += new System.EventHandler(this.databaseStatistics_Click);
            // 
            // cmsIndex
            // 
            this.cmsIndex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dropIndexToolStripMenuItem,
            this.indexPropertiesToolStripMenuItem});
            this.cmsIndex.Name = "cmsIndex";
            this.cmsIndex.Size = new System.Drawing.Size(137, 48);
            // 
            // dropIndexToolStripMenuItem
            // 
            this.dropIndexToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dropIndexToolStripMenuItem.Image")));
            this.dropIndexToolStripMenuItem.Name = "dropIndexToolStripMenuItem";
            this.dropIndexToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.dropIndexToolStripMenuItem.Text = "Drop";
            this.dropIndexToolStripMenuItem.Click += new System.EventHandler(this.dropIndexToolStripMenuItem_Click);
            // 
            // indexPropertiesToolStripMenuItem
            // 
            this.indexPropertiesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("indexPropertiesToolStripMenuItem.Image")));
            this.indexPropertiesToolStripMenuItem.Name = "indexPropertiesToolStripMenuItem";
            this.indexPropertiesToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.indexPropertiesToolStripMenuItem.Text = "Properties...";
            this.indexPropertiesToolStripMenuItem.Click += new System.EventHandler(this.indexPropertiesToolStripMenuItem_Click);
            // 
            // ConnectionBrowserWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 519);
            this.Controls.Add(this.tvConnections);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectionBrowserWindow";
            this.Text = "Connection Browser";
            this.Load += new System.EventHandler(this.ConnectionBrowserWindow_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.cmsCollectionNode.ResumeLayout(false);
            this.cmsConnection.ResumeLayout(false);
            this.cmsModel.ResumeLayout(false);
            this.cmsDatabaseNode.ResumeLayout(false);
            this.cmsIndex.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TreeView tvConnections;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip cmsCollectionNode;
        private System.Windows.Forms.ToolStripMenuItem newEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddConnection;
        private System.Windows.Forms.ContextMenuStrip cmsConnection;
        private System.Windows.Forms.ToolStripMenuItem addDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsModel;
        private System.Windows.Forms.ToolStripMenuItem editModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripMenuItem dropToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertImportDocumentsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsDatabaseNode;
        private System.Windows.Forms.ToolStripMenuItem createCollectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createIndexToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsIndex;
        private System.Windows.Forms.ToolStripMenuItem dropIndexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editModelToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem compactToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem1;

    }
}