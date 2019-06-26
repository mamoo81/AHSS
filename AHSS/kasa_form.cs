using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AHSS
{
    public partial class kasa_form : Form
    {
        public kasa_form()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=AHSS_database;Integrated Security=True");// bu benim pc için.
        //SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-RPV5FRQ\\SQLEXPRESS;Initial Catalog=AHSS_database;Integrated Security=True");// bu yılmaz abinin için
        SqlCommand komut;
        DataTable sqlDataTable;

        List<decimal> yapilanKar = new List<decimal>();

        private void Kasa_form_Load(object sender, EventArgs e)
        {
            BaslangicTarihi.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);// içinde bulunduğumuz ayın ilk gününü datetimepickere giriyorum.
            BitisTarihi.Value = DateTime.Now.Date;// bugünü datetimepickere giriyorum.
            KazancRadioButton.Checked = true;
            KazancLabel.Text = "₺0,00";
        }

        private void GosterButton_Click(object sender, EventArgs e)
        {
            SatislariCek(BaslangicTarihi.Value, BitisTarihi.Value);
        }

        void SatislariCek(DateTime ilkTarih, DateTime sonTarih)
        {
            komut = new SqlCommand();
            if (KazancRadioButton.Enabled)
            {
                komut.CommandText = "SELECT * FROM stok_hareketleri WHERE islem_turu='satis' and islem_tarihi >= @ilk_tarih AND islem_tarihi <= @son_tarih";
            }
            else if (GiderRadioButton.Enabled)
            {
                komut.CommandText = "SELECT * FROM stok_hareketleri WHERE islem_turu='gider' AND islem_odeme='gider' and islem_tarihi >= @ilk_tarih AND islem_tarihi <= @son_tarih";
            }
            komut.Parameters.AddWithValue("@ilk_tarih", ilkTarih);
            komut.Parameters.AddWithValue("@son_tarih", sonTarih);
            komut.Connection = baglanti;
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            sqlDataTable = new DataTable();
            da.Fill(sqlDataTable);
            baglanti.Close();
            Hesapla(sqlDataTable);
        }

        void Hesapla(DataTable satislar)
        {
            for (int i = 0; i < satislar.Rows.Count; i++)
            {
                yapilanKar.Add((decimal)satislar.Rows[i]["islem_kar"]);
            }
            KazancLabel.Text = string.Format("{0:C}", yapilanKar.Sum());
            yapilanKar.Clear();
        }

        private void BaslangicTarihi_ValueChanged(object sender, EventArgs e)
        {
            if (BaslangicTarihi.Value > DateTime.Today)// ilk tarih, bugünden büyük ise
            {
                BaslangicTarihi.Value = DateTime.Today;
            }
        }

        private void BitisTarihi_ValueChanged(object sender, EventArgs e)
        {
            if (BitisTarihi.Value < BaslangicTarihi.Value)// son tarih, ilk tarihten küçük ise
            {
                BitisTarihi.Value = BaslangicTarihi.Value;
            }
        }
    }
}
