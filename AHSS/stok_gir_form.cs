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
    public partial class stok_gir_form : Form
    {
        public stok_gir_form()
        {
            InitializeComponent();
        }

        #region tanımlamalar

        public string eklenecekStokBirimi { get; set; }
        public string eklenecekBarkodNo { get; set; }
        public string eklenecekStokAdi { get; set; }
        public float stokAdeti { get; set; }

        SqlCommand komut;
        SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=AHSS_database;Integrated Security=True");// bu benim pc için.
        //SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-RPV5FRQ\\SQLEXPRESS;Initial Catalog=AHSS_database;Integrated Security=True");// bu yılmaz abinin için


        #endregion



        private void Stok_gir_form_Load(object sender, EventArgs e)
        {
            StokAdiLabel.Text = eklenecekStokAdi;
            BarkodNoLabel.Text = eklenecekBarkodNo;
            MevcutStokAdetLabel.Text = stokAdeti.ToString();
            GirilecekStokTarihi.Value = DateTime.Now;
            if (eklenecekStokBirimi == "adet")
            {
                label3.Text = "Girilecek ADET";
            }
            else
            {
                label3.Text = "Girilecek KG";
            }
        }

        private void StokGirButton_Click(object sender, EventArgs e)
        {
            StokGir();
        }


        void StokGir()
        {
            DialogResult cevap = MessageBox.Show("Stok adetini girmek istediğinize emin misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                if (StokAdetTextBox.Text != "0")
                {
                    komut = new SqlCommand();
                    // stok kartı update başlangıç --------------------------------
                    komut.CommandText = "UPDATE stok_kartlari SET stok_adet=@stok_adet where barkod_no='" + eklenecekBarkodNo + "'";
                    komut.Connection = baglanti;
                    komut.Parameters.AddWithValue("@stok_adet", stokAdeti + float.Parse(StokAdetTextBox.Text));// mevcut adeti yeni adet ile toplayarak DB yi güncelliyorum.
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    komut.ExecuteNonQuery();
                    // stok kartı update bitiş -----------------------------------

                    // işlemin stok hareketleri tablosuna eklenmesi ------------------------
                    komut.CommandText = "INSERT INTO stok_hareketleri (barkod_no, islem_turu, islem_tarihi, miktar, aciklama) values(@barkod_no, @islem_turu, @islem_tarihi, @miktar, @aciklama)";
                    komut.Parameters.AddWithValue("@barkod_no", eklenecekBarkodNo);
                    komut.Parameters.AddWithValue("@islem_turu", "giris");
                    if (GirilecekStokTarihi.Value.Date == DateTime.Now.Date)
                    {
                        komut.Parameters.AddWithValue("@islem_tarihi", DateTime.Now);
                    }
                    else
                    {
                        komut.Parameters.AddWithValue("@islem_tarihi", GirilecekStokTarihi.Value.Date);
                    }
                    komut.Parameters.AddWithValue("@miktar", float.Parse(StokAdetTextBox.Text));
                    komut.Parameters.AddWithValue("@aciklama", AciklamaTextBox.Text);
                    komut.ExecuteNonQuery();
                    // işlemin stok hareketleri tablosuna eklenmesi işlemi sonu -----------------------

                    if (baglanti.State == ConnectionState.Open)
                    {
                        baglanti.Close();
                    }
                    MessageBox.Show("Stok girildi", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AlanlariTemizle();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Girilecek stok adeti '0' olamaz.\nLütfen pozitif bir değer girin.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void StokAdetTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))// textboxa sadece rakam girmek için
            {
                e.Handled = true;
            }
        }

        private void StokAdetTextBox_Leave(object sender, EventArgs e)// negatif değer mi diye bakmak için kullanıyorum.
        {
            if (float.Parse(StokAdetTextBox.Text) < 0)// 0 dan küçükse mantıken negatif bir değerdir.
            {
                StokAdetTextBox.Text = (float.Parse(StokAdetTextBox.Text) * -1).ToString();// girilen adet, negatif bir sayı ise onu pozitif yapıyorum.
            }
            //if (Convert.ToInt32(StokAdetTextBox.Text) > MevcutStokAdeti)// girilen adet mevcut stok adetinden büyükse;
            //{
            //    StokAdetTextBox.Text = MevcutStokAdeti.ToString();// mevcut stok adetini yazıyorum ki stoktaki adet negatif değere düşmesin 0 olsun diye.
            //}
        }

        private void StokAdetTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                StokGirButton.PerformClick();
                e.SuppressKeyPress = true;// klavyeden entere basınca "bling" diye ötmesini engelliyorum.
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
