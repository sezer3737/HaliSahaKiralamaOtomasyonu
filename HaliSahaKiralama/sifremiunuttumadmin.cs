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
    public partial class sifremiunuttumadmin : Form
    {
        public sifremiunuttumadmin()
        {
            InitializeComponent();
        }
        string dogrulamaKodu;

        private void btnDogrula_Click(object sender, EventArgs e)
        {
            string girilenKod = textboxKod.Text.Trim();
            string email = textboxEmail.Text.Trim();

            if (girilenKod != dogrulamaKodu)
            {
                MessageBox.Show("Kod hatalı, lütfen tekrar deneyin.");
                return;
            }

            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");
            SqlCommand komut = new SqlCommand("SELECT adminkadi FROM admin WHERE adminemail = @eposta", baglanti);
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
                    sifresifirlamaadmin sifreForm = new sifresifirlamaadmin(email, kullaniciAdi);
                    sifreForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bu e-posta ile ilişkili admin bulunamadı.");
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

            string konu = "Admin Şifre Sıfırlama Kodu";
            string icerik = $"Şifre sıfırlama işlemi için doğrulama kodunuz: {dogrulamaKodu}";

            try
            {
                MailMessage mesaj = new MailMessage();
                mesaj.From = new MailAddress("halisaham27@gmail.com");
                mesaj.To.Add(aliciMail);
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

        private void label4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lütfen e-posta adresinizi küçük harflerle girin. Kod gelmiyorsa spam klasörünüzü kontrol edin. Sorun devam ederse: halisaham27@gmail.com", "Yardım");
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.Hide();
        }

        private void sifremiunuttumadmin_Load(object sender, EventArgs e)
        {

        }
    }
}
