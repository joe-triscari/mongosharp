﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using MongoSharp.Model;

namespace MongoSharp
{
    public partial class FormPreferences : Form
    {
        private Preferences _preferences;
        private bool _isLoading = true;

        public FormPreferences()
        {
            InitializeComponent();
        }

        private void FormPreferences_Load(object sender, EventArgs e)
        {
            _preferences = Settings.Instance.Preferences.Clone();

            listBoxSections.SelectedIndex = 0;
            tabControlMain.Appearance = TabAppearance.Buttons;
            tabControlMain.SizeMode = TabSizeMode.Fixed;
            tabControlMain.ItemSize = new System.Drawing.Size(0, 1);

            if (String.IsNullOrWhiteSpace(_preferences.EditorSyntaxLanguage))
            {
                _preferences.EditorSyntaxLanguage = "mssql";
                comboBoxSynLang.SelectedItem = _preferences.EditorSyntaxLanguage;
            }                
            else
                comboBoxSynLang.SelectedItem = _preferences.EditorSyntaxLanguage;

            txtBoxEditorBackgroundColor.BackColor = Settings.Instance.Preferences.EditorBackColor.ToArgb() == 0 ?
                                                        Color.White :
                                                        Settings.Instance.Preferences.EditorBackColor;
            if (_preferences.ResultGridFont != null)
                lblResultGridSampleText.Font = _preferences.ResultGridFont;    

            if(_preferences.OutputShowTimestamp)
            {
                checkBoxOutputTimestamp.Checked = true;
               
                for(int idx=0; idx < comboBoxOutputTimestampFormat.Items.Count; idx++)
                    if ( ((string)comboBoxOutputTimestampFormat.Items[idx]).Equals(_preferences.OutputTimestampFormat) )
                        comboBoxOutputTimestampFormat.SelectedIndex = idx;
            }

            checkBoxOutputClearOnExecute.Checked = _preferences.OutputClearOnExecute;

            lblSettingsFile.Text = Settings.Instance.GetSettingsFile();

            checkBoxAllowAutoModel.Checked = _preferences.AllowAutoGeneratedModels;
            checkBoxShowLineNumbers.Checked = _preferences.ShowEditorLineNumbers;

            rbTable.Checked = _preferences.DefaultResultsView == 0;
            rbTreeView.Checked = _preferences.DefaultResultsView == 1;
            rbJson.Checked = _preferences.DefaultResultsView == 2;

            _isLoading = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlMain.SelectedIndex = listBoxSections.SelectedIndex;
        }

        private void btnEditorFont_Click(object sender, EventArgs e)
        {
            // Show the dialog.
            DialogResult result = fontDialog1.ShowDialog();
            // See if OK was pressed.
            if (result == DialogResult.OK)
            {
               // _preferences.EditorFont = fontDialog1.Font;     
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Settings.Instance.Preferences = _preferences;
            Settings.Instance.Save();
            EditorWindowManager.SetEditorPreferences(_preferences);

            Close();
        }

        private void comboBoxSynLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            _preferences.EditorSyntaxLanguage = (string)comboBoxSynLang.SelectedItem;
        }

        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;
 
            var dialogResult = colorDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _preferences.EditorBackColor = colorDialog1.Color;
                txtBoxEditorBackgroundColor.BackColor = _preferences.EditorBackColor;
            }
        }

        private void btnResultGridFont_Click(object sender, EventArgs e)
        {
            if (_preferences.ResultGridFont != null)
                fontDialog1.Font = _preferences.ResultGridFont; 

            // Show the dialog.
            DialogResult result = fontDialog1.ShowDialog();
            // See if OK was pressed.
            if (result == DialogResult.OK)
            {
                 _preferences.ResultGridFont = fontDialog1.Font;
                 lblResultGridSampleText.Font = _preferences.ResultGridFont;
            }
        }

        private void checkBoxOutputTimestamp_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxOutputTimestampFormat.Visible = checkBoxOutputTimestamp.Checked;
            lblOutputTimestampFormat.Visible = checkBoxOutputTimestamp.Checked;
            _preferences.OutputShowTimestamp = checkBoxOutputTimestamp.Checked;
        }

        private void comboBoxOutputTimestampFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFormat = comboBoxOutputTimestampFormat.SelectedItem as string;
            if(!String.IsNullOrWhiteSpace(selectedFormat))
            {
                _preferences.OutputTimestampFormat = selectedFormat;
                lblOutputTimestampFormat.Text = DateTime.Now.ToString(selectedFormat);
            }
        }

        private void checkBoxOutputClearOnExecute_CheckedChanged(object sender, EventArgs e)
        {
            _preferences.OutputClearOnExecute = checkBoxOutputClearOnExecute.Checked;
        }

        private void btnCleanSettingsFile_Click(object sender, EventArgs e)
        {
            Settings.Instance.Clean();
            MessageBox.Show("Clean complete.", "Clean Settings File");
        }

        private void lblSettingsFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(lblSettingsFile.Text);
        }

        private void checkBoxAllowAutoModel_CheckedChanged(object sender, EventArgs e)
        {
            _preferences.AllowAutoGeneratedModels = checkBoxAllowAutoModel.Checked;
        }

        private void checkBoxShowLineNumbers_CheckedChanged(object sender, EventArgs e)
        {
            _preferences.ShowEditorLineNumbers = checkBoxShowLineNumbers.Checked;
        }


        private void rbResutView_Click(object sender, EventArgs e)
        {
            _preferences.DefaultResultsView = rbTable.Checked ? 0 : rbTreeView.Checked ? 1 : 2;
        }
    }
}