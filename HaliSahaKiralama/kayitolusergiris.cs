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
    public partial class kayitolusergiris : Form
    {
        public kayitolusergiris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKaydol_Click(object sender, EventArgs e)
        {

            string kullaniciAdi = txtKullaniciAdi.Text.Trim();
            string parola = txtParola.Text.Trim();
            string email = txtEmail.Text.Trim();
            string kullaniciTipi = cmbKullaniciTipi.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(parola) || string.IsNullOrEmpty(kullaniciTipi))
            {
                MessageBox.Show("Tüm alanları doldurunuz.");
                return;
            }

            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");

            try
            {
                baglanti.Open();

                string sorgu = "";

                if (kullaniciTipi == "ADMIN")
                {
                    sorgu = "INSERT INTO [admin] (adminkadi, adminparola, adminemail) VALUES (@kadi, @parola, @email)";
                }
                else if (kullaniciTipi == "KULLANICI")
                {
                    sorgu = "INSERT INTO [user] (kullaniciadi, parola, email) VALUES (@kadi, @parola, @email)";
                }
                else
                {
                    MessageBox.Show("Geçersiz kullanıcı tipi seçimi.");
                    return;
                }

                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@kadi", kullaniciAdi);
                komut.Parameters.AddWithValue("@parola", parola);
                komut.Parameters.AddWithValue("@email", email);

                komut.ExecuteNonQuery();
                MessageBox.Show("Kayıt başarıyla oluşturuldu.");
                this.Close();
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

        private void kayitolusergiris_Load(object sender, EventArgs e)
        {
            cmbKullaniciTipi.Items.Add("ADMIN");
            cmbKullaniciTipi.Items.Add("KULLANICI");
            cmbKullaniciTipi.SelectedIndex = 0;
        }
    }
}
