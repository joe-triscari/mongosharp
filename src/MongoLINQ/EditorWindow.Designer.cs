namespace MongoSharp
{
    partial class EditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorWindow));
            this.toolStripConnection = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelConnection = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxConnection = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabelDB = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxDatabase = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabelCollection = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxCollection = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonNavigateToColl = new System.Windows.Forms.ToolStripButton();
            this.contextMenuTabPage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExecute = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxModes = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonFormatCode = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonViewCode = new System.Windows.Forms.ToolStripButton();
            this.tabControlCollection = new MongoSharp.TabControlExtended();
            this.tabPageLinq = new System.Windows.Forms.TabPage();
            this.scintillaLinqCode = new MongoSharp.StandardScintilla();
            this.imageListAutoComplete = new System.Windows.Forms.ImageList(this.components);
            this.toolStripConnection.SuspendLayout();
            this.contextMenuTabPage.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabControlCollection.SuspendLayout();
            this.tabPageLinq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scintillaLinqCode)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripConnection
            // 
            this.toolStripConnection.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripConnection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelConnection,
            this.toolStripComboBoxConnection,
            this.toolStripLabelDB,
            this.toolStripComboBoxDatabase,
            this.toolStripLabelCollection,
            this.toolStripComboBoxCollection,
            this.toolStripButtonNavigateToColl});
            this.toolStripConnection.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStripConnection.Location = new System.Drawing.Point(0, 0);
            this.toolStripConnection.Name = "toolStripConnection";
            this.toolStripConnection.Padding = new System.Windows.Forms.Padding(0, 3, 1, 3);
            this.toolStripConnection.Size = new System.Drawing.Size(944, 29);
            this.toolStripConnection.TabIndex = 1;
            this.toolStripConnection.Text = "toolStrip1";
            // 
            // toolStripLabelConnection
            // 
            this.toolStripLabelConnection.Name = "toolStripLabelConnection";
            this.toolStripLabelConnection.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.toolStripLabelConnection.Size = new System.Drawing.Size(69, 20);
            this.toolStripLabelConnection.Text = "Connection";
            // 
            // toolStripComboBoxConnection
            // 
            this.toolStripComboBoxConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxConnection.DropDownWidth = 300;
            this.toolStripComboBoxConnection.Name = "toolStripComboBoxConnection";
            this.toolStripComboBoxConnection.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxConnection.DropDown += new System.EventHandler(this.toolStripComboBoxConnection_DropDown);
            this.toolStripComboBoxConnection.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxConnection_SelectedIndexChanged);
            // 
            // toolStripLabelDB
            // 
            this.toolStripLabelDB.Name = "toolStripLabelDB";
            this.toolStripLabelDB.Padding = new System.Windows.Forms.Padding(25, 5, 0, 0);
            this.toolStripLabelDB.Size = new System.Drawing.Size(80, 20);
            this.toolStripLabelDB.Text = "Database";
            // 
            // toolStripComboBoxDatabase
            // 
            this.toolStripComboBoxDatabase.DropDownWidth = 300;
            this.toolStripComboBoxDatabase.Name = "toolStripComboBoxDatabase";
            this.toolStripComboBoxDatabase.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxDatabase.DropDown += new System.EventHandler(this.toolStripComboBoxConnection_DropDown);
            this.toolStripComboBoxDatabase.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxDatabase_SelectedIndexChanged);
            // 
            // toolStripLabelCollection
            // 
            this.toolStripLabelCollection.Name = "toolStripLabelCollection";
            this.toolStripLabelCollection.Padding = new System.Windows.Forms.Padding(25, 5, 0, 0);
            this.toolStripLabelCollection.Size = new System.Drawing.Size(86, 20);
            this.toolStripLabelCollection.Text = "Collection";
            // 
            // toolStripComboBoxCollection
            // 
            this.toolStripComboBoxCollection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxCollection.DropDownWidth = 300;
            this.toolStripComboBoxCollection.Name = "toolStripComboBoxCollection";
            this.toolStripComboBoxCollection.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxCollection.DropDown += new System.EventHandler(this.toolStripComboBoxConnection_DropDown);
            this.toolStripComboBoxCollection.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxCollection_SelectedIndexChanged);
            // 
            // toolStripButtonNavigateToColl
            // 
            this.toolStripButtonNavigateToColl.Enabled = false;
            this.toolStripButtonNavigateToColl.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNavigateToColl.Image")));
            this.toolStripButtonNavigateToColl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNavigateToColl.Name = "toolStripButtonNavigateToColl";
            this.toolStripButtonNavigateToColl.Size = new System.Drawing.Size(23, 20);
            this.toolStripButtonNavigateToColl.ToolTipText = "Click to view properties";
            this.toolStripButtonNavigateToColl.Click += new System.EventHandler(this.toolStripButtonNavigateToColl_Click);
            // 
            // contextMenuTabPage
            // 
            this.contextMenuTabPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.closeAllButThisToolStripMenuItem,
            this.closeAllToolStripMenuItem});
            this.contextMenuTabPage.Name = "contextMenuTabPage";
            this.contextMenuTabPage.Size = new System.Drawing.Size(162, 70);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // closeAllButThisToolStripMenuItem
            // 
            this.closeAllButThisToolStripMenuItem.Name = "closeAllButThisToolStripMenuItem";
            this.closeAllButThisToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.closeAllButThisToolStripMenuItem.Text = "Close all but this";
            this.closeAllButThisToolStripMenuItem.Click += new System.EventHandler(this.closeAllButThisToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.closeAllToolStripMenuItem.Text = "Close All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExecute,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this.toolStripComboBoxModes,
            this.toolStripSeparator1,
            this.toolStripButtonFormatCode,
            this.toolStripButtonViewCode});
            this.toolStrip2.Location = new System.Drawing.Point(0, 29);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(944, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButtonExecute
            // 
            this.toolStripButtonExecute.Image = global::MongoSharp.Properties.Resources.Execute;
            this.toolStripButtonExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExecute.Name = "toolStripButtonExecute";
            this.toolStripButtonExecute.Size = new System.Drawing.Size(67, 22);
            this.toolStripButtonExecute.Text = "Execute";
            this.toolStripButtonExecute.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(38, 22);
            this.toolStripLabel2.Text = "Mode";
            // 
            // toolStripComboBoxModes
            // 
            this.toolStripComboBoxModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxModes.Items.AddRange(new object[] {
            "C# Query",
            "C# Statements",
            "JSON"});
            this.toolStripComboBoxModes.Name = "toolStripComboBoxModes";
            this.toolStripComboBoxModes.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonFormatCode
            // 
            this.toolStripButtonFormatCode.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFormatCode.Image")));
            this.toolStripButtonFormatCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFormatCode.Name = "toolStripButtonFormatCode";
            this.toolStripButtonFormatCode.Size = new System.Drawing.Size(96, 22);
            this.toolStripButtonFormatCode.Text = "Format Code";
            this.toolStripButtonFormatCode.Click += new System.EventHandler(this.toolStripButtonFormatCode_Click);
            // 
            // toolStripButtonViewCode
            // 
            this.toolStripButtonViewCode.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonViewCode.Image")));
            this.toolStripButtonViewCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonViewCode.Name = "toolStripButtonViewCode";
            this.toolStripButtonViewCode.Size = new System.Drawing.Size(123, 22);
            this.toolStripButtonViewCode.Text = "View Code Behind";
            this.toolStripButtonViewCode.Click += new System.EventHandler(this.toolStripButtonViewCode_Click);
            // 
            // tabControlCollection
            // 
            this.tabControlCollection.Controls.Add(this.tabPageLinq);
            this.tabControlCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlCollection.Location = new System.Drawing.Point(0, 54);
            this.tabControlCollection.Name = "tabControlCollection";
            this.tabControlCollection.SelectedIndex = 0;
            this.tabControlCollection.Size = new System.Drawing.Size(944, 497);
            this.tabControlCollection.TabIndex = 4;
            // 
            // tabPageLinq
            // 
            this.tabPageLinq.Controls.Add(this.scintillaLinqCode);
            this.tabPageLinq.Location = new System.Drawing.Point(4, 22);
            this.tabPageLinq.Name = "tabPageLinq";
            this.tabPageLinq.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLinq.Size = new System.Drawing.Size(936, 471);
            this.tabPageLinq.TabIndex = 0;
            this.tabPageLinq.Text = "LINQ";
            this.tabPageLinq.UseVisualStyleBackColor = true;
            // 
            // scintillaLinqCode
            // 
            this.scintillaLinqCode.Caret.Style = ScintillaNET.CaretStyle.Invisible;
            this.scintillaLinqCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaLinqCode.Location = new System.Drawing.Point(3, 3);
            this.scintillaLinqCode.Name = "scintillaLinqCode";
            this.scintillaLinqCode.Size = new System.Drawing.Size(930, 465);
            this.scintillaLinqCode.TabIndex = 0;
            this.scintillaLinqCode.DocumentChange += new System.EventHandler<ScintillaNET.NativeScintillaEventArgs>(this.scintillaLinqCode_DocumentChange);
            this.scintillaLinqCode.Load += new System.EventHandler(this.scintillaLinqCode_Load);
            // 
            // imageListAutoComplete
            // 
            this.imageListAutoComplete.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListAutoComplete.ImageStream")));
            this.imageListAutoComplete.TransparentColor = System.Drawing.Color.Empty;
            this.imageListAutoComplete.Images.SetKeyName(0, "Method_pub_ext.png");
            this.imageListAutoComplete.Images.SetKeyName(1, "Method_pub.png");
            this.imageListAutoComplete.Images.SetKeyName(2, "Property_pub.png");
            // 
            // EditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 551);
            this.Controls.Add(this.tabControlCollection);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStripConnection);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditorWindow";
            this.TabPageContextMenuStrip = this.contextMenuTabPage;
            this.Text = "EditorWindow";
            this.Activated += new System.EventHandler(this.EditorWindow_Activated);
            this.Deactivate += new System.EventHandler(this.EditorWindow_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorWindow_FormClosing);
            this.Load += new System.EventHandler(this.EditorWindow_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditorWindow_KeyDown);
            this.toolStripConnection.ResumeLayout(false);
            this.toolStripConnection.PerformLayout();
            this.contextMenuTabPage.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabControlCollection.ResumeLayout(false);
            this.tabPageLinq.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scintillaLinqCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripConnection;
        private System.Windows.Forms.ToolStripLabel toolStripLabelConnection;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxConnection;
        private System.Windows.Forms.ToolStripLabel toolStripLabelCollection;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxCollection;
        private System.Windows.Forms.ContextMenuStrip contextMenuTabPage;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabelDB;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxDatabase;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButtonExecute;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxModes;
        private TabControlExtended tabControlCollection;
        private System.Windows.Forms.TabPage tabPageLinq;
        private StandardScintilla scintillaLinqCode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonViewCode;
        private System.Windows.Forms.ToolStripButton toolStripButtonNavigateToColl;
        private System.Windows.Forms.ToolStripButton toolStripButtonFormatCode;
        private System.Windows.Forms.ImageList imageListAutoComplete;
    }
}