using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AHSS
{
    public class stokDrurumKontrol
    {
        //SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=AHSS_database;Integrated Security=True");// bu benim pc için.
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-RPV5FRQ\\SQLEXPRESS;Initial Catalog=AHSS_database;Integrated Security=True");// bu yılmaz abinin için
        SqlCommand komut;
        List<STOK_KARTI> stokKartlari = new List<STOK_KARTI>();

        public List<STOK_KARTI> StokBitenUrunler()
        {
            




            return stokKartlari;
        }








    }
}
