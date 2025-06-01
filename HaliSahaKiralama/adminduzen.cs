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
    public partial class adminduzen : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O637T3V;Initial Catalog=HalisahaVeritabanim;Integrated Security=True");
       
        public adminduzen()
        {
            InitializeComponent();
        }

        private void adminduzen_Load(object sender, EventArgs e)
        {
            KullaniciListele();
        }
        private void KullaniciListele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [admin] ORDER BY adminkadi ASC", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;

            if (dataGridView1.Columns.Contains("adminkadi"))
                dataGridView1.Columns["adminkadi"].HeaderText = "Kullanıcı Adı";
            if (dataGridView1.Columns.Contains("adminparola"))
                dataGridView1.Columns["adminparola"].HeaderText = "Parola";
            if (dataGridView1.Columns.Contains("adminemail"))
                dataGridView1.Columns["adminemail"].HeaderText = "E-Mail";

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
                textBoxKadi.Text = dataGridView1.Rows[0].Cells["adminkadi"].Value.ToString();
                textBoxParola.Text = dataGridView1.Rows[0].Cells["adminparola"].Value.ToString();
                textBoxemail.Text = dataGridView1.Rows[0].Cells["adminemail"].Value.ToString(); // yeni
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string secilenKadi = dataGridView1.SelectedRows[0].Cells["adminkadi"].Value.ToString();

                SqlCommand komut = new SqlCommand("UPDATE [admin] SET adminparola = @parola, adminemail = @email WHERE adminkadi = @kadi", baglanti);
                komut.Parameters.AddWithValue("@kadi", secilenKadi);
                komut.Parameters.AddWithValue("@parola", textBoxParola.Text);
                komut.Parameters.AddWithValue("@email", textBoxemail.Text); // yeni

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                KullaniciListele();
                MessageBox.Show("Admin bilgisi güncellendi.");
            }
            else
            {
                MessageBox.Show("Lütfen bir admin seçin.");
            }
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string secilenKadi = dataGridView1.SelectedRows[0].Cells["adminkadi"].Value.ToString();

                SqlCommand komut = new SqlCommand("DELETE FROM [admin] WHERE adminkadi = @kadi", baglanti);
                komut.Parameters.AddWithValue("@kadi", secilenKadi);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                KullaniciListele();
            }
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            adminyonetim frm = new adminyonetim();
            // Kullanıcı ekleme formunu açarken event'i abone ediyoruz
   
            frm.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBoxKadi.Text = dataGridView1.Rows[e.RowIndex].Cells["adminkadi"].Value.ToString();
                textBoxParola.Text = dataGridView1.Rows[e.RowIndex].Cells["adminparola"].Value.ToString();
               textBoxemail.Text = dataGridView1.Rows[e.RowIndex].Cells["adminemail"].Value.ToString(); // yeni
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void adminduzen_Activated(object sender, EventArgs e)
        {
            KullaniciListele();
        }
    }
}
