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
    public partial class KullaniciEkleForm : Form
    {
        public KullaniciEkleForm()
        {
            InitializeComponent();
        }

        
        

        private void KullaniciEkleForm_Load(object sender, EventArgs e)
        {

        }

        private void btnkaydet_Click_1(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");

            SqlCommand kontrolKomutu = new SqlCommand("SELECT COUNT(*) FROM [user] WHERE kullaniciadi = @kadi", baglanti);
            kontrolKomutu.Parameters.AddWithValue("@kadi", textBoxKadi.Text);

            try
            {
                baglanti.Open();

                int sayi = (int)kontrolKomutu.ExecuteScalar();

                if (sayi > 0)
                {
                    MessageBox.Show("Bu kullanıcı adı zaten mevcut. Lütfen başka bir kullanıcı adı seçin.");
                }
                else
                {
                    // Burada email alanı da ekleniyor
                    SqlCommand komut = new SqlCommand("INSERT INTO [user] (kullaniciadi, parola, email) VALUES (@kadi, @parola, @email)", baglanti);
                    komut.Parameters.AddWithValue("@kadi", textBoxKadi.Text);
                    komut.Parameters.AddWithValue("@parola", textBoxParola.Text);
                    komut.Parameters.AddWithValue("@email", textBoxmail.Text);  // yeni eklendi

                    komut.ExecuteNonQuery();

                    MessageBox.Show("Kullanıcı başarıyla eklendi!");
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

      

        private void textBoxKadi_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
