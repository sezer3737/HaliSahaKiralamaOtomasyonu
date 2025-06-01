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
    public partial class sahalar : UserControl
    {
        public sahalar()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmsahaduzenlemeekrani form = new frmsahaduzenlemeekrani();
            form.gelenkod = lblsahanumara.Text;
            form.ShowDialog();
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            this.BackColor = Color.FromArgb(64,64,64);
            lblsahaadi.BackColor=Color.FromArgb(64, 64, 64);
            lblsahaadi.ForeColor = Color.White;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.CornflowerBlue;
            lblsahaadi.BackColor = Color.CornflowerBlue;
            lblsahaadi.ForeColor = Color.Black;
        }
    }
}
