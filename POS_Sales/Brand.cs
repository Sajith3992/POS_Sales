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

        //Data retrive from tdBrand to dvgBrand on Brand form
        public void LoadBrand()
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
            BrandModule moduleForm = new BrandModule(this);
            moduleForm.ShowDialog();
        }

        private void dvgBrand_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //for update and delete brand by cell click from tdBrand
            String colName = dvgBrand.Columns[e.ColumnIndex].Name;
            if(colName == "Delete")
            {
                if(MessageBox.Show("Are you want to delete this record?","Delete Record",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tdBrand WHERE id LIKE'" + dvgBrand[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Brand has been successfully deleted,", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if(colName == "Edit")
            {
                BrandModule brandModule = new BrandModule(this);
                brandModule.lblid.Text = dvgBrand[1, e.RowIndex].Value.ToString();
                brandModule.txtBrand.Text = dvgBrand[2, e.RowIndex].Value.ToString();
                brandModule.btnsave.Enabled = false;
                brandModule.btnupdate.Enabled = true;
                brandModule.ShowDialog();
            }
            LoadBrand();
        }
    }
}
