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
    public partial class NewDiscount : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;

        public NewDiscount()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
        }

        private void picclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void NewDiscount_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double disc = double.Parse(txtTotalPrice.Text) * double.Parse(txtDiscount.Text) * 0.01;
                txtDiscountAmount.Text = disc.ToString("#,##0.00");
            }

            catch (Exception)

            {
                txtDiscountAmount.Text = "0.00";
            }
        }
    }
}
