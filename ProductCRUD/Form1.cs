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
using System.Configuration;
using System.Xml.Linq;


namespace ProductCRUD
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(constr);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public DataSet GetAllEmp()
        {
            da = new SqlDataAdapter("select * from Product", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb=new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Product");// Product is a table name given to DataTable
            return ds;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmp();
                DataRow row = ds.Tables["Product"].NewRow();
                row["Pname"] = txtProductName.Text;
                row["PPrice"] = txtProductPrice.Text;
                row["CompanyName"] = txtCompanyName.Text;
                ds.Tables["Product"].Rows.Add(row);
                int result = da.Update(ds.Tables["Product"]);
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmp();
                DataRow row = ds.Tables["Product"].Rows.Find(txtProductId.Text);
                if (row != null)
                {
                    row["Pname"] = txtProductName.Text;
                    row["Pprice"] = txtProductPrice.Text;
                    row["CompanyName"] = txtCompanyName.Text;
                    int result = da.Update(ds.Tables["Product"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record updated");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmp();
                DataRow row = ds.Tables["Product"].Rows.Find(txtProductId.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["Product"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record deleted");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmp();
                DataRow row = ds.Tables["Product"].Rows.Find(txtProductId.Text);
                if (row != null)
                {
                    txtProductName.Text = row["Pname"].ToString();
                    txtProductPrice.Text = row["Pprice"].ToString();
                    txtCompanyName.Text = row["CompanyName"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmp();
              dataGridView1.DataSource = ds.Tables["Product"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
