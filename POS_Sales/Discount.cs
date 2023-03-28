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
    public partial class Discount : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
       
        string stitle = "Point Of Sales";
        Cashier cashier;

        public Discount(Cashier cash)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            cashier = cash;
            txtDiscount.Focus();
            this.KeyPreview = true;
        }

        private void picclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Discount_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Escape) this.Dispose();
            else if (e.KeyCode == Keys.Enter) btnsave.PerformClick();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double disc = double.Parse(txtTotalPrice.Text) * double.Parse(txtDiscount.Text)*0.01;
                txtDiscountAmount.Text = disc.ToString("#,##0,00");
            }

            catch(Exception )

            {
                txtDiscountAmount.Text = "0.00";
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Add discount? click yes to confirm",stitle,MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("Update tbCart SET disc_percent=@disc_percent WHERE id=@id", cn);
                    cm.Parameters.AddWithValue("@disc_percent", double.Parse(txtDiscount.Text));
                    cm.Parameters.AddWithValue("@id", int.Parse(lbId.Text));
                    cm.ExecuteNonQuery();
                    cn.Close();
                    cashier.LoadCart();
                    this.Dispose();
                }
            }

            catch (Exception ex)

            {
                cn.Close();
                MessageBox.Show(ex.Message, stitle);
            }
        }
    }
}
