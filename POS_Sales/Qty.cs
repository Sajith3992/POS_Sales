﻿using System;
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
    public partial class Qty : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;
        string stitle = "Point Of Sales";


        private string pcode;
        private double price;
        private string transno;
        private int qty;
        Cashier cashier;

        public Qty(Cashier cash)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            cashier = cash;
        }

        public void ProductDetails(string pcode, double price, string transno, int qty)
        {
            this.pcode = pcode;
            this.price = price;
            this.transno = transno;
            this.qty = qty;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar == 13) && (txtQty.Text != string.Empty))
            {
                try
                {
                    string id = "";
                    int cart_qty = 0;
                    bool found = false;
                    cn.Open();
                    cm = new SqlCommand("Select * from tbCart Where transno = @transno and pcode =@pcode", cn);
                    cm.Parameters.AddWithValue("@transno", transno);
                    cm.Parameters.AddWithValue("@pcode", pcode);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        id = dr["id"].ToString();
                        cart_qty = int.Parse(dr["qty"].ToString());
                        found = true;
                    }
                    else found = false;

                    dr.Close();
                    cn.Close();

                    if (found)
                    {
                        if (qty < (int.Parse(txtQty.Text) + cart_qty))
                        {
                            MessageBox.Show("Unable to procced. Remaining qty on hand is" + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cn.Open();
                        cm = new SqlCommand("Update tbCart set qty=(qty+ " + int.Parse(txtQty.Text) + ")Where id='" + id + "'", cn);
                        cm.ExecuteReader();
                        cn.Close();
                        cashier.txtBarcode.Clear();
                        cashier.txtBarcode.Focus();
                        cashier.LoadCart();
                        this.Dispose();
                    }
                    else
                    {
                        if (qty < (int.Parse(txtQty.Text) + cart_qty))
                        {
                            MessageBox.Show("Unable to procced. Remaining qty on hand is" + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tbCart(transno, pcode, price, qty, sdate, cashier)VALUES(@transno, @pcode, @price, @qty, @sdate, @cashier)", cn);
                        cm.Parameters.AddWithValue("@transno", transno);
                        cm.Parameters.AddWithValue("@pcode", pcode);
                        cm.Parameters.AddWithValue("@price", price);
                        cm.Parameters.AddWithValue("@qty", qty);
                        cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                        cm.Parameters.AddWithValue("@cashier", cashier.lblUsername.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        cashier.txtBarcode.Clear();
                        cashier.txtBarcode.Focus();
                        cashier.LoadCart();
                        this.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, stitle);
                }
            }
        }
        }
    }
}