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
    public partial class Frmsecimekrani : Form
    {
        public Frmsecimekrani()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Frmsahakayitekrani frm = new Frmsahakayitekrani();
            frm.ShowDialog();
            
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Frmsahasec frm = new Frmsahasec();
            frm.ShowDialog();
            
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            button2.Image = Properties.Resources.m11;
            button2.ForeColor = Color.MediumPurple;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {

            button2.Image = Properties.Resources.m1;
            button2.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Frmsecimekrani_Load(object sender, EventArgs e)
        {

        }
    }
}
