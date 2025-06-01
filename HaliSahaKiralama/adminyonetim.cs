using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HaliSahaKiralama
{
    public partial class adminyonetim : Form
    {
        public adminyonetim()
        {
            InitializeComponent();
        }

        private void adminyonetim_Load(object sender, EventArgs e)
        {

        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");

            // Kullanıcı adı var mı kontrol et
            SqlCommand kontrolKomutu = new SqlCommand("SELECT COUNT(*) FROM [admin] WHERE adminkadi = @kadi", baglanti);
            kontrolKomutu.Parameters.AddWithValue("@kadi", textBoxKadi.Text);

            try
            {
                baglanti.Open();

                int sayi = (int)kontrolKomutu.ExecuteScalar();

                if (sayi > 0)
                {
                    MessageBox.Show("Bu admin kullanıcı adı zaten mevcut. Lütfen başka bir kullanıcı adı seçin.");
                }
                else
                {
                    // Yeni admin ekleme işlemi (email dahil)
                    SqlCommand komut = new SqlCommand("INSERT INTO [admin] (adminkadi, adminparola, adminemail) VALUES (@kadi, @parola, @email)", baglanti);
                    komut.Parameters.AddWithValue("@kadi", textBoxKadi.Text);
                    komut.Parameters.AddWithValue("@parola", textBoxParola.Text);
                    komut.Parameters.AddWithValue("@email", textBoxemail.Text); // Yeni eklendi

                    komut.ExecuteNonQuery();

                    MessageBox.Show("Admin başarıyla eklendi!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
