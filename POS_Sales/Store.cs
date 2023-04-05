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
    public partial class Store : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;
        bool havestoreinfo = false;

        public Store()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            LoadStore();
        }

        public void LoadStore()//6.02
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tdStore", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    havestoreinfo = true;
                    txtstname.Text = dr["store"].ToString();
                    txtAddress.Text = dr["address"].ToString();
                }
                else
                {
                    txtstname.Clear();
                    txtAddress.Clear();
                }
                dr.Close();
                cn.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }

        private void btnsave_Click(object sender, EventArgs e)//6.07
        {
            try
            {
              if(MessageBox.Show("Save store Details?","Confirm", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (havestoreinfo)
                    {
                        dbcn.ExecuteQuery("UPDATE tdStore SET store ='" + txtstname.Text + "', address='" + txtAddress.Text + "'");
                    }
                    else
                    {
                        dbcn.ExecuteQuery("INSERT INTO tdStore (store,address)VALUES('" + txtstname.Text + "','" + txtAddress.Text + "')");
                    }
                    MessageBox.Show("Store detail has been successfully saved!", "Saved Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btncansel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Store_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
