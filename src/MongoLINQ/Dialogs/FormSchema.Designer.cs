namespace MongoSharp
{
    partial class FormSchema
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSchema));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxRootClass = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelSpacer = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonAutoGen = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelSampSize = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxSampleSize = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonValidate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.scintillaCode = new ScintillaNET.Scintilla();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelNamespace = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxNamespace = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scintillaCode)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripComboBoxRootClass,
            this.toolStripSeparator1,
            this.toolStripLabelSpacer,
            this.toolStripButtonAutoGen,
            this.toolStripLabelSampSize,
            this.toolStripTextBoxSampleSize,
            this.toolStripSeparator2,
            this.toolStripButtonValidate,
            this.toolStripButtonSaveAndClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(830, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(62, 22);
            this.toolStripLabel1.Text = "Root Class";
            // 
            // toolStripComboBoxRootClass
            // 
            this.toolStripComboBoxRootClass.Name = "toolStripComboBoxRootClass";
            this.toolStripComboBoxRootClass.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabelSpacer
            // 
            this.toolStripLabelSpacer.Name = "toolStripLabelSpacer";
            this.toolStripLabelSpacer.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripButtonAutoGen
            // 
            this.toolStripButtonAutoGen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAutoGen.Image")));
            this.toolStripButtonAutoGen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAutoGen.Name = "toolStripButtonAutoGen";
            this.toolStripButtonAutoGen.Size = new System.Drawing.Size(103, 22);
            this.toolStripButtonAutoGen.Text = "Auto Generate";
            this.toolStripButtonAutoGen.Click += new System.EventHandler(this.toolStripButtonAutoGen_Click);
            // 
            // toolStripLabelSampSize
            // 
            this.toolStripLabelSampSize.Name = "toolStripLabelSampSize";
            this.toolStripLabelSampSize.Size = new System.Drawing.Size(94, 22);
            this.toolStripLabelSampSize.Text = "Max Sample Size";
            // 
            // toolStripTextBoxSampleSize
            // 
            this.toolStripTextBoxSampleSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxSampleSize.Name = "toolStripTextBoxSampleSize";
            this.toolStripTextBoxSampleSize.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBoxSampleSize.Text = "1000";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonValidate
            // 
            this.toolStripButtonValidate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonValidate.Image")));
            this.toolStripButtonValidate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonValidate.Name = "toolStripButtonValidate";
            this.toolStripButtonValidate.Size = new System.Drawing.Size(68, 22);
            this.toolStripButtonValidate.Text = "Validate";
            this.toolStripButtonValidate.Click += new System.EventHandler(this.toolStripButtonValidate_Click);
            // 
            // toolStripButtonSaveAndClose
            // 
            this.toolStripButtonSaveAndClose.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveAndClose.Image")));
            this.toolStripButtonSaveAndClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveAndClose.Name = "toolStripButtonSaveAndClose";
            this.toolStripButtonSaveAndClose.Size = new System.Drawing.Size(96, 22);
            this.toolStripButtonSaveAndClose.Text = "Save && Close";
            this.toolStripButtonSaveAndClose.Click += new System.EventHandler(this.toolStripButtonSaveAndClose_Click);
            // 
            // scintillaCode
            // 
            this.scintillaCode.Caret.Style = ScintillaNET.CaretStyle.Invisible;
            this.scintillaCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaCode.Location = new System.Drawing.Point(0, 50);
            this.scintillaCode.Name = "scintillaCode";
            this.scintillaCode.Scrolling.HorizontalScrollTracking = false;
            this.scintillaCode.Size = new System.Drawing.Size(830, 448);
            this.scintillaCode.Styles.BraceBad.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scintillaCode.Styles.BraceLight.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scintillaCode.Styles.CallTip.FontName = "Segoe UI\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scintillaCode.Styles.ControlChar.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scintillaCode.Styles.Default.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scintillaCode.Styles.IndentGuide.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scintillaCode.Styles.LastPredefined.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scintillaCode.Styles.LineNumber.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scintillaCode.Styles.Max.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scintillaCode.TabIndex = 1;
            this.scintillaCode.Click += new System.EventHandler(this.scintillaCode_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 498);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(830, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabelStatus.Text = "Ready";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelNamespace,
            this.toolStripTextBoxNamespace});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(830, 25);
            this.toolStrip2.TabIndex = 5;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabelNamespace
            // 
            this.toolStripLabelNamespace.Name = "toolStripLabelNamespace";
            this.toolStripLabelNamespace.Size = new System.Drawing.Size(72, 22);
            this.toolStripLabelNamespace.Text = "Namespace:";
            // 
            // toolStripTextBoxNamespace
            // 
            this.toolStripTextBoxNamespace.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxNamespace.Name = "toolStripTextBoxNamespace";
            this.toolStripTextBoxNamespace.Size = new System.Drawing.Size(300, 25);
            // 
            // FormSchema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 520);
            this.Controls.Add(this.scintillaCode);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormSchema";
            this.Text = "Model";
            this.Load += new System.EventHandler(this.FormSchema_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scintillaCode)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxRootClass;
        private ScintillaNET.Scintilla scintillaCode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSpacer;
        private System.Windows.Forms.ToolStripButton toolStripButtonAutoGen;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSampSize;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSampleSize;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonValidate;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveAndClose;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabelNamespace;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxNamespace;
    }
}