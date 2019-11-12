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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        Form openForm = null;

        private void Admin_Load(object sender, EventArgs e)
        {

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
