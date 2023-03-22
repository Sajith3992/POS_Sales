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
    public partial class Product : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;
        public Product()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            LoadProduct();
        }

        //Loading Product in to the Table
        public void LoadProduct()
        {
            int i = 0;
            dvgProduct.Rows.Clear();
            cm = new SqlCommand("SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.reorder FROM tdProduct AS p INNER JOIN tdBrand AS b ON b.id = p.bid INNER JOIN tdCatagory AS c ON c.id = p.cid WHERE CONCAT( p.pdesc,b.brand, c.category) LIKE '%" + txtSearch.Text + "%'", cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                //display data row
                i++;
                dvgProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(),dr[6].ToString());

            }
            dr.Close();
            cn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductModule productModule = new ProductModule(this);
            productModule.ShowDialog();
        }

        private void dvgProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string colName = dvgProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModule product = new ProductModule(this);
                product.txtPcode.Text = dvgProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                product.txtBarcode.Text = dvgProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                product.txtDesc.Text = dvgProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                product.cboBrand.Text = dvgProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                product.cboCategory.Text = dvgProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                product.txtPrice.Text = dvgProduct.Rows[e.RowIndex].Cells[6].Value.ToString();
                //product.UDReOrder.Value = int.Parse(dvgProduct.Rows[e.RowIndex].Cells[7].Value.ToString());
                product.txtReorder.Text = dvgProduct.Rows[e.RowIndex].Cells[7].Value.ToString();

                product.txtPcode.Enabled = false;
                product.btnsave.Enabled = false;
                product.btnupdate.Enabled = true;
                product.ShowDialog();
            }

            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tdProduct WHERE pcode LIKE'" + dvgProduct[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully deleted,", "Point Of Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            LoadProduct();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
