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
    public partial class BrandModule : Form
    {
        /*Add a connection line */
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        public BrandModule()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            /* close page */
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //To insert brand name to brand table
            try
            {
                if (MessageBox.Show("Are you want to save this brand ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tdBrand(brand)VALUES(@brand)", cn);
                    cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successful saved.", "POS");
                    Clear();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
          Clear();
        }

        public void Clear()
        {
            txtBrand.Clear();
        }
    }
}
