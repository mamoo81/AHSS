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
    public partial class stok_dus : Form
    {
        public stok_dus()
        {
            InitializeComponent();
        }

        #region tanımlamalar
        public string mevcutBarkod { get; set; }
        public string mevcutStokAdi { get; set; }
        public float mevcutStokAdeti { get; set; }

        SqlCommand komut;
        SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=AHSS_database;Integrated Security=True");// bu benim pc için.
        //SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-RPV5FRQ\\SQLEXPRESS;Initial Catalog=AHSS_database;Integrated Security=True");// bu yılmaz abinin için


        #endregion

        void StokDus()
        {
            DialogResult cevap = MessageBox.Show("Stok adetini girmek istediğinize emin misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                if (StokAdetTextBox.Text != "0")// sıfır yazarsa işlem yapmasın diye. stok hareketlerine de kaydediyor boşuna önüne geçiyorum.
                {
                    komut = new SqlCommand();
                    // stok kartı update başlangıç --------------------------------
                    komut.CommandText = "UPDATE stok_kartlari SET stok_adet=@stok_adet where barkod_no='" + mevcutBarkod + "'";
                    komut.Connection = baglanti;
                    komut.Parameters.AddWithValue("@stok_adet", mevcutStokAdeti - float.Parse(StokAdetTextBox.Text));// mevcut adeti yeni adet ile toplayarak DB yi güncelliyorum.
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    komut.ExecuteNonQuery();
                    // stok kartı update bitiş -----------------------------------

                    // işlemin stok hareketleri tablosuna eklenmesi ------------------------
                    komut.CommandText = "INSERT INTO stok_hareketleri (barkod_no, islem_turu, islem_tarihi, miktar, aciklama) values(@barkod_no, @islem_turu, @islem_tarihi, @miktar, @aciklama)";
                    komut.Parameters.AddWithValue("@barkod_no", mevcutBarkod);
                    komut.Parameters.AddWithValue("@islem_turu", "giris");
                    if (GirilecekStokTarihi.Value.Date == DateTime.Now.Date)
                    {
                        komut.Parameters.AddWithValue("@islem_tarihi", DateTime.Now).DbType = DbType.DateTime;
                    }
                    else
                    {
                        komut.Parameters.AddWithValue("@islem_tarihi", GirilecekStokTarihi.Value.Date).DbType = DbType.DateTime;
                    }
                    komut.Parameters.AddWithValue("@miktar", float.Parse(StokAdetTextBox.Text));
                    komut.Parameters.AddWithValue("@aciklama", AciklamaTextBox.Text);
                    komut.ExecuteNonQuery();
                    // işlemin stok hareketleri tablosuna eklenmesi işlemi sonu -----------------------

                    if (baglanti.State == ConnectionState.Open)
                    {
                        baglanti.Close();
                    }
                    MessageBox.Show("Stok düşüldü", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AlanlariTemizle();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Düşülecek stok adeti '0' olamaz", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        void AlanlariTemizle()
        {
            StokAdetTextBox.Text = string.Empty;
            StokAdiLabel.Text = string.Empty;
            BarkodNoLabel.Text = string.Empty;
            AciklamaTextBox.Text = string.Empty;
            GirilecekStokTarihi.Value = DateTime.Today;// datetimepickeri bugünün tarihine ayarlıyorum.
        }

        private void Stok_dus_Load(object sender, EventArgs e)
        {
            StokAdiLabel.Text = mevcutStokAdi;
            BarkodNoLabel.Text = mevcutBarkod;
            MevcutStokAdetLabel.Text = mevcutStokAdeti.ToString();
            GirilecekStokTarihi.Value = DateTime.Now;
        }

        private void StokDusButton_Click(object sender, EventArgs e)
        {
            StokDus();
        }

        private void StokAdetTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (float.Parse(StokAdetTextBox.Text) < mevcutStokAdeti)
                {
                    StokDusButton.PerformClick();
                    e.SuppressKeyPress = true;// klavyeden entere basınca "bling" diye ötmesini engelliyorum.
                }
                else
                {
                    MessageBox.Show("Stok miktarından fazlasını düşemezsiniz.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StokAdetTextBox.Focus();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void StokAdetTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))// textboxa sadece rakam girmek için
            {
                e.Handled = true;
            }
        }

        private void StokAdetTextBox_Leave(object sender, EventArgs e)
        {
            if (float.Parse(StokAdetTextBox.Text) < 0)// 0 dan küçükse mantıken negatif bir değerdir.
            {
                StokAdetTextBox.Text = (Convert.ToInt32(StokAdetTextBox.Text) * -1).ToString();// girilen adet negatif bir sayı ise onu pozitif yapıyorum.
            }
            if (float.Parse(StokAdetTextBox.Text) > mevcutStokAdeti)// girilen adet mevcut stok adetinden büyükse;
            {
                StokAdetTextBox.Text = mevcutStokAdeti.ToString();// mevcut stok adetini yazıyorum ki stoktaki adet negatif değere düşmesin 0 olsun diye.
            }
        }
    }
}
