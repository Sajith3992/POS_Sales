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
                cm = new SqlCommand("SELECT TOP 10 pcode, pdesc, isnull(sum(qty),0) AS qty, ISNULL(SUM(total),0) AS total FROM vwTopSelling WHERE sdate BETWEEN '" + dtFromTopSelling.Value.ToString() + "' AND '" + dtToTopsell.Value.ToString() + "' AND status LIKE 'Sold' GROUP BY pcode, pdesc ORDER BY qty DESC", cn);
            }
            else if(cboTopSell.Text == "Sort By Total Amount")
            {
                cm = new SqlCommand("SELECT TOP 10 pcode, pdesc, isnull(sum(qty),0) AS qty, ISNULL(SUM(total),0) AS total FROM vwTopSelling WHERE sdate BETWEEN '" + dtFromTopSelling.Value.ToString() + "' AND '" + dtToTopsell.Value.ToString() + "' AND status LIKE 'Sold' GROUP BY pcode, pdesc ORDER BY total DESC", cn);
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

        public void LoadSoldItem()//8.59
        {
            try
            {
                dvgSoldItems.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("SELECT c.pcode, p.pdesc, c.price, sum(c.qty)as qty, SUM(c.disc) AS disc, SUM(c.total) AS total FROM tbCart AS c INNER JOIN tdProduct AS p ON c.pcode=p.pcode WHERE status LIKE 'Sold' AND sdate BETWEEN '" + dtFromSoldItem.Value.ToString() + "' AND '" + dtToSoldItem.Value.ToString() + "' GROUP BY c.pcode, p.pdesc, c.price",cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dvgSoldItems.Rows.Add(i, dr["pcode"].ToString(), dr["pdesc"].ToString(), double.Parse(dr["price"].ToString()).ToString("#,##0,00"), dr["qty"].ToString(), dr["disc"].ToString(), double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
                }
                dr.Close();
                cn.Close();

                cn.Open();
                cm = new SqlCommand("SELECT ISNULL(SUM(total),0) FROM tbCart WHERE status LIKE 'Sold' AND sdate BETWEEN '" + dtFromSoldItem.Value.ToString() + "' AND '" + dtToSoldItem.Value.ToString() + "' ", cn);
                lblTotal.Text = double.Parse(cm.ExecuteScalar().ToString()).ToString("#,##0.00");
                cn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void btnLoadSoldItem_Click(object sender, EventArgs e)//8.59-9.5
        {
            LoadSoldItem();
        }
    }
}
