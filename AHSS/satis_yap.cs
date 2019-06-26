using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AHSS
{
    public class satis_yap
    {
        //SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=AHSS_database;Integrated Security=True");// bu benim pc için.
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-RPV5FRQ\\SQLEXPRESS;Initial Catalog=AHSS_database;Integrated Security=True");
        SqlCommand komut;
        SqlDataReader stokKarti;

        string gelenOdemeTipi;
        string gelenIslemTipi;

        string islemNumarasi;
        public void SatisYap(string odemeTipi, string islemTipi, List<SATIN_ALINACAK_URUN> urunler)
        {
            gelenOdemeTipi = odemeTipi;
            gelenIslemTipi = islemTipi;
            komut = new SqlCommand();

            switch (odemeTipi)
            {
                case "nakit":

                    break;
                case "pos":

                    break;
                case "gider":
                    islemNumarasi = IslemNumarasiVer();
                    UrunleriStoktanDus(urunler);
                    break;
            }
        }


        public string IslemNumarasiVer()// stok_hareketleri ne kaydederken işlem numarası vermek için. ileride cari karta kaydederken, yaptığı alışveriş te neleri satın aldığını görmemi sağlayacak.
        {
            DateTime sonIslemGunu = ayarlar.Default.islemGunu;// ayarlar dosyasından işlem tarihini alıyorum.
            int enSonNumara = ayarlar.Default.islemNumarasi;// ayarlar dosyasından işlem numarasını alıyorum.
            string sonuc;
            if (sonIslemGunu.Date == DateTime.Today.Date)// son işlem günü bugün ile aynı ise 
            {
                enSonNumara++;// işlem numarasını +1 arttırıyorum.
                sonuc = DateTime.Now.Date.ToString("ddMMyyyy") + enSonNumara;// sonuc değişkenine tarih ve işlemnumarasını birleştirip kaydediyorum.
                ayarlar.Default.islemNumarasi = enSonNumara;// ayarlar dosyasına en son numarayı kaydediyorum.
                //ayarlar.Default.islemGunu = DateTime.Now.AddDays(-1);// test ederken lazım dı. finalde lazım değil.
                ayarlar.Default.Save();// ayarlar dosyasına kaydediyorum.
                return sonuc;
            }
            else// yeni bir güne geçildi ise
            {
                ayarlar.Default.islemNumarasi = 0;//  işlem numarasını sıfırlıyorum.
                ayarlar.Default.islemGunu = DateTime.Now.Date;// işlem tarihine yeni günün tarihini atıyorum.
                ayarlar.Default.Save();// ayarları ayarlar dosyasına kaydediyorum.
                sonIslemGunu = ayarlar.Default.islemGunu;// ayarlar dosyasından işlem tarihini alıyorum.
                enSonNumara = ayarlar.Default.islemNumarasi;// ayarlar dosyasından işlem numarasını alıyorum.
                sonuc = DateTime.Now.Date.ToString("ddMMyyyy") + ayarlar.Default.islemNumarasi.ToString();// yeni işlem tarihi ve numarasını sonuc değişkenine atıyorum.
                return sonuc;
            }
        }

        void UrunleriStoktanDus(List<SATIN_ALINACAK_URUN> satinAlinacakUrunler)
        {
            for (int i = 0; i < satinAlinacakUrunler.Count; i++)
            {
                // mevcut stokta ki adeti bulma başlangıç----------------------
                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT stok_adet FROM stok_kartlari WHERE barkod_no=@barkod_no";
                komut.Parameters.AddWithValue("@barkod_no", satinAlinacakUrunler[i].BarkodNumarasi);
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
                komut.Parameters.AddWithValue("@stok_adet", adet - satinAlinacakUrunler[i].Adet);
                komut.Parameters.AddWithValue("@barkod_no", satinAlinacakUrunler[i].BarkodNumarasi);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                // stok kartından satın alınan adet kadar düşme bitiş ---------------------------

                StokHareketlerineKaydet(satinAlinacakUrunler[i]);// stok hareketlerine kaydedilmesi.
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
            komut.Parameters.AddWithValue("@islem_odeme", gelenOdemeTipi);
            komut.Parameters.AddWithValue("@islem_tarihi", DateTime.Now);
            komut.Parameters.AddWithValue("@islem_birim", urun.Birimi);
            komut.Parameters.AddWithValue("@islem_kar", (urun.SatisFiyati - urun.AlisFiyati) * urun.Adet);// satıştan yapılan Kârı hesaplayıp kaydediyorum sonradan kasa formunda hesaplaması kolay olsun diye.
            komut.Parameters.AddWithValue("@miktar", urun.Adet);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            // işlemin stok hareketleri tablosuna eklenmesi işlemi sonu -----------------------
        }

    }
}
