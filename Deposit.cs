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
    public partial class Deposit : Form
    {
        public Deposit()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(AccountCreation.conString);


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public static string balance;
        private void Deposit_Load(object sender, EventArgs e)
        {
            con.Open();
            string query = "select accountId,balance from [Account] where customerId=@user";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user", frmLogin.getLoggedInUser());
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txtCustId.Text = dr[0].ToString();
                balance = dr[1].ToString();
                txtTotalBalance.Text = "₦"+balance;
            }

            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            string accNum, date;
            double bal, deposit;

            accNum = txtCustId.Text;
            date = txtdate.Text;
            bal = double.Parse(balance);

            try
            {
              deposit = double.Parse(txtDeposit.Text);

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
                command.Parameters.AddWithValue("@deposit", deposit);
                

                command.CommandText = "INSERT INTO Transactions(AccountId,date,balance,deposit) VALUES(@accNum,@date,@bal,@deposit)";
                command.ExecuteNonQuery();

                command.CommandText = "update [Account] set balance = balance +@deposit where accountId=@accNum";
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
                txtDeposit.Clear();

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
            catch (Exception)
            {

                MessageBox.Show("Enter an amount!");
            }

        }
    }
}
