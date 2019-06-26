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
    public partial class gramaj_gir_form : Form
    {
        public gramaj_gir_form()
        {
            InitializeComponent();
        }

        public float gramaj { get; set; }
        public bool gramajGirildimi { get; set; }
        public float mevcutStokMiktari { get; set; }
        private void Button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "0" && textBox1.Text != string.Empty)
            {
                if (float.Parse(textBox1.Text) > 0)
                {
                    if (float.Parse(textBox1.Text) <= mevcutStokMiktari)
                    {
                        gramaj = float.Parse(textBox1.Text);
                        gramajGirildimi = true;
                        button2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0:0.000}", mevcutStokMiktari) + " kg den fazla olamaz.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Focus();
                        textBox1.SelectAll();
                    }
                }
                else
                {
                    MessageBox.Show("Pozitif KG/Gramaj girin", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("KG yada gram olarak girin", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Gramaj_gir_form_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                button2.PerformClick();
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))// textboa sadece rakam girmek için
            {
                e.Handled = true;
            }
        }

        private void Gramaj_gir_form_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            gramaj = 0;
            textBox1.Text = gramaj.ToString();
            gramajGirildimi = false;
        }

        private void TextBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void TextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.SelectAll();
        }
    }
}
