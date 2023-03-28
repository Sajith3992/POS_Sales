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
    public partial class Cashier : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;

        int qty;
        string id;
        string price;


        string stitle = "Point Of Sales";
        public Cashier()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            GetTransNo();
        }

        private void picclose_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Exit Application?","Confirm" , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        public void slide(Button button)
        {
            panelSlide.BackColor = Color.White;
            panelSlide.Height = button.Height;
            panelSlide.Top = button.Top;
        }
        #region button
        private void btnNTran_Click(object sender, EventArgs e)
        {

            slide(btnNTran);
            GetTransNo();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            slide(btnSearch);
            LookUpProduct lookup = new LookUpProduct(this);
            lookup.LoadProduct();
            lookup.ShowDialog();
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            slide(btnDiscount);
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            slide(btnSettle);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            slide(btnClear);
        }

        private void btnDsale_Click(object sender, EventArgs e)
        {
            slide(btnDsale);
        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            slide(btnPass);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            slide(btnLogout);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion button

        // Table Load Produc Here 
        public void LoadCart()
        {
            try
            {
                int i = 0;
                double total = 0;
                double discount = 0;
                dvgCash.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("SELECT c.id, c.pcode, p.pdesc, c.price, c.qty, c.total FROM tbCart AS c INNER JOIN tdProduct AS p ON c.pcode=p.pcode WHERE c.transno LIKE @transno and c.status LIKE 'Pending'", cn);
                cm.Parameters.AddWithValue("@transno", lblTransNo.Text);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    total += Convert.ToDouble(dr["total"].ToString());
                    discount += Convert.ToDouble(dr["disc"].ToString());
                    dvgCash.Rows.Add(i, dr["id"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), double.Parse(dr["total"].ToString()).ToString("#,##0.00"));//
                }
                dr.Close();
                cn.Close();
                lblSaleTotal.Text = total.ToString("#,##0.00");
                lblDiscount.Text = discount.ToString("#,##0.00");
                GetCartTotal();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,stitle);
            }

          
        }

        //Vat add here 
        public void GetCartTotal()
        {
            double discount = double.Parse(lblDiscount.Text);
            double sales = double.Parse(lblSaleTotal.Text) - discount;
            double vat = sales * 0.12; //VAT: 12% of vat payble (OutPut tax less input text)
            double vatable = sales - vat;

            lblTax.Text = vat.ToString("#,##0.00");
            lblVat.Text = vatable.ToString("#,##0.00");
            lblDisplayTotal.Text = sales.ToString("#,##0.00");

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        //Auto Genarate Transaction Number 
        public void GetTransNo()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                int count;
                string transno = sdate + "1001";
                lblTransNo.Text = transno;
                cn.Open();
                cm = new SqlCommand("SELECT TOP 1 transno FROM tbCart WHERE transno LIKE '" + sdate + "%'ORDER BY id desc", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    transno = dr[0].ToString();
                    count = int.Parse(transno.Substring(8, 4));
                    lblTransNo.Text = sdate + (count + 1);
                }
                else
                {
                    transno = sdate + "1001";
                    lblTransNo.Text = transno;
                }
                dr.Close();
                cn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, stitle);
            }
           
        }

        private void dvgCash_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBarcode.Text == string.Empty) return;
                else
                {
                    string _pcode;
                    double _price;
                    int _qty;
                    cn.Open();
                    cm = new SqlCommand("SELECT * FROM tdProduct WHERE barcode LIKE '" + txtBarcode.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        qty = int.Parse(dr["qty"].ToString());
                        _pcode = dr["pcode"].ToString();
                        _price = double.Parse(dr["price"].ToString());
                        _qty = int.Parse(txtQty.Text);
                      
                        dr.Close();
                        cn.Close();
                        //insert to tdCart
                        AddToCart(_pcode, _price, _qty);
                    }
                    dr.Close();
                    cn.Close();
                }
            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        public void AddToCart(string _pcode, double _price, int _qty)
        {
            try
            {
                string id = "";
                int cart_qty = 0;
                bool found = false;
                cn.Open();
                cm = new SqlCommand("Select * from tbCart Where transno = @transno and pcode =@pcode", cn);
                cm.Parameters.AddWithValue("@transno", lblTransNo.Text);
                cm.Parameters.AddWithValue("@pcode", _pcode);
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
                    if(qty <(int.Parse(txtQty.Text)+cart_qty))
                    {
                        MessageBox.Show("Unable to procced. Remaining qty on hand is" + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cn.Open();
                    cm = new SqlCommand("Update tbCart set qty=(qty+ " + _qty + ")Where id='" + id + "'", cn);
                    cm.ExecuteReader();
                    cn.Close();
                    txtBarcode.SelectionStart = 0;
                    txtBarcode.SelectionLength = txtBarcode.Text.Length;
                    LoadCart();
                   
                }
                else {
                    if (qty < (int.Parse(txtQty.Text) + cart_qty))
                    {
                        MessageBox.Show("Unable to procced. Remaining qty on hand is" + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    cn.Open();
                cm = new SqlCommand("INSERT INTO tbCart(transno, pcode, price, qty, sdate, cashier)VALUES(@transno, @pcode, @price, @qty, @sdate, @cashier)", cn);
                cm.Parameters.AddWithValue("@transno", lblTransNo.Text);
                cm.Parameters.AddWithValue("@pcode", _pcode);
                cm.Parameters.AddWithValue("@price", _price);
                cm.Parameters.AddWithValue("@qty", _qty);
                cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                cm.Parameters.AddWithValue("@cashier", lblUsername.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                LoadCart();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, stitle);
            }
        }
    }
}
