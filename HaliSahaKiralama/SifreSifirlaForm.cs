using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HaliSahaKiralama
{
    public partial class SifreSifirlaForm : Form
    {
        private string email;
        private string kullaniciAdi;

        public SifreSifirlaForm(string email, string kullaniciAdi)
        {
            InitializeComponent();
            this.email = email;
            this.kullaniciAdi = kullaniciAdi;
        }

        private void SifreSifirlaForm_Load(object sender, EventArgs e)
        {
            textBoxEmail.Text = email;
            textBoxKadi.Text = kullaniciAdi;
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
            string kullaniciAdiTrimmed = kullaniciAdi.Trim();

            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");

            try
            {
                baglanti.Open();

                SqlCommand kontrolUser = new SqlCommand("SELECT COUNT(*) FROM [user] WHERE email = @eposta AND kullaniciadi = @kadi", baglanti);
                kontrolUser.Parameters.AddWithValue("@eposta", emailTrimmed);
                kontrolUser.Parameters.AddWithValue("@kadi", kullaniciAdiTrimmed);

                int userCount = (int)kontrolUser.ExecuteScalar();

                if (userCount > 0)
                {
                    SqlCommand updateUser = new SqlCommand("UPDATE [user] SET parola = @parola WHERE email = @eposta AND kullaniciadi = @kadi", baglanti);
                    updateUser.Parameters.AddWithValue("@parola", yeniParola);
                    updateUser.Parameters.AddWithValue("@eposta", emailTrimmed);
                    updateUser.Parameters.AddWithValue("@kadi", kullaniciAdiTrimmed);
                    updateUser.ExecuteNonQuery();

                    MessageBox.Show("Şifre başarıyla güncellendi.");
                    this.Close();
                    new usergiris().Show();
                }
                else
                {
                    MessageBox.Show("E-posta ile ilişkili kullanıcı bulunamadı.");
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

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
