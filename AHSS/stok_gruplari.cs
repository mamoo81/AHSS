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

namespace AHSS
{
    public partial class stok_gruplari : Form
    {
        public stok_gruplari()
        {
            InitializeComponent();
        }

        //SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=AHSS_database;Integrated Security=True");// bu benim pc için.
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-RPV5FRQ\\SQLEXPRESS;Initial Catalog=AHSS_database;Integrated Security=True");// bu yılmaz abinin için
        SqlCommand komut;

        public bool StokGrupVarmi(string StokGrup)
        {
            komut = new SqlCommand();
            komut.CommandText = "select stok_grup from stok_gruplari";
            komut.Connection = baglanti;
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            using (SqlDataReader sqlDataReader = komut.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    if (sqlDataReader["stok_grup"].ToString() == StokGrup.ToUpper())
                    {
                        return true;
                    }
                }
            }
            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }
            return false;
        }

        public void GruplarıGetir()
        {
            StokGrupListBox.Items.Clear();
            komut = new SqlCommand();
            komut.CommandText = "select * from stok_gruplari";
            komut.Connection = baglanti;
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            using (SqlDataReader sqlDataReader = komut.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    StokGrupListBox.Items.Add(sqlDataReader["stok_grup"]);
                }
            }
            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }
        }
        private void EkleButton_Click(object sender, EventArgs e)
        {
            DialogResult Cevap = MessageBox.Show("Yeni stok gurubunu eklemek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Cevap == DialogResult.Yes)
            {
                if (!(StokGrupVarmi(StokGrupTextBox.Text)))
                {
                    komut = new SqlCommand();
                    komut.CommandText = "insert into stok_gruplari (stok_grup) values(@stok_grup)";
                    komut.Connection = baglanti;
                    komut.Parameters.AddWithValue("@stok_grup", StokGrupTextBox.Text.ToUpper());
                    if (baglanti.State != ConnectionState.Open)
                    {
                        baglanti.Open();
                    }
                    komut.ExecuteNonQuery();
                    if (baglanti.State == ConnectionState.Open)
                    {
                        baglanti.Close();
                    }
                    GruplarıGetir();
                }
                else
                {
                    MessageBox.Show("Bu stok gurubu zaten mevcut. Başka bir grup ismi belirleyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                StokGrupTextBox.Clear();
            }
        }

        private void CikarButton_Click(object sender, EventArgs e)
        {
            DialogResult Cevap = MessageBox.Show("Yeni stok gurubunu silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Cevap == DialogResult.Yes)
            {
                komut = new SqlCommand();
                komut.CommandText = "delete from stok_gruplari where stok_grup=@stok_grup";
                komut.Connection = baglanti;
                komut.Parameters.AddWithValue("@stok_grup", StokGrupListBox.SelectedItem.ToString());
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                komut.ExecuteNonQuery();
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
                StokGrupTextBox.Clear();
                StokGrupListBox.Items.RemoveAt(StokGrupListBox.SelectedIndex);
            }
        }

        private void Stok_gruplari_Load(object sender, EventArgs e)
        {
            GruplarıGetir();
            StokGrupTextBox.Focus();
        }

        private void Stok_gruplari_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!(StokGrupTextBox.Text.Length < 5) && StokGrupTextBox.Text != string.Empty)
                {
                    EkleButton.PerformClick();
                }
            }
        }

    }
}
