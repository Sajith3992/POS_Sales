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
    public partial class Catagory : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;

        public Catagory()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            LoadCategory();
        }

        //Data retrive from tdCategory to dvgBrand on Catagory form
        public void LoadCategory()
        {
            int i = 0;
            dvgCatagory.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tdCatagory ORDER BY category", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dvgCatagory.Rows.Add(i, dr["id"].ToString(), dr["category"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CategoryModule module = new CategoryModule(this);
            module.ShowDialog();
        }

        private void dvgCatagory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //for update and delete brand by cell click from tdBrand
            String colName = dvgCatagory.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tdCatagory WHERE id LIKE'" + dvgCatagory[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Category has been successfully deleted,", "Point Of Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (colName == "Edit")
            {
                CategoryModule module = new CategoryModule(this);
                module.lblid.Text = dvgCatagory[1, e.RowIndex].Value.ToString();
                module.txtCategory.Text = dvgCatagory[2, e.RowIndex].Value.ToString();
                module.btnsave.Enabled = false;
                module.btnupdate.Enabled = true;
                module.ShowDialog();
            }
            LoadCategory();
        }
    }
}
