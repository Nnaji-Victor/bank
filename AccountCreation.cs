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

namespace Banking_System
{
    public partial class AccountCreation : Form
    {
        public AccountCreation()
        {
            InitializeComponent();
        }


        public static string conString = "Data Source=DESKTOP-D0IE7T9\\SQLSERVER2017DEV;Initial Catalog=Decabank;Integrated Security=True";

        SqlConnection con = new SqlConnection(conString);

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void AccountCreation_Load(object sender, EventArgs e)
        {
            customerID();
            AccountNumber();
        }

        
        public void AccountNumber()
        {
            con.Open();
            string query = "SELECT max(accountId) from Account";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();

                if (val == "")
                {
                    txtAccntNum.Text = "6060000";
                }
                else
                {
                    string a;
                    a = dr[0].ToString();
                    a = Increment.IncrementId(a);
                    txtAccntNum.Text = a;
                }
                con.Close();
            }


        }
        
        public void customerID()
        {
            con.Open();
            string query = "SELECT max(customerId) from Customer";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();

                if (val == "")
                {
                    txtCustId.Text = "VIC0001";
                }
                else
                {
                    string a;
                    a = dr[0].ToString();
                    a = Increment.IncrementId(a);
                    txtCustId.Text = a;
                }
                con.Close();
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            string customerId = txtCustId.Text;
            string fname = txtFname.Text;
            string lname = txtLname.Text;
            string city = txtCity.Text;
            string phone = txtPhone.Text;
            string datePicker = datepkr.Text;

            string accnumber = txtAccntNum.Text;
            string acctype = TxtAccntType.Text;
            string accdes = txtAccntDes.Text;
            string password = txtPassword.Text;
           //var result = SecurePasswordHasher.Verify("mypassword", hash); 
            double bal = double.Parse(txtBal.Text);

            con.Open();
            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            transaction = con.BeginTransaction();
            command.Connection = con;
            command.Transaction = transaction;

            try
            {
                command.CommandText = "INSERT INTO Customer(customerId,lastName,firstName,city,phone,date) VALUES('"+customerId +"','"+lname+"','"+fname+"','"+city+"','"+phone+"','"+datePicker+"')";  
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Account(accountId,customerId,accountType,description,balance, password) " +
                    "VALUES('"+accnumber+"','"+customerId+"','"+acctype+"','"+accdes+"','"+bal+"', '"+password+"')";
                command.ExecuteNonQuery();

                transaction.Commit();
                MessageBox.Show("Account Created....");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
    }
}
