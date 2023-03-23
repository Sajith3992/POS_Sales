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
    public partial class SupplierModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        string stitle = "Point Of Sales";
        Supplier supplier;

        public SupplierModule(Supplier sp)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            supplier = sp;
        }

        private void picclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Clear()
        {
            txtAddress.Clear();
            txtEmail.Clear();
            txtFax.Clear();
            txtPerson.Clear();
            txtPhone.Clear();
            txtsupplier.Clear();

            btnsave.Enabled = true;
            btnupdate.Enabled = false;
            txtsupplier.Focus();
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
               if(MessageBox.Show("Save this record? click yes to confirm.","CONFIRM",MessageBoxButtons.YesNo,MessageBoxIcon.Question )== DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tdSupplier(supplier, address, contactperson, phone, email, fax)VALUES (@supplier, @address, @contactperson, @phone, @email, @fax)", cn);
                    cm.Parameters.AddWithValue("@supplier", txtsupplier.Text);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@contactperson", txtPerson.Text);
                    cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cm.Parameters.AddWithValue("@email", txtEmail.Text);
                    cm.Parameters.AddWithValue("@fax", txtFax.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully saved!", "Saved Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    supplier.LoadSupplier();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, stitle);
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
                if (MessageBox.Show("Update this record? click yes to confirm.", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("Update tdSupplier set supplier=@supplier, address=@address, contactperson=@contactperson, phone=@phone, email=@email, fax=@fax where id=@id ", cn);
                    cm.Parameters.AddWithValue("@id", lblid.Text);
                    cm.Parameters.AddWithValue("@supplier", txtsupplier.Text);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@contactperson", txtPerson.Text);
                    cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cm.Parameters.AddWithValue("@email", txtEmail.Text);
                    cm.Parameters.AddWithValue("@fax", txtFax.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully updated!", "Updated Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                    supplier.LoadSupplier();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
        }
    }
}
