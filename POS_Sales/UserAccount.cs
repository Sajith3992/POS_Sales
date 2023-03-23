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
    public partial class UserAccount : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataAdapter dr;
        public UserAccount()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
        }

        private void metroTabPage1_Click(object sender, EventArgs e)
        {
            //non 
        }
        public void Clear()
        {
            txtFullname.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirm.Clear();
            txtUser.Clear();
            cboRole.Text = "";
            txtUsername.Focus();
        }
        private void btnAccsave_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtPassword.Text != txtConfirm.Text)
                {
                    MessageBox.Show("Password did not Match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cn.Open();
                cm = new SqlCommand("Insert into tdUserAcc(username, password, role, name) Values(@username, @password, @role, @name)", cn);
                cm.Parameters.AddWithValue("@username", txtUsername.Text);
                cm.Parameters.AddWithValue("@password", txtPassword.Text);
                cm.Parameters.AddWithValue("@role", cboRole.Text);
                cm.Parameters.AddWithValue("@name", txtFullname.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("New Account has been successfully saved!", "Saved Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Warning");
            }
        }

        private void btnAcccansel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
