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
    public partial class Supplier : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcn = new DBConnect();
        SqlDataReader dr;
        public Supplier()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcn.myConnection());
            LoadSupplier();
        }

        public void LoadSupplier()
        {
            dvgSupplier.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tdSupplier", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dvgSupplier.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SupplierModule supplierModule = new SupplierModule(this);
            supplierModule.ShowDialog();
        }

        //double click on the tabel then show 
        //edit and delete data Table 
        private void dvgSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dvgSupplier.Columns[e.ColumnIndex].Name;
            if(colName == "Edit")
            {
                SupplierModule supplierModule = new SupplierModule(this);
                supplierModule.lblid.Text = dvgSupplier.Rows[e.RowIndex].Cells[1].Value.ToString();
                supplierModule.txtsupplier.Text = dvgSupplier.Rows[e.RowIndex].Cells[2].Value.ToString();
                supplierModule.txtAddress.Text = dvgSupplier.Rows[e.RowIndex].Cells[3].Value.ToString();
                supplierModule.txtPerson.Text = dvgSupplier.Rows[e.RowIndex].Cells[4].Value.ToString();
                supplierModule.txtPhone.Text = dvgSupplier.Rows[e.RowIndex].Cells[5].Value.ToString();
                supplierModule.txtEmail.Text = dvgSupplier.Rows[e.RowIndex].Cells[6].Value.ToString();
                supplierModule.txtFax.Text = dvgSupplier.Rows[e.RowIndex].Cells[7].Value.ToString();

                supplierModule.btnsave.Enabled = false;
                supplierModule.btnupdate.Enabled = true;
                supplierModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if(MessageBox.Show("Delete this record? click yes to confirm","CONFIRM", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("Delete from tdSupplier where id like '" + dvgSupplier.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been sucessfully deleted.","Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
