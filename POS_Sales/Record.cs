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
    public partial class Record : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;

        public Record()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
        }

        public void LoadTopSelling()//8.50
        {
            int i = 0;
            dvgTopSelling.Rows.Clear();
            cn.Open();
            
            //Sort By Total Amount
            if (cboTopSell.Text == "Sort By Qty")
            {
                cm = new SqlCommand("SELECT TOP 10 pcode, pdesc, isnull(sum(qty),0) AS qty, ISNULL(SUM(total),0) AS total FROM vwSoldItems WHERE sdate BETWEEN '" + dtFromTopSelling.Value.ToString() + "' AND '" + dtToTopsell.Value.ToString() + "' AND status LIKE 'Sold' GROUP BY pcode, pdesc, ORDER BY qty DESC", cn);
            }
            else if(cboTopSell.Text == "Sort By Total Amount")
            {
                cm = new SqlCommand("SELECT TOP 10 pcode, pdesc, isnull(sum(qty),0) AS qty, ISNULL(SUM(total),0) AS total FROM vwSoldItems WHERE sdate BETWEEN '" + dtFromTopSelling.Value.ToString() + "' AND '" + dtToTopsell.Value.ToString() + "' AND status LIKE 'Sold' GROUP BY pcode, pdesc, ORDER BY total DESC", cn);
            }
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dvgTopSelling.Rows.Add(i, dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["qty"].ToString(), double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
            }
            dr.Close();
            cn.Close();
        }

        private void btnLoadTopSell_Click(object sender, EventArgs e)//8.53
        {
            if(cboTopSell.Text == "Select sort type")
            {
                MessageBox.Show("Please select sort type from the dropdawn list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTopSell.Focus();
                return;
            }
            LoadTopSelling();
        }
    }
}
