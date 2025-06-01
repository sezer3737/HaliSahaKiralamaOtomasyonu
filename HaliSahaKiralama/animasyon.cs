using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaliSahaKiralama
{
    public partial class animasyon : Form
    {
        public animasyon()
        {
            InitializeComponent();
        }

        private void animasyon_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Magenta;
            this.TransparencyKey = Color.Magenta;

            pictureBox1.Visible = true;
            pictureBox1.Image = Properties.Resources.giphy_4__unscreen;

            // Timer oluştur ve başlat
            timer1 = new Timer();
            timer1.Interval = 1500; // 3 saniye
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop(); // Timer'ı durdur
            usergiris userForm = new usergiris(); // Açılacak formun ismi "UserForm" olarak varsayıldı
            userForm.Show();
            this.Hide(); // animasyon formunu gizle
        }
    }
}
