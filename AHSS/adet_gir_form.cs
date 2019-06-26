using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AHSS
{
    public partial class adet_gir_form : Form
    {
        public adet_gir_form()
        {
            InitializeComponent();
        }


        public bool adetGirildimi { get; set; }
        public float girilenAdet { get; set; }
        public string adetKG { get; set; }
        public float mevcutMiktar { get; set; }

        private void Adet_gir_form_Load(object sender, EventArgs e)
        {
            label1.Text = adetKG;
            textBox1.Text = mevcutMiktar.ToString();
            textBox1.Focus();
            textBox1.SelectAll();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            switch (adetKG)// girilecek birim kontrolü
            {
                case "ADET":// adet ise küsüratı atmak için int çeviriyorum
                    if (!string.IsNullOrEmpty(textBox1.Text))
                    {
                        girilenAdet = Convert.ToSingle(textBox1.Text);
                        adetGirildimi = true;
                    }
                    break;
                case "KG":
                    if (!string.IsNullOrEmpty(textBox1.Text))
                    {
                        girilenAdet = float.Parse(textBox1.Text);
                        adetGirildimi = true;
                    }
                    break;
            }
            textBox1.Clear();
            this.Close();
        }

        private void Adet_gir_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            textBox1.Clear();
        }

        private void Adet_gir_form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                textBox1.Clear();
                this.Close();
            }
        }
    }
}
