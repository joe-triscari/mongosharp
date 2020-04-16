using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MongoSharp.Model;

namespace MongoSharp
{
    public partial class UserControlQueryResults : UserControl
    {
        public Action<string> OnCreateTableFromResults;
        private UserControlQueryResultsGrid _gridUserControl;
        private List<TabPage> _tabPages = new List<TabPage>(); 

        public UserControlQueryResults()
        {
            InitializeComponent();

            tabControlResults.SelectedIndexChanged += tabControlResults_SelectedIndexChanged;
        }

        void tabControlResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControlResults.SelectedTab == null)
                return;

            var result = tabControlResults.SelectedTab.Controls.OfType<IUserControlQueryResult>().ToList();
            if (result.Any())
            {
                result.First().OnSelected();
            }
        }

        public void LoadResults(QueryResult queryResult, MongoCollectionInfo mongoCollectionInfo)
        {
            AddGridTabpage(queryResult);
            AddTreeTabpage(queryResult, mongoCollectionInfo);
            AddJsonTabpage(queryResult);

            tabControlResults.SelectedIndex = Settings.Instance.Preferences.DefaultResultsView;

            tabControlResults_SelectedIndexChanged(tabControlResults, null);
        }

        public void PostLoadProcessing()
        {
            _gridUserControl?.AddRowNumbers();
        }

        private TabPage AddGridTabpage(QueryResult queryResult)
        {
            var gridTabPage = new TabPage("Table View");
            var queryResultControl = new UserControlQueryResultsGrid
                {
                    Dock = DockStyle.Fill,
                    OnCreateTableFromResults = tbl =>
                    {
                        OnCreateTableFromResults?.Invoke(tbl);
                    }
                };

            queryResultControl.LoadResults(queryResult);

            gridTabPage.Controls.Add(queryResultControl);
            tabControlResults.TabPages.Add(gridTabPage);
            gridTabPage.Select();
            _gridUserControl = queryResultControl;

            return gridTabPage;
        }

        private TabPage AddTreeTabpage(QueryResult queryResult, MongoCollectionInfo mongoCollectionInfo)
        {
            var treeTabPage = new TabPage("Tree View");
            var queryResultControl = new UserControlQueryResultsTree
                {
                    Dock = DockStyle.Fill
                };
            queryResultControl.LoadResults(queryResult, mongoCollectionInfo);
            treeTabPage.Controls.Add(queryResultControl);
            tabControlResults.TabPages.Add(treeTabPage);

            return treeTabPage;
        }

        private TabPage AddJsonTabpage(QueryResult queryResult)
        {
            var jsonTabPage = new TabPage("Json View");
            var jsonResultControl = new UserControlResultsJson
                {
                    Dock = DockStyle.Fill
                };
            jsonResultControl.LoadResults(queryResult);
            jsonTabPage.Controls.Add(jsonResultControl);
            tabControlResults.TabPages.Add(jsonTabPage);

            return jsonTabPage;
        }
    }
}
