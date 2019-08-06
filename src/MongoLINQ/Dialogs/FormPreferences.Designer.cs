namespace MongoSharp
{
    partial class FormPreferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreferences));
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageEditor = new System.Windows.Forms.TabPage();
            this.checkBoxShowLineNumbers = new System.Windows.Forms.CheckBox();
            this.txtBoxEditorBackgroundColor = new System.Windows.Forms.TextBox();
            this.btnChangeColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxSynLang = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageGrid = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbJson = new System.Windows.Forms.RadioButton();
            this.rbTreeView = new System.Windows.Forms.RadioButton();
            this.rbTable = new System.Windows.Forms.RadioButton();
            this.lblResultGridSampleText = new System.Windows.Forms.Label();
            this.btnResultGridFont = new System.Windows.Forms.Button();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.checkBoxOutputClearOnExecute = new System.Windows.Forms.CheckBox();
            this.lblOutputTimestampFormat = new System.Windows.Forms.Label();
            this.comboBoxOutputTimestampFormat = new System.Windows.Forms.ComboBox();
            this.checkBoxOutputTimestamp = new System.Windows.Forms.CheckBox();
            this.tabPageSettingsFile = new System.Windows.Forms.TabPage();
            this.lblSettingsFile = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCleanSettingsFile = new System.Windows.Forms.Button();
            this.tabPageModel = new System.Windows.Forms.TabPage();
            this.checkBoxAllowAutoModel = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.listBoxSections = new System.Windows.Forms.ListBox();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlMain.SuspendLayout();
            this.tabPageEditor.SuspendLayout();
            this.tabPageGrid.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            this.tabPageSettingsFile.SuspendLayout();
            this.tabPageModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageEditor);
            this.tabControlMain.Controls.Add(this.tabPageGrid);
            this.tabControlMain.Controls.Add(this.tabPageOutput);
            this.tabControlMain.Controls.Add(this.tabPageSettingsFile);
            this.tabControlMain.Controls.Add(this.tabPageModel);
            this.tabControlMain.Location = new System.Drawing.Point(106, 61);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(424, 259);
            this.tabControlMain.TabIndex = 2;
            // 
            // tabPageEditor
            // 
            this.tabPageEditor.Controls.Add(this.checkBoxShowLineNumbers);
            this.tabPageEditor.Controls.Add(this.txtBoxEditorBackgroundColor);
            this.tabPageEditor.Controls.Add(this.btnChangeColor);
            this.tabPageEditor.Controls.Add(this.label3);
            this.tabPageEditor.Controls.Add(this.comboBoxSynLang);
            this.tabPageEditor.Controls.Add(this.label2);
            this.tabPageEditor.Location = new System.Drawing.Point(4, 22);
            this.tabPageEditor.Name = "tabPageEditor";
            this.tabPageEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEditor.Size = new System.Drawing.Size(416, 233);
            this.tabPageEditor.TabIndex = 0;
            this.tabPageEditor.Text = "Editor";
            this.tabPageEditor.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowLineNumbers
            // 
            this.checkBoxShowLineNumbers.AutoSize = true;
            this.checkBoxShowLineNumbers.Location = new System.Drawing.Point(10, 98);
            this.checkBoxShowLineNumbers.Name = "checkBoxShowLineNumbers";
            this.checkBoxShowLineNumbers.Size = new System.Drawing.Size(127, 17);
            this.checkBoxShowLineNumbers.TabIndex = 6;
            this.checkBoxShowLineNumbers.Text = "Show Line Numbers?";
            this.checkBoxShowLineNumbers.UseVisualStyleBackColor = true;
            this.checkBoxShowLineNumbers.CheckedChanged += new System.EventHandler(this.checkBoxShowLineNumbers_CheckedChanged);
            // 
            // txtBoxEditorBackgroundColor
            // 
            this.txtBoxEditorBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxEditorBackgroundColor.Location = new System.Drawing.Point(151, 56);
            this.txtBoxEditorBackgroundColor.Name = "txtBoxEditorBackgroundColor";
            this.txtBoxEditorBackgroundColor.ReadOnly = true;
            this.txtBoxEditorBackgroundColor.Size = new System.Drawing.Size(69, 20);
            this.txtBoxEditorBackgroundColor.TabIndex = 5;
            // 
            // btnChangeColor
            // 
            this.btnChangeColor.Location = new System.Drawing.Point(230, 54);
            this.btnChangeColor.Name = "btnChangeColor";
            this.btnChangeColor.Size = new System.Drawing.Size(75, 23);
            this.btnChangeColor.TabIndex = 4;
            this.btnChangeColor.Text = "Change...";
            this.btnChangeColor.UseVisualStyleBackColor = true;
            this.btnChangeColor.Click += new System.EventHandler(this.btnChangeColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Background Color:";
            // 
            // comboBoxSynLang
            // 
            this.comboBoxSynLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSynLang.FormattingEnabled = true;
            this.comboBoxSynLang.Items.AddRange(new object[] {
            "mssql",
            "cs"});
            this.comboBoxSynLang.Location = new System.Drawing.Point(151, 17);
            this.comboBoxSynLang.Name = "comboBoxSynLang";
            this.comboBoxSynLang.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSynLang.TabIndex = 2;
            this.comboBoxSynLang.SelectedIndexChanged += new System.EventHandler(this.comboBoxSynLang_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Syntax Highlight Language:";
            // 
            // tabPageGrid
            // 
            this.tabPageGrid.Controls.Add(this.groupBox1);
            this.tabPageGrid.Controls.Add(this.lblResultGridSampleText);
            this.tabPageGrid.Controls.Add(this.btnResultGridFont);
            this.tabPageGrid.Location = new System.Drawing.Point(4, 22);
            this.tabPageGrid.Name = "tabPageGrid";
            this.tabPageGrid.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGrid.Size = new System.Drawing.Size(416, 233);
            this.tabPageGrid.TabIndex = 1;
            this.tabPageGrid.Text = "Grid";
            this.tabPageGrid.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbJson);
            this.groupBox1.Controls.Add(this.rbTreeView);
            this.groupBox1.Controls.Add(this.rbTable);
            this.groupBox1.Location = new System.Drawing.Point(18, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 47);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Default Results View";
            // 
            // rbJson
            // 
            this.rbJson.AutoSize = true;
            this.rbJson.Location = new System.Drawing.Point(185, 20);
            this.rbJson.Name = "rbJson";
            this.rbJson.Size = new System.Drawing.Size(47, 17);
            this.rbJson.TabIndex = 2;
            this.rbJson.TabStop = true;
            this.rbJson.Text = "Json";
            this.rbJson.UseVisualStyleBackColor = true;
            this.rbJson.Click += new System.EventHandler(this.rbResutView_Click);
            // 
            // rbTreeView
            // 
            this.rbTreeView.AutoSize = true;
            this.rbTreeView.Location = new System.Drawing.Point(86, 20);
            this.rbTreeView.Name = "rbTreeView";
            this.rbTreeView.Size = new System.Drawing.Size(73, 17);
            this.rbTreeView.TabIndex = 1;
            this.rbTreeView.TabStop = true;
            this.rbTreeView.Text = "Tree View";
            this.rbTreeView.UseVisualStyleBackColor = true;
            this.rbTreeView.Click += new System.EventHandler(this.rbResutView_Click);
            // 
            // rbTable
            // 
            this.rbTable.AutoSize = true;
            this.rbTable.Location = new System.Drawing.Point(7, 20);
            this.rbTable.Name = "rbTable";
            this.rbTable.Size = new System.Drawing.Size(52, 17);
            this.rbTable.TabIndex = 0;
            this.rbTable.TabStop = true;
            this.rbTable.Text = "Table";
            this.rbTable.UseVisualStyleBackColor = true;
            this.rbTable.Click += new System.EventHandler(this.rbResutView_Click);
            // 
            // lblResultGridSampleText
            // 
            this.lblResultGridSampleText.AutoSize = true;
            this.lblResultGridSampleText.Location = new System.Drawing.Point(124, 23);
            this.lblResultGridSampleText.Name = "lblResultGridSampleText";
            this.lblResultGridSampleText.Size = new System.Drawing.Size(66, 13);
            this.lblResultGridSampleText.TabIndex = 1;
            this.lblResultGridSampleText.Text = "Sample Text";
            // 
            // btnResultGridFont
            // 
            this.btnResultGridFont.Location = new System.Drawing.Point(18, 17);
            this.btnResultGridFont.Name = "btnResultGridFont";
            this.btnResultGridFont.Size = new System.Drawing.Size(96, 23);
            this.btnResultGridFont.TabIndex = 0;
            this.btnResultGridFont.Text = "Change Font...";
            this.btnResultGridFont.UseVisualStyleBackColor = true;
            this.btnResultGridFont.Click += new System.EventHandler(this.btnResultGridFont_Click);
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.checkBoxOutputClearOnExecute);
            this.tabPageOutput.Controls.Add(this.lblOutputTimestampFormat);
            this.tabPageOutput.Controls.Add(this.comboBoxOutputTimestampFormat);
            this.tabPageOutput.Controls.Add(this.checkBoxOutputTimestamp);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(416, 233);
            this.tabPageOutput.TabIndex = 2;
            this.tabPageOutput.Text = "Output";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // checkBoxOutputClearOnExecute
            // 
            this.checkBoxOutputClearOnExecute.AutoSize = true;
            this.checkBoxOutputClearOnExecute.Location = new System.Drawing.Point(20, 56);
            this.checkBoxOutputClearOnExecute.Name = "checkBoxOutputClearOnExecute";
            this.checkBoxOutputClearOnExecute.Size = new System.Drawing.Size(139, 17);
            this.checkBoxOutputClearOnExecute.TabIndex = 3;
            this.checkBoxOutputClearOnExecute.Text = "Clear on each execute?";
            this.checkBoxOutputClearOnExecute.UseVisualStyleBackColor = true;
            this.checkBoxOutputClearOnExecute.CheckedChanged += new System.EventHandler(this.checkBoxOutputClearOnExecute_CheckedChanged);
            // 
            // lblOutputTimestampFormat
            // 
            this.lblOutputTimestampFormat.AutoSize = true;
            this.lblOutputTimestampFormat.Location = new System.Drawing.Point(304, 21);
            this.lblOutputTimestampFormat.Name = "lblOutputTimestampFormat";
            this.lblOutputTimestampFormat.Size = new System.Drawing.Size(42, 13);
            this.lblOutputTimestampFormat.TabIndex = 2;
            this.lblOutputTimestampFormat.Text = "xxxxxxx";
            this.lblOutputTimestampFormat.Visible = false;
            // 
            // comboBoxOutputTimestampFormat
            // 
            this.comboBoxOutputTimestampFormat.FormattingEnabled = true;
            this.comboBoxOutputTimestampFormat.Items.AddRange(new object[] {
            "hh:mm:ss.ffff",
            "HH:mm:ss.f",
            "HH:mm:ss.ffffzzz",
            "dd MMM HH:mm:ss"});
            this.comboBoxOutputTimestampFormat.Location = new System.Drawing.Point(139, 18);
            this.comboBoxOutputTimestampFormat.Name = "comboBoxOutputTimestampFormat";
            this.comboBoxOutputTimestampFormat.Size = new System.Drawing.Size(159, 21);
            this.comboBoxOutputTimestampFormat.TabIndex = 1;
            this.comboBoxOutputTimestampFormat.Visible = false;
            this.comboBoxOutputTimestampFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxOutputTimestampFormat_SelectedIndexChanged);
            // 
            // checkBoxOutputTimestamp
            // 
            this.checkBoxOutputTimestamp.AutoSize = true;
            this.checkBoxOutputTimestamp.Location = new System.Drawing.Point(20, 20);
            this.checkBoxOutputTimestamp.Name = "checkBoxOutputTimestamp";
            this.checkBoxOutputTimestamp.Size = new System.Drawing.Size(113, 17);
            this.checkBoxOutputTimestamp.TabIndex = 0;
            this.checkBoxOutputTimestamp.Text = "Show Timestamp?";
            this.checkBoxOutputTimestamp.UseVisualStyleBackColor = true;
            this.checkBoxOutputTimestamp.CheckedChanged += new System.EventHandler(this.checkBoxOutputTimestamp_CheckedChanged);
            // 
            // tabPageSettingsFile
            // 
            this.tabPageSettingsFile.Controls.Add(this.lblSettingsFile);
            this.tabPageSettingsFile.Controls.Add(this.label4);
            this.tabPageSettingsFile.Controls.Add(this.btnCleanSettingsFile);
            this.tabPageSettingsFile.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettingsFile.Name = "tabPageSettingsFile";
            this.tabPageSettingsFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettingsFile.Size = new System.Drawing.Size(416, 233);
            this.tabPageSettingsFile.TabIndex = 3;
            this.tabPageSettingsFile.Text = "Settings File";
            this.tabPageSettingsFile.UseVisualStyleBackColor = true;
            // 
            // lblSettingsFile
            // 
            this.lblSettingsFile.AutoSize = true;
            this.lblSettingsFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSettingsFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettingsFile.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblSettingsFile.Location = new System.Drawing.Point(28, 12);
            this.lblSettingsFile.Name = "lblSettingsFile";
            this.lblSettingsFile.Size = new System.Drawing.Size(30, 13);
            this.lblSettingsFile.TabIndex = 3;
            this.lblSettingsFile.Text = "c:\\...";
            this.lblSettingsFile.Click += new System.EventHandler(this.lblSettingsFile_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(292, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Remove databases and collection data that no longer exists.";
            // 
            // btnCleanSettingsFile
            // 
            this.btnCleanSettingsFile.Location = new System.Drawing.Point(25, 39);
            this.btnCleanSettingsFile.Name = "btnCleanSettingsFile";
            this.btnCleanSettingsFile.Size = new System.Drawing.Size(75, 23);
            this.btnCleanSettingsFile.TabIndex = 0;
            this.btnCleanSettingsFile.Text = "Clean";
            this.btnCleanSettingsFile.UseVisualStyleBackColor = true;
            this.btnCleanSettingsFile.Click += new System.EventHandler(this.btnCleanSettingsFile_Click);
            // 
            // tabPageModel
            // 
            this.tabPageModel.Controls.Add(this.checkBoxAllowAutoModel);
            this.tabPageModel.Location = new System.Drawing.Point(4, 22);
            this.tabPageModel.Name = "tabPageModel";
            this.tabPageModel.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageModel.Size = new System.Drawing.Size(416, 233);
            this.tabPageModel.TabIndex = 4;
            this.tabPageModel.Text = "Model";
            this.tabPageModel.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllowAutoModel
            // 
            this.checkBoxAllowAutoModel.AutoSize = true;
            this.checkBoxAllowAutoModel.Location = new System.Drawing.Point(27, 20);
            this.checkBoxAllowAutoModel.Name = "checkBoxAllowAutoModel";
            this.checkBoxAllowAutoModel.Size = new System.Drawing.Size(349, 17);
            this.checkBoxAllowAutoModel.TabIndex = 0;
            this.checkBoxAllowAutoModel.Text = "Allow MongoSharp to generate models as needed when not defined.";
            this.checkBoxAllowAutoModel.UseVisualStyleBackColor = true;
            this.checkBoxAllowAutoModel.CheckedChanged += new System.EventHandler(this.checkBoxAllowAutoModel_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(350, 325);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(441, 325);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // listBoxSections
            // 
            this.listBoxSections.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxSections.FormattingEnabled = true;
            this.listBoxSections.ItemHeight = 16;
            this.listBoxSections.Items.AddRange(new object[] {
            "Editor",
            "Result Grid",
            "Output",
            "Settings File",
            "Model"});
            this.listBoxSections.Location = new System.Drawing.Point(6, 60);
            this.listBoxSections.Name = "listBoxSections";
            this.listBoxSections.Size = new System.Drawing.Size(94, 260);
            this.listBoxSections.TabIndex = 5;
            this.listBoxSections.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Image = global::MongoSharp.Properties.Resources.Settings32x32;
            this.pictureBox1.Location = new System.Drawing.Point(12, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(-1, 1);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(531, 54);
            this.textBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Edit Preferences";
            // 
            // FormPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 358);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBoxSections);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tabControlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPreferences";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.FormPreferences_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageEditor.ResumeLayout(false);
            this.tabPageEditor.PerformLayout();
            this.tabPageGrid.ResumeLayout(false);
            this.tabPageGrid.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageOutput.ResumeLayout(false);
            this.tabPageOutput.PerformLayout();
            this.tabPageSettingsFile.ResumeLayout(false);
            this.tabPageSettingsFile.PerformLayout();
            this.tabPageModel.ResumeLayout(false);
            this.tabPageModel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageEditor;
        private System.Windows.Forms.TabPage tabPageGrid;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox listBoxSections;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ComboBox comboBoxSynLang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnChangeColor;
        private System.Windows.Forms.TextBox txtBoxEditorBackgroundColor;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnResultGridFont;
        private System.Windows.Forms.Label lblResultGridSampleText;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.ComboBox comboBoxOutputTimestampFormat;
        private System.Windows.Forms.CheckBox checkBoxOutputTimestamp;
        private System.Windows.Forms.Label lblOutputTimestampFormat;
        private System.Windows.Forms.CheckBox checkBoxOutputClearOnExecute;
        private System.Windows.Forms.TabPage tabPageSettingsFile;
        private System.Windows.Forms.Label lblSettingsFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCleanSettingsFile;
        private System.Windows.Forms.TabPage tabPageModel;
        private System.Windows.Forms.CheckBox checkBoxAllowAutoModel;
        private System.Windows.Forms.CheckBox checkBoxShowLineNumbers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbJson;
        private System.Windows.Forms.RadioButton rbTreeView;
        private System.Windows.Forms.RadioButton rbTable;
    }
}