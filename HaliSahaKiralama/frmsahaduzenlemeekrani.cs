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
    public partial class frmsahaduzenlemeekrani : Form
    {
        public frmsahaduzenlemeekrani()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");
        public string gelenkod = "";
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             guncelle();
            MessageBox.Show("Saha Güncelleme İşlemi Başarılı");
            this.Close();
        }
        private void VerileriGetir(string sahaKodu)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM sahatablom WHERE kod = @kod", baglanti);
            komut.Parameters.AddWithValue("@kod", sahaKodu);
            SqlDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                // Verileri formdaki kontrollere yükle
                txtsahaadi.Text = oku["ad"].ToString();
                txtaciklama.Text = oku["aciklama"].ToString();

                // Tür ve boy bilgilerini kontrollere yükle
                if (oku["tur"].ToString() == "1")
                {
                    rbacik.Checked = true;
                }
                else
                {
                    rbkapali.Checked = true;
                }

                if (oku["boy"].ToString() == "1")
                {
                   rbacik.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
            }
            else
            {
                MessageBox.Show("Kayıt bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            baglanti.Close();
        }
        void guncelle()
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand("update sahatablom set ad=@ad,tur=@tur,boy=@boy,aciklama=@aciklama where kod=@kod", baglanti);
            komut.Parameters.AddWithValue("@kod", gelenkod);
            komut.Parameters.AddWithValue("@ad", txtsahaadi.Text.ToUpper());
            if (rbacik.Checked)// bu halı saha türünü açık olarak seçtiğmiz anlamına geliyor 2 türümüz var bir açık bir kapalı onun yerine "1","0" kavramları
                               //yani doğru yanlış diye alabilriz açık=doğru kapalı=yanlış
            {
                komut.Parameters.AddWithValue("@tur", "1");
            }
            else
            {
                komut.Parameters.AddWithValue("@tur", "2");
            }
            if (radioButton2.Checked)
            {
                komut.Parameters.AddWithValue("@boy", "1");
            }
            else
            {
                komut.Parameters.AddWithValue("@boy", "2");
            }

            komut.Parameters.AddWithValue("@aciklama", txtaciklama.Text.ToUpper());

            komut.ExecuteNonQuery();


            baglanti.Close();
          
            void temizle()
            {

                txtsahaadi.Text = "";
                txtaciklama.Text = "";
                rbacik.Checked = true;
                radioButton2.Checked = true;
            };

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void frmsahaduzenlemeekrani_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(gelenkod))
            {
               VerileriGetir(gelenkod); // gelenkod değerini kullanarak verileri çek
            }
            else
            {
                MessageBox.Show("Geçersiz saha ID'si!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_MouseMove(object sender, MouseEventArgs e)
        {
           
         

            button3.BackColor = Color.FromArgb(229, 125, 0);
            button3.ForeColor = Color.White;

        }
   

        private void button3_MouseLeave(object sender, EventArgs e)
        {
        button3.ForeColor = Color.FromArgb(224, 125, 0);
        button3.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sahatablom where kod=@kod",baglanti);
            komut.Parameters.AddWithValue("@kod", gelenkod);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Saha Başarıyla Silindi","SAHA SİLME EKRANI");
            this.Close();
        }
    }
}
