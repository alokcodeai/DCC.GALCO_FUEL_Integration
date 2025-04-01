using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCC.App.Monitor
{
    public partial class Form1 : Form
    {
        string Connection = ConfigurationManager.ConnectionStrings["db"].ToString();

        public Form1()
        {
            InitializeComponent();
            GetSourceType();
            cmbsrc.SelectedIndex = 0;
            cmbstatus.SelectedIndex = 0; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetGridData();
        }

        public void GetSourceType()
        {
            using (SqlConnection con = new SqlConnection(Connection))
            {
                SqlDataAdapter adt = new SqlDataAdapter();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT * from int_details", con);
               // cmd.CommandType = System.Data.CommandType.StoredProcedure;
                adt.SelectCommand = cmd;
                adt.Fill(dt);

                cmbsrc.ValueMember = dt.Columns[0].ToString();
                cmbsrc.DisplayMember = dt.Columns[1].ToString();
                cmbsrc.DataSource = dt;
            }
        }

        public void GetGridData()
        {
                int Status;

                if (cmbstatus.SelectedItem == "Processed")
                {
                    Status = 1;
                }
                else if (cmbstatus.SelectedItem == "Not Processed")
                {
                    Status = 0;
                }
                else if (cmbstatus.SelectedItem == "Failed")
                {
                    Status = 2;
                }
                else
                {
                    Status = 3;     //  For Please Select
                }

            int Sourcetype = Convert.ToInt32(cmbsrc.SelectedValue);

            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    SqlDataAdapter adt = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("SELECT * from int_details", con);
                   // cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FromDate", dtfrom.Text);
                    cmd.Parameters.AddWithValue("@ToDate", dtto.Text);
                    cmd.Parameters.AddWithValue("@SourceType", Sourcetype);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    adt.SelectCommand = cmd;
                    adt.Fill(dt);
                    GridService.DataSource = dt;
                    //GridService.Columns[0].DisplayIndex = 7;
                    GridService.Columns["IntegrationId"].Visible = true;
                    GridService.Columns["SourceType"].Visible = true;
                    GridService.Columns["SourceId"].Visible = true;
                    GridService.Columns["OperationType"].Visible = false;
                    GridService.Columns["StoreId"].Visible = false;
                    GridService.Columns["Flag"].Visible = false;
                    GridService.Columns["Status"].Visible = false;
                    GridService.Columns["ErrorMessage"].Visible = true;
                    GridService.Columns["ErrorMessage"].DisplayIndex = 6;
                    GridService.Columns["LogDatetime"].Visible = true;
                    GridService.Columns["LogDatetime"].DisplayIndex = 4;
                    GridService.Columns["UnboundStaus"].DisplayIndex = 5;
                    GridService.Columns["UnboundRetry"].DisplayIndex = 7;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void cmbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGridData();
        }

        private void cmbsrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGridData();
        }

        private void dtto_ValueChanged(object sender, EventArgs e)
        {
            GetGridData();
        }

        private void dtfrom_ValueChanged(object sender, EventArgs e)
        {
            GetGridData();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            GetGridData();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int IntegrationId = 0;
                foreach (DataGridViewRow row in GridService.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["UnboundRetry"].Value);
                    if (isSelected)
                    {
                        IntegrationId = Convert.ToInt32(row.Cells["IntegrationId"].Value);
                        using (SqlConnection con = new SqlConnection(Connection))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE int_details SET Status=0 WHERE IntegrationId='" + IntegrationId + "' ", con);
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Flag Updated Successfully with Integration ID " + IntegrationId);
                                con.Close();
                            }
                        }
                    }
                }
                GetGridData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void GridService_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //int RowCount = GridService.Rows.Count;
                //int i = 0;
                foreach (DataGridViewRow row in GridService.Rows)
                {

                   

                    int Flag = Convert.ToInt32(row.Cells["Flag"].Value);
                    int Status = Convert.ToInt32(row.Cells["Status"].Value);
                    if (Flag == 0 && Status == 0)
                    {
                        row.Cells["UnboundStaus"].Value = "Not Processed";
                    }
                    else if (Flag == 1 && Status == 1)
                    {
                        row.Cells["UnboundStaus"].Value = "Processed";
                    }
                    else
                    {
                        row.Cells["UnboundStaus"].Value = "Failed";
                    }

                    //i++;
                    //if (RowCount == i)
                    //    return;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
