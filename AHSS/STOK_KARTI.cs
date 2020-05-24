using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AHSS
{
    public class STOK_KARTI
    {

        SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=AHSS_database;Integrated Security=True");
        //SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-RPV5FRQ\\SQLEXPRESS;Initial Catalog=AHSS_database;Integrated Security=True");
        SqlCommand komut;

        public string BarkodNumarasi { get; set; }
        public int StokKodu { get; set; }
        public string Adi { get; set; }
        public string Birimi { get; set; }
        public string Grubu { get; set; }
        public float Adet { get; set; }
        public float AlisFiyati { get; set; }
        public float SatisFiyati { get; set; }
        public int KdvYuzde { get; set; }
        public int OtvYuzde { get; set; }
        public bool KdvDahilmi { get; set; }
        public bool OtvDahilmi { get; set; }
        public int IndirimOrani { get; set; }
        public bool HizliSatis { get; set; }


        /// <summary>
        /// Girilen bilgiler ile yeni stok kartı açıp kaydeder.
        /// </summary>
        /// <param name="urun"> ürün tipinde nesne alır ve veritabanına kaydeder.</param>
        public void YeniKaydet(urun urun)
        {
            komut = new SqlCommand();
            komut.CommandText = "INSERT INTO stok_kartlari (barkod_no, stok_adi, stok_adet, stok_birim, stok_grup, alis_fiyat, satis_fiyat, kdv, kdv_dahil, otv, otv_dahil, indirim, hizli_urun) VALUES (@barkod_no, @stok_adi, @stok_adet, @stok_birim, @stok_grup, @alis_fiyat, @satis_fiyat, @kdv, @kdv_dahil, @otv, @otv_dahil, @indirim, @hizli_urun)";
            komut.Connection = baglanti;
            komut.Parameters.AddWithValue("@barkod_no", urun.BarkodNumarasi);
            komut.Parameters.AddWithValue("@stok_adi", urun.Adi.ToUpper());
            komut.Parameters.AddWithValue("@stok_adet", urun.Adet);
            komut.Parameters.AddWithValue("@stok_birim", urun.Birimi.ToUpper());
            komut.Parameters.AddWithValue("@stok_grup", urun.Grubu.ToUpper());
            komut.Parameters.AddWithValue("@alis_fiyat", urun.AlisFiyati);
            komut.Parameters.AddWithValue("@satis_fiyat", urun.SatisFiyati);
            komut.Parameters.AddWithValue("@kdv", urun.KdvYuzde);
            komut.Parameters.AddWithValue("@kdv_dahil", urun.KdvDahilmi);
            komut.Parameters.AddWithValue("@otv", urun.OtvYuzde);
            komut.Parameters.AddWithValue("@otv_dahil", urun.OtvDahilmi);
            komut.Parameters.AddWithValue("@indirim", urun.IndirimOrani);
            komut.Parameters.AddWithValue("@hizli_urun", urun.HizliSatis);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        public void Duzenle(urun urun)
        {
            komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "UPDATE stok_kartlari SET barkod_no=@barkod, stok_adi=@stok_adi, stok_adet=@stok_adet, stok_birim=@stok_birim, stok_grup=@stok_grup, alis_fiyat=@alis_fiyat, satis_fiyat=@satis_fiyat, kdv=@kdv, kdv_dahil=@kdv_dahil, otv=@otv, otv_dahil=@otv_dahil, indirim=@indirim, hizli_urun=@hizli_urun WHERE barkod_no=@barkod";
            komut.Parameters.AddWithValue("@barkod", urun.BarkodNumarasi);
            komut.Parameters.AddWithValue("@stok_adi", urun.Adi.ToUpper());
            komut.Parameters.AddWithValue("@stok_adet", urun.Adet);
            komut.Parameters.AddWithValue("@stok_birim", urun.Birimi.ToUpper());
            komut.Parameters.AddWithValue("@stok_grup", urun.Grubu.ToUpper());
            komut.Parameters.AddWithValue("@alis_fiyat", urun.AlisFiyati);
            komut.Parameters.AddWithValue("@satis_fiyat", urun.SatisFiyati);
            komut.Parameters.AddWithValue("@kdv", urun.KdvYuzde);
            komut.Parameters.AddWithValue("@kdv_dahil", urun.KdvDahilmi);
            komut.Parameters.AddWithValue("@otv", urun.OtvYuzde);
            komut.Parameters.AddWithValue("@otv_dahil", urun.OtvDahilmi);
            komut.Parameters.AddWithValue("@indirim", urun.IndirimOrani);
            komut.Parameters.AddWithValue("@hizli_urun", urun.HizliSatis);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        public void Sil(urun urun, bool stokHareketleriSilinsinmi)
        {
            komut = new SqlCommand();

            // stok kartını silme kısmı

            komut.CommandText = "delete from stok_kartlari where barkod_no=@barkod";
            komut.Parameters.AddWithValue("@barkod", urun.BarkodNumarasi);
            komut.Connection = baglanti;
            baglanti.Open();
            komut.ExecuteNonQuery();

            // stok hareketlerini silme kısmı
            if (stokHareketleriSilinsinmi)
            {
                komut.CommandText = "delete from stok_hareketleri where barkod_no=@barkod";
                komut.Parameters.AddWithValue("@barkod", urun.BarkodNumarasi);
                komut.ExecuteNonQuery();
            }
            
            baglanti.Close();
        }


        public void AdetGir(urun urun, float girilecekAdet)
        {

        }

        public void AdetDus(urun urun, float dusulecekAdet)
        {

        }
    }



}
