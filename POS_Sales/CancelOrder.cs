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
    //5.39.08
    public partial class CancelOrder : Form
    {
        DailySale dailySale;

        public CancelOrder(DailySale sale)
        {
            InitializeComponent();
            dailySale = sale;
        }

        private void picclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)//5.39
        {
            try
            {
                if(cboInventory.Text != string.Empty && udCancelQty.Value >0 && txtReason.Text != string.Empty)
                {
                    int qty;
                    if(int.TryParse(txtQty.Text, out qty) && qty >= udCancelQty.Value)
                    {
                        Void @void = new Void(this);
                        @void.txtUsername.Focus();
                        @void.ShowDialog();

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        public void ReloadSold() //5.41.54
        {
            dailySale.LoadSold();
        }

        private void cboInventory_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
