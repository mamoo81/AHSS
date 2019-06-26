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
    public partial class stok_form : Form
    {
        public stok_form()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = true;
        }
        stok_gruplari stok_Gruplari_Form = new stok_gruplari();
        stok_gir_form stok_Gir_Form = new stok_gir_form();
        stok_dus stok_Dus_form = new stok_dus();
        SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=AHSS_database;Integrated Security=True");// bu benim pc için.
        //SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-RPV5FRQ\\SQLEXPRESS;Initial Catalog=AHSS_database;Integrated Security=True");// bu yılmaz abinin için
        SqlCommand komut;
        DataTable dataTable;
        STOK_KARTI stokKarti = new STOK_KARTI();
        STOK_KARTI seciliStokKarti = new STOK_KARTI();// stok kartını düzenleme için kullanacağım.

        public string StokYapilacakIslem;

        private void Stok_form_Load(object sender, EventArgs e)
        {
            StokListesiDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            StokGruplariGetir();
            StokKartlariniListele();
            
            
            IptalButton.Enabled = false;
            StokBilgileriGroupBox.Enabled = false;
            KaydetButton.Enabled = false;
            StokKartiSilButton.Enabled = false;
            DuzenleButton.Enabled = false;
            YeniStokGrupComboBox.SelectedIndex = -1;
            YeniKDVCheckBox.Checked = false;
            YeniOTVCheckBox.Checked = false;

            HizliUrunCheckBox.Checked = false;
        }

        void YeniStokKartiEkle()
        {
            if (YeniBarkodTextBox.Text != string.Empty || YeniBarkodTextBox.Text.Length > 6)// barkod no boş değilse ve 6 karakterden büyük ise
            {
                if (YeniStokAdiTextBox.Text != string.Empty || YeniStokAdiTextBox.Text.Length > 6)// stok adi boş değilse ve 6 karakterden büyük ise
                {
                    if (YeniStokGrupComboBox.SelectedIndex > -1)// stok grubu seçilmiş ise
                    {
                        if (double.Parse(YeniAlisFiyatTextBox.Text) > 0 && double.Parse(YeniSatisFiyatTextBox.Text) > double.Parse(YeniAlisFiyatTextBox.Text))// alis fiyatı büyükse 0 dan ve satis fiyatı alis fiyatından büyükse.
                        {
                            if (StokYapilacakIslem == "yenikart")
                            {
                                if (!(BarkodVarmiKontrol(YeniBarkodTextBox.Text)))
                                {
                                    stokKarti.YeniKaydet(new urun
                                    {
                                        BarkodNumarasi = YeniBarkodTextBox.Text,
                                        Adi = YeniStokAdiTextBox.Text,
                                        Birimi = YeniBirimComboBox.SelectedItem.ToString(),
                                        Grubu = YeniStokGrupComboBox.SelectedItem.ToString(),
                                        Adet = float.Parse(YeniStokAdetTextBox.Text),
                                        AlisFiyati = float.Parse(YeniAlisFiyatTextBox.Text),
                                        SatisFiyati = float.Parse(YeniSatisFiyatTextBox.Text),
                                        KdvYuzde = Convert.ToInt32(YeniKDVOranTextBox.Text),
                                        KdvDahilmi = YeniKDVCheckBox.Checked,
                                        OtvYuzde = Convert.ToInt32(YeniOTVOranTextBox.Text),
                                        OtvDahilmi = YeniOTVCheckBox.Checked,
                                        IndirimOrani = Convert.ToInt32(IndirimOraniTextBox.Text),
                                        HizliSatis = HizliUrunCheckBox.Checked
                                    });
                                    MessageBox.Show("YENİ STOK KARTI GİRİLDİ.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    StokKartlariniListele();
                                    AlanlariTemizle();
                                    StokBilgileriGroupBox.Enabled = false;
                                    IptalButton.Enabled = false;
                                    KaydetButton.Enabled = false;
                                    DuzenleButton.Enabled = true;
                                    YeniKartButton.Enabled = true;
                                    StokGirButton.Enabled = true;
                                    StokDusButton.Enabled = true;
                                    StokKartiSilButton.Enabled = true;
                                }
                                else
                                {
                                    MessageBox.Show("BU BARKOD NUMARASI ZATEN STOKTA KAYITLI.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else if (StokYapilacakIslem == "düzenle")
                            {
                                stokKarti.Duzenle(new urun
                                {
                                    BarkodNumarasi = YeniBarkodTextBox.Text,
                                    Adi = YeniStokAdiTextBox.Text,
                                    Birimi = YeniBirimComboBox.SelectedItem.ToString(),
                                    Grubu = YeniStokGrupComboBox.SelectedItem.ToString(),
                                    Adet = float.Parse(YeniStokAdetTextBox.Text),
                                    AlisFiyati = float.Parse(YeniAlisFiyatTextBox.Text),
                                    SatisFiyati = float.Parse(YeniSatisFiyatTextBox.Text),
                                    KdvYuzde = Convert.ToInt32(YeniKDVOranTextBox.Text),
                                    KdvDahilmi = YeniKDVCheckBox.Checked,
                                    OtvYuzde = Convert.ToInt32(YeniOTVOranTextBox.Text),
                                    OtvDahilmi = YeniOTVCheckBox.Checked,
                                    IndirimOrani = Convert.ToInt32(IndirimOraniTextBox.Text),
                                    HizliSatis = HizliUrunCheckBox.Checked
                                });

                                MessageBox.Show("STOK KARTI DÜZELTİLDİ.", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                StokKartlariniListele();
                                AlanlariTemizle();
                                StokBilgileriGroupBox.Enabled = false;
                                IptalButton.Enabled = false;
                                KaydetButton.Enabled = false;
                                DuzenleButton.Enabled = true;
                                YeniKartButton.Enabled = true;
                                StokKartiSilButton.Enabled = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("SATIŞ FİYATI ALIŞ FİYATINDAN DÜŞÜK OLAMAZ!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            YeniSatisFiyatTextBox.Focus();
                            YeniSatisFiyatTextBox.SelectAll();
                        }
                    }
                    else
                    {
                        MessageBox.Show("STOK GRUBU SEÇİNİZ!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("STOK ADINI GİRİNİZ!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    YeniStokAdiTextBox.Focus();
                    YeniStokAdiTextBox.SelectAll();
                }
            }
            else
            {
                MessageBox.Show("BARKOD NUMARASINI KONTROL EDİNİZ!\nÇOK KISA VEYA BOŞ BIRAKTINIZ.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YeniBarkodTextBox.Focus();
                YeniBarkodTextBox.SelectAll();
            }
        }

        void StokKartiniSil()
        {
            if (StokListesiDataGridView.Rows.Count > 0)
            {
                DialogResult cevap = MessageBox.Show("SEÇİLİ STOK KARTINI SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    // stok kartını silme kısmı
                    komut = new SqlCommand();
                    komut.CommandText = "DELETE FROM stok_kartlari WHERE barkod_no='" + StokListesiDataGridView.SelectedRows[0].Cells["barkod_no"].Value + "'";
                    komut.Connection = baglanti;
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    // stok kartını silme kısmı bitiş

                    // stok kartına dair stok hareketleri tablosundan silme işlemleri başlandgıç
                    cevap = MessageBox.Show("STOK KARTI SİLİNDİ. STOK HAREKETLERİ DE SİLİNSİN Mİ?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (cevap == DialogResult.Yes)
                    {
                        komut.CommandText = "DELETE FROM stok_hareketleri WHERE barkod_no='" + StokListesiDataGridView.SelectedRows[0].Cells["barkod_no"].Value + "'";
                        komut.ExecuteNonQuery();
                    }
                    // stok kartına dair stok hareketleri tablosundan silme işlemleri bitiş
                    baglanti.Close();
                    StokKartlariniListele();
                }
            }
        }

        bool BarkodVarmiKontrol(string Barkod)
        {
            komut = new SqlCommand();
            komut.CommandText = "select barkod_no from stok_kartlari where barkod_no='" + Barkod + "'";
            komut.Connection = baglanti;
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            using (SqlDataReader sqlDataReader = komut.ExecuteReader())
            {
                while(sqlDataReader.Read())
                {
                    baglanti.Close();
                    return true;
                }
            }
            return false;
        }
        
        public void StokKartlariniListele()
        {
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM stok_kartlari", baglanti);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            StokListesiDataGridView.DataSource = dataTable;
            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }
            KalemSayisiLabel.Text = StokListesiDataGridView.RowCount.ToString();
            StokListesiDataGridView.ClearSelection();
        }

        public void StokHareketleriniListele(string ListelenecekBarkodNo)
        {
            komut = new SqlCommand();
            komut.CommandText = "select * from stok_hareketleri where barkod_no=@barkod_no";
            komut.Connection = baglanti;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(komut);
            komut.Parameters.AddWithValue("@barkod_no", ListelenecekBarkodNo);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            StokHareketleriDataGridView.DataSource = dataTable;
            StokHareketleriDataGridView.ReadOnly = true;
            StokHareketleriDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }
            StokHareketleriDataGridView.Rows[0].Selected = false;
        }

        public void StokGruplariGetir()
        {
            YeniStokGrupComboBox.Items.Clear();
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
                    YeniStokGrupComboBox.Items.Add(sqlDataReader["stok_grup"]);
                }
            }
            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            stok_Gruplari_Form.ShowDialog();
            StokGruplariGetir();
        }

        void AlanlariTemizle()
        {
            YeniBarkodTextBox.Clear();
            YeniStokKodTextBox.Clear();
            YeniStokAdiTextBox.Clear();
            YeniStokGrupComboBox.SelectedIndex = -1;
            YeniBirimComboBox.SelectedIndex = -1;
            YeniStokAdetTextBox.Text = "";
            YeniAlisFiyatTextBox.Text = "0.00";
            YeniSatisFiyatTextBox.Text = "0.00";
            YeniKDVOranTextBox.Text = "0";
            YeniOTVOranTextBox.Text = "0";
            YeniKDVCheckBox.Checked = false;
            YeniOTVCheckBox.Checked = false;
            HizliUrunCheckBox.Checked = false;
        }

        private void YeniKartButton_Click(object sender, EventArgs e)
        {
            StokYapilacakIslem = "yenikart";
            StokBilgileriGroupBox.Enabled = true;
            DuzenleButton.Enabled = false;
            IptalButton.Enabled = true;
            KaydetButton.Enabled = true;
            YeniKartButton.Enabled = false;
            StokKartiSilButton.Enabled = false;
            StokGirButton.Enabled = false;
            StokDusButton.Enabled = false;
            YeniBarkodTextBox.Focus();
        }

        private void KaydetButton_Click(object sender, EventArgs e)
        {
            YeniStokKartiEkle();
            StokListesiDataGridView.Enabled = true;
            AranacakBarkodTextBox.Focus();
            // buton aktif pasif olaylarını bir üst satırda ki YeniStokKartıEkle() metodunun içinde yapyıorum. işlem başarılı ise buton aktif pasif olaylarını yapsın diye.
        }
        private void IptalButton_Click(object sender, EventArgs e)
        {
            AlanlariTemizle();
            StokBilgileriGroupBox.Enabled = false;
            IptalButton.Enabled = false;
            KaydetButton.Enabled = false;
            DuzenleButton.Enabled = true;
            YeniKartButton.Enabled = true;
            StokKartiSilButton.Enabled = true;
            StokListesiDataGridView.Enabled = true;
            StokGirButton.Enabled = true;
            StokDusButton.Enabled = true;

            AranacakBarkodTextBox.Focus();
        }

        private void YeniAlisFiyatTextBox_Enter(object sender, EventArgs e)// textboxdaki TL simgesini kaldırmak ve float olarak güzelce para birimine dönüştürme metodu.
        {
            YeniAlisFiyatTextBox.SelectAll();
        }
        private void YeniAlisFiyatTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            YeniAlisFiyatTextBox.SelectAll();
        }
        private void YeniAlisFiyatTextBox_Leave(object sender, EventArgs e)
        {
            if (YeniAlisFiyatTextBox.Text != "0,00" && YeniAlisFiyatTextBox.Text != "")
            {
                YeniAlisFiyatTextBox.Text = string.Format("{0:#0.00}", Convert.ToDecimal(YeniAlisFiyatTextBox.Text));
            }
            else
            {
                YeniAlisFiyatTextBox.Text = "0,00";
            }
        }
        private void YeniSatisFiyatTextBox_Enter(object sender, EventArgs e)
        {
            YeniSatisFiyatTextBox.SelectAll();
        }
        private void YeniSatisFiyatTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            YeniSatisFiyatTextBox.SelectAll();
        }
        private void YeniSatisFiyatTextBox_Leave(object sender, EventArgs e)
        {
            decimal para = Convert.ToDecimal(YeniSatisFiyatTextBox.Text);
            if (!string.IsNullOrEmpty(YeniSatisFiyatTextBox.Text) && para >= 0)
            {
                YeniSatisFiyatTextBox.Text = string.Format("{0:#0.00}", para);
            }
            else if (YeniSatisFiyatTextBox.Text == "0,00")
            {
                YeniSatisFiyatTextBox.Text = "0,00";
            }
        }

        private void YeniAlisFiyatTextBox_KeyPress(object sender, KeyPressEventArgs e)// yenialisfiyatı textboxuna sadece rakam girmek için
        {
            if (char.IsLetter(e.KeyChar))// textboa sadece rakam girmek için
            {
                e.Handled = true;
            }
        }

        private void YeniSatisFiyatTextBox_KeyPress(object sender, KeyPressEventArgs e)// yenisatisfiyatı textboxuna sadece rakam girmek için
        {
            if (char.IsLetter(e.KeyChar))// textboa sadece rakam girmek için
            {
                e.Handled = true;
            }
        }

        private void YeniBarkodTextBox_KeyDown(object sender, KeyEventArgs e)// yeni stokkartı eklerken eklenecek ürünün barkodu okuttuğumda "bling" sesi çıkmasın diye
        {
            if (e.KeyCode == Keys.Enter)// entere basıldığında.
            {
                e.SuppressKeyPress = true;// "bling" sesi çıkmasın diye
            }
        }

        private void IndirimOraniTextBox_KeyPress(object sender, KeyPressEventArgs e)// indirim oranı textboxuna sadece rakam girmesi için
        {
            if (char.IsLetter(e.KeyChar))// textboa sadece rakam girmek için
            {
                e.Handled = true;
            }
            if (IndirimOraniTextBox.Text != string.Empty)
            {
                if (Convert.ToInt32(IndirimOraniTextBox.Text) > 100)
                {
                    IndirimOraniTextBox.Text = "100";
                }
            }
        }

        private void YeniKDVOranTextBox_KeyPress(object sender, KeyPressEventArgs e)// kdv oranı textboxuna sadece rakam girmesi için
        {
            if (char.IsLetter(e.KeyChar))// textboa sadece rakam girmek için
            {
                e.Handled = true;
            }
            if (YeniKDVOranTextBox.Text != string.Empty)
            {
                if (Convert.ToInt32(YeniKDVOranTextBox.Text) > 100)
                {
                    YeniKDVOranTextBox.Text = "100";
                }
            }
        }

        private void YeniOTVOranTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))// textboa sadece rakam girmek için
            {
                e.Handled = true;
            }
            if (YeniOTVOranTextBox.Text != string.Empty)
            {
                if (Convert.ToInt32(YeniOTVOranTextBox.Text) > 100)
                {
                    YeniOTVOranTextBox.Text = "100";
                }
            }
        }

        private void YeniStokAdetTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }

        }


        private void YeniKDVOranTextBox_Enter(object sender, EventArgs e)
        {
            YeniKDVOranTextBox.SelectAll();
        }
        private void YeniKDVOranTextBox_Leave(object sender, EventArgs e)
        {
            if (YeniKDVOranTextBox.Text == string.Empty)
            {
                YeniKDVOranTextBox.Text = "0";
            }
        }
        private void YeniKDVOranTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            YeniKDVOranTextBox.SelectAll();
        }

        private void YeniOTVOranTextBox_Enter(object sender, EventArgs e)
        {
            YeniOTVOranTextBox.SelectAll();
        }
        private void YeniOTVOranTextBox_Leave(object sender, EventArgs e)
        {
            if (YeniOTVOranTextBox.Text == string.Empty)
            {
                YeniOTVOranTextBox.Text = "0";
            }
        }

        private void YeniOTVOranTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            YeniOTVOranTextBox.SelectAll();
        }

        private void IndirimOraniTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            IndirimOraniTextBox.SelectAll();
        }

        private void IndirimOraniTextBox_Enter(object sender, EventArgs e)
        {
            IndirimOraniTextBox.SelectAll();
        }

        private void IndirimOraniTextBox_Leave(object sender, EventArgs e)
        {
            if (IndirimOraniTextBox.Text == string.Empty)
            {
                IndirimOraniTextBox.Text = "0";
            }
        }

        private void YeniStokAdetTextBox_Enter(object sender, EventArgs e)
        {
            YeniStokAdetTextBox.SelectAll();
        }

        private void YeniStokAdetTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(YeniStokAdetTextBox.Text))
            {
                YeniStokAdetTextBox.Text = "0";
            }
        }



        private void StokKartiSilButton_Click(object sender, EventArgs e)
        {
            StokKartiniSil();
        }

        private void StokGirButton_Click(object sender, EventArgs e)
        {
            if (StokListesiDataGridView.SelectedRows.Count != 0) 
            {
                stok_Gir_Form.eklenecekBarkodNo = StokListesiDataGridView.SelectedRows[0].Cells["barkod_no"].Value.ToString();
                stok_Gir_Form.eklenecekStokAdi = StokListesiDataGridView.SelectedRows[0].Cells["stok_adi"].Value.ToString();
                stok_Gir_Form.stokAdeti = float.Parse(StokListesiDataGridView.SelectedRows[0].Cells["stok_adet"].Value.ToString());
                stok_Gir_Form.eklenecekStokBirimi = StokListesiDataGridView.SelectedRows[0].Cells["stok_birim"].Value.ToString();
                stok_Gir_Form.ShowDialog();
                StokKartlariniListele();
                StokGruplariGetir();
            }
            else
            {
                MessageBox.Show("ÖNCE STOK KARTINI SEÇİN !", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;// datagridview de seçim yapmamışsa hataya düşmesin diye
            }
            
        }

        private void DuzenleButton_Click(object sender, EventArgs e)
        {
            if (StokListesiDataGridView.SelectedRows.Count != 0)
            {
                StokYapilacakIslem = "düzenle";
                StokBilgileriGroupBox.Enabled = true;
                StokListesiDataGridView.Enabled = false;
                YeniKartButton.Enabled = false;
                StokKartiSilButton.Enabled = false;
                KaydetButton.Enabled = true;
                IptalButton.Enabled = true;
                StokGirButton.Enabled = false;
                StokDusButton.Enabled = false;
                YeniBarkodTextBox.Text = StokListesiDataGridView.SelectedRows[0].Cells["barkod_no"].Value.ToString();
                YeniStokKodTextBox.Text = StokListesiDataGridView.SelectedRows[0].Cells["stok_kod"].Value.ToString();
                YeniStokAdiTextBox.Text = StokListesiDataGridView.SelectedRows[0].Cells["stok_adi"].Value.ToString();
                string birimi = StokListesiDataGridView.SelectedRows[0].Cells["stok_birim"].Value.ToString();
                for (int i = 0; i < YeniBirimComboBox.Items.Count; i++)
                {
                    if (YeniBirimComboBox.Items[i].ToString() == birimi.ToUpper())
                    {
                        YeniBirimComboBox.SelectedIndex = i;
                        break;
                    }
                }
                YeniStokAdetTextBox.Text = string.Format("{0:#0.000}", StokListesiDataGridView.SelectedRows[0].Cells["stok_adet"].Value.ToString());
                string stokGrubu = StokListesiDataGridView.SelectedRows[0].Cells["stok_grup"].Value.ToString();
                for (int i = 0; i < YeniStokGrupComboBox.Items.Count; i++)
                {
                    if (YeniStokGrupComboBox.Items[i].ToString() == stokGrubu)
                    {
                        YeniStokGrupComboBox.SelectedIndex = i;
                        break;
                    }
                }
                YeniAlisFiyatTextBox.Text = string.Format("{0:#.00}", Convert.ToDecimal(StokListesiDataGridView.SelectedRows[0].Cells["alis_fiyat"].Value.ToString()));
                YeniSatisFiyatTextBox.Text = string.Format("{0:#.00}", Convert.ToDecimal(StokListesiDataGridView.SelectedRows[0].Cells["satis_fiyat"].Value.ToString()));
                YeniKDVOranTextBox.Text = StokListesiDataGridView.SelectedRows[0].Cells["kdv"].Value.ToString();
                YeniOTVOranTextBox.Text = StokListesiDataGridView.SelectedRows[0].Cells["otv"].Value.ToString();
                if (StokListesiDataGridView.SelectedRows[0].Cells["kdv_dahil"].Value.ToString() == "1")
                {
                    YeniKDVCheckBox.Checked = true;
                }
                else
                {
                    YeniKDVCheckBox.Checked = false;
                }
                if (StokListesiDataGridView.SelectedRows[0].Cells["otv_dahil"].Value.ToString() == "1")
                {
                    YeniOTVCheckBox.Checked = true;
                }
                else
                {
                    YeniOTVCheckBox.Checked = false;
                }
                IndirimOraniTextBox.Text = StokListesiDataGridView.SelectedRows[0].Cells["indirim"].Value.ToString();
                HizliUrunCheckBox.Checked = (bool)StokListesiDataGridView.SelectedRows[0].Cells["hizli_urun"].Value;
            }
            else
            {
                MessageBox.Show("ÖNCE STOK KARTINI SEÇİN !", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;// datagridview de seçim yapmamışsa hataya düşmesin diye
            }
        }

        private void Stok_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            // yeni stok kartı aç aktif ise kapanırken soru sorsun diye.
            if (!YeniKartButton.Enabled)// false ise yani; yeni kayıt işlemi açık ise
            {
                DialogResult cevap = MessageBox.Show("YENİ STOK KARTI KAYIT EDİLMEDİ.\n\nÇIKMAK İSTEDİĞİNİZE EMİN MİSİNİZ ?", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else // yeni stok kartı aç buton enabled true ise.  yani yeni stok kartı aç butonuna basılmamış ise.
                {
                    YeniKartButton.Enabled = true;
                    StokKartiSilButton.Enabled = true;
                    DuzenleButton.Enabled = true;
                    KaydetButton.Enabled = false;
                    IptalButton.Enabled = false;
                    StokBilgileriGroupBox.Enabled = false;
                    AlanlariTemizle();
                }
            }
            else
            {
                YeniKartButton.Enabled = true;
                StokKartiSilButton.Enabled = true;
                DuzenleButton.Enabled = true;
                KaydetButton.Enabled = false;
                IptalButton.Enabled = false;
                StokBilgileriGroupBox.Enabled = false;
                AranacakBarkodTextBox.Clear();
                AranacakStokAdiTextBox.Clear();
                AlanlariTemizle();
            }
        }

        private void StokDusButton_Click(object sender, EventArgs e)
        {
            if (StokListesiDataGridView.SelectedRows.Count != 0)
            {
                stok_Dus_form.mevcutBarkod = StokListesiDataGridView.SelectedRows[0].Cells["barkod_no"].Value.ToString();
                stok_Dus_form.mevcutStokAdi = StokListesiDataGridView.SelectedRows[0].Cells["stok_adi"].Value.ToString();
                stok_Dus_form.mevcutStokAdeti = Convert.ToInt32(StokListesiDataGridView.SelectedRows[0].Cells["stok_adet"].Value);
                stok_Dus_form.ShowDialog();
                StokKartlariniListele();
                StokGruplariGetir();
            }
            else
            {
                MessageBox.Show("ÖNCE STOK KARTINI SEÇİN !", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;// datagridview de seçim yapmamışsa hataya düşmesin diye
            }
        }

        private void StokListesiDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                StokHareketleriniListele(StokListesiDataGridView.SelectedRows[0].Cells["barkod_no"].Value.ToString());
            }
            catch (Exception)
            {

            }
            if (StokListesiDataGridView.Rows.Count <= 0)
            {
                StokKartiSilButton.Enabled = false;
                DuzenleButton.Enabled = false;
            }
            else
            {
                StokKartiSilButton.Enabled = true;
                DuzenleButton.Enabled = true;
            }
        }


        private void KapatButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AranacakBarkodTextBox_KeyPress(object sender, KeyPressEventArgs e)// datagridview de arama yapıyorum. barkoda göre
        {
            if (e.KeyChar == (char)Keys.Enter)// entere basılırsa
            {
                foreach (DataGridViewRow row in StokListesiDataGridView.Rows)// datagridview rowları row objesine atıyorum.
                {
                    if (row.Cells["barkod_no"].Value.ToString().Equals(AranacakBarkodTextBox.Text.ToUpper()))// girilen barkod datagridview de var mı kontrol ediyorum.
                    {
                        row.Selected = true;// o satırı seçiyorum
                        StokListesiDataGridView.FirstDisplayedScrollingRowIndex = row.Index;// seçili olan satırı en üstte göstertiyorum.
                        AranacakBarkodTextBox.Clear();
                        AranacakBarkodTextBox.BackColor = Color.White;
                        break;
                    }
                    else
                    {
                        AranacakBarkodTextBox.BackColor = Color.Red;
                        AranacakBarkodTextBox.SelectAll();
                        StokListesiDataGridView.ClearSelection();
                    }
                }
                
            }
        }

        private void AranacakStokAdiTextBox_KeyPress(object sender, KeyPressEventArgs e)// datagridview de arama yapıyorum. isme göre
        {
            if (e.KeyChar == (char)Keys.Enter)// entere basılırsa
            {
                (StokListesiDataGridView.DataSource as DataTable).DefaultView.RowFilter = "stok_adi LIKE '%" + AranacakStokAdiTextBox.Text + "%'";// datagridviewde stok_adi kolonunda aranacakstokaditextbox.text' deki metni içeren satırları filtreliyoruz.
                StokListesiDataGridView.Refresh();
            }
        }

        private void AranacakStokAdiTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AranacakStokAdiTextBox.Text))// textbox boş ise tüm kayıtları getirsin diye.
            {
                (StokListesiDataGridView.DataSource as DataTable).DefaultView.RowFilter = "stok_adi LIKE '%%'";// datagridviewde stok_adi kolonunda aranacakstokaditextbox.text' deki metni içeren satırları filtreliyoruz.
                StokListesiDataGridView.Refresh();
            }
        }
    }
}
