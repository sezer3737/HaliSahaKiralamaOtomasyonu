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
    public partial class Frmsahasec : Form
    {
        public Frmsahasec()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frmsahasec_Load(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from sahatablom order by ad asc", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            int top = 10; // Butonların başlangıç konumu (Y ekseni)
            while (oku.Read())
            {
                Button btn = new Button();
                btn.Text = oku["ad"].ToString();
                btn.Tag = oku["kod"].ToString();
                btn.Width = 150;
                btn.Height = 80;
                btn.Top = top; // Butonun Y konumu
                btn.Left = 10; // Butonun X konumub
                btn.BackColor = Color.MediumPurple;
                btn.ForeColor = Color.White;
                btn.Click += btn_click;
                btn.MouseMove += btn_MouseMove;
                btn.MouseLeave += btn_mouseleave; ;
                panel2.Controls.Add(btn);


                top += btn.Height + 10; // Bir sonraki butonun konumunu ayarla
            }


        }

        private void btn_mouseleave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.MediumPurple;
        }

        private void btn_MouseMove(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.FromArgb(64, 64, 64);
        }


        private void btn_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Tag != null)
            {
                frmsahaduzenlemeekrani form = new frmsahaduzenlemeekrani();
                form.gelenkod = btn.Tag.ToString(); // Tag özelliğini gelenkod'a aktar
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Saha ID'si bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnrustmenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Frmsahasec_Activated(object sender, EventArgs e)
        {
            VerileriYukle();
        }
        private void VerileriYukle()
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
                    btn.Width = 150;
                    btn.Height = 80;
                    btn.Top = top; // Butonun Y konumu
                    btn.Left = 10; // Butonun X konumu
                    btn.BackColor = Color.MediumPurple;
                    btn.ForeColor = Color.White;
                    btn.Click += btn_click;
                    btn.MouseMove += btn_MouseMove;
                    btn.MouseLeave += btn_mouseleave;
                    panel2.Controls.Add(btn);

                    top += btn.Height + 10; // Bir sonraki butonun konumunu ayarla
                }

                oku.Close(); // DataReader'ı kapat
                baglanti.Close(); // Bağlantıyı kapat
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
