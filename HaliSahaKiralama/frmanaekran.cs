using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace HaliSahaKiralama
{
    public partial class frmanaekran : Form
    {
        public frmanaekran()
        {
            InitializeComponent();
            this.AcceptButton = null;
        }
            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");
            public bool AdminMi { get; set; }
    

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmanaekran_Load(object sender, EventArgs e)
        { // Başlangıçta gif görünsün


          


            pictureBox1.Image = Properties.Resources.vidooo; // GIF dosyan buraya
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.Visible = true;
           
           
        
            panel3.Visible = false;
            if (AdminMi)
            {
                // Admin girişi yapıldıysa kullanıcı yönetim paneli gösterilsin
                panelKullaniciYonetim.Visible = true;
            }
            else
            {
                // Normal kullanıcıysa gizli kalsın
                panelKullaniciYonetim.Visible = false;
            }
           


        }
       

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.Image = Properties.Resources.m11;
            button1.ForeColor = Color.MediumPurple;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.m1;
            button1.ForeColor = Color.FromArgb(64,64,64);
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            button2.Image = Properties.Resources.m22;
            button2.ForeColor = Color.MediumPurple;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.m2;
            button2.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void button3_MouseMove(object sender, MouseEventArgs e)
        {

            button3.Image = Properties.Resources.m33;
            button3.ForeColor = Color.MediumPurple;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.m3;
            button3.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            temizlik();
            Frmsecimekrani frm = new Frmsecimekrani();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            temizlik();
          frmsahalistesics frm = new frmsahalistesics();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            temizlik();
            frmkarsilasma frm = new frmkarsilasma();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            temizlik();
            KullaniciYonetimForm form = new KullaniciYonetimForm();
            form.ShowDialog();

        }

        private void button5_MouseMove(object sender, MouseEventArgs e)
        {
            button5.Image = Properties.Resources.user__9_;
            button5.ForeColor = Color.MediumPurple;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.ForeColor = Color.FromArgb(64, 64, 64);
            button5.Image = Properties.Resources.user__8_;
        }

        private void button6_MouseMove(object sender, MouseEventArgs e)
        {
            button6.Image = Properties.Resources.user__6_;
            button6.ForeColor = Color.MediumPurple;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.user__7_;
            button6.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void pnrustmenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            temizlik();
            adminduzen frm = new adminduzen();
          frm.ShowDialog();
        }
        private void sahamethod()
        {// GIF'i gizle
         
            pictureBox1.SendToBack(); // GIF'i arka plana at
    
            pictureBox1.Visible = false;
     
            panel3.Visible =true;

            label2.Text = DateTime.Today.ToShortDateString() + "\n" + DateTime.Today.ToString("dddd");
            label3.Text = DateTime.Today.AddDays(1).ToShortDateString() + "\n" + DateTime.Today.AddDays(1).ToString("dddd");
            label4.Text = DateTime.Today.AddDays(2).ToShortDateString() + "\n" + DateTime.Today.AddDays(2).ToString("dddd");
            label5.Text = DateTime.Today.AddDays(3).ToShortDateString() + "\n" + DateTime.Today.AddDays(3).ToString("dddd");
            label6.Text = DateTime.Today.AddDays(4).ToShortDateString() + "\n" + DateTime.Today.AddDays(4).ToString("dddd");
            label7.Text = DateTime.Today.AddDays(5).ToShortDateString() + "\n" + DateTime.Today.AddDays(5).ToString("dddd");
            label8.Text = DateTime.Today.AddDays(6).ToShortDateString() + "\n" + DateTime.Today.AddDays(6).ToString("dddd");

            birincipanel.Controls.Clear();
            for (int i = 10; i < 24; i++)
            {
                Sahakontrol sablon = new Sahakontrol();
                sablon.label1.Text = i + ":00-" + (i + 1) + ":00";
                sablon.lbltarih.Text = DateTime.Today.ToShortDateString();
                sablon.gelensahakodu = sahakodu;
                birincipanel.Controls.Add(sablon);
            }

            ikincipanel.Controls.Clear();
            for (int i = 10; i < 24; i++)
            {
                Sahakontrol sablon = new Sahakontrol();
                sablon.label1.Text = i + ":00-" + (i + 1) + ":00";
                sablon.lbltarih.Text = DateTime.Today.AddDays(1).ToShortDateString();
                sablon.gelensahakodu = sahakodu;
                ikincipanel.Controls.Add(sablon);
            }

            ucuncupanel.Controls.Clear();
            for (int i = 10; i < 24; i++)
            {
                Sahakontrol sablon = new Sahakontrol();
                sablon.label1.Text = i + ":00-" + (i + 1) + ":00";
                sablon.lbltarih.Text = DateTime.Today.AddDays(2).ToShortDateString();
                sablon.gelensahakodu = sahakodu;
                ucuncupanel.Controls.Add(sablon);
            }

            dorduncupanel.Controls.Clear();
            for (int i = 10; i < 24; i++)
            {
                Sahakontrol sablon = new Sahakontrol();
                sablon.label1.Text = i + ":00-" + (i + 1) + ":00";
                sablon.lbltarih.Text = DateTime.Today.AddDays(3).ToShortDateString();
                sablon.gelensahakodu = sahakodu;
                dorduncupanel.Controls.Add(sablon);
            }

            besincipanel.Controls.Clear();
            for (int i = 10; i < 24; i++)
            {
                Sahakontrol sablon = new Sahakontrol();
                sablon.label1.Text = i + ":00-" + (i + 1) + ":00";
                sablon.lbltarih.Text = DateTime.Today.AddDays(4).ToShortDateString();
                sablon.gelensahakodu = sahakodu;
                besincipanel.Controls.Add(sablon);
            }

            altincipanel.Controls.Clear();
            for (int i = 10; i < 24; i++)
            {
                Sahakontrol sablon = new Sahakontrol();
                sablon.label1.Text = i + ":00-" + (i + 1) + ":00";
                sablon.lbltarih.Text = DateTime.Today.AddDays(5).ToShortDateString();
                sablon.gelensahakodu = sahakodu;
                altincipanel.Controls.Add(sablon);
            }

            yedincipanel.Controls.Clear();
            for (int i = 10; i < 24; i++)
            {
                Sahakontrol sablon = new Sahakontrol();
                sablon.label1.Text = i + ":00-" + (i + 1) + ":00";
                sablon.lbltarih.Text = DateTime.Today.AddDays(6).ToShortDateString();
                sablon.gelensahakodu = sahakodu;
                yedincipanel.Controls.Add(sablon);
            }
        }
        string sahakodu = "";
        private void button7_Click(object sender, EventArgs e)
        {
            sahamethod();
        }
        private void temizlik()
        {
            pictureBox1.Visible = true;
            
            panel4.Visible = false; 
            panel3.Visible = false;
            birincipanel.Controls.Clear();
            ikincipanel.Controls.Clear();
            ucuncupanel.Controls.Clear();
            dorduncupanel.Controls.Clear();
            besincipanel.Controls.Clear();
            altincipanel.Controls.Clear();
            yedincipanel.Controls.Clear();
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";



        }
        private void sahagetir()
        {
            panel2.Controls.Clear();

            try
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close(); // Eğer bağlantı açık ise kapat
                }

                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM sahatablom ORDER BY ad ASC", baglanti);
                SqlDataReader oku = komut.ExecuteReader();

                int top = 10; // Butonların başlangıç konumu (Y ekseni)

                while (oku.Read())
                {
                    Button btn = new Button();
                    btn.Text = oku["ad"].ToString();
                    btn.Tag = oku["kod"].ToString();
                    btn.Width = 245;
                    btn.Height = 60;
                    btn.Top = top; // Butonun Y konumu
                    btn.Left = 0; // Butonun X konumu
                    btn.ForeColor = Color.FromArgb(64,64,64);
                    btn.BackColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;

                    btn.Click += btn_click;
                    //btn.MouseMove += btn_MouseMove;
                    //btn.MouseLeave += btn_mouseleave;
                    panel2.Controls.Add(btn);

                    top += btn.Height + 1; // Bir sonraki butonun konumunu ayarla
                }

                oku.Close(); // DataReader'ı kapat
                baglanti.Close(); // Bağlantıyı kapat
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
           
            sahakodu = btn.Tag.ToString();
            sahamethod();
        }

        private void sahapaneli_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void sahapaneli_Paint_1(object sender, PaintEventArgs e)
        {
            sahagetir();
        }

        private void frmanaekran_Activated(object sender, EventArgs e)
        {
            sahagetir();
            if (!string.IsNullOrEmpty(sahakodu))
            {
                sahamethod();
            }
        }
    }
}
