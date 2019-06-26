namespace AHSS
{
    partial class stok_gir_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StokGirButton = new System.Windows.Forms.Button();
            this.StokAdetTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BarkodNoLabel = new System.Windows.Forms.Label();
            this.GirilecekStokTarihi = new System.Windows.Forms.DateTimePicker();
            this.StokAdiLabel = new System.Windows.Forms.Label();
            this.AciklamaTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.MevcutStokAdetLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StokGirButton
            // 
            this.StokGirButton.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.StokGirButton.Location = new System.Drawing.Point(12, 341);
            this.StokGirButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.StokGirButton.Name = "StokGirButton";
            this.StokGirButton.Size = new System.Drawing.Size(225, 46);
            this.StokGirButton.TabIndex = 3;
            this.StokGirButton.Text = "STOK GİR";
            this.StokGirButton.UseVisualStyleBackColor = true;
            this.StokGirButton.Click += new System.EventHandler(this.StokGirButton_Click);
            // 
            // StokAdetTextBox
            // 
            this.StokAdetTextBox.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.StokAdetTextBox.Location = new System.Drawing.Point(172, 154);
            this.StokAdetTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.StokAdetTextBox.Name = "StokAdetTextBox";
            this.StokAdetTextBox.Size = new System.Drawing.Size(66, 44);
            this.StokAdetTextBox.TabIndex = 0;
            this.StokAdetTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.StokAdetTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StokAdetTextBox_KeyDown);
            this.StokAdetTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StokAdetTextBox_KeyPress);
            this.StokAdetTextBox.Leave += new System.EventHandler(this.StokAdetTextBox_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(23, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Stok girilecek barkod no:";
            // 
            // BarkodNoLabel
            // 
            this.BarkodNoLabel.AutoSize = true;
            this.BarkodNoLabel.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BarkodNoLabel.Location = new System.Drawing.Point(36, 39);
            this.BarkodNoLabel.Name = "BarkodNoLabel";
            this.BarkodNoLabel.Size = new System.Drawing.Size(169, 29);
            this.BarkodNoLabel.TabIndex = 6;
            this.BarkodNoLabel.Text = "1234567890123";
            // 
            // GirilecekStokTarihi
            // 
            this.GirilecekStokTarihi.Location = new System.Drawing.Point(13, 238);
            this.GirilecekStokTarihi.Name = "GirilecekStokTarihi";
            this.GirilecekStokTarihi.Size = new System.Drawing.Size(225, 28);
            this.GirilecekStokTarihi.TabIndex = 1;
            this.GirilecekStokTarihi.Value = new System.DateTime(2019, 5, 10, 0, 0, 0, 0);
            // 
            // StokAdiLabel
            // 
            this.StokAdiLabel.AutoSize = true;
            this.StokAdiLabel.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.StokAdiLabel.Location = new System.Drawing.Point(36, 71);
            this.StokAdiLabel.Name = "StokAdiLabel";
            this.StokAdiLabel.Size = new System.Drawing.Size(103, 29);
            this.StokAdiLabel.TabIndex = 7;
            this.StokAdiLabel.Text = "STOK ADI";
            // 
            // AciklamaTextBox
            // 
            this.AciklamaTextBox.Location = new System.Drawing.Point(13, 306);
            this.AciklamaTextBox.Name = "AciklamaTextBox";
            this.AciklamaTextBox.Size = new System.Drawing.Size(225, 28);
            this.AciklamaTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(12, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 29);
            this.label3.TabIndex = 7;
            this.label3.Text = "Girilecek adet";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(12, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 29);
            this.label4.TabIndex = 7;
            this.label4.Text = "Mevcut stok";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(142, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 29);
            this.label5.TabIndex = 7;
            this.label5.Text = ":";
            // 
            // MevcutStokAdetLabel
            // 
            this.MevcutStokAdetLabel.AutoSize = true;
            this.MevcutStokAdetLabel.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.MevcutStokAdetLabel.Location = new System.Drawing.Point(177, 110);
            this.MevcutStokAdetLabel.Name = "MevcutStokAdetLabel";
            this.MevcutStokAdetLabel.Size = new System.Drawing.Size(49, 29);
            this.MevcutStokAdetLabel.TabIndex = 7;
            this.MevcutStokAdetLabel.Text = "999";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(-23, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(481, 29);
            this.label7.TabIndex = 7;
            this.label7.Text = "_______________________________________";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(-23, 177);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(445, 29);
            this.label8.TabIndex = 7;
            this.label8.Text = "____________________________________";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(12, 206);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(128, 29);
            this.label9.TabIndex = 7;
            this.label9.Text = "Giriş Tarihi :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.Location = new System.Drawing.Point(-23, 245);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(457, 29);
            this.label10.TabIndex = 7;
            this.label10.Text = "_____________________________________";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label11.Location = new System.Drawing.Point(12, 274);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(114, 29);
            this.label11.TabIndex = 7;
            this.label11.Text = "Açıklama :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(-23, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(613, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "__________________________________________________";
            // 
            // stok_gir_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 398);
            this.Controls.Add(this.AciklamaTextBox);
            this.Controls.Add(this.GirilecekStokTarihi);
            this.Controls.Add(this.StokAdetTextBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MevcutStokAdetLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.StokAdiLabel);
            this.Controls.Add(this.BarkodNoLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StokGirButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "stok_gir_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "STOK GİRİŞ";
            this.Load += new System.EventHandler(this.Stok_gir_form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StokGirButton;
        private System.Windows.Forms.TextBox StokAdetTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label BarkodNoLabel;
        private System.Windows.Forms.DateTimePicker GirilecekStokTarihi;
        private System.Windows.Forms.Label StokAdiLabel;
        private System.Windows.Forms.TextBox AciklamaTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label MevcutStokAdetLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
    }
}