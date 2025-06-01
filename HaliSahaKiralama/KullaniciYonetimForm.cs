using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HaliSahaKiralama
{
    public partial class KullaniciYonetimForm : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");

        public KullaniciYonetimForm()
        {
            InitializeComponent();
        }

        private void KullaniciYonetimForm_Load(object sender, EventArgs e)
        {
            KullaniciListele(); // Form yüklendiğinde kullanıcıları listele
        }

        // Kullanıcıları listeleme fonksiyonu
        private void KullaniciListele()
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM [user]", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt; // Kullanıcıları grid'e aktar
        }

        // Kullanıcı ekleme fonksiyonu
        private void KullaniciEkle()
        {
            SqlCommand komut = new SqlCommand("INSERT INTO [user] (kullaniciadi, parola, email) VALUES (@kadi, @parola, @email)", baglanti);
            komut.Parameters.AddWithValue("@kadi", textBoxKadi.Text);  // Kullanıcı adı
            komut.Parameters.AddWithValue("@parola", textBoxParola.Text); // Parola
            komut.Parameters.AddWithValue("@email", textemail.Text);  // E-posta

            try
            {
                baglanti.Open();
                komut.ExecuteNonQuery();  // Veritabanına kullanıcı ekle
                MessageBox.Show("Yeni kullanıcı başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
                KullaniciListele();  // Listeyi güncelle
            }
        }

        // Kullanıcı güncelleme fonksiyonu
        private void KullaniciGuncelle()
        {
            SqlCommand komut = new SqlCommand("UPDATE [user] SET parola = @parola, email = @email WHERE kullaniciadi = @kadi", baglanti);
            komut.Parameters.AddWithValue("@kadi", textBoxKadi.Text);  // Kullanıcı adı
            komut.Parameters.AddWithValue("@parola", textBoxParola.Text); // Parola
            komut.Parameters.AddWithValue("@email", textemail.Text);  // E-posta

            try
            {
                baglanti.Open();
                komut.ExecuteNonQuery();  // Veritabanında kullanıcıyı güncelle
                MessageBox.Show("Kullanıcı başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
                KullaniciListele();  // Listeyi güncelle
            }
        }

        // Kullanıcı güncelleme butonuna tıklandığında
        private void btnguncelle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string secilenKadi = dataGridView1.SelectedRows[0].Cells["kullaniciadi"].Value.ToString();

                // Güncelleme işlemi için kullanıcı bilgilerini al ve güncelle
                textBoxKadi.Text = secilenKadi; // Seçilen kullanıcı adı text box'a aktarılıyor
                KullaniciGuncelle(); // Güncelleme fonksiyonu çağrılıyor
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz kullanıcıyı seçin.");
            }
        }

        // Kullanıcı silme butonuna tıklandığında
        private void btndel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string secilenKadi = dataGridView1.SelectedRows[0].Cells["kullaniciadi"].Value.ToString();

                // Kullanıcıyı silme komutu
                SqlCommand komut = new SqlCommand("DELETE FROM [user] WHERE kullaniciadi = @kadi", baglanti);
                komut.Parameters.AddWithValue("@kadi", secilenKadi);

                try
                {
                    baglanti.Open();
                    komut.ExecuteNonQuery();  // Veritabanından kullanıcıyı sil
                    MessageBox.Show("Kullanıcı başarıyla silindi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    baglanti.Close();
                    KullaniciListele();  // Listeyi güncelle
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz kullanıcıyı seçin.");
            }
        }

        // Kullanıcı ekleme butonuna tıklandığında
        private void btnekle_Click(object sender, EventArgs e)
        {
            KullaniciEkleForm frm = new KullaniciEkleForm();
            frm.ShowDialog();
        }

        // Kullanıcı yönetim formu aktive olduğunda kullanıcıları listele
        private void KullaniciYonetimForm_Activated(object sender, EventArgs e)
        {
            KullaniciListele();  // Kullanıcıları güncelle
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBoxKadi.Text = row.Cells["kullaniciadi"].Value.ToString();
                textBoxParola.Text = row.Cells["parola"].Value.ToString();
                textemail.Text = row.Cells["email"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
