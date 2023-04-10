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
    public partial class Void: Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;
        CancelOrder cancelOrder;

        public Void(CancelOrder cancel)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            txtUsername.Focus();
            cancelOrder = cancel;
        }

        private void btnVoid_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text == cancelOrder.txtCanceledBy.Text)
                {
                    MessageBox.Show("Void by name and canceled by name are same!. Please void by another person.", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string user;
                cn.Open();
                cm = new SqlCommand("Select * from tdUserAcc Where username = @username and password = @password", cn);
                cm.Parameters.AddWithValue("@username", txtUsername.Text);
                cm.Parameters.AddWithValue("@password", txtPassword.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    user = dr["username"].ToString();
                    dr.Close();
                    cn.Close();
                    SaveCancelOrder(user);

                    if(cancelOrder.cboInventory.Text=="yes")//5.37.34
                    {
                        dbcn.ExecuteQuery("UPDATE tdProduct SET qty = qty + " + cancelOrder.udCancelQty.Value + " where pcode = '" + cancelOrder.txtPcode.Text + "'");//5.37.51
                    }
                    dbcn.ExecuteQuery("UPDATE tbCart SET qty = qty + " + cancelOrder.udCancelQty.Value + " where id LIKE '" + cancelOrder.txtid.Text + "'");
                    MessageBox.Show("Order transaction successfully cancelled !", "Cancel Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                    cancelOrder.ReloadSold(); //5.42.31
                    cancelOrder.Dispose();
                }
                dr.Close();
                cn.Close();
            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SaveCancelOrder(string user)//5.37
        {
            try
            {
               
                cn.Open();
                cm = new SqlCommand("insert into tdCancel (transno, pcode, price, qty, total, sdate, voidby, cancelledby, reason, action) values(@transno, @pcode, @price, @qty, @total, @sdate, @voidby, @cancelledby, @reason, @action)", cn);
                cm.Parameters.AddWithValue("@transno", cancelOrder.txtTransno.Text);
                cm.Parameters.AddWithValue("@pcode", cancelOrder.txtPcode.Text);
                cm.Parameters.AddWithValue("@price",double.Parse(cancelOrder.txtPrice.Text));
                cm.Parameters.AddWithValue("@qty", int.Parse(cancelOrder.txtQty.Text));
                cm.Parameters.AddWithValue("@total", double.Parse(cancelOrder.txtTotal.Text));
                cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                cm.Parameters.AddWithValue("@voidby", user);
                cm.Parameters.AddWithValue("@cancelledby", cancelOrder.txtCanceledBy.Text);
                cm.Parameters.AddWithValue("@reason", cancelOrder.cboInventory.Text);
                cm.Parameters.AddWithValue("@action", cancelOrder.cboInventory.Text);
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void picclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
