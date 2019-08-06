using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
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

            var dataTable = dataGridView1[e.ColumnIndex, e.RowIndex].Value as DataTable;
            if (dataTable != null)
            {
                //var nested = new MultiDataGridView();
                //nested.toolStripNav.Visible = false;
                //nested.DataTable = dataTable;
                //Controls.Add(nested);
                //nested.Dock = DockStyle.Fill;
                //nested.Location = new Point(0, dataGridView1.Location.Y - nested.toolStripNav.Height);
                //nested.BringToFront();

                //var tsb = new ToolStripButton();
                //tsb.Text = "#1";
                //toolStripNav.Items.Add(tsb);
                var name = dataGridView1[e.ColumnIndex, e.RowIndex].OwningColumn.Name;
                AddDataTableCurrentRow(e.RowIndex);
                name = dataGridView1[e.ColumnIndex, e.RowIndex].OwningColumn.Name;
                AddDataTable(dataTable, name);
            }
        }

        public DataTable DataTable
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

        private void AddDataTableCurrentRow(int rowIndex)
        {
            if (toolStripNav.Items.Count != 1)
                return;

            var dataTable = toolStripNav.Items[0].Tag as DataTable;
            if(dataTable != null)
            {
                DataRow dataRow = dataTable.Rows[rowIndex];
                var newDataTable = dataTable.Clone();
                var newDataRow = newDataTable.Rows.Add();
                newDataRow.ItemArray = dataRow.ItemArray.Clone() as object[];
                AddDataTable(newDataTable, rowIndex.ToString());
            }
        }

        private void AddDataTable(DataTable dataTable, string text)
        {
            dataGridView1.DataSource = new DataView(dataTable);
            AddRowNumbersToResults(dataGridView1);
            if (toolStripNav.Items.Count != 0)
            {                
                var tsl = new ToolStripLabel();
                tsl.Text = ">";
                toolStripNav.Items.Add(tsl);
            }

            var tsb = new ToolStripButton
                            {
                                Text = text,
                                Tag = dataTable
                            };
            tsb.Click += (sender, e) => { DisplayDataTable(tsb); };
            toolStripNav.Items.Add(tsb);
        }

        private void DisplayDataTable(ToolStripButton tsb)
        {
            var dataTable = tsb.Tag as DataTable;
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
                e.Value = "MongoSharp.MemoryInfo -->";
            }
        }
    }
}
