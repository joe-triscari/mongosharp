using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MongoSharp
{
    public interface ISchemaWindow
    {
        string[] Properties { get; set; }
        void Show();
        void Clear();
    }

    public partial class SchemaWindow : DockContent, ISchemaWindow
    {
        public SchemaWindow()
        {
            InitializeComponent();
        }

        public string [] Properties
        {
            set 
            { 
                var items = value ?? new string[0];
                listBoxSchema.Items.Clear();
                foreach (string item in items)
                {
                    listBoxSchema.Items.Add(item);
                }

            }
            get
            {
                return listBoxSchema.Items.Cast<string>().ToArray();
            }
        }

        public void Clear()
        {
            listBoxSchema.Items.Clear();
        }
    }
}
