namespace AHSS
{
    partial class stok_gruplari
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
            this.StokGrupTextBox = new System.Windows.Forms.TextBox();
            this.EkleButton = new System.Windows.Forms.Button();
            this.StokGrupListBox = new System.Windows.Forms.ListBox();
            this.CikarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StokGrupTextBox
            // 
            this.StokGrupTextBox.Location = new System.Drawing.Point(12, 12);
            this.StokGrupTextBox.Name = "StokGrupTextBox";
            this.StokGrupTextBox.Size = new System.Drawing.Size(191, 22);
            this.StokGrupTextBox.TabIndex = 0;
            // 
            // EkleButton
            // 
            this.EkleButton.ForeColor = System.Drawing.Color.Green;
            this.EkleButton.Location = new System.Drawing.Point(12, 40);
            this.EkleButton.Name = "EkleButton";
            this.EkleButton.Size = new System.Drawing.Size(98, 23);
            this.EkleButton.TabIndex = 1;
            this.EkleButton.Text = "EKLE";
            this.EkleButton.UseVisualStyleBackColor = true;
            this.EkleButton.Click += new System.EventHandler(this.EkleButton_Click);
            // 
            // StokGrupListBox
            // 
            this.StokGrupListBox.FormattingEnabled = true;
            this.StokGrupListBox.ItemHeight = 16;
            this.StokGrupListBox.Location = new System.Drawing.Point(12, 69);
            this.StokGrupListBox.Name = "StokGrupListBox";
            this.StokGrupListBox.Size = new System.Drawing.Size(191, 436);
            this.StokGrupListBox.TabIndex = 3;
            // 
            // CikarButton
            // 
            this.CikarButton.ForeColor = System.Drawing.Color.Red;
            this.CikarButton.Location = new System.Drawing.Point(116, 40);
            this.CikarButton.Name = "CikarButton";
            this.CikarButton.Size = new System.Drawing.Size(87, 23);
            this.CikarButton.TabIndex = 2;
            this.CikarButton.Text = "ÇIKAR";
            this.CikarButton.UseVisualStyleBackColor = true;
            this.CikarButton.Click += new System.EventHandler(this.CikarButton_Click);
            // 
            // stok_gruplari
            // 
            this.AcceptButton = this.EkleButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 523);
            this.Controls.Add(this.StokGrupListBox);
            this.Controls.Add(this.CikarButton);
            this.Controls.Add(this.EkleButton);
            this.Controls.Add(this.StokGrupTextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "stok_gruplari";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "STOK GRUPLARI";
            this.Load += new System.EventHandler(this.Stok_gruplari_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Stok_gruplari_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox StokGrupTextBox;
        private System.Windows.Forms.Button EkleButton;
        private System.Windows.Forms.ListBox StokGrupListBox;
        private System.Windows.Forms.Button CikarButton;
    }
}