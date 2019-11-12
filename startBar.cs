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
    public partial class prgStart : Form
    {
        public prgStart()
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
            prgBar.Value = prgBar.Value + 1;

            if(prgBar.Value >= 99)
            {
                Customer c = new Customer();
                this.Hide();
                c.Show();

                timer1.Enabled = false;
                prgBar.Value = prgBar.Value - 1;
            }
        }
    }
}
