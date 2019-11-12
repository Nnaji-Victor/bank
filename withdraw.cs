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
    public partial class withdraw : Form
    {
        public withdraw()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(AccountCreation.conString);
        public static string balance;

        private void withdraw_Load(object sender, EventArgs e)
        {
            
            con.Open();
            string query = "select accountId,balance,accountType from Account where customerId=@loggedin";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlParameter val;
            val = new SqlParameter();
            val.SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@loggedin", frmLogin.getLoggedInUser());
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txtCustId.Text = dr[0].ToString();
                balance = dr[1].ToString();
                txtTotalBalance.Text = "₦" + balance;
                txtAccntType.Text = "Big boys "+ dr[2].ToString() + " Account";
            }

            con.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            string accNum, date;
            double bal, withdraw;

            accNum = txtCustId.Text;
            date = txtdate.Text;
            bal = double.Parse(balance);
            withdraw = double.Parse(txtWithdraw.Text);

            con.Open();
            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            transaction = con.BeginTransaction();
            command.Connection = con;
            command.Transaction = transaction;

            try
            {
                command.Parameters.AddWithValue("@accNum", accNum);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@bal", bal);
                command.Parameters.AddWithValue("@withdraw", withdraw);

                command.CommandText = "INSERT INTO Transactions(AccountId,date,balance,withdraw) VALUES(@accNum,@date,@bal,@withdraw)";
                command.ExecuteNonQuery();

                command.CommandText = "update [Account] set balance = balance - @deposit where accountId=@accNum";
                command.ExecuteNonQuery();

                transaction.Commit();
                MessageBox.Show("Transaction Successful!!!");
                string query = "select accountId,balance from [Account] where customerId=@user";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", frmLogin.getLoggedInUser());
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtCustId.Text = dr[0].ToString();
                    balance = dr[1].ToString();
                    txtTotalBalance.Text = "₦" + balance;
                }
                txtWithdraw.Clear();

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
