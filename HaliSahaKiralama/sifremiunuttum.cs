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
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;

namespace HaliSahaKiralama
{
    public partial class sifremiunuttum : Form
    {
        public sifremiunuttum()
        {
            InitializeComponent();
        }

        string dogrulamaKodu;

        private void btnKodGonder_Click(object sender, EventArgs e)
        {
            string aliciMail = textboxEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(aliciMail))
            {
                MessageBox.Show("Lütfen e-posta adresinizi girin.");
                return;
            }

            // 6 haneli doğrulama kodu üret
            Random rnd = new Random();
            dogrulamaKodu = rnd.Next(100000, 999999).ToString();

            string konu = "Şifre Sıfırlama Kodu";
            string icerik = $"Şifre sıfırlama işlemi için doğrulama kodunuz: {dogrulamaKodu}";

            try
            {
                MailMessage mesaj = new MailMessage();
                mesaj.From = new MailAddress("halisaham27@gmail.com"); // Gönderici
                mesaj.To.Add(aliciMail); // Alıcı
                mesaj.Subject = konu;
                mesaj.Body = icerik;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("halisaham27@gmail.com", "pbweoybdglgeryuq");
                smtp.EnableSsl = true;
                smtp.Send(mesaj);

                MessageBox.Show("Doğrulama kodu e-posta adresinize gönderildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mail gönderilemedi: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string girilenKod = textboxKod.Text.Trim();
            string email = textboxEmail.Text.Trim();

            if (girilenKod != dogrulamaKodu)
            {
                MessageBox.Show("Kod hatalı, lütfen tekrar deneyin.");
                return;
            }

            // Kullanıcı adını veritabanından bul
            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");
            SqlCommand komut = new SqlCommand("SELECT kullaniciadi FROM [user] WHERE email = @eposta", baglanti);
            komut.Parameters.AddWithValue("@eposta", email);

            try
            {
                baglanti.Open();
                object sonuc = komut.ExecuteScalar();

                if (sonuc != null)
                {
                    string kullaniciAdi = sonuc.ToString();
                    MessageBox.Show("Kod doğru! Şifre sıfırlama ekranına yönlendiriliyorsunuz...");
                    this.Hide();
                    SifreSifirlaForm sifreForm = new SifreSifirlaForm(email, kullaniciAdi);
                    sifreForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bu e-posta ile ilişkili kullanıcı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void textboxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textboxKod_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lütfen E-Postanızın Tamamını Küçük Harflerle Yazınız,Doğrulama Kodu E-Postanıza Gelmiyorsa,E-postanızdaki Spam Klasörüne Bakınız,Halen Aynı sorunları Yaşıyorsanız 'halisaham27@gmail.com' Gmailinden Bizimle İletişime Geçebilirsiniz","Yardım Ekranı");
        }

        private void sifremiunuttum_Load(object sender, EventArgs e)
        {
           
        }
    }
}
