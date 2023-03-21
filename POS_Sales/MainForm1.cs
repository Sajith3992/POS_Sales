using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POS_Sales
{

    public partial class MainForm1 : Form
    {
        /*Add a connection line */
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();

        public MainForm1()
        {
            InitializeComponent();
            customizeDesign();
            cn = new SqlConnection(dbcn.myConnection());
            cn.Open();
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
             hideSubmenu();
        }
        #region pannelSlide
        private void customizeDesign()
        {
            panelSubProduct.Visible = false;
            panelSubRecord.Visible = false;
            panelSubStock.Visible = false;
            panelSubSetting.Visible = false;
        }

        private void hideSubmenu()
        {
            if (panelSubProduct.Visible == true)
                panelSubProduct.Visible = false;
            if (panelSubRecord.Visible == true)
                panelSubRecord.Visible = false;
            if (panelSubStock.Visible == true)
                panelSubStock.Visible = false;
            if (panelSubSetting.Visible == true)
                panelSubSetting.Visible = false;

        }

        private void showSubmenu(Panel submenu)
        {
            if(submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }
        #endregion pannelSlide

        private void btnDashboard_Click(object sender, EventArgs e)
        {

        }

        private void buttonProduct_Click(object sender, EventArgs e)
        {
            showSubmenu(panelSubProduct);
        }

        private void buttonProductList_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void buttonCatagory_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void buttonBrand_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void buttonInStock_Click(object sender, EventArgs e)
        {
            showSubmenu(panelSubStock);
        }

        private void buttonStockAdjectment_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void buttonSupplier_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void buttonRecord_Click(object sender, EventArgs e)
        {
            showSubmenu(panelSubRecord);
        }

        private void buttonSaleHistory_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void buttonPosRecord_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void buttonSetting_Click(object sender, EventArgs e)
        {
            showSubmenu(panelSubSetting);
        }

        private void buttonUser_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void buttonStore_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }
    }
}
