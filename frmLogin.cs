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
    public partial class frmLogin : Form
    {

        public static string username;
        public frmLogin()
        {
            InitializeComponent();
            
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public static string getLoggedInUser()
        {
            return username;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            username = txtUsername.Text;
            string password = txtPassword.Text;

            var a = AccountCreation.conString;
            SqlConnection con = new SqlConnection(a);
           

            if (username.StartsWith("ADM",StringComparison.OrdinalIgnoreCase))
            {
                con.Open();
                string query = "Select * from [Admin] where username='" + username + "' and password='" + password + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dtb = new DataTable();

                sda.Fill(dtb);
                if (dtb.Rows.Count == 1)
                {
                    txtLoadStatus.ForeColor = Color.Green;
                    txtLoadStatus.Visible = true;
                    txtLoadStatus.Text = "Login succesful!....";
                    prgStartAdmin sba = new prgStartAdmin();
                    this.Hide();
                    sba.Show();
                }

                else
                {
                    txtLoadStatus.Text = "Invalid Username or Password";
                }
                con.Close();
            }
            else if(username.StartsWith("VIC",StringComparison.OrdinalIgnoreCase))
            {
                con.Open();
                string query = "Select * from [Account] where customerId='" + username + "'";

                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dtb = new DataTable();

                sda.Fill(dtb);
                if (dtb.Rows.Count == 1)
                {
                    string savedPasswordHash = dtb.Rows[0][6].ToString();
                    bool verify = SecurePasswordHasher.verify(savedPasswordHash, password);

                    if (verify)
                    {
                        getLoggedInUser();
                        txtLoadStatus.Text = "Login succesful!....";
                        prgStart sb = new prgStart();
                        this.Hide();
                        sb.Show();
                    }
                    else
                    {
                        txtLoadStatus.Text = "Invalid Username or Password";
                    }
                }


                else
                {
                    txtLoadStatus.Text = "Invalid Username or Password";
                }

                con.Close();
            }
            else if ( username == "" && password == "")
            {
                txtLoadStatus.Text = "Type in your username and password";
                txtLoadStatus.ForeColor = Color.Red;
                txtLoadStatus.Visible = true;
            }
            else
            {
                txtLoadStatus.Text = "Incorrect username and or password";
                txtLoadStatus.ForeColor = Color.Red;
                txtLoadStatus.Visible = true;
                
            }

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            txtLoadStatus.Text = "";
        }
    }
}
