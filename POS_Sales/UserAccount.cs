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
        SqlDataReader dr;
        MainForm1 main;

        string username;
        string name;
        string role;
        string accstatus;

        public UserAccount(MainForm1 mn)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            main = mn;
            LoadUser();
        }

        public void LoadUser()//6.24
        {
            int i = 0;
            dvgUser.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tdUserAcc ", cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                //display data row
                i++;
                dvgUser.Rows.Add(i, dr[0].ToString(), dr[3].ToString(), dr[4].ToString(), dr[2].ToString());

            }
            dr.Close();
            cn.Close();
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
           // txtUser.Clear();
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

        private void btnPassSave_Click(object sender, EventArgs e)//6.14
        {
            try
            {
            if(txtCuPass.Text != main._pass)
                {
                    MessageBox.Show("Current Password did not match!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            if(txtNewPass.Text != txtRePassword.Text)
                {
                    MessageBox.Show("Confirm new Password did not match!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dbcn.ExecuteQuery("UPDATE tdUserAcc SET password='" + txtNewPass.Text + "'WHERE username='" + lblUsername.Text + "'");
                MessageBox.Show("Password has been succesfully changed!", "Changed Password", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }

        private void UserAccount_Load(object sender, EventArgs e)//6.16
        {
            lblUsername.Text = main.labelUserName.Text;
        }

        private void btnPassCansel_Click(object sender, EventArgs e)
        {
            ClearCp();
        }

        public void ClearCp()
        {
            txtNewPass.Clear();
            txtRePassword.Clear();
            txtCuPass.Clear();
        }

        private void dvgUser_SelectionChanged(object sender, EventArgs e)//6.25
        {
            int i = dvgUser.CurrentRow.Index;
            username = dvgUser[1, i].Value.ToString();
            name = dvgUser[2, i].Value.ToString();
            role = dvgUser[4, i].Value.ToString();
            accstatus = dvgUser[3, i].Value.ToString();

            if(lblUsername.Text == username)
            {
                btnRemove.Enabled = false;
                btnResetPass.Enabled = false;
                lblAccNote.Text = "To Change the password, go to change password tag.";
            }
            else
            {
                btnRemove.Enabled = true;
                btnResetPass.Enabled = true;
                lblAccNote.Text = "To Change the password for " + username + ", click Reset Password";
            }
            gbUser.Text = "Password For " + username;
           
        }

        private void btnRemove_Click(object sender, EventArgs e)//6.30
        {
            if ((MessageBox.Show("You chose to remove this account from this Point of Sales System's user list.\n\n Are you sure want to remove '" + username + "'\\'" + role + "'", "User Account", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
            {
                dbcn.ExecuteQuery("DELETE FROM tdUserAcc WHERE username ='" + username + "'");
                MessageBox.Show("Account has been successfully deleted");
                LoadUser();
            }

        }
    }
}
