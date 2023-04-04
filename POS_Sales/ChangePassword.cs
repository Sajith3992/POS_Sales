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
    public partial class ChangePassword : Form //5.49.59
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;
        Cashier cashier;
        Login login;
        public ChangePassword(Cashier cash, Login log)
        {
            InitializeComponent();
            cashier = cash;
            login = log;
            
            lblUsername.Text = cashier.lblUsername.Text;
        }

        private void picclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                string oldpass = dbcn.getPassword(lblUsername.Text);//5.53.09
                if(oldpass != txtPass.text)
                {
                    MessageBox.Show("Wrong password, please try again!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    txtPass.Visible = false;
                    btnNext.Visible = false;


                    txtNewPass.Visible = true;
                    txtConfirmPass.Visible = true;
                    btnSave.Visible = true;
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)//5.54
        {
            try
            {
               if(txtNewPass.Text != txtConfirmPass.Text)
                {
                    MessageBox.Show("New password and confirm password did not matched!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    if(MessageBox.Show("change password?","confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dbcn.ExecuteQuery("UPDATE tdUserAcc set password= '" + txtNewPass.Text + "' WHERE username='" + lblUsername.Text + "'");
                        MessageBox.Show("Password has been sucessfully update!", "save Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
