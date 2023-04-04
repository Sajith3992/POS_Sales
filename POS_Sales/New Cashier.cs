using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Sales
{
    public partial class New_Cashier : Form
    {
        public New_Cashier()
        {
            InitializeComponent();
        }

        //close button
        private void picclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //panelSlide icon Display
        public void slide(Button button)
        {
            panelSlide.BackColor = Color.White;
            panelSlide.Height = button.Height;
            panelSlide.Top = button.Top;
        }

        private void btnTranaction_Click(object sender, EventArgs e)
        {
            slide(btnTranaction);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            slide(btnSearch);
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            slide(btnDiscount);
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            slide(btnPayment);
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            slide(btnClearCart);
        }

        private void btnDailySales_Click(object sender, EventArgs e)
        {
            slide(btnDailySales);
        }

        private void btnChangePasswd_Click(object sender, EventArgs e)
        {
            slide(btnChangePasswd);
        }
    }
}
