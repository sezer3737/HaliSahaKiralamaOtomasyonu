using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Configuration;


namespace HaliSahaKiralama
{
    public partial class usergiris : Form
    {
        private string girilenTuslar = "";

        public usergiris()
        {
            InitializeComponent();


        }
      
        private void usergiris_Load(object sender, EventArgs e)
        {
           

            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;

            label4.Parent = pictureBox1;
            label4.BackColor = Color.Transparent;

        }
       




       

       

        private void button2_Click_1(object sender, EventArgs e)
        {
            string kullaniciadi = textBox1.Text;
            string parola = textBox3.Text;

            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");

            try
            {
                baglanti.Open();

                // Admin sorgusu
                SqlCommand adminKomut = new SqlCommand("SELECT * FROM [admin] WHERE adminkadi = @kadi AND adminparola = @parola", baglanti);
                adminKomut.Parameters.AddWithValue("@kadi", kullaniciadi);
                adminKomut.Parameters.AddWithValue("@parola", parola);

                SqlDataReader dr = adminKomut.ExecuteReader();

                if (dr.HasRows)
                {
                    MessageBox.Show("Admin Girişi Başarılı", "Giriş Ekranı");
                    frmanaekran anaEkran = new frmanaekran();
                    anaEkran.AdminMi = true;  // Admin girişi
                    anaEkran.ShowDialog();
                    this.Close();
                }
                else
                {
                    dr.Close();  // Admin sorgusundan sonra veri okuyucusunu kapatıyoruz.

                    // User sorgusu
                    SqlCommand userKomut = new SqlCommand("SELECT * FROM [user] WHERE kullaniciadi = @kadi AND parola = @parola", baglanti);
                    userKomut.Parameters.AddWithValue("@kadi", kullaniciadi);
                    userKomut.Parameters.AddWithValue("@parola", parola);
                    SqlDataReader drUser = userKomut.ExecuteReader();

                    if (drUser.HasRows)
                    {
                        MessageBox.Show("Kullanıcı Girişi Başarılı", "Giriş Ekranı");
                        frmanaekran anaEkran = new frmanaekran();
                        anaEkran.AdminMi = false;  // Normal kullanıcı girişi
                        anaEkran.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı Adı Veya Şifre Hatalı", "Giriş Ekranı");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Veritabanı Hatası");
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            sifresecimekrani frm = new sifresecimekrani();
            frm.ShowDialog();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            kayitolusergiris frm = new kayitolusergiris();
            frm.Show();
           
        }
    }
    }

