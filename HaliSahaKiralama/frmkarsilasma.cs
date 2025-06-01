using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HaliSahaKiralama
{
    public partial class frmkarsilasma : Form
    {
        public frmkarsilasma()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");

        private string seciliSahaAdi = "";
        private string seciliSahaKodu = "";

        private void frmkarsilasma_Load(object sender, EventArgs e)
        {
            DateTime bugun = DateTime.Today;
            SetButtonsWithStartDate(bugun);
            bilsahagetir();

            // RadioButton event'ları burada atanabilir
            rbGoster.CheckedChanged += RadioButtons_CheckedChanged;
            rbGizle.CheckedChanged += RadioButtons_CheckedChanged;
        }
        private void Temizle()
        {
            txtadsoyad.Clear();
            texttelefono.Clear();
            txtaciklama1.Clear();
            textBox2.Clear();
            gbsaat.Text = "";
            GBTarih.Text = "";
            GBTarih.Tag = null;

            if (oncekiSeciliSaatBtn != null)
            {
                oncekiSeciliSaatBtn.BackColor = Color.FromArgb(64, 64, 64);
                oncekiSeciliSaatBtn = null;
            }

            if (oncekiSeciliTarihBtn != null)
            {
                oncekiSeciliTarihBtn.BackColor = Color.FromArgb(43, 178, 123);
                oncekiSeciliTarihBtn = null;
            }

            // RadioButton'ları sıfırla
            rbkirala.Checked = false;
            rbrezerve.Checked = false;
            rbnakit.Checked = false;
            rbeft.Checked = false;
            rbodendi.Checked = false;
            rbodenmedi.Checked = false;

            seciliSahaAdi = "";
            seciliSahaKodu = "";
            labelSeciliSaha.Text = "Seçilen Saha: -";

            SaatButonlariniTemizle();
        }


        private void RadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            // Sadece saha seçildiyse ve göster seçiliyse saat butonlarını vurgula
            if (!string.IsNullOrEmpty(seciliSahaAdi) && rbGoster.Checked)
            {
                VurgulaCakisanSaatler();
            }
            else
            {
                SaatButonlariniTemizle();
            }
        }

        private void SahaSecildi(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            seciliSahaAdi = btn.Text;
            seciliSahaKodu = btn.Tag.ToString();

            labelSeciliSaha.Text = $"Seçilen Saha: {seciliSahaAdi}";

            // RadioButton "Göster" seçili ise saat butonlarını kontrol et ve vurgula
            if (rbGoster.Checked)
            {
                VurgulaCakisanSaatler();
            }
            else
            {
                SaatButonlariniTemizle();
            }
        }

        private void SetButtonsWithStartDate(DateTime startDate)
        {
            Button[] buttons = { button2, button3, button4, button5, button6, button7, button8 };

            for (int i = 0; i < buttons.Length; i++)
            {
                DateTime currentDate = startDate.AddDays(i);
                buttons[i].Text = currentDate.ToString("yyyy/MM/dd dddd ", new CultureInfo("tr-TR"));
                buttons[i].Tag = currentDate;
            }

            UpdateButtonColors();
        }

        private void UpdateButtonColors()
        {
            foreach (Control control in GBTarih.Controls)
            {
                if (control is Button btn && btn.Tag is DateTime tarih)
                {
                    TimeSpan fark = tarih - DateTime.Today;
                    if (fark.Days < 0)
                        btn.BackColor = Color.FromArgb(213, 76, 98); // geçmiş
                    else
                        btn.BackColor = Color.FromArgb(43, 178, 123); // gelecek/güncel
                }
            }

            if (oncekiSeciliTarihBtn != null)
                oncekiSeciliTarihBtn.BackColor = Color.FromArgb(64, 64, 64);
        }

        private Button oncekiSeciliTarihBtn = null;

        private void button2_Click(object sender, EventArgs e)
        {
            if (oncekiSeciliTarihBtn != null && oncekiSeciliTarihBtn.Tag is DateTime eskiTarih)
            {
                TimeSpan fark = eskiTarih - DateTime.Today;
                if (fark.Days < 0)
                    oncekiSeciliTarihBtn.BackColor = Color.FromArgb(213, 76, 98);
                else
                    oncekiSeciliTarihBtn.BackColor = Color.FromArgb(43, 178, 123);
            }

            Button btn = (Button)sender;
            btn.BackColor = Color.FromArgb(64, 64, 64);
            GBTarih.Text = btn.Text;
            GBTarih.Tag = btn.Tag;
            oncekiSeciliTarihBtn = btn;

            // Saha seçilmiş ve Göster seçiliyse vurgula
            if (!string.IsNullOrEmpty(seciliSahaAdi) && rbGoster.Checked)
                VurgulaCakisanSaatler();
            else
                SaatButonlariniTemizle();
        }

        private Button oncekiSeciliSaatBtn = null;

        private void saatclick(object sender, EventArgs e)
        {
            if (oncekiSeciliSaatBtn != null)
                oncekiSeciliSaatBtn.BackColor = Color.FromArgb(64, 64, 64);

            Button clicked = sender as Button;
            clicked.BackColor = Color.OrangeRed;
            gbsaat.Text = clicked.Text;
            oncekiSeciliSaatBtn = clicked;
        }

        private void VurgulaCakisanSaatler()
        {
            if (string.IsNullOrEmpty(seciliSahaAdi) || GBTarih.Tag == null)
                return;

            DateTime secilenTarih = (DateTime)GBTarih.Tag;
            string seciliSahaKoduLocal = seciliSahaKodu;

            SaatButonlariniTemizle();

            try
            {
                baglanti.Open();

                SqlCommand komut = new SqlCommand(@"
            SELECT Saat, IslemTuru 
            FROM Islemler 
            WHERE Tarih = @Tarih AND SahaKodu = @SahaKodu
        ", baglanti);

                komut.Parameters.AddWithValue("@Tarih", secilenTarih);
                komut.Parameters.AddWithValue("@SahaKodu", seciliSahaKoduLocal);

                SqlDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    string saat = oku["Saat"].ToString();
                    string islemTuru = oku["IslemTuru"].ToString();

                    foreach (Control ctrl in gbsaat.Controls)
                    {
                        if (ctrl is Button btnSaat)
                        {
                            string saatKontrol = btnSaat.Tag != null ? btnSaat.Tag.ToString() : btnSaat.Text;

                            if (saatKontrol == saat)
                            {
                                // Orijinal saat bilgisini Tag olarak sakla
                                if (btnSaat.Tag == null || btnSaat.Tag.ToString() == btnSaat.Text)
                                    btnSaat.Tag = btnSaat.Text;

                                // İşlem türüne göre renk ve metin ayarla
                                if (islemTuru.Equals("Kirala", StringComparison.OrdinalIgnoreCase))
                                {
                                    btnSaat.BackColor = Color.Red;
                                    btnSaat.ForeColor = Color.White;
                                    btnSaat.Text = "DOLU";
                                    btnSaat.Enabled = false;
                                }
                                else if (islemTuru.Equals("Rezerve", StringComparison.OrdinalIgnoreCase))
                                {
                                    btnSaat.BackColor = Color.DarkOrange;
                                    btnSaat.ForeColor = Color.White;
                                    btnSaat.Text = "REZERVE";
                                    btnSaat.Enabled = false;
                                }
                                else
                                {
                                    // Diğer durumlar için (istersen)
                                    btnSaat.BackColor = Color.Gray;
                                    btnSaat.ForeColor = Color.White;
                                    btnSaat.Enabled = false;
                                }
                            }
                        }
                    }
                }
                oku.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Saat butonları kontrol edilirken hata oluştu: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            }
        }

        private void SaatButonlariniTemizle()
        {
            foreach (Control ctrl in gbsaat.Controls)
            {
                if (ctrl is Button btnSaat)
                {
                    btnSaat.BackColor = Color.FromArgb(64, 64, 64);
                    btnSaat.ForeColor = Color.White;
                    btnSaat.Enabled = true;

                    // Eğer Tag'ta orijinal saat metni varsa onu geri yükle
                    if (btnSaat.Tag != null)
                        btnSaat.Text = btnSaat.Tag.ToString();
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        { // Temel alan kontrolleri
            if (string.IsNullOrWhiteSpace(txtadsoyad.Text) || string.IsNullOrWhiteSpace(gbsaat.Text) || GBTarih.Tag == null)
            {
                MessageBox.Show("Lütfen Ad Soyad, Tarih ve Saat alanlarını doldurun.");
                return;
            }

            // Tarih geçmişse kayıt engelle
            DateTime secilenTarih = (DateTime)GBTarih.Tag;
            if (secilenTarih.Date < DateTime.Today)
            {
                MessageBox.Show("Geçmiş tarihe kiralama yapılamaz.");
                return;
            }

            // Toplam tutar kontrolü
            if (string.IsNullOrWhiteSpace(textBox2.Text) || !decimal.TryParse(textBox2.Text, out decimal tutar) || tutar <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir toplam tutar giriniz (0'dan büyük olmalıdır).");
                return;
            }

            // Saha seçimi kontrolü
            if (string.IsNullOrWhiteSpace(seciliSahaAdi) || string.IsNullOrWhiteSpace(seciliSahaKodu))
            {
                MessageBox.Show("Lütfen bir saha seçiniz.");
                return;
            }

            // İşlem türü, ödeme türü ve ödeme durumu kontrolleri
            string islemTuru = rbkirala.Checked ? "Kirala" : (rbrezerve.Checked ? "Rezerve" : "");
            string odemeTuru = rbnakit.Checked ? "Nakit" : (rbeft.Checked ? "EFT" : "");
            string odemeDurumu = rbodendi.Checked ? "Ödendi" : (rbodenmedi.Checked ? "Ödenmedi" : "");

            if (islemTuru == "" || odemeTuru == "" || odemeDurumu == "")
            {
                MessageBox.Show("Lütfen işlem türü, ödeme türü ve ödeme durumunu seçiniz.");
                return;
            }

            // Aynı saha, tarih, saat için kayıt var mı kontrol et
            bool kayitVar = false;
            try
            {
                using (SqlConnection kontrolBaglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True"))
                {
                    kontrolBaglanti.Open();

                    SqlCommand kontrolKomut = new SqlCommand(@"
                SELECT COUNT(*) FROM Islemler 
                WHERE SahaKodu = @SahaKodu AND Tarih = @Tarih AND Saat = @Saat
            ", kontrolBaglanti);

                    kontrolKomut.Parameters.AddWithValue("@SahaKodu", seciliSahaKodu);
                    kontrolKomut.Parameters.AddWithValue("@Tarih", secilenTarih);
                    kontrolKomut.Parameters.AddWithValue("@Saat", gbsaat.Text);

                    int sayi = (int)kontrolKomut.ExecuteScalar();

                    if (sayi > 0)
                        kayitVar = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt kontrolü sırasında hata oluştu: " + ex.Message);
                return;
            }

            if (kayitVar)
            {
                MessageBox.Show("Seçilen saha, tarih ve saat için zaten bir kayıt mevcut. Lütfen farklı bir zaman seçin veya çakışan kaydı düzenleyin.");
                return;
            }

            // Kayıt bilgileri onayı
            string bilgiOzeti = $"Lütfen bilgileri kontrol edin:\n\n" +
                                $"👤 Ad Soyad: {txtadsoyad.Text}\n" +
                                $"📞 Telefon: {texttelefono.Text}\n" +
                                $"🏟 Saha: {seciliSahaAdi}\n" +
                                $"📝 Açıklama: {txtaciklama1.Text}\n" +
                                $"📅 Tarih: {secilenTarih.ToShortDateString()}\n" +
                                $"⏰ Saat: {gbsaat.Text}\n" +
                                $"💵 Tutar: {tutar} TL\n" +
                                $"🛠 İşlem Türü: {islemTuru}\n" +
                                $"💳 Ödeme Türü: {odemeTuru}\n" +
                                $"💰 Ödeme Durumu: {odemeDurumu}\n\n" +
                                $"Bu bilgileri kaydetmek istiyor musunuz?";

            DialogResult onay = MessageBox.Show(bilgiOzeti, "Kayıt Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay == DialogResult.No)
            {
                MessageBox.Show("Kiralama işlemi iptal edildi.");
                return;
            }

            // Kayıt işlemi
            try
            {
                using (SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True"))
                {
                    baglanti.Open();

                    SqlCommand komut = new SqlCommand(@"
                INSERT INTO Islemler 
                (AdSoyad, Telefon, Aciklama, Tarih, Saat, ToplamTutar, IslemTuru, OdemeDurumu, OdemeTuru, SahaAdi, SahaKodu)
                VALUES 
                (@AdSoyad, @Telefon, @Aciklama, @Tarih, @Saat, @Tutar, @IslemTuru, @OdemeDurumu, @OdemeTuru, @SahaAdi, @SahaKodu)
            ", baglanti);

                    komut.Parameters.AddWithValue("@AdSoyad", txtadsoyad.Text);
                    komut.Parameters.AddWithValue("@Telefon", texttelefono.Text);
                    komut.Parameters.AddWithValue("@Aciklama", txtaciklama1.Text ?? "");
                    komut.Parameters.AddWithValue("@Tarih", secilenTarih);
                    komut.Parameters.AddWithValue("@Saat", gbsaat.Text);
                    komut.Parameters.AddWithValue("@Tutar", tutar);
                    komut.Parameters.AddWithValue("@IslemTuru", islemTuru);
                    komut.Parameters.AddWithValue("@OdemeDurumu", odemeDurumu);
                    komut.Parameters.AddWithValue("@OdemeTuru", odemeTuru);
                    komut.Parameters.AddWithValue("@SahaAdi", seciliSahaAdi);
                    komut.Parameters.AddWithValue("@SahaKodu", seciliSahaKodu);

                    komut.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Saha Başarıyla Kiralandı!");

                // Kayıt sonrası kırmızı saat butonlarını güncelle
                if (rbGoster.Checked)
                    VurgulaCakisanSaatler();
                else
                    SaatButonlariniTemizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Hata oluştu: " + ex.Message);
            }
        }

        private void bilsahagetir()
        {
            panel3.Controls.Clear();

            using (SqlConnection baglanti2 = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True"))
            {
                baglanti2.Open();
                SqlCommand komut = new SqlCommand("select * from sahatablom order by ad asc", baglanti2);
                SqlDataReader oku = komut.ExecuteReader();

                int top = 10;
                int left = 10;
                int butonSayisi = 0;

                while (oku.Read())
                {
                    Button btn = new Button();
                    btn.Text = oku["ad"].ToString();
                    btn.Tag = oku["kod"].ToString();
                    btn.Width = 150;
                    btn.Height = 80;
                    btn.Top = top;
                    btn.Left = left;
                    btn.BackColor = Color.MediumPurple;
                    btn.ForeColor = Color.White;

                    panel3.Controls.Add(btn);

                    left += btn.Width + 5;
                    butonSayisi++;
                    btn.Click += SahaSecildi;

                    if (butonSayisi % 6 == 0)
                    {
                        left = 10;
                        top += btn.Height + 10;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button2.Tag is DateTime)
            {
                DateTime baslangicTarihi = (DateTime)button2.Tag;
                SetButtonsWithStartDate(baslangicTarihi.AddDays(7));
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (button2.Tag is DateTime)
            {
                DateTime baslangicTarihi = (DateTime)button2.Tag;
                SetButtonsWithStartDate(baslangicTarihi.AddDays(-7));
            }
        }

        private void rbnakit_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbGoster_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void AÇIKLAMA_Enter(object sender, EventArgs e)
        {

        }
    }
}

