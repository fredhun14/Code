
namespace ReplacementForWestMark
{
    partial class PrinterConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrinterConfig));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NewPrinterButton = new System.Windows.Forms.Button();
            this.PrinterCount = new System.Windows.Forms.Label();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Deleted = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(209, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(368, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(53, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Printer Name";
            // 
            // NewPrinterButton
            // 
            this.NewPrinterButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.NewPrinterButton.Location = new System.Drawing.Point(428, 9);
            this.NewPrinterButton.Name = "NewPrinterButton";
            this.NewPrinterButton.Size = new System.Drawing.Size(107, 34);
            this.NewPrinterButton.TabIndex = 7;
            this.NewPrinterButton.Text = "New Printer";
            this.NewPrinterButton.UseVisualStyleBackColor = false;
            this.NewPrinterButton.Click += new System.EventHandler(this.NewPrinterButton_Click);
            // 
            // PrinterCount
            // 
            this.PrinterCount.AutoSize = true;
            this.PrinterCount.Location = new System.Drawing.Point(17, 11);
            this.PrinterCount.Name = "PrinterCount";
            this.PrinterCount.Size = new System.Drawing.Size(13, 13);
            this.PrinterCount.TabIndex = 8;
            this.PrinterCount.Text = "0";
            this.PrinterCount.Visible = false;
            // 
            // OK
            // 
            this.OK.BackColor = System.Drawing.SystemColors.ControlDark;
            this.OK.Location = new System.Drawing.Point(191, 9);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(107, 34);
            this.OK.TabIndex = 9;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = false;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(304, 9);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(107, 34);
            this.Cancel.TabIndex = 10;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Deleted
            // 
            this.Deleted.AutoSize = true;
            this.Deleted.Location = new System.Drawing.Point(53, 13);
            this.Deleted.Name = "Deleted";
            this.Deleted.Size = new System.Drawing.Size(0, 13);
            this.Deleted.TabIndex = 11;
            this.Deleted.Visible = false;
            // 
            // PrinterConfig
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(549, 644);
            this.Controls.Add(this.Deleted);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.PrinterCount);
            this.Controls.Add(this.NewPrinterButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PrinterConfig";
            this.Text = "Printer Configuration";
            this.Load += new System.EventHandler(this.PrinterConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button NewPrinterButton;
        private System.Windows.Forms.Label PrinterCount;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label Deleted;
    }
}