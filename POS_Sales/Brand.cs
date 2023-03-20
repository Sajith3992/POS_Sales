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
    public partial class Brand : Form
    {
        /*Add a connection line */
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;
        public Brand()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            LoadBrand();
        }

        internal void LoadBrand()
        {
            int i = 0;
            dvgBrand.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tdBrand ORDER BY brand", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dvgBrand.Rows.Add(i, dr["id"].ToString(), dr["brand"].ToString());
            }
            dr.Close();
            cn.Close();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            BrandModule moduleForm = new BrandModule();
            moduleForm.ShowDialog();
        }
    }
}
