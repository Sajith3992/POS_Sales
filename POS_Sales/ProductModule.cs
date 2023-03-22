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
    public partial class ProductModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        string stitle = "Point Of Sales";
        Product product;


        public ProductModule(Product pd)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            product = pd;
            LoadBrand();
            LoadCategory();
        }

        // Data base Category & Brand filter data in to the Product Module Drop DawnList  
        public void LoadCategory()
        {
            cboCategory.Items.Clear();
            cboCategory.DataSource = dbcn.getTable("SELECT * FROM tdCatagory");
            cboCategory.DisplayMember = "category";
            cboCategory.ValueMember = "id";
        }

        public void LoadBrand()
        {
            cboBrand.Items.Clear();
            cboBrand.DataSource = dbcn.getTable("SELECT * FROM tdBrand");
            cboBrand.DisplayMember = "brand";
            cboBrand.ValueMember = "id";
        }

        // end dropdawnlist

        private void picclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Clear()
        {
            txtPcode.Clear();
            txtBarcode.Clear();
            txtDesc.Clear();
            txtPrice.Clear();
            cboBrand.SelectedIndex = 0;
            cboCategory.SelectedIndex = 0;
            // UDReOrder.Value = 1;
            txtReorder.Clear();

            txtPcode.Enabled = true;
            txtPcode.Focus();
            btnsave.Enabled = true;
            btnupdate.Enabled = false;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Are you want to Save this Product?","Save Product",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tdProduct(pcode, barcode, pdesc, bid, cid, price, reorder)VALUES (@pcode, @barcode, @pdesc, @bid, @cid, @price, @reorder)", cn);
                    cm.Parameters.AddWithValue("@pcode", txtPcode.Text);
                    cm.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    cm.Parameters.AddWithValue("@pdesc", txtDesc.Text);
                    cm.Parameters.AddWithValue("@bid", cboBrand.SelectedValue);
                    cm.Parameters.AddWithValue("@cid", cboCategory.SelectedValue);
                    cm.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
                    // cm.Parameters.AddWithValue("@reorder", UDReOrder.Value);
                    cm.Parameters.AddWithValue("@reorder", txtReorder.Text);
                    cn.Open();
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully saved", stitle);
                    Clear();
                  // product.LoadProduct();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btncansel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Are you sure want to update this product?","Update Product",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tdProduct SET barcode=@barcode,pdesc=@pdesc,bid=@bid,cid=@cid,price=@price, reorder=@reorder WHERE pcode LIKE @pcode", cn);
                    cm.Parameters.AddWithValue("@pcode", txtPcode.Text);
                    cm.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    cm.Parameters.AddWithValue("@pdesc", txtDesc.Text);
                    cm.Parameters.AddWithValue("@bid", cboBrand.SelectedValue);
                    cm.Parameters.AddWithValue("@cid", cboCategory.SelectedValue);
                    cm.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
                    // cm.Parameters.AddWithValue("@reorder", UDReOrder.Value);
                    cm.Parameters.AddWithValue("@reorder", txtReorder.Text);
                    cn.Open();
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully updated", stitle);
                    Clear();
                    this.Dispose();
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
