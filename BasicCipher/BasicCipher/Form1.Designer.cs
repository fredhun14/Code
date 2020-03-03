namespace BasicCipher
{
    partial class Form1
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
            this.CipherText = new System.Windows.Forms.TextBox();
            this.PlainText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Key = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Encrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CipherText
            // 
            this.CipherText.Location = new System.Drawing.Point(28, 63);
            this.CipherText.MinimumSize = new System.Drawing.Size(4, 200);
            this.CipherText.Name = "CipherText";
            this.CipherText.Size = new System.Drawing.Size(273, 20);
            this.CipherText.TabIndex = 0;
            // 
            // PlainText
            // 
            this.PlainText.Location = new System.Drawing.Point(388, 63);
            this.PlainText.MinimumSize = new System.Drawing.Size(4, 200);
            this.PlainText.Name = "PlainText";
            this.PlainText.Size = new System.Drawing.Size(273, 20);
            this.PlainText.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cipher text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(385, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Plain text";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 332);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Key";
            // 
            // Key
            // 
            this.Key.Location = new System.Drawing.Point(42, 363);
            this.Key.Name = "Key";
            this.Key.Size = new System.Drawing.Size(100, 20);
            this.Key.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(307, 159);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Decrypt";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 275);
            this.label4.MaximumSize = new System.Drawing.Size(500, 0);
            this.label4.MinimumSize = new System.Drawing.Size(0, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(498, 40);
            this.label4.TabIndex = 7;
            this.label4.Text = "Enter the encrypted text into the cipher text box enter the number of letters the" +
    " text has been shifted into the key box then press decrypt. The plain text will " +
    "appear into the plain text textbox.";
            // 
            // Encrypt
            // 
            this.Encrypt.Location = new System.Drawing.Point(214, 159);
            this.Encrypt.Name = "Encrypt";
            this.Encrypt.Size = new System.Drawing.Size(75, 23);
            this.Encrypt.TabIndex = 8;
            this.Encrypt.Text = "Encrypt";
            this.Encrypt.UseVisualStyleBackColor = true;
            this.Encrypt.Click += new System.EventHandler(this.Encrypt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 450);
            this.Controls.Add(this.Encrypt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Key);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlainText);
            this.Controls.Add(this.CipherText);
            this.Name = "Form1";
            this.Text = "Caesar Cipher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CipherText;
        private System.Windows.Forms.TextBox PlainText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Key;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Encrypt;
    }
}

