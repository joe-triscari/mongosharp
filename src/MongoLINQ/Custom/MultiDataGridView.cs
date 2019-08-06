using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using MongoSharp.Model;

namespace MongoSharp
{
    public partial class MultiDataGridView : UserControl
    {
        public MultiDataGridView()
        {
            InitializeComponent();
            dataGridView1.Sorted += (s, e) => AddRowNumbersToResults(dataGridView1);
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            var dataTable = dataGridView1[e.ColumnIndex, e.RowIndex].Value as CustomDataTable;
            if (dataTable != null)
            {

                if (!dataTable.IsLoaded)
                {
                    var queryResult = QueryResult.ToQueryResult(dataTable.OriginalObject);
                    var realDataTable = new QueryResultConverter().ConvertToDataTable(queryResult);
                    realDataTable.IsLoaded = true;
                    realDataTable.OriginalObject = dataTable.OriginalObject;
                    dataTable = realDataTable;
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = dataTable;
                }

                var name = dataGridView1[e.ColumnIndex, e.RowIndex].OwningColumn.Name;
                AddDataTableCurrentRow(e.RowIndex);
                //name = dataGridView1[e.ColumnIndex, e.RowIndex].OwningColumn.Name;
                AddDataTable(dataTable, name);
            }
        }

        public CustomDataTable DataTable
        {
            set
            {
                if (value == null)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.DataSource = null;
                    return;
                }
                    
                AddDataTable(value, value.Rows.Count + " Rows");
            }
        }

        public DataGridView DataGridView
        {
            get { return dataGridView1; }
        }

        private void AddDataTableCurrentRow(int rowIndex)
        {
            if (toolStripNav.Items.Count != 1)
                return;

            var dataTable = toolStripNav.Items[0].Tag as CustomDataTable;
            if(dataTable != null)
            {
                DataRow dataRow = dataTable.Rows[rowIndex];
                var newDataTable = dataTable.Clone();
                var newDataRow = newDataTable.Rows.Add();
                newDataRow.ItemArray = dataRow.ItemArray.Clone() as object[];
                AddDataTable(newDataTable, "Row " + (rowIndex + 1));
            }
        }

        internal void AddRowNumbers()
        {
            if (dataGridView1 != null)
                AddRowNumbersToResults(dataGridView1);
        }

        private void AddDataTable(DataTable dataTable, string text)
        {
            dataGridView1.DataSource = new DataView(dataTable);
            AddRowNumbersToResults(dataGridView1);
            if (toolStripNav.Items.Count != 0)
            {                
                var tsl = new ToolStripLabel {Text = ">"};
                toolStripNav.Items.Add(tsl);
            }

            var tsb = new ToolStripButton
                            {
                                Text = text,
                                Tag = dataTable
                            };
            tsb.Click += (sender, e) => DisplayDataTable(tsb);
            toolStripNav.Items.Add(tsb);
        }

        private void DisplayDataTable(ToolStripButton tsb)
        {
            var dataTable = tsb.Tag as CustomDataTable;
            dataGridView1.DataSource = new DataView(dataTable);
            AddRowNumbersToResults(dataGridView1);
            dataGridView1.BringToFront();
            RemoveNavigationButtons(tsb);
        }

        private void RemoveNavigationButtons(ToolStripButton tsb)
        {
            for (int i = toolStripNav.Items.Count - 1; i >= 0; --i)
            {
                var currentTsb = toolStripNav.Items[i];
                if (currentTsb != tsb)
                {
                    toolStripNav.Items.RemoveAt(i);
                }
                else
                    break;
            }
        }

        private void AddRowNumbersToResults(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                DataGridViewRowHeaderCell cell = dataGridView.Rows[i].HeaderCell;
                cell.Value = (i + 1).ToString().Trim();
                dataGridView.Rows[i].HeaderCell = cell;
            }
            dataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dataTable = dataGridView1[e.ColumnIndex, e.RowIndex].Value as DataTable;
            if (dataTable != null)
            {
               // var cell = dataGridView1[e.ColumnIndex, e.RowIndex];
                e.Value = "{Double-Click} -->";
            }
        }

        public string CreateTable(string tableName)
        {
            var sb = new StringBuilder("create table " + tableName + " (" + Environment.NewLine);
            string insertSyntax = String.Format("insert into {0} (", tableName);

            var columns = new List<DataGridViewColumn>();
            for (int i = 0; i < dataGridView1.Columns.Count; ++i)
            {
                var column = dataGridView1.Columns[i];
                columns.Add(column);
                sb.AppendFormat("\t{0} {1} NULL{2}{3}", column.Name, GetDatabaseType(column.ValueType),
                                (i == dataGridView1.Columns.Count - 1 ? "" : ","),
                                Environment.NewLine);
                insertSyntax += column.Name + ",";
            }
            //insertSyntax = insertSyntax.TrimEnd(',') + ")" + Environment.NewLine + "\tvalues (";
            insertSyntax = insertSyntax.TrimEnd(',') + ") values (";
            sb.AppendLine(")" + Environment.NewLine);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                sb.Append(insertSyntax);
                for (int i = 0; i < row.Cells.Count; ++i)
                {
                    sb.AppendFormat("{0}{1}", FormatValue(columns[i].ValueType, row.Cells[i].Value),
                                    i == row.Cells.Count - 1 ? "" : ",");
                }
                sb.AppendLine(")");
            }

            return sb.ToString();
        }

        private string FormatValue(Type type, object value)
        {
            if (value == null || value.GetType() == typeof(DBNull))
                return "null";
            if (type == typeof(DateTime))
                return "'" + ((DateTime)value).ToString("MM/dd/yyyy hh:mm:ss.fff tt") + "'";
            if (type.IsNumericType())
                return value.ToString().TrimEnd();

            return "'" + value.ToString().Replace("'", "''").TrimEnd() + "'";
        }

        private string GetDatabaseType(Type type)
        {
            if (type == typeof(DateTime))
                return "DATETIME";
            if (type.IsNumericType())
                return "INT";

            return "VARCHAR(1000)";
        }

        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dataTable = dataGridView1[e.ColumnIndex, e.RowIndex].Value as CustomDataTable;
            if(dataTable != null && dataTable.OriginalObject != null)
            {
                try
                {
                    string json = JsonConvert.SerializeObject(dataTable.OriginalObject, Formatting.Indented);
                    e.ToolTipText = json;
                }
                catch (Exception ex)
                {
                    e.ToolTipText = ex.Message;
                }                
            }
        }
    }
}
