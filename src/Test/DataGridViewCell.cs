using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace Test
{
    public class NestedDataGridViewCell : DataGridViewCell
    {
        private DataGridView _dataGridView;

        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);  
	        graphics.FillRectangle(new SolidBrush(cellStyle.BackColor), cellBounds);
	 
	        SetupDGVForDraw();
	        var abbreviation = new Bitmap(cellBounds.Width, cellBounds.Height); 
	        _dataGridView.DrawToBitmap(abbreviation, new Rectangle(0, 0, cellBounds.Width, cellBounds.Height));
	        graphics.DrawImage(abbreviation, cellBounds, new Rectangle(0, 0, abbreviation.Width, abbreviation.Height), GraphicsUnit.Pixel);
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle)
        {
	        // Set the value of the editing control to the current cell value.        
	        base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
        }

        //public override Type EditType
        //{
        //    get
        //    {
        //        return typeof(DgvEditingControl);
        //    }
        //}

        public override Type ValueType
        {
            get
            {
                return typeof(object);
            }
            set
            {
                base.ValueType = value;
            }
        }

        private void SetupDGVForDraw()
        {             
	        _dataGridView.BackgroundColor = System.Drawing.Color.Coral;
	        _dataGridView.Size = new System.Drawing.Size(400, 100);
	        _dataGridView.AllowUserToAddRows = false; 
	        _dataGridView.RowHeadersVisible = false; 
	        _dataGridView.ColumnHeadersVisible = false; 
	        _dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; 
	        _dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;  
	        _dataGridView.AutoGenerateColumns = false; 
	 
	        if (Value.GetType() == typeof(System.Data.DataTable) )
            {
	            //reachrishikh edit  
	            //if you simply want to add columns to the nested datagridview as per the schema  
	            //of the datatable you're binding to it, then use the code below  
	            //dgv.Columns.Clear()  
	            //For Each column As DataColumn In CType(Value, DataTable).Columns  
	            //    dgv.Columns.Add(column.ColumnName, column.ColumnName)  
	            //Next  
	            //end reachrishikh edit  
	 
	            //reachrishikh edit  
	            //else use this code if you want to custom format your columns  
                var table = (System.Data.DataTable)Value;
	            _dataGridView.Columns.Clear();
                foreach(System.Data.DataColumn column in table.Columns)
                {
	                var dgvColumn = new DataGridViewTextBoxColumn
                        {
                            Name = column.ColumnName,
	                        HeaderText = column.ColumnName,	                        
	                        ValueType = typeof(Decimal)
                        }; 
	                dgvColumn.DefaultCellStyle.Format = "C2";

	                _dataGridView.Columns.Add(dgvColumn); 
	            } 
	            //end reachrishikh edit  
	             
                foreach(System.Data.DataRow dataRow in ((System.Data.DataTable)Value).Rows)
                {
                    _dataGridView.Rows.Add(dataRow.ItemArray);
                }
	        }
        }
    }
}
