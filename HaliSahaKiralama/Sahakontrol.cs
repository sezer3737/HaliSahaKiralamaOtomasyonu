using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace HaliSahaKiralama
{
    public partial class Sahakontrol : UserControl
    {
        public Sahakontrol()
        {
            InitializeComponent();
        }

        public string gelensahakodu = ""; // Butondan gelecek saha kodu

        private void Sahakontrol_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gelensahakodu))
                return;

            DateTime tarih;
            if (!DateTime.TryParse(lbltarih.Text.Trim(), out tarih))
            {
                MessageBox.Show("Geçersiz tarih formatı!");
                return;
            }

            using (SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True"))
            using (SqlCommand komut = new SqlCommand("SELECT * FROM Islemler WHERE Tarih=@tarih AND Saat=@saat AND SahaKodu=@sahakodu", baglanti))
            {
                komut.Parameters.AddWithValue("@tarih", tarih.Date);
                komut.Parameters.AddWithValue("@saat", label1.Text.Trim());
                komut.Parameters.AddWithValue("@sahakodu", gelensahakodu);

                baglanti.Open();
                using (SqlDataReader oku = komut.ExecuteReader())
                {
                    if (oku.Read())
                    {
                        if (oku["IslemTuru"].ToString() == "Kirala")
                        {
                            pictureBox1.Image = Properties.Resources.cK;
                            label1.ForeColor = Color.FromArgb(223, 65, 73);
                        }
                        else
                        {
                            pictureBox1.Image = Properties.Resources.cT;
                            label1.ForeColor = Color.FromArgb(229, 125, 0);
                        }
                    }
                }
            }
        }
    }
}
