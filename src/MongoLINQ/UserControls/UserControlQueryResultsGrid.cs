using System;
using System.Windows.Forms;
using MongoSharp.Model;

namespace MongoSharp
{
    public partial class UserControlQueryResultsGrid : UserControl, IUserControlQueryResult
    {
        private bool _isLoaded;
        private QueryResult _queryResult;

        public UserControlQueryResultsGrid()
        {
            InitializeComponent();            
        }

        public Action<string> OnCreateTableFromResults;

        public void OnSelected()
        {
            if (!_isLoaded)
            {
                Cursor.Current = Cursors.WaitCursor;
                
                dataGridViewResults.DataTable = new QueryResultConverter().ConvertToDataTable(_queryResult);
                if (Settings.Instance.Preferences.ResultGridFont != null)
                    dataGridViewResults.Font = Settings.Instance.Preferences.ResultGridFont;

                Cursor.Current = Cursors.Default;
                _isLoaded = true;
            }
        }

        public void LoadResults(QueryResult queryResult)
        {
            _queryResult = queryResult;
        }

        internal void AddRowNumbers()
        {
            dataGridViewResults.AddRowNumbers();
        }

        private void toolStripButtonExportToExcel_Click(object sender, EventArgs e)
        {
            var ex = new ExcelExport();
            ex.exportToExcel(dataGridViewResults.DataGridView);
        }

        private void toolStripButtonCreateTable_Click(object sender, EventArgs e)
        {
            var frm = new FormTableName {StartPosition = FormStartPosition.CenterParent};
            frm.ShowDialog(this);

            if (!frm.Canceled)
            {
                string table = dataGridViewResults.CreateTable(frm.TableName);
                if (OnCreateTableFromResults != null)
                    OnCreateTableFromResults(table);
            }            
        }

        private void dataGridViewResults_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            
            //var cell = dataGridViewResults.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //object value = cell.Value;
            //if(value != null && !value.GetType().IsSimpleType())
            //{
            //    e.ToolTipText = "AASDFDF";
            //}
        }        
    }
}
