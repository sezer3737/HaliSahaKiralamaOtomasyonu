using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HaliSahaKiralama
{
    public partial class sifresifirlamaadmin : Form
    {
        private string email;
        private string kullaniciAdi;
        public sifresifirlamaadmin()
        {
            InitializeComponent();
            this.email = email;
            this.kullaniciAdi = kullaniciAdi;

        }

        public sifresifirlamaadmin(string email, string kullaniciAdi)
        {
            InitializeComponent();
            this.email = email;
            this.kullaniciAdi = kullaniciAdi;
        }

        private void btnSifreGuncelle_Click(object sender, EventArgs e)
        {
            string yeniParola = textBoxYeniParola.Text;

            if (string.IsNullOrWhiteSpace(yeniParola))
            {
                MessageBox.Show("Lütfen yeni bir parola girin.");
                return;
            }

            string emailTrimmed = email.Trim();
            string adminKadiTrimmed = kullaniciAdi.Trim();

            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");

            try
            {
                baglanti.Open();

                SqlCommand kontrolAdmin = new SqlCommand("SELECT COUNT(*) FROM admin WHERE adminemail = @eposta AND adminkadi = @kadi", baglanti);
                kontrolAdmin.Parameters.AddWithValue("@eposta", emailTrimmed);
                kontrolAdmin.Parameters.AddWithValue("@kadi", adminKadiTrimmed);

                int adminCount = (int)kontrolAdmin.ExecuteScalar();

                if (adminCount > 0)
                {
                    SqlCommand updateAdmin = new SqlCommand("UPDATE admin SET adminparola = @parola WHERE adminemail = @eposta AND adminkadi = @kadi", baglanti);
                    updateAdmin.Parameters.AddWithValue("@parola", yeniParola);
                    updateAdmin.Parameters.AddWithValue("@eposta", emailTrimmed);
                    updateAdmin.Parameters.AddWithValue("@kadi", adminKadiTrimmed);
                    updateAdmin.ExecuteNonQuery();

                    MessageBox.Show("Admin şifresi başarıyla güncellendi.");
                    this.Close();
                    new usergiris().Show(); // Admin de aynı giriş formunu kullanıyorsa
                }
                else
                {
                    MessageBox.Show("E-posta ile ilişkili admin bulunamadı.");
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

        private void sifresıfırlamaadmin_Load(object sender, EventArgs e)
        {
            textBoxEmail.Text = email;
            textBoxKadi.Text = kullaniciAdi;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
