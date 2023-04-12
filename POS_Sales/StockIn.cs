using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Sales
{
    public partial class StockIn : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;
        string stitle = "Point Of Sales";

        public StockIn()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            loadSupplier();
            GetRefNo();
        }

        public void GetRefNo()
        {
            Random rnd = new Random();
            txtRefNo.Clear();
            txtRefNo.Text += rnd.Next();
        }


        public void loadSupplier()
        {
            cbSupplier.Items.Clear();
            cbSupplier.DataSource = dbcn.getTable("SELECT * FROM tdSupplier");
            cbSupplier.DisplayMember = "supplier";
        }

        public void LoadStock()
        {
            int i = 0;
            dvgStockIn.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM vwStockin WHERE refno LIKE '" + txtRefNo.Text+ "'AND status LIKE 'Pending'",cn);//vwStockIn
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dvgStockIn.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr["supplier"].ToString());

            }
            dr.Close();
            cn.Close();
        }

        private void cbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tdSupplier WHERE supplier LIKE '" + cbSupplier.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                lblId.Text = dr["id"].ToString();
                txtConPerson.Text = dr["contactperson"].ToString();
                txtAddress.Text = dr["address"].ToString();
            }
            dr.Close();
            cn.Close();
        }

        private void cbSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void LinGenarate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GetRefNo();
        }

        private void LinkProduct_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProductStockIn productStock = new ProductStockIn(this);
            productStock.ShowDialog();
      
        }

        private void btnEntry_Click(object sender, EventArgs e)
        {
            try
            {
                if(dvgStockIn.Rows.Count > 0)
                {
                    if(MessageBox.Show("Are you sure want to save this record", stitle,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for(int i=0; i<dvgStockIn.Rows.Count; i++)
                        {
                            //update product quantity
                            cn.Open();
                            cm = new SqlCommand("UPDATE tdProduct SET qty = qty + " + int.Parse(dvgStockIn.Rows[i].Cells[5].Value.ToString()) + " WHERE pcode LIKE '" + dvgStockIn.Rows[i].Cells[3].Value.ToString() + "'", cn);
                            cm.ExecuteNonQuery();
                            cn.Close();

                            //update stockin quantity
                            cn.Open();
                            cm = new SqlCommand("UPDATE tdStock SET qty = qty + " + int.Parse(dvgStockIn.Rows[i].Cells[5].Value.ToString()) + ", status='Done' WHERE id LIKE '" + dvgStockIn.Rows[i].Cells[1].Value.ToString() + "'", cn);
                            cm.ExecuteNonQuery();
                            cn.Close();
                        }
                        Clear();
                        LoadStock();
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message,stitle , MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        public void Clear()
        {
            txtRefNo.Clear();
            txtStockInBy.Clear();
            dtStockIn.Value = DateTime.Now;
        }

        private void dvgStockIn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dvgStockIn.Columns[e.ColumnIndex].Name;
            if(colName == "Delete")
            {
                if(MessageBox.Show("Remove this item?",stitle,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tdStock WHERE id='" + dvgStockIn.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Item has been sucessfully removed", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStock();
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)//8.20.18
        {
            try
            {
                int i = 0;
                dvgInStockHistory.Rows.Clear();
                //  cn.Open();
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }
                cm = new SqlCommand("SELECT * FROM vwStockin WHERE CAST(sdate as date) BETWEEN '" + dtFrom.Value.ToShortDateString()+ "' AND '" + dtTo.Value.ToShortDateString() + "' AND status LIKE 'Done'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    /* string dateString = dr[5].ToString();
                     DateTime dateTime;
                     if (DateTime.TryParse(dateString, out dateTime))
                     {
                         dvgInStockHistory.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dateTime.ToShortDateString(), dr[6].ToString(), dr[7].ToString());
                     }
                     else
                     {
                         MessageBox.Show("Nothing Data....! Please Try Again");
                     }*/
                      dvgInStockHistory.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), DateTime.Parse(dr[5].ToString()).ToShortDateString(), dr[6].ToString(), dr["supplier"].ToString());

                }
                dr.Close();
                cn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
