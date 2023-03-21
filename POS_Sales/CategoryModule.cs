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
    public partial class CategoryModule : Form
    {

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        Catagory catagory;

        public CategoryModule(Catagory ct)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            catagory = ct;
        }

        public void Clear()
        {
            txtCategory.Clear();
            txtCategory.Focus();
            btnsave.Enabled = true;
            btnupdate.Enabled = false;
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            //to insert brand name to brand table
            try
            {
                if (MessageBox.Show("Are you want to save this Category?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tdCatagory(category)VALUES(@category)", cn);
                    cm.Parameters.AddWithValue("@category", txtCategory.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successful saved.", "Point Of Sales");
                    Clear();
                }
                catagory.LoadCategory();
            }
            catch (Exception ex)
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
            //update category name
            if (MessageBox.Show("Are you sure you want to update this Category?", "Update Record!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cn.Open();
                cm = new SqlCommand("UPDATE tdCatagory SET category = @category WHERE id LIKE'" + lblid.Text + "'", cn);
                cm.Parameters.AddWithValue("@category", txtCategory.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Category has been Successfully updated.", "Point Of Sales");
                this.Dispose(); // to close this form after update data
            }
        }

        private void picclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
