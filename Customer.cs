using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banking_System
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }

        Form openForm = null;
        private void Customer_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openForm != null)
            {
                openForm.Hide();
            }
            withdraw wd = new withdraw();
            openForm = wd;
            wd.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            frmLogin f = new frmLogin();
            this.Hide();
            f.Show();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (openForm != null)
            {
                openForm.Hide();
            }
            Deposit dp = new Deposit();
            openForm = dp;
            dp.Show();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (openForm != null)
            {
                openForm.Hide();
            }
            Transfer dp = new Transfer();
            openForm = dp;
            dp.Show();
        }
    }
}
