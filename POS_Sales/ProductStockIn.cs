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
    public partial class ProductStockIn : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;
        string stitle = "Point Of Sales";
        StockIn stockIn;

        public ProductStockIn(StockIn stk)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            stockIn = stk;
            LoadProduct();
            
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadProduct()
        {
            int i = 0;
            dvgProduct.Rows.Clear();
            cm = new SqlCommand("SELECT pcode, pdesc, qty FROM tdProduct WHERE pdesc LIKE '%" + txtSearch.Text + "%'", cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                //display data row
                i++;
                dvgProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());

            }
            dr.Close();
            cn.Close();
        }

        private void dvgProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dvgProduct.Columns[e.ColumnIndex].Name;
            if(colName == "Select")
            {
                if(stockIn.txtStockInBy.Text == string.Empty)
                {
                    MessageBox.Show("Please enter stock in by name", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    stockIn.txtStockInBy.Focus();
                    this.Dispose();
                  
                }

                if(MessageBox.Show("Add this item?",stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
                {
                    try
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tdStock(refno, pcode, sdate, stockinby, supplieid)VALUES(@refno, @pcode, @sdate, @stockinby, @supplieid)", cn);
                        cm.Parameters.AddWithValue("@refno", stockIn.txtRefNo.Text);
                        cm.Parameters.AddWithValue("@pcode", dvgProduct.Rows[e.RowIndex].Cells[1].Value.ToString());
                        cm.Parameters.AddWithValue("@sdate", stockIn.dtStockIn.Value);
                        cm.Parameters.AddWithValue("@stockinby", stockIn.txtStockInBy.Text);
                        cm.Parameters.AddWithValue("@supplieid", stockIn.lblId.Text);
                       
                        cm.ExecuteNonQuery();
                        cn.Close();
                        stockIn.LoadStock();
                        MessageBox.Show("Successfullt added", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, stitle);
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
