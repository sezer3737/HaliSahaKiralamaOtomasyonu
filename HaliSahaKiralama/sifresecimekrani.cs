using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace HaliSahaKiralama
{
    public partial class sifresecimekrani : Form
    {
        public sifresecimekrani()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void sifresecimekrani_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sifremiunuttum frm = new sifremiunuttum();
            frm.ShowDialog();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            sifremiunuttumadmin frm = new sifremiunuttumadmin();
            frm.ShowDialog();
            this.Close();
        }
    }
}
