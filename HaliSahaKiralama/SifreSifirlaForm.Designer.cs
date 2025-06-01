
namespace HaliSahaKiralama
{
    partial class SifreSifirlaForm
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
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.textBoxKadi = new System.Windows.Forms.TextBox();
            this.textBoxYeniParola = new System.Windows.Forms.TextBox();
            this.btnSifreGuncelle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnrustmenu = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnrustmenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(175, 48);
            this.textBoxEmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(197, 34);
            this.textBoxEmail.TabIndex = 0;
            this.textBoxEmail.TextChanged += new System.EventHandler(this.textBoxEmail_TextChanged);
            // 
            // textBoxKadi
            // 
            this.textBoxKadi.Location = new System.Drawing.Point(175, 101);
            this.textBoxKadi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxKadi.Name = "textBoxKadi";
            this.textBoxKadi.Size = new System.Drawing.Size(197, 34);
            this.textBoxKadi.TabIndex = 1;
            // 
            // textBoxYeniParola
            // 
            this.textBoxYeniParola.Location = new System.Drawing.Point(175, 156);
            this.textBoxYeniParola.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxYeniParola.Name = "textBoxYeniParola";
            this.textBoxYeniParola.Size = new System.Drawing.Size(197, 34);
            this.textBoxYeniParola.TabIndex = 2;
            // 
            // btnSifreGuncelle
            // 
            this.btnSifreGuncelle.Location = new System.Drawing.Point(175, 200);
            this.btnSifreGuncelle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSifreGuncelle.Name = "btnSifreGuncelle";
            this.btnSifreGuncelle.Size = new System.Drawing.Size(197, 40);
            this.btnSifreGuncelle.TabIndex = 3;
            this.btnSifreGuncelle.Text = "ŞİFREYİ SIFIRLA";
            this.btnSifreGuncelle.UseVisualStyleBackColor = true;
            this.btnSifreGuncelle.Click += new System.EventHandler(this.btnSifreGuncelle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 28);
            this.label1.TabIndex = 4;
            this.label1.Text = "E-Posta:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 107);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 28);
            this.label2.TabIndex = 5;
            this.label2.Text = "Kullanıcı Adı:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 162);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 28);
            this.label3.TabIndex = 6;
            this.label3.Text = "Yeni Şifre Gir:";
            // 
            // pnrustmenu
            // 
            this.pnrustmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnrustmenu.Controls.Add(this.label4);
            this.pnrustmenu.Controls.Add(this.button1);
            this.pnrustmenu.Controls.Add(this.panel1);
            this.pnrustmenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnrustmenu.Location = new System.Drawing.Point(0, 0);
            this.pnrustmenu.Name = "pnrustmenu";
            this.pnrustmenu.Size = new System.Drawing.Size(433, 40);
            this.pnrustmenu.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(26, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(346, 28);
            this.label4.TabIndex = 2;
            this.label4.Text = "KULLANICI ŞİFRE SIFIRLAMA EKRANI";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumPurple;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(383, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MediumPurple;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 40);
            this.panel1.TabIndex = 0;
            // 
            // SifreSifirlaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 254);
            this.Controls.Add(this.pnrustmenu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSifreGuncelle);
            this.Controls.Add(this.textBoxYeniParola);
            this.Controls.Add(this.textBoxKadi);
            this.Controls.Add(this.textBoxEmail);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SifreSifirlaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SifreSifirlaForm";
            this.Load += new System.EventHandler(this.SifreSifirlaForm_Load);
            this.pnrustmenu.ResumeLayout(false);
            this.pnrustmenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxKadi;
        private System.Windows.Forms.TextBox textBoxYeniParola;
        private System.Windows.Forms.Button btnSifreGuncelle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnrustmenu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
    }
}