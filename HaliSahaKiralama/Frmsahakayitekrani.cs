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
    public partial class Frmsahakayitekrani : Form
    {
        public Frmsahakayitekrani()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frmsahakayitekrani_Load(object sender, EventArgs e)
        {
            sahakoduolustur();
        }

        void sahakoduolustur()
        {
            Random rastgele = new Random();
            string semboller = "0123456789987654321123456780978424531000";
            string olusankod = "";

            for (int i = 1; i < 6; i++)
            {
                olusankod += semboller[rastgele.Next(semboller.Length)];
            }

            label3.Text = olusankod.ToString();
           
        }

        private void btntamamla_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtsahaadi.Text))
            {
                kaydet();
            }
            else
            {
                MessageBox.Show("Lütfen Saha İsmi Girin", "İsim Hatası Ekranı");
            }
        }

        void kaydet()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO sahatablom (kod, ad, tur, boy, aciklama) VALUES (@kod, @ad, @tur, @boy, @aciklama)", baglanti);

                komut.Parameters.AddWithValue("@kod", label3.Text);
                komut.Parameters.AddWithValue("@ad", txtsahaadi.Text.ToUpper());
                komut.Parameters.AddWithValue("@tur", rbacik.Checked ? "1" : "2");
                komut.Parameters.AddWithValue("@boy", radioButton2.Checked ? "1" : "2");
                komut.Parameters.AddWithValue("@aciklama", txtaciklama.Text.ToUpper());

                int sonuc = komut.ExecuteNonQuery();

                if (sonuc > 0)
                {
                    MessageBox.Show("Saha başarıyla kaydedildi.");
                    sahakoduolustur();
                    temizle();
                }
                else
                {
                    MessageBox.Show("Kayıt eklenemedi! Lütfen tekrar deneyin.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Hatası: " + ex.Message);
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

        void temizle()
        {
            txtsahaadi.Text = "";
            txtaciklama.Text = "";
            rbacik.Checked = true;
            radioButton2.Checked = true;
        }
    }
}
