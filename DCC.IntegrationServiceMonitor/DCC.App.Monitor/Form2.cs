using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Windows.Forms;


namespace DCC.App.Monitor
{
    public partial class Form2 : Form
    {
        public int i = 1;
        string sChangedRows = string.Empty;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void GetData()
        {
            try
            {
                SqlHelper oSql = new SqlHelper();
                var table = new DataTable();
                table = oSql.ExecuteDataQuery(@"SELECT Id,Response,BatchDate,Vehicle_Id [Vehicle],Shipment_Number [Shipment No],Line_Id [Line Id],Total_Fuel_Litres [Total Fuel Liters],Per_Litre_Amount [Per Liter Amount],Currency,Service_Station_Code [Station Code] FROM [GLC_tbl_Integration] WHERE IsProcessed = 'Y' AND Status = 0", System.Data.CommandType.Text).Tables[0];


                DataTable oDatable = table;
                gridControl1.DataSource = oDatable;
                gridView2.ExpandAllGroups();

                int visibleEventsCount = gridView2.DataRowCount;
                textBox1.Text = visibleEventsCount.ToString();
                gridView1.Columns["Id"].Width = 80;
                gridView1.Columns["Response"].Width = 550;
            }
            catch (Exception ex)
            {

            }

        }
        private void gridControl1_Load(object sender, EventArgs e)
        {
            GridView View = sender as GridView;
            GetData();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (i == 1)
            {
                this.WindowState = FormWindowState.Maximized;
                i = 2;
            }
            else if (i == 2)
            {
                this.WindowState = FormWindowState.Normal;
                i = 1;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            GetData();
        }
        private void Expand_Click(object sender, EventArgs e)
        {
            gridView2.ExpandAllGroups();
        }
        private void Collapse_Click(object sender, EventArgs e)
        {
            gridView2.CollapseAllGroups();
        }
        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView View = sender as GridView;
            if (View == null)
                return;

            string sValue = e.Value.ToString();
            string id = View.GetRowCellValue(e.RowHandle, View.Columns["id"]).ToString();

            if (e.Column.Caption == "Retry" && e.Value.ToString() == "True")
            {
                if (sChangedRows.Length > 0)
                    sChangedRows = sChangedRows + "," + id;
                else
                    sChangedRows = id;

            }

            int visibleEventsCount = View.DataRowCount;
            textBox1.Text = visibleEventsCount.ToString();
        }
        private void Save_Click(object sender, EventArgs e)
        {
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);

                if (sChangedRows.Length > 0)
                    sChangedRows = sChangedRows + "," + row[0].ToString();
                else
                    sChangedRows = row[0].ToString();
            }

            if (sChangedRows.Length > 0)
            {
                SqlHelper oSql = new SqlHelper();
                oSql.ExecuteNonQuery("update [GLC_tbl_Integration] set IsProcessed ='N', Status = 0 where id in (" + sChangedRows + ")", CommandType.Text);
                sChangedRows = string.Empty;
            }
            GetData();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Left = Top = 0;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
        }
    }
}



