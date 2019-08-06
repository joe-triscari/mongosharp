namespace MongoSharp
{
    partial class FormAddIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddIndex));
            this.lblAddIndex = new System.Windows.Forms.Label();
            this.txtBoxHeader = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSelector = new System.Windows.Forms.TabPage();
            this.buttonRemoveIndex = new System.Windows.Forms.Button();
            this.btnAddIndex = new System.Windows.Forms.Button();
            this.dataGridViewIndex = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Sort = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tabPageJson = new System.Windows.Forms.TabPage();
            this.btnIndexJsonClear = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxIndexOptionsJson = new System.Windows.Forms.TextBox();
            this.comboBoxIndexTypes = new System.Windows.Forms.ComboBox();
            this.textBoxIndexJson = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtBoxIndexName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxUnique = new System.Windows.Forms.CheckBox();
            this.checkBoxBackground = new System.Windows.Forms.CheckBox();
            this.checkBoxDropDups = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnIndexOptionsJsonClear = new System.Windows.Forms.Button();
            this.linkLabelIndexTutorial = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabelHelp = new System.Windows.Forms.LinkLabel();
            this.checkBoxSparse = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPageSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIndex)).BeginInit();
            this.tabPageJson.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAddIndex
            // 
            this.lblAddIndex.AutoSize = true;
            this.lblAddIndex.BackColor = System.Drawing.SystemColors.Window;
            this.lblAddIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddIndex.Location = new System.Drawing.Point(52, 9);
            this.lblAddIndex.Name = "lblAddIndex";
            this.lblAddIndex.Size = new System.Drawing.Size(90, 20);
            this.lblAddIndex.TabIndex = 10;
            this.lblAddIndex.Text = "Add Index";
            // 
            // txtBoxHeader
            // 
            this.txtBoxHeader.BackColor = System.Drawing.SystemColors.Window;
            this.txtBoxHeader.Enabled = false;
            this.txtBoxHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxHeader.Location = new System.Drawing.Point(0, 0);
            this.txtBoxHeader.Multiline = true;
            this.txtBoxHeader.Name = "txtBoxHeader";
            this.txtBoxHeader.ReadOnly = true;
            this.txtBoxHeader.Size = new System.Drawing.Size(542, 42);
            this.txtBoxHeader.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageSelector);
            this.tabControl1.Controls.Add(this.tabPageJson);
            this.tabControl1.Location = new System.Drawing.Point(3, 79);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(539, 203);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPageSelector
            // 
            this.tabPageSelector.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSelector.Controls.Add(this.buttonRemoveIndex);
            this.tabPageSelector.Controls.Add(this.btnAddIndex);
            this.tabPageSelector.Controls.Add(this.dataGridViewIndex);
            this.tabPageSelector.Location = new System.Drawing.Point(4, 22);
            this.tabPageSelector.Name = "tabPageSelector";
            this.tabPageSelector.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSelector.Size = new System.Drawing.Size(531, 177);
            this.tabPageSelector.TabIndex = 1;
            this.tabPageSelector.Text = "Basic";
            // 
            // buttonRemoveIndex
            // 
            this.buttonRemoveIndex.Location = new System.Drawing.Point(49, 150);
            this.buttonRemoveIndex.Name = "buttonRemoveIndex";
            this.buttonRemoveIndex.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveIndex.TabIndex = 17;
            this.buttonRemoveIndex.Text = "Remove";
            this.buttonRemoveIndex.UseVisualStyleBackColor = true;
            this.buttonRemoveIndex.Click += new System.EventHandler(this.buttonRemoveIndex_Click);
            // 
            // btnAddIndex
            // 
            this.btnAddIndex.Location = new System.Drawing.Point(3, 149);
            this.btnAddIndex.Name = "btnAddIndex";
            this.btnAddIndex.Size = new System.Drawing.Size(43, 23);
            this.btnAddIndex.TabIndex = 16;
            this.btnAddIndex.Text = "Add";
            this.btnAddIndex.UseVisualStyleBackColor = true;
            this.btnAddIndex.Click += new System.EventHandler(this.btnAddIndex_Click);
            // 
            // dataGridViewIndex
            // 
            this.dataGridViewIndex.AllowUserToAddRows = false;
            this.dataGridViewIndex.AllowUserToDeleteRows = false;
            this.dataGridViewIndex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewIndex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIndex.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.Sort});
            this.dataGridViewIndex.Location = new System.Drawing.Point(0, 3);
            this.dataGridViewIndex.Name = "dataGridViewIndex";
            this.dataGridViewIndex.Size = new System.Drawing.Size(528, 140);
            this.dataGridViewIndex.TabIndex = 0;
            this.dataGridViewIndex.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewIndex_CellValidating);
            this.dataGridViewIndex.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewIndex_EditingControlShowing);
            this.dataGridViewIndex.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewIndex_RowsAdded);
            // 
            // Index
            // 
            this.Index.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Index.HeaderText = "Index";
            this.Index.Name = "Index";
            this.Index.Width = 300;
            // 
            // Sort
            // 
            this.Sort.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Sort.HeaderText = "Sort";
            this.Sort.Name = "Sort";
            this.Sort.Width = 150;
            // 
            // tabPageJson
            // 
            this.tabPageJson.Controls.Add(this.linkLabelHelp);
            this.tabPageJson.Controls.Add(this.label4);
            this.tabPageJson.Controls.Add(this.linkLabelIndexTutorial);
            this.tabPageJson.Controls.Add(this.btnIndexOptionsJsonClear);
            this.tabPageJson.Controls.Add(this.btnIndexJsonClear);
            this.tabPageJson.Controls.Add(this.label3);
            this.tabPageJson.Controls.Add(this.label2);
            this.tabPageJson.Controls.Add(this.textBoxIndexOptionsJson);
            this.tabPageJson.Controls.Add(this.comboBoxIndexTypes);
            this.tabPageJson.Controls.Add(this.textBoxIndexJson);
            this.tabPageJson.Location = new System.Drawing.Point(4, 22);
            this.tabPageJson.Name = "tabPageJson";
            this.tabPageJson.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJson.Size = new System.Drawing.Size(531, 177);
            this.tabPageJson.TabIndex = 2;
            this.tabPageJson.Text = "Custom";
            this.tabPageJson.UseVisualStyleBackColor = true;
            // 
            // btnIndexJsonClear
            // 
            this.btnIndexJsonClear.Image = global::MongoSharp.Properties.Resources.Erase;
            this.btnIndexJsonClear.Location = new System.Drawing.Point(260, 48);
            this.btnIndexJsonClear.Name = "btnIndexJsonClear";
            this.btnIndexJsonClear.Size = new System.Drawing.Size(27, 23);
            this.btnIndexJsonClear.TabIndex = 5;
            this.btnIndexJsonClear.UseVisualStyleBackColor = true;
            this.btnIndexJsonClear.Click += new System.EventHandler(this.btnIndexJsonClear_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Index Json";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(288, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Options Json";
            // 
            // textBoxIndexOptionsJson
            // 
            this.textBoxIndexOptionsJson.Location = new System.Drawing.Point(291, 48);
            this.textBoxIndexOptionsJson.Multiline = true;
            this.textBoxIndexOptionsJson.Name = "textBoxIndexOptionsJson";
            this.textBoxIndexOptionsJson.Size = new System.Drawing.Size(204, 123);
            this.textBoxIndexOptionsJson.TabIndex = 2;
            // 
            // comboBoxIndexTypes
            // 
            this.comboBoxIndexTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIndexTypes.FormattingEnabled = true;
            this.comboBoxIndexTypes.Items.AddRange(new object[] {
            "ascending",
            "descending",
            "2d",
            "2dsphere"});
            this.comboBoxIndexTypes.Location = new System.Drawing.Point(63, 5);
            this.comboBoxIndexTypes.Name = "comboBoxIndexTypes";
            this.comboBoxIndexTypes.Size = new System.Drawing.Size(193, 21);
            this.comboBoxIndexTypes.TabIndex = 1;
            this.comboBoxIndexTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxIndexTypes_SelectedIndexChanged);
            // 
            // textBoxIndexJson
            // 
            this.textBoxIndexJson.Location = new System.Drawing.Point(4, 48);
            this.textBoxIndexJson.Multiline = true;
            this.textBoxIndexJson.Name = "textBoxIndexJson";
            this.textBoxIndexJson.Size = new System.Drawing.Size(254, 123);
            this.textBoxIndexJson.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(441, 303);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(350, 303);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click_1);
            // 
            // txtBoxIndexName
            // 
            this.txtBoxIndexName.Location = new System.Drawing.Point(56, 48);
            this.txtBoxIndexName.Name = "txtBoxIndexName";
            this.txtBoxIndexName.Size = new System.Drawing.Size(168, 20);
            this.txtBoxIndexName.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Name";
            // 
            // checkBoxUnique
            // 
            this.checkBoxUnique.AutoSize = true;
            this.checkBoxUnique.Location = new System.Drawing.Point(13, 289);
            this.checkBoxUnique.Name = "checkBoxUnique";
            this.checkBoxUnique.Size = new System.Drawing.Size(66, 17);
            this.checkBoxUnique.TabIndex = 16;
            this.checkBoxUnique.Text = "Unique?";
            this.checkBoxUnique.UseVisualStyleBackColor = true;
            this.checkBoxUnique.CheckedChanged += new System.EventHandler(this.checkBoxUnique_CheckedChanged);
            // 
            // checkBoxBackground
            // 
            this.checkBoxBackground.AutoSize = true;
            this.checkBoxBackground.Location = new System.Drawing.Point(13, 313);
            this.checkBoxBackground.Name = "checkBoxBackground";
            this.checkBoxBackground.Size = new System.Drawing.Size(90, 17);
            this.checkBoxBackground.TabIndex = 17;
            this.checkBoxBackground.Text = "Background?";
            this.checkBoxBackground.UseVisualStyleBackColor = true;
            // 
            // checkBoxDropDups
            // 
            this.checkBoxDropDups.AutoSize = true;
            this.checkBoxDropDups.Enabled = false;
            this.checkBoxDropDups.ForeColor = System.Drawing.Color.Red;
            this.checkBoxDropDups.Location = new System.Drawing.Point(86, 289);
            this.checkBoxDropDups.Name = "checkBoxDropDups";
            this.checkBoxDropDups.Size = new System.Drawing.Size(108, 17);
            this.checkBoxDropDups.TabIndex = 18;
            this.checkBoxDropDups.Text = "Drop Duplicates?";
            this.checkBoxDropDups.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(11, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // btnIndexOptionsJsonClear
            // 
            this.btnIndexOptionsJsonClear.Image = global::MongoSharp.Properties.Resources.Erase;
            this.btnIndexOptionsJsonClear.Location = new System.Drawing.Point(501, 48);
            this.btnIndexOptionsJsonClear.Name = "btnIndexOptionsJsonClear";
            this.btnIndexOptionsJsonClear.Size = new System.Drawing.Size(26, 23);
            this.btnIndexOptionsJsonClear.TabIndex = 6;
            this.btnIndexOptionsJsonClear.UseVisualStyleBackColor = true;
            this.btnIndexOptionsJsonClear.Click += new System.EventHandler(this.btnIndexOptionsJsonClear_Click);
            // 
            // linkLabelIndexTutorial
            // 
            this.linkLabelIndexTutorial.AutoSize = true;
            this.linkLabelIndexTutorial.Location = new System.Drawing.Point(257, 7);
            this.linkLabelIndexTutorial.Name = "linkLabelIndexTutorial";
            this.linkLabelIndexTutorial.Size = new System.Drawing.Size(42, 13);
            this.linkLabelIndexTutorial.TabIndex = 7;
            this.linkLabelIndexTutorial.TabStop = true;
            this.linkLabelIndexTutorial.Text = "Tutorial";
            this.linkLabelIndexTutorial.Visible = false;
            this.linkLabelIndexTutorial.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelIndexTutorial_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Templates:";
            // 
            // linkLabelHelp
            // 
            this.linkLabelHelp.AutoSize = true;
            this.linkLabelHelp.Location = new System.Drawing.Point(467, 9);
            this.linkLabelHelp.Name = "linkLabelHelp";
            this.linkLabelHelp.Size = new System.Drawing.Size(58, 13);
            this.linkLabelHelp.TabIndex = 9;
            this.linkLabelHelp.TabStop = true;
            this.linkLabelHelp.Text = "Index Help";
            this.linkLabelHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelHelp_LinkClicked);
            // 
            // checkBoxSparse
            // 
            this.checkBoxSparse.AutoSize = true;
            this.checkBoxSparse.Location = new System.Drawing.Point(114, 313);
            this.checkBoxSparse.Name = "checkBoxSparse";
            this.checkBoxSparse.Size = new System.Drawing.Size(65, 17);
            this.checkBoxSparse.TabIndex = 20;
            this.checkBoxSparse.Text = "Sparse?";
            this.checkBoxSparse.UseVisualStyleBackColor = true;
            // 
            // FormAddIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 338);
            this.Controls.Add(this.checkBoxSparse);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBoxDropDups);
            this.Controls.Add(this.checkBoxBackground);
            this.Controls.Add(this.checkBoxUnique);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxIndexName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblAddIndex);
            this.Controls.Add(this.txtBoxHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddIndex";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Add Index";
            this.Load += new System.EventHandler(this.FormAddIndex_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageSelector.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIndex)).EndInit();
            this.tabPageJson.ResumeLayout(false);
            this.tabPageJson.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAddIndex;
        private System.Windows.Forms.TextBox txtBoxHeader;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageSelector;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtBoxIndexName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewIndex;
        private System.Windows.Forms.Button btnAddIndex;
        private System.Windows.Forms.DataGridViewComboBoxColumn Index;
        private System.Windows.Forms.DataGridViewComboBoxColumn Sort;
        private System.Windows.Forms.CheckBox checkBoxUnique;
        private System.Windows.Forms.CheckBox checkBoxBackground;
        private System.Windows.Forms.CheckBox checkBoxDropDups;
        private System.Windows.Forms.Button buttonRemoveIndex;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tabPageJson;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxIndexOptionsJson;
        private System.Windows.Forms.ComboBox comboBoxIndexTypes;
        private System.Windows.Forms.TextBox textBoxIndexJson;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnIndexJsonClear;
        private System.Windows.Forms.Button btnIndexOptionsJsonClear;
        private System.Windows.Forms.LinkLabel linkLabelIndexTutorial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabelHelp;
        private System.Windows.Forms.CheckBox checkBoxSparse;
    }
}