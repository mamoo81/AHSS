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
    public partial class nakit_satis_form : Form
    {
        public nakit_satis_form()
        {
            InitializeComponent();
        }

        public float odenenPara = 0;
        public float paraUstu = 0;
        public float toplamTutar = 0;
        public string islemNumarasi;
        public bool odemeAlindimi = false;
        public List<SATIN_ALINACAK_URUN> urunler = new List<SATIN_ALINACAK_URUN>();
        SqlCommand komut;
        SqlDataReader stokKarti;
        //SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=AHSS_database;Integrated Security=True");// bu benim pc için.
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-RPV5FRQ\\SQLEXPRESS;Initial Catalog=AHSS_database;Integrated Security=True");// bu yılmaz abinin için
        private void Nakit_satis_form_Load(object sender, EventArgs e)
        {
            ToplamLabel.Text = string.Format("{0:C}", toplamTutar);
            odemeAlindimi = false;
        }

        #region para resimlerine tıklama olayları
        private void ParaUstuSilButton_Click(object sender, EventArgs e)
        {
            OdenenLabel.Text = "₺0,00";
            odenenPara = 0;
            ParaUstuHesapla();
        }

        private void PictureBox200TL_Click(object sender, EventArgs e)
        {
            odenenPara += 200;
            OdenenLabel.Text = string.Format("{0:C}", odenenPara);
            ParaUstuHesapla();
        }

        private void PictureBox100TL_Click(object sender, EventArgs e)
        {
            odenenPara += 100;
            OdenenLabel.Text = string.Format("{0:C}", odenenPara);
            ParaUstuHesapla();
        }

        private void PictureBox50TL_Click(object sender, EventArgs e)
        {
            odenenPara += 50;
            OdenenLabel.Text = string.Format("{0:C}", odenenPara);
            ParaUstuHesapla();
        }

        private void PictureBox20TL_Click(object sender, EventArgs e)
        {
            odenenPara += 20;
            OdenenLabel.Text = string.Format("{0:C}", odenenPara);
            ParaUstuHesapla();
        }

        private void PictureBox10TL_Click(object sender, EventArgs e)
        {
            odenenPara += 10;
            OdenenLabel.Text = string.Format("{0:C}", odenenPara);
            ParaUstuHesapla();
        }

        private void PictureBox5TL_Click(object sender, EventArgs e)
        {
            odenenPara += 5;
            OdenenLabel.Text = string.Format("{0:C}", odenenPara);
            ParaUstuHesapla();
        }
        #endregion

        #region KeyDown Olayları
        private void Nakit_satis_form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                odenenPara = 0;
                OdenenLabel.Text = string.Format("{0:C}", odenenPara);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.NumPad7)
            {
                odenenPara += 200;
                OdenenLabel.Text = string.Format("{0:C}", odenenPara);
            }
            else if (e.KeyCode == Keys.NumPad4)
            {
                odenenPara += 100;
                OdenenLabel.Text = string.Format("{0:C}", odenenPara);
            }
            else if (e.KeyCode == Keys.NumPad1)
            {
                odenenPara += 50;
                OdenenLabel.Text = string.Format("{0:C}", odenenPara);
            }
            else if (e.KeyCode == Keys.NumPad9)
            {
                odenenPara += 20;
                OdenenLabel.Text = string.Format("{0:C}", odenenPara);
            }
            else if (e.KeyCode == Keys.NumPad6)
            {
                odenenPara += 10;
                OdenenLabel.Text = string.Format("{0:C}", odenenPara);
            }
            else if (e.KeyCode == Keys.NumPad3)
            {
                odenenPara += 5;
                OdenenLabel.Text = string.Format("{0:C}", odenenPara);
            }

            ParaUstuHesapla();
        }
        #endregion

        void ParaUstuHesapla()
        {
            if (odenenPara > toplamTutar)
            {
                ParaUstuLabel.Text = string.Format("{0:C}", odenenPara - toplamTutar);
                OdenenLabel.ForeColor = Color.Green;
            }
            else
            {
                ParaUstuLabel.Text = "₺0,00";
                OdenenLabel.ForeColor = Color.Red;
            }
        }
        void AlanlariTemizle()
        {
            odenenPara = 0;
            OdenenLabel.Text = "0,00";
            paraUstu = 0;
            ParaUstuLabel.Text = "0,00";
        }
        void UrunleriStoktanDus()
        {
            for (int i = 0; i < urunler.Count; i++)
            {
                // mevcut stokta ki adeti bulma başlangıç----------------------
                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT stok_adet FROM stok_kartlari WHERE barkod_no=@barkod_no";
                komut.Parameters.AddWithValue("@barkod_no", urunler[i].BarkodNumarasi);
                baglanti.Open();
                stokKarti = komut.ExecuteReader();
                stokKarti.Read();
                float adet = float.Parse(stokKarti["stok_adet"].ToString());
                baglanti.Close();
                // mevcut stokta ki adeti bulma bitiş----------------------

                // stok kartından satın alınan adet kadar düşme başlangıç ---------------------------
                komut = new SqlCommand();
                komut.CommandText = "UPDATE stok_kartlari SET stok_adet=@stok_adet WHERE barkod_no=@barkod_no";
                komut.Connection = baglanti;
                komut.Parameters.AddWithValue("@stok_adet", adet - urunler[i].Adet);
                komut.Parameters.AddWithValue("@barkod_no", urunler[i].BarkodNumarasi);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                // stok kartından satın alınan adet kadar düşme bitiş ---------------------------

                StokHareketlerineKaydet(urunler[i]);// stok hareketlerine kaydedilmesi.
            }
        }
        
        void StokHareketlerineKaydet(SATIN_ALINACAK_URUN urun)
        {
            komut = new SqlCommand();
            komut.Connection = baglanti;
            // işlemin stok hareketleri tablosuna eklenmesi ------------------------
            komut.CommandText = "INSERT INTO stok_hareketleri (barkod_no, islem_turu, islem_no, islem_odeme, islem_tarihi, islem_birim, islem_kar, miktar) values(@barkod_no, @islem_turu, @islem_no, @islem_odeme, @islem_tarihi, @islem_birim, @islem_kar, @miktar)";
            komut.Parameters.AddWithValue("@barkod_no", urun.BarkodNumarasi);
            komut.Parameters.AddWithValue("@islem_turu", "satis");
            komut.Parameters.AddWithValue("@islem_no", islemNumarasi);
            komut.Parameters.AddWithValue("@islem_odeme", "nakit");
            komut.Parameters.AddWithValue("@islem_tarihi", DateTime.Now);
            komut.Parameters.AddWithValue("@islem_birim", urun.Birimi);
            komut.Parameters.AddWithValue("@islem_kar", (urun.SatisFiyati - urun.AlisFiyati) * urun.Adet);// satıştan yapılan Kârı hesaplayıp kaydediyorum sonradan kasa formunda hesaplaması kolay olsun diye.
            komut.Parameters.AddWithValue("@miktar", urun.Adet);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            // işlemin stok hareketleri tablosuna eklenmesi işlemi sonu -----------------------
        }


        private void SatisYapButton_Click(object sender, EventArgs e)
        {
            islemNumarasi = IslemNumarasiVer();// bunu aşağıdaki(UrunleriStoktanDus()) metodda kullanıyorum. burda yapıyorrum ki urunleristoktandus() metodu içinde ki for her seferinde yeni numara alıyor yoksa metodun içinde. birden fazla ürün olduğunda döngü her döndüğünde yenisini alıyor. almasın diye.
            UrunleriStoktanDus();
            AlanlariTemizle();
            odemeAlindimi = true;
            this.Close();
        }


        public string IslemNumarasiVer()
        {
            DateTime sonIslemGunu = ayarlar.Default.islemGunu;
            int enSonNumara = ayarlar.Default.islemNumarasi;
            string sonuc = string.Empty;
            if (sonIslemGunu.Date == DateTime.Today.Date)
            {
                enSonNumara++;
                sonuc = DateTime.Now.Date.ToString("ddMMyyyy") + enSonNumara;
                ayarlar.Default.islemNumarasi = enSonNumara;
                //ayarlar.Default.islemGunu = DateTime.Now.AddDays(-1);// test ederken lazım dı. finalde lazım değil.
                ayarlar.Default.Save();
                return sonuc;
            }
            else
            {
                ayarlar.Default.islemNumarasi = 0;
                ayarlar.Default.islemGunu = DateTime.Now.Date;
                ayarlar.Default.Save();
                sonuc = ayarlar.Default.islemGunu.ToString("ddMMyyyy") + ayarlar.Default.islemNumarasi.ToString();
            }
            return sonuc;
        }

        private void KapatButton_Click(object sender, EventArgs e)
        {
            odemeAlindimi = false;
            AlanlariTemizle();
            this.Close();
        }
    }
}
