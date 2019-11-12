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
    public partial class prgStartAdmin : Form
    {
        public prgStartAdmin()
        {
            InitializeComponent();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            
        }

        private void startBar_Load(object sender, EventArgs e)
        {
            
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            prgBarAdmin.Value = prgBarAdmin.Value + 1;

            if (prgBarAdmin.Value >= 99)
            {
                Admin a = new Admin();
                this.Hide();
                a.Show();

                timer1.Enabled = false;
                prgBarAdmin.Value -= 1;
            }
        }
    }
}
