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
    public partial class satis_form : Form
    {
        public satis_form()
        {
            InitializeComponent();
        }

        #region tanımlamalar

        public string islemNumarasi;
        stok_form STOK_FORM = new stok_form();
        gramaj_gir_form GRAMAJ_FORM = new gramaj_gir_form();
        nakit_satis_form NAKİT_SATİS_FORM = new nakit_satis_form();
        kasa_form KASA_FORM = new kasa_form();
        adet_gir_form adetGirForm = new adet_gir_form();
        SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=AHSS_database;Integrated Security=True");// bu benim pc için.
        //SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-RPV5FRQ\\SQLEXPRESS;Initial Catalog=AHSS_database;Integrated Security=True");// bu yılmaz abinin için
        SqlCommand komut;
        SqlDataReader stokKarti;// POS SATIŞ METODLARINDA KULLANIYORUM.
        int listeyeEklenmisUrunIndexi = 0;// bunu satın alınacaklar listesine eklenmişmi diye kontrol ettiğimde gridviewde ki indexi atıyorum.
        List<SATIN_ALINACAK_URUN> satinAlinacakUrunler = new List<SATIN_ALINACAK_URUN>();// URUN tipinde List classı oluşturdum.
        STOK_KARTI stok_Karti = new STOK_KARTI();
        List<STOK_KARTI> hizli_urunler = new List<STOK_KARTI>();
        

        satis_yap satis = new satis_yap();




        #endregion



        

        void BarkodaBak(string gelenBarkod)
        {
            if (UrunStoktaVarmi(gelenBarkod))// ürün stok kartı varmı ve stokta varmı. ürün stokta var ise true döndürür diğer durumlarda false.
            {
                if (ListedeVarmi(gelenBarkod))// listede var ise. yani daha önceden barkod okutulup satın alınacaklar listesine eklenmiş ise.
                {
                    SatinAlinacaklarListesineEkle(true, stok_Karti);// urunStoktaVarmi metodunda stok var ise oluşturduğum stok_Karti classını bu metoda gönderiyorum.
                }
                else// listede yok ise. yani daha önceden barkod okutulup satın alınacaklar listesine eklenmemiş ise.
                {
                    SatinAlinacaklarListesineEkle(false, stok_Karti);
                }
            }
            SatisButonlariAktifPasif();
            BarkodTextBox.Clear();
            BarkodTextBox.Focus();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (char.IsLetter((char)e.KeyCode))// textboa sadece rakam girmek için
            {
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Enter)// barkodun okunup entere basıldıktan sonra işlemlerin başladığı yer.
            {
                if (BarkodTextBox.Text != string.Empty)
                {
                    BarkodaBak(BarkodTextBox.Text);
                    e.SuppressKeyPress = true;// klavyeden yada barkod okuyucudan enter e bastıktan sonra "bling" sesi gelmesin diye. tam olarak nasıl çalıştığını bilmiyorum.
                }
                else
                {
                    e.SuppressKeyPress = true;// klavyeden yada barkod okuyucudan enter e bastıktan sonra "bling" sesi gelmesin diye. tam olarak nasıl çalıştığını bilmiyorum.
                    //NakitSatisButton.PerformClick();
                    //NakitSatisYap();
                    NAKİT_SATİS_FORM.toplamTutar = float.Parse(ToplamFiyatTextBox.Text);
                    NAKİT_SATİS_FORM.urunler = satinAlinacakUrunler;
                    NAKİT_SATİS_FORM.ShowDialog();
                    if (NAKİT_SATİS_FORM.odemeAlindimi)// nakit satis formunda ödeme alındı ise 
                    {
                        // nakit satıs formunda ödeme alındı ise alanları temizliyorum.
                        AlanlariTemizle();// alanları temizleyip barkodtextboxuna focus yapıyor.
                        IslemNumarasiLabel.Text = IslemNumarasiniGetir().ToString();
                    }
                    BarkodTextBox.Focus();
                }
            }
            else if (e.KeyCode == Keys.F1)
            {
                NakitSatisButton.PerformClick();
            }
            else if (e.KeyCode == Keys.F2)
            {
                PosSatisButton.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                StirSilButton.PerformClick();
            }
            else if (e.KeyCode == Keys.F4)
            {
                SatirDuzeltButton.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
                SatisIptalButton.PerformClick();
            }
            else if (e.KeyCode == Keys.F6)
            {
                GiderKaydetButton.PerformClick();
            }
            else if (e.KeyCode == Keys.F9)
            {
                stokButton.PerformClick();
            }
            else if (e.KeyCode == Keys.F11)
            {
                KasaButton.PerformClick();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                BarkodTextBox.Clear();
                BarkodTextBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                YonTuslari(e.KeyCode);
            }
            else if (e.KeyCode == Keys.Up)
            {
                YonTuslari(e.KeyCode);
            }
            else if (e.KeyCode == Keys.Add)
            {
                BarkodTextBox.Clear();
                BarkodTextBox.Focus();
                if (UrunlerDataGridView.SelectedRows[0].Cells[3].Value.ToString() == "ADET")
                {
                    if (UrunlerDataGridView.Rows.Count < 1)
                    {
                        BarkodTextBox.Clear();
                        return;
                    }
                    BarkodaBak(UrunlerDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.KeyCode == Keys.Subtract)
            {
                //if (char.IsLetter((char)e.KeyCode))// textboa sadece rakam girmek için
                //{
                //    e.Handled = true;
                //}
                BarkodTextBox.Clear();
                BarkodTextBox.Focus();
                if (UrunlerDataGridView.SelectedRows[0].Cells[3].Value.ToString() == "ADET")
                {
                    if ((Convert.ToInt32(UrunlerDataGridView.SelectedRows[0].Cells[2].Value) - 1) > 0)
                    {
                        satinAlinacakUrunler[UrunlerDataGridView.SelectedRows[0].Index].Adet = (float)UrunlerDataGridView.SelectedRows[0].Cells[2].Value - 1;
                        UrunlerDataGridView.SelectedRows[0].Cells[2].Value = (float)UrunlerDataGridView.SelectedRows[0].Cells[2].Value - 1;
                    }
                    Toplamlar();
                    e.SuppressKeyPress = true;
                }
            }
        }
    

        public bool UrunStoktaVarmi(string bakilacakBarkod)
        {
            komut = new SqlCommand();
            komut.CommandText = "SELECT * FROM stok_kartlari WHERE barkod_no=@barkod_no";
            komut.Connection = baglanti;
            komut.Parameters.AddWithValue("@barkod_no", bakilacakBarkod);

            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }

            SqlDataReader sqlDataReader = komut.ExecuteReader();

            if (sqlDataReader.Read())
            {
                if (Convert.ToInt32(sqlDataReader["stok_adet"]) > 0)// stokta ürün var ise.
                {
                    // stok_karti classına atıyorum stok bilgilerini ki tekrar tekrar sql bağlantı yapıp data çekmeyeyim. yeni barkod sorgulayanakadar bu stok_karti classı değişmeyecek
                    //----------------------
                    stok_Karti.StokKodu = (int)sqlDataReader["stok_kod"];
                    stok_Karti.BarkodNumarasi = (string)sqlDataReader["barkod_no"];
                    stok_Karti.Adi = (string)sqlDataReader["stok_adi"];
                    stok_Karti.Birimi = (string)sqlDataReader["stok_birim"];
                    stok_Karti.Adet = float.Parse(sqlDataReader["stok_adet"].ToString());
                    stok_Karti.Grubu = (string)sqlDataReader["stok_grup"];
                    stok_Karti.AlisFiyati = float.Parse(sqlDataReader["alis_fiyat"].ToString());
                    stok_Karti.SatisFiyati = float.Parse(sqlDataReader["satis_fiyat"].ToString());
                    //------------------------

                    if (baglanti.State == ConnectionState.Open)
                    {
                        baglanti.Close();
                    }
                    return true;
                }
                else
                {
                    if (baglanti.State == ConnectionState.Open)
                    {
                        baglanti.Close();
                    }
                    MessageBox.Show("ÜRÜN STOĞU BİTMİŞ \n\nLİSTEYE EKLENMEDİ.", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
                MessageBox.Show("ÜRÜN BULUNAMADI. \n\nLİSTEYE EKLENMEDİ.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }


        public bool ListedeVarmi(string bakilacakBarkod)
        {
            for (int i = 0; i < satinAlinacakUrunler.Count; i++)
            {
                if (satinAlinacakUrunler[i].BarkodNumarasi == bakilacakBarkod)
                {
                    listeyeEklenmisUrunIndexi = i;
                    return true;
                }
            }
            return false;
        }

        public void SatinAlinacaklarListesineEkle(bool listedeVarmi, STOK_KARTI gelenStok_Karti)// datagridview eklenmesi ve satinAlinacaklarUrunler List classına eklenmesi.
        {
            if (listedeVarmi)// listede var ise
            {
                if ((satinAlinacakUrunler[listeyeEklenmisUrunIndexi].Adet + 1) <= gelenStok_Karti.Adet)// stoktan fazla okutmasın diye kontrol ediyorum. örn: stokta 2 adet varsa ve kasiyer ürünü 3 kere okutursa 3. okutmada stok kalmadı uyarısı versin.
                {
                    if (gelenStok_Karti.Birimi == "kg")// gramajlı ürünü aynı listede yokmuş gibi tekra ryeniden ekliyorum.
                    {
                        GRAMAJ_FORM.mevcutStokMiktari = gelenStok_Karti.Adet;
                        GRAMAJ_FORM.ShowDialog();
                        if (GRAMAJ_FORM.gramajGirildimi)// gramaj_form 'da gramaj girildiyse 
                        {
                            string gram = GRAMAJ_FORM.gramaj.ToString();
                            UrunlerDataGridView.Rows.Add(gelenStok_Karti.BarkodNumarasi, gelenStok_Karti.Adi, string.Format("{0:D3}", gram), gelenStok_Karti.Birimi.ToUpper(), gelenStok_Karti.SatisFiyati);// datagridview yeni ürün ekledim.
                            SATIN_ALINACAK_URUN urun = new SATIN_ALINACAK_URUN();// yeni ürün class oluşturdum.
                            urun.BarkodNumarasi = gelenStok_Karti.BarkodNumarasi;// bilgiler
                            urun.Adi = gelenStok_Karti.Adi;// bilgiler
                            urun.Birimi = gelenStok_Karti.Birimi;// bilgiler
                            urun.Adet = float.Parse(gram);//bilgiler
                            urun.AlisFiyati = gelenStok_Karti.AlisFiyati;// bilgiler
                            urun.SatisFiyati = gelenStok_Karti.SatisFiyati;//bilgiler
                            satinAlinacakUrunler.Add(urun);// satinAlinacakUrunler list classına yeni yukarda oluşturduğum ürün classını ekledim.
                        }
                    }
                    else
                    {
                        satinAlinacakUrunler[listeyeEklenmisUrunIndexi].Adet++;// list clasında ki ürün sayısına 1 ekledim.
                        UrunlerDataGridView.Rows[listeyeEklenmisUrunIndexi].Cells[2].Value = satinAlinacakUrunler[listeyeEklenmisUrunIndexi].Adet;// datagridview de ürünün sayısını 1 arttırdım.
                    }
                }
                else
                {
                    MessageBox.Show("MEVCUT STOK ADETİNİ AŞAMAZSINIZ", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BarkodTextBox.Focus();
                }
            }
            else// listede yok ise
            {
                if (gelenStok_Karti.Birimi.ToUpper() == "ADET")// ürün birimi adet ise
                {
                    UrunlerDataGridView.Rows.Add(gelenStok_Karti.BarkodNumarasi, gelenStok_Karti.Adi, 1, gelenStok_Karti.Birimi.ToUpper(), gelenStok_Karti.SatisFiyati);// datagridview yeni ürün ekledim.
                    SATIN_ALINACAK_URUN urun = new SATIN_ALINACAK_URUN();// yeni ürün class oluşturdum.
                    urun.BarkodNumarasi = gelenStok_Karti.BarkodNumarasi;// bilgiler
                    urun.Adi = gelenStok_Karti.Adi;// bilgiler
                    urun.Birimi = gelenStok_Karti.Birimi;// bilgiler
                    urun.Adet = 1;//bilgiler
                    urun.AlisFiyati = gelenStok_Karti.AlisFiyati;// bilgiler
                    urun.SatisFiyati = gelenStok_Karti.SatisFiyati;//bilgiler
                    satinAlinacakUrunler.Add(urun);// satinAlinacakUrunler list classına yeni yukarda oluşturduğum ürün classını ekledim.
                }
                else
                {
                    GRAMAJ_FORM.mevcutStokMiktari = gelenStok_Karti.Adet;
                    GRAMAJ_FORM.ShowDialog();
                    if (GRAMAJ_FORM.gramajGirildimi)// gramaj_form 'da gramaj girildiyse 
                    {
                        string gram = GRAMAJ_FORM.gramaj.ToString();
                        UrunlerDataGridView.Rows.Add(gelenStok_Karti.BarkodNumarasi, gelenStok_Karti.Adi, string.Format("{0:D3}", gram), gelenStok_Karti.Birimi.ToUpper(), gelenStok_Karti.SatisFiyati);// datagridview yeni ürün ekledim.
                        SATIN_ALINACAK_URUN urun = new SATIN_ALINACAK_URUN();// yeni ürün class oluşturdum.
                        urun.BarkodNumarasi = gelenStok_Karti.BarkodNumarasi;// bilgiler
                        urun.Adi = gelenStok_Karti.Adi;// bilgiler
                        urun.Birimi = gelenStok_Karti.Birimi;// bilgiler
                        urun.Adet = float.Parse(gram);//bilgiler
                        urun.AlisFiyati = gelenStok_Karti.AlisFiyati;// bilgiler
                        urun.SatisFiyati = gelenStok_Karti.SatisFiyati;//bilgiler
                        satinAlinacakUrunler.Add(urun);// satinAlinacakUrunler list classına yeni yukarda oluşturduğum ürün classını ekledim.
                    }
                }
            }
            Toplamlar();
            BarkodTextBox.Focus();
        }

        void Toplamlar()
        {
            float toplam = 0;
            for (int i = 0; i < satinAlinacakUrunler.Count; i++)
            {
                toplam += satinAlinacakUrunler[i].Adet * satinAlinacakUrunler[i].SatisFiyati;
            }
            NAKİT_SATİS_FORM.toplamTutar = toplam;// nakit satış ekranında göstermek için.

            ToplamFiyatTextBox.Text = string.Format("{0:#0.00}", toplam);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.Text = this.Width + "x" + this.Height;
        }

        private void Satis_form_Load(object sender, EventArgs e)
        {
            if (sqlBaglantiKontrol())
            {
                panel1.BackColor = Color.Green;
            }
            else
            {
                panel1.BackColor = Color.Red;
            }
            SatisButonlariAktifPasif();// başlangıçta satış butonlarını aktif pasif ayarlamak için.
            HizliUrunleriGetir();
            IslemNumarasiLabel.Text = IslemNumarasiniGetir().ToString();
            //BarkodTextBox.Multiline = true; //bunu entere basınca "bling" sesini yapmasın diye yaptım. çalıştı ama tam istediğim gibi olmadı.
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;

            BarkodTextBox.Focus();
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        public bool sqlBaglantiKontrol()// başlangıçta veritabanına bağlantı varmı yok mu kontrol etmek için kullanıyorum bir kere ye mahsus. bağlantı sağlıklı ise geri true döndürecek değilse false.
        {
            using (var ConnectionState = new SqlConnection(baglanti.ConnectionString))// using içinde kullanıyorum işim bitince bellekten otomatik silinsin diye
            {
                try
                {
                    ConnectionState.Open();
                    ConnectionState.Close();
                    return true;// bu satıra geldiyse bağlantı sağlıklı.
                }
                catch (Exception HataMesaji)
                {
                    MessageBox.Show("Veritabanına bağlanamadı.\nHata mesajı :\n" + HataMesaji.ToString(), "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
        }

        private void StokButton_Click(object sender, EventArgs e)
        {
            STOK_FORM.ShowDialog();
            BarkodTextBox.Focus();
        }

        private void SatisIptalButton_Click(object sender, EventArgs e)// satın alınacaklar ürün listesini ve datagridview temizlemek için.
        {
            DialogResult cevap = MessageBox.Show("SATIŞ İPTAL EDİLECEK.", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                AlanlariTemizle();
                BarkodTextBox.Focus();
            }
        }

        void AlanlariTemizle()
        {
            ToplamFiyatTextBox.Text = "0,00";
            UrunlerDataGridView.Rows.Clear();// datagridview deki satırları temizliyorum.
            UrunlerDataGridView.Refresh();// dataagridviewi yeniliyorum.
            satinAlinacakUrunler.Clear();// urunler List'i temizliyorum.
            SatisButonlariAktifPasif();// satis butonlarının aktif veya pasif durumlarını kontrol etmek için.
            BarkodTextBox.Focus();
        }

        private void NakitSatisButton_Click(object sender, EventArgs e)
        {
            //e.SuppressKeyPress = true;// klavyeden yada barkod okuyucudan enter e bastıktan sonra "bling" sesi gelmesin diye. tam olarak nasıl çalıştığını bilmiyorum.
            //NAKİT_SATİS_FORM.toplamTutar = float.Parse(ToplamFiyatTextBox.Text);
            //NAKİT_SATİS_FORM.urunler = satinAlinacakUrunler;
            //NAKİT_SATİS_FORM.ShowDialog();
            //if (NAKİT_SATİS_FORM.odemeAlindimi)// nakit satis formunda ödeme alındı ise 
            //{
            //    // nakit satıs formunda ödeme alındı ise alanları temizliyorum.
            //    AlanlariTemizle();// alanları temizleyip barkodtextboxuna focus yapıyor.
            //    IslemNumarasiLabel.Text = IslemNumarasiniGetir().ToString();
            //}
            //BarkodTextBox.Focus();
            NakitSatisYap();
        }

        public void NakitSatisYap()
        {
            NAKİT_SATİS_FORM.toplamTutar = float.Parse(ToplamFiyatTextBox.Text);
            NAKİT_SATİS_FORM.urunler = satinAlinacakUrunler;
            NAKİT_SATİS_FORM.ShowDialog();
            if (NAKİT_SATİS_FORM.odemeAlindimi)// nakit satis formunda ödeme alındı ise 
            {
                // nakit satıs formunda ödeme alındı ise alanları temizliyorum.
                AlanlariTemizle();// alanları temizleyip barkodtextboxuna focus yapıyor.
                IslemNumarasiLabel.Text = IslemNumarasiniGetir().ToString();
            }
            BarkodTextBox.Focus();
        }

        private void Satis_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (satinAlinacakUrunler.Count >= 1)// kapatırken işlemde bekleyen ürün var ise 
            {
                DialogResult cevap = MessageBox.Show("BEKLEYEN İŞLEM VAR. ÇIKMAK İSTEDİĞİNİZE EMİN MİSİNİZ?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.No)// soruya hayır der ise 
                {
                    e.Cancel = true;// event cansel true diyerek form closing işlemini iptal ediyorum.
                }
            }
        }

        private void PosSatisButton_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("POS ÖDEMESİ OLARAK KAYDEDİLECEK !!!", "DİKKAT", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                islemNumarasi = IslemNumarasiVer();// bunu aşağıdaki(UrunleriStoktanDus()) metodda kullanıyorum. burda yapıyorrum ki her seferinde yeni numara alıyor yoksa metodun içinde. birden fazla ürün olduğunda döngü her döndüğünde yenisini alıyor. almasın diye.
                UrunleriStoktanDus();
                AlanlariTemizle();// alanları temizleyip barkodtextboxuna focus yapıyor.
                IslemNumarasiLabel.Text = IslemNumarasiniGetir().ToString();
            }
            BarkodTextBox.Focus();
        }

        #region POS SATIŞ İŞLEMLERİ ----   BUNU İLERİDE BAŞKA BİR FORMA AKTARABİLİRİM.

        void StokHareketlerineKaydet(SATIN_ALINACAK_URUN urun)
        {
            komut = new SqlCommand();
            komut.Connection = baglanti;
            // işlemin stok hareketleri tablosuna eklenmesi ------------------------
            komut.CommandText = "INSERT INTO stok_hareketleri (barkod_no, islem_turu, islem_no, islem_odeme, islem_tarihi, islem_birim, islem_kar, miktar) values(@barkod_no, @islem_turu, @islem_no, @islem_odeme, @islem_tarihi, @islem_birim, @islem_kar, @miktar)";
            komut.Parameters.AddWithValue("@barkod_no", urun.BarkodNumarasi);
            komut.Parameters.AddWithValue("@islem_turu", "satis");
            komut.Parameters.AddWithValue("@islem_no", islemNumarasi);
            komut.Parameters.AddWithValue("@islem_odeme", "pos");
            komut.Parameters.AddWithValue("@islem_tarihi", DateTime.Now);
            komut.Parameters.AddWithValue("@islem_birim", urun.Birimi);
            komut.Parameters.AddWithValue("@islem_kar", (urun.SatisFiyati - urun.AlisFiyati) * urun.Adet);// satıştan yapılan Kârı hesaplayıp kaydediyorum sonradan kasa formunda hesaplaması kolay olsun diye.
            komut.Parameters.AddWithValue("@miktar", urun.Adet);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            // işlemin stok hareketleri tablosuna eklenmesi işlemi sonu -----------------------
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
                ayarlar.Default.islemNumarasi = 1;//  işlem numarasını sıfırlıyorum.
                ayarlar.Default.islemGunu = DateTime.Now.Date;// işlem tarihine yeni günün tarihini atıyorum.
                ayarlar.Default.Save();// ayarları ayarlar dosyasına kaydediyorum.
                sonIslemGunu = ayarlar.Default.islemGunu;// ayarlar dosyasından işlem tarihini alıyorum.
                enSonNumara = ayarlar.Default.islemNumarasi;// ayarlar dosyasından işlem numarasını alıyorum.
                sonuc = DateTime.Now.Date.ToString("ddMMyyyy") + ayarlar.Default.islemNumarasi.ToString();// yeni işlem tarihi ve numarasını sonuc değişkenine atıyorum.
                return sonuc;
            }
        }


        void UrunleriStoktanDus()
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


        #endregion


        public void SatisButonlariAktifPasif()// satinalinacaklar listesinde ürün yok ise satış butonlarını pasif, var ise aktif yapmak için.
        {
            if (satinAlinacakUrunler.Count >= 1)// satinalinacaklar list classında ürün yok ise uzunluğu 1'den düşük olacak.
            {
                NakitSatisButton.Enabled = true;
                PosSatisButton.Enabled = true;
                StirSilButton.Enabled = true;
                SatirDuzeltButton.Enabled = true;
                SatisIptalButton.Enabled = true;
                GiderKaydetButton.Enabled = true;
            }
            else
            {
                NakitSatisButton.Enabled = false;
                PosSatisButton.Enabled = false;
                StirSilButton.Enabled = false;
                SatirDuzeltButton.Enabled = false;
                SatisIptalButton.Enabled = false;
                GiderKaydetButton.Enabled = false;
            }
        }

        private void KasaButton_Click(object sender, EventArgs e)
        {
            KASA_FORM.ShowDialog();
            BarkodTextBox.Focus();
        }

        #region HIZLI ÜRÜN BUTONU EKLEME METODLARI
        void HizliUrunleriGetir()
        {
            komut = new SqlCommand();
            komut.CommandText = "SELECT * FROM stok_kartlari WHERE hizli_urun=@hizli_urun";
            komut.Parameters.AddWithValue("@hizli_urun", true);
            komut.Connection = baglanti;
            hizli_urunler = new List<STOK_KARTI>();
            baglanti.Open();
            SqlDataReader sqlDataReader = komut.ExecuteReader();
            while (sqlDataReader.Read())
            {
                STOK_KARTI stokKart = new STOK_KARTI();
                stokKart.Adi = sqlDataReader["stok_adi"].ToString();
                stokKart.BarkodNumarasi = sqlDataReader["barkod_no"].ToString();
                hizli_urunler.Add(stokKart);
            }
            baglanti.Close();
            HizliButtonlariAyarla(hizli_urunler);
        }

        void HizliButtonlariAyarla(List<STOK_KARTI> gelenHizli_urun)
        {
            int sayac = 0;
            foreach (var urun in gelenHizli_urun)
            {
                if (sayac > gelenHizli_urun.Count)
                {
                    return;
                }
                switch (sayac)
                {
                    case 0:
                        hizli_button1.Text = urun.Adi;
                        hizli_button1.Tag = urun.BarkodNumarasi;
                        break;
                    case 1:
                        hizli_button2.Text = urun.Adi;
                        hizli_button2.Tag = urun.BarkodNumarasi;
                        break;
                    case 2:
                        hizli_button3.Text = urun.Adi;
                        hizli_button3.Tag = urun.BarkodNumarasi;
                        break;
                    case 3:
                        hizli_button4.Text = urun.Adi;
                        hizli_button4.Tag = urun.BarkodNumarasi;
                        break;
                    case 4:
                        hizli_button5.Text = urun.Adi;
                        hizli_button5.Tag = urun.BarkodNumarasi;
                        break;
                    case 5:
                        hizli_button6.Text = urun.Adi;
                        hizli_button6.Tag = urun.BarkodNumarasi;
                        break;
                    case 6:
                        hizli_button7.Text = urun.Adi;
                        hizli_button7.Tag = urun.BarkodNumarasi;
                        break;
                    case 7:
                        hizli_button8.Text = urun.Adi;
                        hizli_button8.Tag = urun.BarkodNumarasi;
                        break;
                    case 8:
                        hizli_button9.Text = urun.Adi;
                        hizli_button9.Tag = urun.BarkodNumarasi;
                        break;
                    case 9:
                        hizli_button10.Text = urun.Adi;
                        hizli_button10.Tag = urun.BarkodNumarasi;
                        break;
                    case 10:
                        hizli_button11.Text = urun.Adi;
                        hizli_button11.Tag = urun.BarkodNumarasi;
                        break;
                    case 11:
                        hizli_button12.Text = urun.Adi;
                        hizli_button12.Tag = urun.BarkodNumarasi;
                        break;
                    case 12:
                        hizli_button13.Text = urun.Adi;
                        hizli_button13.Tag = urun.BarkodNumarasi;
                        break;
                    case 13:
                        hizli_button14.Text = urun.Adi;
                        hizli_button14.Tag = urun.BarkodNumarasi;
                        break;
                    case 14:
                        hizli_button15.Text = urun.Adi;
                        hizli_button15.Tag = urun.BarkodNumarasi;
                        break;
                    case 15:
                        hizli_button16.Text = urun.Adi;
                        hizli_button16.Tag = urun.BarkodNumarasi;
                        break;
                    case 16:
                        hizli_button17.Text = urun.Adi;
                        hizli_button17.Tag = urun.BarkodNumarasi;
                        break;
                    case 17:
                        hizli_button18.Text = urun.Adi;
                        hizli_button18.Tag = urun.BarkodNumarasi;
                        break;
                    case 18:
                        hizli_button19.Text = urun.Adi;
                        hizli_button19.Tag = urun.BarkodNumarasi;
                        break;
                    case 19:
                        hizli_button20.Text = urun.Adi;
                        hizli_button20.Tag = urun.BarkodNumarasi;
                        break;
                    case 20:
                        hizli_button21.Text = urun.Adi;
                        hizli_button21.Tag = urun.BarkodNumarasi;
                        break;
                    case 21:
                        hizli_button22.Text = urun.Adi;
                        hizli_button22.Tag = urun.BarkodNumarasi;
                        break;
                    case 22:
                        hizli_button23.Text = urun.Adi;
                        hizli_button23.Tag = urun.BarkodNumarasi;
                        break;
                    case 23:
                        hizli_button24.Text = urun.Adi;
                        hizli_button24.Tag = urun.BarkodNumarasi;
                        break;
                    case 24:
                        hizli_button25.Text = urun.Adi;
                        hizli_button25.Tag = urun.BarkodNumarasi;
                        break;
                    case 25:
                        hizli_button26.Text = urun.Adi;
                        hizli_button26.Tag = urun.BarkodNumarasi;
                        break;
                    case 26:
                        hizli_button27.Text = urun.Adi;
                        hizli_button27.Tag = urun.BarkodNumarasi;
                        break;
                    case 27:
                        hizli_button28.Text = urun.Adi;
                        hizli_button28.Tag = urun.BarkodNumarasi;
                        break;
                    case 28:
                        hizli_button29.Text = urun.Adi;
                        hizli_button29.Tag = urun.BarkodNumarasi;
                        break;
                    case 29:
                        hizli_button30.Text = urun.Adi;
                        hizli_button30.Tag = urun.BarkodNumarasi;
                        break;
                    case 30:
                        hizli_button31.Text = urun.Adi;
                        hizli_button31.Tag = urun.BarkodNumarasi;
                        break;
                    case 31:
                        hizli_button32.Text = urun.Adi;
                        hizli_button32.Tag = urun.BarkodNumarasi;
                        break;
                    case 32:
                        hizli_button33.Text = urun.Adi;
                        hizli_button33.Tag = urun.BarkodNumarasi;
                        break;
                    case 33:
                        hizli_button34.Text = urun.Adi;
                        hizli_button34.Tag = urun.BarkodNumarasi;
                        break;
                    case 34:
                        hizli_button35.Text = urun.Adi;
                        hizli_button35.Tag = urun.BarkodNumarasi;
                        break;
                    case 35:
                        hizli_button36.Text = urun.Adi;
                        hizli_button36.Tag = urun.BarkodNumarasi;
                        break;
                    case 36:
                        hizli_button37.Text = urun.Adi;
                        hizli_button37.Tag = urun.BarkodNumarasi;
                        break;
                    case 37:
                        hizli_button38.Text = urun.Adi;
                        hizli_button38.Tag = urun.BarkodNumarasi;
                        break;
                    case 38:
                        hizli_button39.Text = urun.Adi;
                        hizli_button39.Tag = urun.BarkodNumarasi;
                        break;
                    case 39:
                        hizli_button40.Text = urun.Adi;
                        hizli_button40.Tag = urun.BarkodNumarasi;
                        break;
                }
                sayac++;
            }
            sayac = 0;
        }

        #region Hızlı ürün ekleme buton click eventleri



        private void Hizli_button1_Click(object sender, EventArgs e)
        {
            if (hizli_button1.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button1.Tag);
            }
        }

        private void Hizli_button2_Click(object sender, EventArgs e)
        {
            if (hizli_button2.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button2.Tag);
            }
        }

        private void Hizli_button3_Click(object sender, EventArgs e)
        {
            if (hizli_button3.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button3.Tag);
            }
        }

        private void Hizli_button4_Click(object sender, EventArgs e)
        {
            if (hizli_button4.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button4.Tag);
            }
        }

        private void Hizli_button5_Click(object sender, EventArgs e)
        {
            if (hizli_button5.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button5.Tag);
            }
        }

        private void Hizli_button6_Click(object sender, EventArgs e)
        {
            if (hizli_button6.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button6.Tag);
            }
        }

        private void Hizli_button7_Click(object sender, EventArgs e)
        {
            if (hizli_button7.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button7.Tag);
            }
        }

        private void Hizli_button8_Click(object sender, EventArgs e)
        {
            if (hizli_button8.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button8.Tag);
            }
        }

        private void Hizli_button9_Click(object sender, EventArgs e)
        {
            if (hizli_button9.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button9.Tag);
            }
        }

        private void Hizli_button10_Click(object sender, EventArgs e)
        {
            if (hizli_button10.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button10.Tag);
            }
        }

        private void Hizli_button11_Click(object sender, EventArgs e)
        {
            if (hizli_button11.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button11.Tag);
            }
        }

        private void Hizli_button12_Click(object sender, EventArgs e)
        {
            if (hizli_button12.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button12.Tag);
            }
        }

        private void Hizli_button13_Click(object sender, EventArgs e)
        {
            if (hizli_button13.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button13.Tag);
            }
        }

        private void Hizli_button14_Click(object sender, EventArgs e)
        {
            if (hizli_button14.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button14.Tag);
            }
        }

        private void Hizli_button15_Click(object sender, EventArgs e)
        {
            if (hizli_button15.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button15.Tag);
            }
        }

        private void Hizli_button16_Click(object sender, EventArgs e)
        {
            if (hizli_button16.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button16.Tag);
            }
        }

        private void Hizli_button17_Click(object sender, EventArgs e)
        {
            if (hizli_button17.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button17.Tag);
            }
        }

        private void Hizli_button18_Click(object sender, EventArgs e)
        {
            if (hizli_button18.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button18.Tag);
            }
        }

        private void Hizli_button19_Click(object sender, EventArgs e)
        {
            if (hizli_button19.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button19.Tag);
            }
        }

        private void Hizli_button20_Click(object sender, EventArgs e)
        {
            if (hizli_button20.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button20.Tag);
            }
        }

        private void Hizli_button21_Click(object sender, EventArgs e)
        {
            if (hizli_button21.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button21.Tag);
            }
        }

        private void Hizli_button22_Click(object sender, EventArgs e)
        {
            if (hizli_button22.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button22.Tag);
            }
        }

        private void Hizli_button23_Click(object sender, EventArgs e)
        {
            if (hizli_button23.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button23.Tag);
            }
        }

        private void Hizli_button24_Click(object sender, EventArgs e)
        {
            if (hizli_button24.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button24.Tag);
            }
        }

        private void Hizli_button25_Click(object sender, EventArgs e)
        {
            if (hizli_button25.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button25.Tag);
            }
        }

        private void Hizli_button26_Click(object sender, EventArgs e)
        {
            if (hizli_button26.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button26.Tag);
            }
        }

        private void Hizli_button27_Click(object sender, EventArgs e)
        {
            if (hizli_button27.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button27.Tag);
            }
        }

        private void Hizli_button28_Click(object sender, EventArgs e)
        {
            if (hizli_button28.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button28.Tag);
            }
        }

        private void Hizli_button29_Click(object sender, EventArgs e)
        {
            if (hizli_button29.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button29.Tag);
            }
        }

        private void Hizli_button30_Click(object sender, EventArgs e)
        {
            if (hizli_button30.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button30.Tag);
            }
        }

        private void Hizli_button31_Click(object sender, EventArgs e)
        {
            if (hizli_button31.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button31.Tag);
            }
        }

        private void Hizli_button32_Click(object sender, EventArgs e)
        {
            if (hizli_button32.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button32.Tag);
            }
        }

        private void Hizli_button33_Click(object sender, EventArgs e)
        {
            if (hizli_button33.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button33.Tag);
            }
        }

        private void Hizli_button34_Click(object sender, EventArgs e)
        {
            if (hizli_button34.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button34.Tag);
            }
        }

        private void Hizli_button35_Click(object sender, EventArgs e)
        {
            if (hizli_button35.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button35.Tag);
            }
        }

        private void Hizli_button36_Click(object sender, EventArgs e)
        {
            if (hizli_button36.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button36.Tag);
            }
        }

        private void Hizli_button37_Click(object sender, EventArgs e)
        {
            if (hizli_button37.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button37.Tag);
            }
        }

        private void Hizli_button38_Click(object sender, EventArgs e)
        {
            if (hizli_button38.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button38.Tag);
            }
        }

        private void Hizli_button39_Click(object sender, EventArgs e)
        {
            if (hizli_button39.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button39.Tag);
            }
        }

        private void Hizli_button40_Click(object sender, EventArgs e)
        {
            if (hizli_button40.Tag.ToString() != string.Empty)
            {
                BarkodaBak((string)hizli_button40.Tag);
            }
        }


        #endregion
        #endregion


        private void StirSilButton_Click(object sender, EventArgs e)
        {
            if (UrunlerDataGridView.CurrentCell.RowIndex != -1 && UrunlerDataGridView.Rows.Count > 0)
            {
                DialogResult cevap = MessageBox.Show("SEÇİLİ SATIR SİLİNECEK", "DİKKAT", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (cevap == DialogResult.OK)
                {
                    satinAlinacakUrunler.RemoveAt(UrunlerDataGridView.SelectedRows[0].Index);
                    UrunlerDataGridView.Rows.RemoveAt(UrunlerDataGridView.SelectedRows[0].Index);
                    Toplamlar();
                    SatisButonlariAktifPasif();
                }
            }
            BarkodTextBox.Focus();
        }

        private void SatirDuzeltButton_Click(object sender, EventArgs e)
        {
            if ((string)UrunlerDataGridView.Rows[UrunlerDataGridView.SelectedRows[0].Index].Cells[3].Value == "ADET")
            {
                adetGirForm.adetKG = "ADET";
            }
            else
            {
                adetGirForm.adetKG = "KG";
            }

            adetGirForm.mevcutMiktar = (float)UrunlerDataGridView.SelectedRows[0].Cells[2].Value;
            adetGirForm.ShowDialog();

            if (adetGirForm.adetGirildimi)
            {
                satinAlinacakUrunler[UrunlerDataGridView.SelectedRows[0].Index].Adet = adetGirForm.girilenAdet;
                UrunlerDataGridView.Rows[UrunlerDataGridView.SelectedRows[0].Index].Cells[2].Value = adetGirForm.girilenAdet;
                UrunlerDataGridView.Refresh();
                Toplamlar();
                SatisButonlariAktifPasif();
            }
            BarkodTextBox.Focus();
        }

        private void GiderKaydetButton_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("GİDER OLARAK KAYDEDİLECEK !!!", "DİKKAT", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                islemNumarasi = IslemNumarasiVer();// bunu aşağıdaki(UrunleriStoktanDus()) metodda kullanıyorum. burda yapıyorrum ki her seferinde yeni numara alıyor yoksa metodun içinde. birden fazla ürün olduğunda döngü her döndüğünde yenisini alıyor. almasın diye.
                //UrunleriStoktanDus();
                satis.SatisYap("gider", "gider", satinAlinacakUrunler);
                AlanlariTemizle();// alanları temizleyip barkodtextboxuna focus yapıyor.
            }
            BarkodTextBox.Focus();
        }

        public int IslemNumarasiniGetir()
        {
            if (ayarlar.Default.islemGunu < DateTime.Now.Date)
            {
                ayarlar.Default.islemNumarasi = 0;
            }
            return Convert.ToInt32(ayarlar.Default.islemNumarasi);
        }

        void YonTuslari(Keys tus)// yön tuşları ile seçili olan satırı değiştirmek için.
        {
            if (UrunlerDataGridView.Rows.Count < 0) return;// hataya düşmesin diye.
            int CurrentIndex = UrunlerDataGridView.SelectedRows[0].Index;
            int NewIndex;
            UrunlerDataGridView.ClearSelection();
            if (tus == Keys.Up)
            {
                NewIndex = CurrentIndex - 1;
                if (NewIndex >= 0)
                {
                    UrunlerDataGridView.Rows[NewIndex].Selected = true;
                }
                else
                {
                    NewIndex++;
                    UrunlerDataGridView.Rows[NewIndex].Selected = true;
                }
            }
            else if (tus == Keys.Down)
            {
                NewIndex = CurrentIndex + 1;
                if (NewIndex < UrunlerDataGridView.RowCount)
                {
                    UrunlerDataGridView.Rows[NewIndex].Selected = true;
                }
                else
                {
                    NewIndex--;
                    UrunlerDataGridView.Rows[NewIndex].Selected = true;
                }
            }
        }

        
    }
}
