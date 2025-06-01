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
using System.Data.SqlClient;

namespace HaliSahaKiralama
{
    public partial class frmsahalistesics : Form
    {
        public frmsahalistesics()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");
        private void frmsahalistesics_Load(object sender, EventArgs e)
        {
            sahagetir();
        }
        private void sahagetir()
        {

            try
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close(); // Eğer bağlantı açık ise kapat
                }

                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM sahatablom ORDER BY ad ASC", baglanti);
                SqlDataReader oku = komut.ExecuteReader();

                // Paneli temizleyin, her seferinde yeni veriler eklemek için
                panel3.Controls.Clear();

                // Panelin başlangıç sağ ve alt kenarlarını eklememiz gerekebilir
                int left = 10; // İlk elemanın başlangıç X noktası
                int top = 10;  // İlk elemanın başlangıç Y noktası
                int padding = 10; // Elemanlar arası boşluk
                int panelGenislik = panel3.Width - 5; // Panelin genişliği (sağ ve sol boşlukları hesaba kat)
                int butonGenislik = 160; // Butonun genişliği
                int butonYukseklik = 140; // Butonun yüksekliği
                int counter = 0; // Kayıt sayacı, her 4 kayıt sonrası alt satıra geçmek için

                // Panelin ScrollBar'larını aktif et
                panel3.AutoScroll = true;

                while (oku.Read())
                {
                    sahalar sablon = new sahalar();
                    sablon.lblsahaadi.Text = oku["ad"].ToString();
                    sablon.lblsahanumara.Text = oku["kod"].ToString();

                    // Her kaydın genişliğini ve yüksekliğini belirleyin
                    sablon.Width = butonGenislik;
                    sablon.Height = butonYukseklik;

                    // Eğer yeni eleman panelin genişliğini aşarsa, alt satıra geç
                    if (left + butonGenislik + padding > panelGenislik)
                    {
                        left = 10; // Yeni satır için X'i sıfırla
                        top += butonYukseklik + padding; // Alt satıra kaydır
                    }

                    // Konum belirleme
                    sablon.Top = top;
                    sablon.Left = left;

                    // Kaydı panel3'e ekleyin
                    panel3.Controls.Add(sablon);

                    // Yana kaydır
                    left += butonGenislik + padding;

                    // Kaydın sayısını artır
                    counter++;

                    // 4 kayıt tamamlandığında alt satıra geç
                    if (counter == 3)
                    {
                        left = 10; // Yeni satıra geçtiğimizde X'i sıfırlıyoruz
                        top += butonYukseklik + padding; // Alt satıra geçiyoruz
                        counter = 0; // Sayacı sıfırla
                    }
                }

                oku.Close(); // DataReader'ı kapat
                baglanti.Close(); // Bağlantıyı kapat
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlsol_Paint(object sender, PaintEventArgs e)
        {

        }
        private void getir(){


            try
            {
                panel3.Controls.Clear(); // Önce paneli temizle
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
                baglanti.Open();

                SqlCommand komut = new SqlCommand("SELECT * FROM sahatablom WHERE ad LIKE @ad ORDER BY ad ASC", baglanti);
                komut.Parameters.AddWithValue("@ad", "%" + textBox1.Text + "%");

                SqlDataReader oku = komut.ExecuteReader();

                int left = 10;
                int top = 10;
                int padding = 10;
                int panelGenislik = panel3.Width - 5;
                int butonGenislik = 160;
                int butonYukseklik = 140;
                int counter = 0;

                panel3.AutoScroll = true; // ScrollBar'ları etkinleştir

                while (oku.Read())
                {
                    sahalar sablon = new sahalar();
                    sablon.lblsahaadi.Text = oku["ad"].ToString();
                    sablon.lblsahanumara.Text = oku["kod"].ToString();
                    sablon.Width = butonGenislik;
                    sablon.Height = butonYukseklik;

                    if (left + butonGenislik + padding > panelGenislik)
                    {
                        left = 10;
                        top += butonYukseklik + padding;
                    }

                    sablon.Top = top;
                    sablon.Left = left;
                    panel3.Controls.Add(sablon);

                    left += butonGenislik + padding;
                    counter++;

                    if (counter == 3)
                    {
                        left = 10;
                        top += butonYukseklik + padding;
                        counter = 0;
                    }
                }

                oku.Close();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Arama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            getir();
        }

        private void frmsahalistesics_Activated(object sender, EventArgs e)
        {
            getir();
        }
    }
      


  
}
