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
    public partial class LookUpProduct : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;
       
        Cashier cashier;

        public LookUpProduct(Cashier cash)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            cashier = cash;
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
            cm = new SqlCommand("SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty FROM tdProduct AS p INNER JOIN tdBrand AS b ON b.id = p.bid INNER JOIN tdCatagory AS c ON c.id = p.cid WHERE CONCAT( p.pdesc,b.brand, c.category) LIKE '%" + txtSearch.Text + "%'", cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                //display data row
                i++;
                dvgProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());

            }
            dr.Close();
            cn.Close();
        }

        private void dvgProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dvgProduct.Columns[e.ColumnIndex].Name;
            if(colName == "Select")
            {
                Qty qty = new Qty(cashier);
                qty.ProductDetails(dvgProduct.Rows[e.RowIndex].Cells[1].Value.ToString(), double.Parse(dvgProduct.Rows[e.RowIndex].Cells[6].Value.ToString()), cashier.lblTransNo.Text, int.Parse(dvgProduct.Rows[e.RowIndex].Cells[7].Value.ToString()));
                qty.ShowDialog();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void LookUpProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
