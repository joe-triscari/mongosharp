namespace MongoSharp
{
    partial class UserControlQueryResultsGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlQueryResultsGrid));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCreateTable = new System.Windows.Forms.ToolStripButton();
            this.dataGridViewResults = new MongoSharp.MultiDataGridView();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExportToExcel,
            this.toolStripButtonCreateTable});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(702, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonExportToExcel
            // 
            this.toolStripButtonExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExportToExcel.Image")));
            this.toolStripButtonExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportToExcel.Name = "toolStripButtonExportToExcel";
            this.toolStripButtonExportToExcel.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonExportToExcel.Text = "Export to Excel";
            this.toolStripButtonExportToExcel.ToolTipText = "Export to Excel";
            this.toolStripButtonExportToExcel.Click += new System.EventHandler(this.toolStripButtonExportToExcel_Click);
            // 
            // toolStripButtonCreateTable
            // 
            this.toolStripButtonCreateTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCreateTable.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCreateTable.Image")));
            this.toolStripButtonCreateTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreateTable.Name = "toolStripButtonCreateTable";
            this.toolStripButtonCreateTable.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCreateTable.Text = "Create table from results";
            this.toolStripButtonCreateTable.Click += new System.EventHandler(this.toolStripButtonCreateTable_Click);
            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewResults.Location = new System.Drawing.Point(0, 25);
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.Size = new System.Drawing.Size(702, 647);
            this.dataGridViewResults.TabIndex = 1;
            // 
            // UserControlQueryResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewResults);
            this.Controls.Add(this.toolStrip1);
            this.Name = "UserControlQueryResults";
            this.Size = new System.Drawing.Size(702, 672);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportToExcel;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreateTable;
        private MongoSharp.MultiDataGridView dataGridViewResults;

    }
}
