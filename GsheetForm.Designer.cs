
namespace ReplacementForWestMark
{
    partial class GsheetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GsheetForm));
            this.label1 = new System.Windows.Forms.Label();
            this.gsheetstringbox = new System.Windows.Forms.TextBox();
            this.Examplepicture = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Okbutton = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Examplepicture)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gsheet String:";
            // 
            // gsheetstringbox
            // 
            this.gsheetstringbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gsheetstringbox.Location = new System.Drawing.Point(16, 51);
            this.gsheetstringbox.Name = "gsheetstringbox";
            this.gsheetstringbox.Size = new System.Drawing.Size(448, 26);
            this.gsheetstringbox.TabIndex = 1;
            // 
            // Examplepicture
            // 
            this.Examplepicture.Image = ((System.Drawing.Image)(resources.GetObject("Examplepicture.Image")));
            this.Examplepicture.Location = new System.Drawing.Point(1, 116);
            this.Examplepicture.Name = "Examplepicture";
            this.Examplepicture.Size = new System.Drawing.Size(473, 33);
            this.Examplepicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Examplepicture.TabIndex = 2;
            this.Examplepicture.TabStop = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(469, 45);
            this.label2.TabIndex = 3;
            this.label2.Text = "The above image is an example of where to get the string for asigning which gshee" +
    "t to look for. Highlighted yellow.";
            // 
            // Okbutton
            // 
            this.Okbutton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Okbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Okbutton.Location = new System.Drawing.Point(15, 224);
            this.Okbutton.Name = "Okbutton";
            this.Okbutton.Size = new System.Drawing.Size(161, 54);
            this.Okbutton.TabIndex = 4;
            this.Okbutton.Text = "Ok";
            this.Okbutton.UseVisualStyleBackColor = false;
            this.Okbutton.Click += new System.EventHandler(this.Okbutton_Click);
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancel.Location = new System.Drawing.Point(260, 224);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(161, 54);
            this.Cancel.TabIndex = 5;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // GsheetForm
            // 
            this.AcceptButton = this.Okbutton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(476, 315);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Okbutton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Examplepicture);
            this.Controls.Add(this.gsheetstringbox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GsheetForm";
            this.Text = "Gsheet Configuration";
            this.Load += new System.EventHandler(this.GsheetForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Examplepicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox gsheetstringbox;
        private System.Windows.Forms.PictureBox Examplepicture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Okbutton;
        private System.Windows.Forms.Button Cancel;
    }
}