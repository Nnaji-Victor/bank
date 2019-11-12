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

namespace Banking_System
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        Form openForm = null;
        SqlConnection con = new SqlConnection(AccountCreation.conString);

        private void Admin_Load(object sender, EventArgs e)
        {
            con.Open();

            try
            {
                string query = "select * from [Customer]";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dtb = new DataTable();
                sda.Fill(dtb);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dtb;
                dataGridView1.DataSource = bsource;
                sda.Update(dtb);

                string q = "select * from [Account]";
                SqlCommand cmd2 = new SqlCommand(q, con);
                SqlDataAdapter sda2 = new SqlDataAdapter();
                sda2.SelectCommand = cmd2;
                DataTable dtb2 = new DataTable();
                sda2.Fill(dtb2);
                BindingSource bsource2 = new BindingSource();

                bsource2.DataSource = dtb2;
                dataGridView2.DataSource = bsource2;
                sda2.Update(dtb2);

                string q2 = "select sum(balance) from [Account]";
                SqlCommand cmd3 = new SqlCommand(q2, con);
                SqlDataReader dr3;
                dr3 = cmd3.ExecuteReader();

                if (dr3.Read())
                {
                    txtTotalCash.Text = "₦" + dr3[0].ToString();
                }

                con.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("something is wrong");
            }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (openForm != null)
            {
                openForm.Hide();
            }
            AccountCreation acc = new AccountCreation();
            openForm = acc;
            acc.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            frmLogin fr = new frmLogin();
            this.Hide();
            fr.Show();
        }
    }
}
