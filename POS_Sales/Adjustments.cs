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
    public partial class Adjustments : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;
        MainForm1 main;
        int _qty;
        public Adjustments(MainForm1 mn)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            main = mn;
            ReferenceNo();
            LoadStock();
            lblUsername.Text = main.lblName.Text;
        }

        public void ReferenceNo()
        {
            Random rnd = new Random();
            lblRefNo.Text = rnd.Next().ToString();
        }

        public void LoadStock()//8.03
        {
            int i = 0;
            dvgAdjustment.Rows.Clear();
            cm = new SqlCommand("SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty FROM tdProduct AS p INNER JOIN tdBrand AS b ON b.id = p.bid INNER JOIN tdCatagory AS c ON c.id = p.cid WHERE CONCAT( p.pdesc,b.brand, c.category) LIKE '%" + txtSearch.Text + "%'", cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                //display data row
                i++;
                dvgAdjustment.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());

            }
            dr.Close();
            cn.Close();
        }

        private void dvgAdjustment_CellContentClick(object sender, DataGridViewCellEventArgs e)//8.05
        {
            string colName = dvgAdjustment.Columns[e.ColumnIndex].Name;
            if(colName == "Select")
            {
                lblPcode.Text = dvgAdjustment.Rows[e.RowIndex].Cells[1].Value.ToString();
                lblDesc.Text = dvgAdjustment.Rows[e.RowIndex].Cells[3].Value.ToString() + " " + dvgAdjustment.Rows[e.RowIndex].Cells[4].Value.ToString() + " " + dvgAdjustment.Rows[e.RowIndex].Cells[5].Value.ToString();
                _qty = int.Parse(dvgAdjustment.Rows[e.RowIndex].Cells[7].Value.ToString());
                btnSave.Enabled = true;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadStock();
        }

        public void Clear()
        {
            lblDesc.Text = "";
            lblPcode.Text = "";
            txtQty.Clear();
            txtRemark.Clear();
            cboAction.Text = "";
            ReferenceNo();
        }

        private void btnSave_Click(object sender, EventArgs e)//8.7
        {
            try
            {
                //validation for empty field 
                if (cboAction.Text =="")
                {
                    MessageBox.Show("Please select action for add or reduce.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboAction.Focus();
                    return;
                }

                if(txtQty.Text == "")
                {
                    MessageBox.Show("Please input quantity for add or reduce.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQty.Focus();
                    return;
                }

                if(txtRemark.Text == "")
                {
                    MessageBox.Show("Need reason for stock Adjustment", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   txtRemark.Focus();
                    return;
                }

                //update Stock
                if (int.Parse(txtQty.Text) > _qty)//8.10
                {
                    MessageBox.Show("Stock an hand quantity should be grater than adjustment quantity.", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if(cboAction.Text =="Remove From Inventory")
                {
                    dbcn.ExecuteQuery("UPDATE tdProduct SET qty - (qty - " + int.Parse(txtQty.Text) + ") WHERE pcode LIKE '" + lblPcode.Text + "'");
                }
                else if (cboAction.Text == "Add To Inventory")
                {
                    dbcn.ExecuteQuery("UPDATE tdProduct SET qty = (qty + " + int.Parse(txtQty.Text) + ") WHERE pcode LIKE '" + lblPcode.Text + "'");
                }

                dbcn.ExecuteQuery("INSERT INTO tdAdjustment(referenceno, pcode, qty, action, remarks, sdate, [user]) VALUES('"+lblRefNo.Text+ "','" + lblPcode.Text + "','" + int.Parse(txtQty.Text) + "', '" + cboAction.Text + "','" + txtRemark.Text + "','" + DateTime.Now.ToShortDateString() + "','" + lblUsername.Text + "')");
                MessageBox.Show("stock has been successfully adjusted.", "Process completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadStock();
                Clear();
                btnSave.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
