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
    public partial class Transfer : Form
    {
        public Transfer()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(AccountCreation.conString);
        public static string balance;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Transfer_Load(object sender, EventArgs e)
        {
            con.Open();
            string query = "select accountId,balance from [Account] where customerId=@user";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user", frmLogin.getLoggedInUser());
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txtfrmAccount.Text = dr[0].ToString();
                balance = dr[1].ToString();
                txtTotalBalance.Text = "₦" + balance;
            }

            con.Close();
        }

        private void txtToAccount_TextChanged(object sender, EventArgs e)
        {
            
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txttransferPrompt.Text = "";
        }

        private void txtToAccount_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txtToAccount_KeyUp(object sender, KeyEventArgs e)
        {
            con.Open();
            string query = "select customerId from [Account] where accountId=@user";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user", txtToAccount.Text);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if(txtToAccount.Text == txtfrmAccount.Text)
                {
                    txttransferPrompt.ForeColor = Color.Red;
                    txttransferPrompt.Visible = true;
                    txttransferPrompt.Text = "Cannot Transfer to your account, use deposit instead!";
                    btnconfirm.Enabled = false;
                    btnconfirm.BackColor = Color.White;
                }
                else
                {
                    string q = "select lastName, firstName from [Customer] where customerId='" + dr[0].ToString() + "'";
                    SqlCommand cmd2 = new SqlCommand(q, con);
                    dr.Close();
                    SqlDataReader dr2;
                    dr2 = cmd2.ExecuteReader();
                    if (dr2.Read())
                    {
                        txttransferPrompt.Text = $"Transfering ₦{txtAmount.Text} to {dr2[0].ToString()} {dr2[1].ToString()}";
                        txttransferPrompt.Visible = true;
                        txttransferPrompt.ForeColor = Color.Black;
                        btnconfirm.Enabled = true;
                        btnconfirm.BackColor = Color.Chartreuse;


                    }
                }
            }
            else
            {
                txttransferPrompt.ForeColor = Color.Red;
                txttransferPrompt.Visible = true;
                txttransferPrompt.Text = "User not found!";
                txttransferPrompt.TextAlign = ContentAlignment.MiddleRight;
                btnconfirm.Enabled = false;
                btnconfirm.BackColor = Color.White;
            }
            con.Close();
        }

        private void btnconfirm_Click(object sender, EventArgs e)
        {
            string frmAccnt, toAccnt, date;
            double amount;

            frmAccnt = txtfrmAccount.Text;
            toAccnt = txtToAccount.Text;
            date = dateTimePicker1.Text;

            try
            {
                amount = double.Parse(txtAmount.Text);

                con.Open();
                SqlCommand command = con.CreateCommand();
                SqlTransaction transaction;

                transaction = con.BeginTransaction();
                command.Connection = con;
                command.Transaction = transaction;

                try
                {
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@frmAccnt", frmAccnt);
                    command.Parameters.AddWithValue("@toAccnt", toAccnt);
                    command.Parameters.AddWithValue("@date", date);

                    command.CommandText = "insert into Transfer(frmAccount,toAccount,amount,date) values(@frmAccnt,@toAccnt,@amount,@date)";
                    command.ExecuteNonQuery();

                    command.CommandText = "update [Account] set balance = balance +@amount where accountId=@toAccnt";
                    command.ExecuteNonQuery();

                    command.CommandText = "update [Account] set balance = balance -@amount where accountId=@frmAccnt";
                    command.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Transaction Successful!!!");

                    string query = "select balance from [Account] where customerId=@user";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user", frmLogin.getLoggedInUser());
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        balance = dr[0].ToString();
                        txtTotalBalance.Text = "₦" + balance;
                    }

                    con.Close();

                }
                catch (Exception)
                {

                    transaction.Rollback();
                    MessageBox.Show("transacrion failed");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Enter an amount!");
            }
        }
    }
}
