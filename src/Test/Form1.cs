using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var table = new MyDataTable();
            table.Columns.Add(new DataColumn
                    {
                        DataType = typeof(string),
                        ColumnName = "Column1",
                        AllowDBNull = true
                    });
            table.Columns.Add(new DataColumn
            {
                DataType = typeof(MyDataTable),
                ColumnName = "Column2",
                AllowDBNull = true
            });

            DataRow dataRow = table.NewRow();
            dataRow["Column1"] = "Row1";
            dataRow["Column2"] = GetNestedDataTable();
            table.Rows.Add(dataRow);

            dataRow = table.NewRow();
            dataRow["Column1"] = "Row2";
            dataRow["Column2"] = GetNestedDataTable();
            table.Rows.Add(dataRow);

            string str = table.ToString();
            multiDataGridView1.DataTable = table;
        }

        private class MyDataTable : DataTable
        {
            override public string ToString()
            {
                return "Click Here...";
            }
        }

        private MyDataTable GetNestedDataTable()
        {
            var table = new MyDataTable();
            table.Columns.Add(new DataColumn
            {
                DataType = typeof(string),
                ColumnName = "InnerColumn1",
                AllowDBNull = true
            });
            table.Columns.Add(new DataColumn
            {
                DataType = typeof(string),
                ColumnName = "InnerColumn2",
                AllowDBNull = true
            });

            DataRow dataRow = table.NewRow();
            dataRow["InnerColumn1"] = "StringValueA";
            dataRow["InnerColumn2"] = "StringValueB";
            table.Rows.Add(dataRow);

            return table;
        }
    }
}
