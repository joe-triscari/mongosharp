using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MongoSharp.Model;
using ScintillaNET;

namespace MongoSharp
{
    public partial class FormSchema : Form
    {
        public FormSchema()
        {
            InitializeComponent();
            scintillaCode.ConfigurationManager.Language = "cs";
            scintillaCode.Margins[0].Width = 20;
            scintillaCode.Caret.Style = CaretStyle.Line;

         //   toolStripButtonAutoGen.Image = MongoSharp.Properties.Resources.AutoGen.ToBitmap();
        }

        public bool WasUpdated { get; private set; }

        private void FormSchema_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.CSFile;

            if (String.IsNullOrWhiteSpace(CollectionInfo.Namespace))
            {
                toolStripTextBoxNamespace.Text = CollectionInfo.DefaultNamespace;
            }
            else
            {
                toolStripTextBoxNamespace.Text = CollectionInfo.Namespace;
            }

            if (CollectionInfo.Models != null && CollectionInfo.Models.Count > 0)
            {                
                scintillaCode.Text = CollectionInfo.Models[0].ModelCode;               
                ValidateModel(CollectionInfo.Models[0].RootClassName, toolStripTextBoxNamespace.Text, false);
            }            
        }

        public MongoCollectionInfo CollectionInfo { get; set; }

        private bool ValidateModel(string selectedRootClass, string @namespace, bool displayError)
        {
            string code = scintillaCode.Text;

            toolStripComboBoxRootClass.Items.Clear();

            try
            {
                if (String.IsNullOrWhiteSpace(code))
                    throw new Exception("Please paste your C# model code into the editor or use the Auto Generate option.");
                if (String.IsNullOrWhiteSpace(@namespace))
                    throw new Exception("Please enter a namespace.");

                @namespace = @namespace.Trim();

                foreach (var connInfo in Settings.Instance.Connections)
                    foreach(var dbInfo in connInfo.Databases)
                        foreach(var collectionInfo in dbInfo.Collections)
                            if (collectionInfo.Path != CollectionInfo.Path && collectionInfo.Namespace == @namespace)
                                throw new Exception("Namespace already used in another collection. Please select a new one.");

                var types = new MongoDynamicCodeRunner().CompileModelCode(code, @namespace);
                if (!types.Any())
                    throw new Exception("No model classes are defined.");

                toolStripComboBoxRootClass.Items.AddRange(
                    (from t in types
                     select t.Name).Cast<object>().ToArray());

                if (!String.IsNullOrWhiteSpace(selectedRootClass))
                {
                    string found =
                        toolStripComboBoxRootClass.Items.Cast<string>().ToList().Find(x => x == selectedRootClass);
                    if (found != null)
                    {
                        toolStripComboBoxRootClass.SelectedItem = found;
                        toolStripComboBoxRootClass.Text = found;
                    }
                }

                if (toolStripComboBoxRootClass.Items.Count == 1)
                {
                    toolStripComboBoxRootClass.SelectedItem = toolStripComboBoxRootClass.Items[0];
                }                

                if(toolStripComboBoxRootClass.SelectedItem == null)
                    throw new Exception("Please select a root model class.");
            }
            catch (Exception ex)
            {
                if(displayError)
                    MessageBox.Show(ex.Message, "Invalid Model", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void toolStripButtonAutoGen_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                toolStripComboBoxRootClass.Items.Clear();

                int sampleSize;
                if (!Int32.TryParse(toolStripTextBoxSampleSize.Text, out sampleSize))
                {
                    sampleSize = 1000;
                    toolStripTextBoxSampleSize.Text = sampleSize.ToString();
                }

                var doc =
                    MongoCollectionSchemaStore.GetSchemaDocument(CollectionInfo.Database, CollectionInfo.Name, sampleSize)
                                              .SchemaDocument;
                List<string> classes = new BsonDocumentConverter().ToCSharpClassDeclarations(doc);
                scintillaCode.Text = String.Join("\r\n", classes.ToArray()).TrimEnd('\r','\n').TrimStart('\r','\n');
                toolStripComboBoxRootClass.Items.Add("Doc0");
                toolStripComboBoxRootClass.SelectedItem = "Doc0";
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            
        }

        private void scintillaCode_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonValidate_Click(object sender, EventArgs e)
        {
            var selectedRootClass = toolStripComboBoxRootClass.SelectedItem as string;
            if (ValidateModel(selectedRootClass, toolStripTextBoxNamespace.Text, true))
                MessageBox.Show("Success!", "Model Validation");
        }

        private void toolStripButtonSaveAndClose_Click(object sender, EventArgs e)
        {
            string code = scintillaCode.Text;
            if (String.IsNullOrWhiteSpace(code))
            {
                CollectionInfo.Models.Clear();
                Settings.Instance.Save();
                WasUpdated = true;
                Close();
            }
            else
            {
                var selectedRootClass = toolStripComboBoxRootClass.SelectedItem as string;
                if (ValidateModel(selectedRootClass, toolStripTextBoxNamespace.Text, true))
                {
                    CollectionInfo.Models.Clear();
                    CollectionInfo.Models.Add(new MongoCollectionModelInfo
                    {
                        Collection = CollectionInfo,
                        RootClassName = toolStripComboBoxRootClass.SelectedItem as string,
                        ModelCode = scintillaCode.Text,                        
                    });
                    CollectionInfo.Namespace = toolStripTextBoxNamespace.Text;
                    Settings.Instance.Save();
                    WasUpdated = true;
                    Close();
                }
            }
        }               
    }
}
