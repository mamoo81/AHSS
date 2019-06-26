namespace AHSS
{
    partial class kasa_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(kasa_form));
            this.label1 = new System.Windows.Forms.Label();
            this.BaslangicTarihi = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BitisTarihi = new System.Windows.Forms.DateTimePicker();
            this.GosterButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.KazancLabel = new System.Windows.Forms.Label();
            this.KazancRadioButton = new System.Windows.Forms.RadioButton();
            this.GiderRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(117, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tarih aralığı";
            // 
            // BaslangicTarihi
            // 
            this.BaslangicTarihi.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BaslangicTarihi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.BaslangicTarihi.Location = new System.Drawing.Point(12, 121);
            this.BaslangicTarihi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BaslangicTarihi.Name = "BaslangicTarihi";
            this.BaslangicTarihi.Size = new System.Drawing.Size(164, 34);
            this.BaslangicTarihi.TabIndex = 2;
            this.BaslangicTarihi.ValueChanged += new System.EventHandler(this.BaslangicTarihi_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(183, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(33, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 29);
            this.label3.TabIndex = 1;
            this.label3.Text = "Başlangıç";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(264, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 29);
            this.label4.TabIndex = 1;
            this.label4.Text = "Bitiş";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(261, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 29);
            this.label5.TabIndex = 1;
            this.label5.Text = "------------";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(13, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 29);
            this.label6.TabIndex = 1;
            this.label6.Text = "------------";
            // 
            // BitisTarihi
            // 
            this.BitisTarihi.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BitisTarihi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.BitisTarihi.Location = new System.Drawing.Point(206, 121);
            this.BitisTarihi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BitisTarihi.Name = "BitisTarihi";
            this.BitisTarihi.Size = new System.Drawing.Size(164, 34);
            this.BitisTarihi.TabIndex = 2;
            this.BitisTarihi.ValueChanged += new System.EventHandler(this.BitisTarihi_ValueChanged);
            // 
            // GosterButton
            // 
            this.GosterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.GosterButton.Location = new System.Drawing.Point(376, 12);
            this.GosterButton.Name = "GosterButton";
            this.GosterButton.Size = new System.Drawing.Size(87, 91);
            this.GosterButton.TabIndex = 3;
            this.GosterButton.Text = "Göster";
            this.GosterButton.UseVisualStyleBackColor = true;
            this.GosterButton.Click += new System.EventHandler(this.GosterButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(41, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(175, 44);
            this.label7.TabIndex = 4;
            this.label7.Text = "Kazanç :";
            // 
            // KazancLabel
            // 
            this.KazancLabel.AutoSize = true;
            this.KazancLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.KazancLabel.Location = new System.Drawing.Point(222, 194);
            this.KazancLabel.Name = "KazancLabel";
            this.KazancLabel.Size = new System.Drawing.Size(131, 44);
            this.KazancLabel.TabIndex = 4;
            this.KazancLabel.Text = "0,00 ₺";
            // 
            // KazancRadioButton
            // 
            this.KazancRadioButton.AutoSize = true;
            this.KazancRadioButton.Checked = true;
            this.KazancRadioButton.Location = new System.Drawing.Point(40, 24);
            this.KazancRadioButton.Name = "KazancRadioButton";
            this.KazancRadioButton.Size = new System.Drawing.Size(130, 21);
            this.KazancRadioButton.TabIndex = 5;
            this.KazancRadioButton.TabStop = true;
            this.KazancRadioButton.Text = "Kazanç hesapla";
            this.KazancRadioButton.UseVisualStyleBackColor = true;
            // 
            // GiderRadioButton
            // 
            this.GiderRadioButton.AutoSize = true;
            this.GiderRadioButton.Location = new System.Drawing.Point(40, 51);
            this.GiderRadioButton.Name = "GiderRadioButton";
            this.GiderRadioButton.Size = new System.Drawing.Size(118, 21);
            this.GiderRadioButton.TabIndex = 5;
            this.GiderRadioButton.Text = "Gider hesapla";
            this.GiderRadioButton.UseVisualStyleBackColor = true;
            // 
            // kasa_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 271);
            this.Controls.Add(this.GiderRadioButton);
            this.Controls.Add(this.KazancRadioButton);
            this.Controls.Add(this.KazancLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.GosterButton);
            this.Controls.Add(this.BitisTarihi);
            this.Controls.Add(this.BaslangicTarihi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "kasa_form";
            this.Text = "KASA";
            this.Load += new System.EventHandler(this.Kasa_form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker BaslangicTarihi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker BitisTarihi;
        private System.Windows.Forms.Button GosterButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label KazancLabel;
        private System.Windows.Forms.RadioButton KazancRadioButton;
        private System.Windows.Forms.RadioButton GiderRadioButton;
    }
}