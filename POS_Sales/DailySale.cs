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
    public partial class DailySale : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;
        public string solduser;

        public DailySale()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            LoadCashier();
        }

        private void picclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCashier()
        {
            cboCashier.Items.Clear();
            cboCashier.Items.Add("All Cashier");
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tdUserAcc WHERE role LIKE 'Cashier'", cn);
            dr = cm.ExecuteReader();
            while(dr.Read()){
                cboCashier.Items.Add(dr["username"].ToString());

            }
            dr.Close();
            cn.Close();
        }

        public void LoadSold()
        {
            int i = 0;
            double total = 0;
            dvgSold.Rows.Clear();
            cn.Open();
            if(cboCashier.Text == "All Cashier")
            {
                cm = new SqlCommand("select c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total  from tbCart as c inner join tdProduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + dateFrom.Value + "' and '" + dateTo.Value + "'", cn);


            }
            else
            {
                cm = new SqlCommand("select c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total from tbCart as c inner join tdProduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + dateFrom.Value + "' and '" + dateTo.Value + "' and cashier like'" + cboCashier.Text + "' ", cn);
            }
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                total += double.Parse(dr["total"].ToString());
                dvgSold.Rows.Add(i, dr["id"].ToString(), dr["transno"].ToString(), dr["pcode"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), dr["total"].ToString());

            }
            dr.Close();
            cn.Close();
            lblTotal.Text = total.ToString("#,#00.00");
        }

        private void cboCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSold();
        }

        private void dateFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadSold();
        }

        private void dateTo_ValueChanged(object sender, EventArgs e)
        {
            LoadSold();
        }

        private void DailySale_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void dvgSold_CellContentClick(object sender, DataGridViewCellEventArgs e)//5.25
        {
            string colName = dvgSold.Columns[e.ColumnIndex].Name;
            if(colName == "Cancel")
            {
                CancelOrder cancelOrder = new CancelOrder(this);
                cancelOrder.txtid.Text = dvgSold.Rows[e.RowIndex].Cells[1].Value.ToString();
                cancelOrder.txtTransno.Text = dvgSold.Rows[e.RowIndex].Cells[2].Value.ToString();
                cancelOrder.txtPcode.Text = dvgSold.Rows[e.RowIndex].Cells[3].Value.ToString();
                cancelOrder.txtDesc.Text = dvgSold.Rows[e.RowIndex].Cells[4].Value.ToString();
                cancelOrder.txtPrice.Text = dvgSold.Rows[e.RowIndex].Cells[5].Value.ToString();
                cancelOrder.txtQty.Text = dvgSold.Rows[e.RowIndex].Cells[6].Value.ToString();
                cancelOrder.txtDiscount.Text = dvgSold.Rows[e.RowIndex].Cells[7].Value.ToString();
                cancelOrder.txtTotal.Text = dvgSold.Rows[e.RowIndex].Cells[8].Value.ToString();
                cancelOrder.txtCanceledBy.Text = solduser;
                cancelOrder.ShowDialog();
            }
        }
    }
}
