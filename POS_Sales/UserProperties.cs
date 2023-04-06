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
    public partial class UserProperties : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();

        UserAccount account;
        public string username;

        public UserProperties(UserAccount user)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            account = user;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnApply_Click(object sender, EventArgs e)//6.54
        {
            try
            {
                if((MessageBox.Show("Are you sure want to change this account properties?","Change Properties",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)== DialogResult.Yes))
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tdUserAcc SET name=@name, role=@role, isactivate=@isactivate WHERE username='" + username + "'", cn);
                    cm.Parameters.AddWithValue("@name", txtFullname.Text);
                    cm.Parameters.AddWithValue("@role", cboRole.Text);
                    cm.Parameters.AddWithValue("@isactivate", cboActivate.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Account properties has been suceessfully changed!", "Update Properties", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    account.LoadUser();
                    this.Dispose();
                }
 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
