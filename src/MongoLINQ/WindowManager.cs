using System.Windows.Forms;
using MongoSharp.Model.Interface;

namespace MongoSharp
{
    public class WindowManager
    {
        public delegate void OnControlActivatedEventHandler(Control control);
        public event OnControlActivatedEventHandler OnControlActivated;

        private WindowManager()
        {

        }

        private static WindowManager _instance;

        public static WindowManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WindowManager();

                return _instance;
            }
        }

        public FormMain MainForm { get; internal set; }
        public CustomDockPanel MainDockPanel { get; internal set; }
        public RibbonTab HomeRibbonTab { get; internal set; }
        public RibbonPanel ClipBoardRibbonPanel { get; internal set; }
        public RibbonPanel EditorRibbonPanel { get; internal set; }
        public IConnectionBrowserWindow ConnectionBrowserWindow { get; internal set; }
        public ISchemaWindow SchemaWindow { get; internal set; }
        public IOutputWindow OutputWindow { get; internal set; }
        public CodeSnippetsWindow CodeSnippetsWindow { get; internal set; }

        public EditorWindow ActiveEditorWindow { get; set; }

        public Control SelectedControl
        {
            get;
            private set;
        }
        public void ControlActivated(Control control)
        {
            SelectedControl = control;

            if (OnControlActivated != null)
                OnControlActivated(SelectedControl);
        }
    }
}
