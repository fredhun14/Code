
namespace ReplacementForWestMark
{
    partial class Print_Manual_Ticket
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Print_Manual_Ticket));
            this.PrinterCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Printbut = new System.Windows.Forms.Button();
            this.CaseCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ColumnLabel02 = new System.Windows.Forms.Label();
            this.ColumnLabel03 = new System.Windows.Forms.Label();
            this.ColumnLabel00 = new System.Windows.Forms.Label();
            this.ColumnLabel01 = new System.Windows.Forms.Label();
            this.ColumnData02 = new System.Windows.Forms.Label();
            this.ColumnData03 = new System.Windows.Forms.Label();
            this.ColumnData00 = new System.Windows.Forms.Label();
            this.ColumnData01 = new System.Windows.Forms.Label();
            this.ResourceLabel = new System.Windows.Forms.TextBox();
            this.Remarks3TextBox = new System.Windows.Forms.TextBox();
            this.Remarks2TextBox = new System.Windows.Forms.TextBox();
            this.Remarks1TextBox = new System.Windows.Forms.TextBox();
            this.RemarksLabel = new System.Windows.Forms.Label();
            this.ButtonTimoutTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.CaseCount)).BeginInit();
            this.SuspendLayout();
            // 
            // PrinterCombo
            // 
            this.PrinterCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrinterCombo.FormattingEnabled = true;
            this.PrinterCombo.Location = new System.Drawing.Point(12, 40);
            this.PrinterCombo.Name = "PrinterCombo";
            this.PrinterCombo.Size = new System.Drawing.Size(235, 33);
            this.PrinterCombo.TabIndex = 0;
            this.PrinterCombo.SelectedIndexChanged += new System.EventHandler(this.PrinterCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Resource Number:";
            // 
            // Printbut
            // 
            this.Printbut.Location = new System.Drawing.Point(17, 383);
            this.Printbut.Name = "Printbut";
            this.Printbut.Size = new System.Drawing.Size(99, 45);
            this.Printbut.TabIndex = 3;
            this.Printbut.Text = "Print Ticket";
            this.Printbut.UseVisualStyleBackColor = true;
            this.Printbut.Click += new System.EventHandler(this.Print_Click);
            // 
            // CaseCount
            // 
            this.CaseCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CaseCount.Location = new System.Drawing.Point(271, 41);
            this.CaseCount.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.CaseCount.Name = "CaseCount";
            this.CaseCount.Size = new System.Drawing.Size(120, 32);
            this.CaseCount.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "1: Select Line";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(266, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(343, 26);
            this.label3.TabIndex = 6;
            this.label3.Text = "2: Enter number of cases on pallet";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 351);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 26);
            this.label4.TabIndex = 7;
            this.label4.Text = "4: Print";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(296, 26);
            this.label5.TabIndex = 8;
            this.label5.Text = "3: Verify resource information";
            // 
            // ColumnLabel02
            // 
            this.ColumnLabel02.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ColumnLabel02.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnLabel02.Location = new System.Drawing.Point(12, 235);
            this.ColumnLabel02.Name = "ColumnLabel02";
            this.ColumnLabel02.Size = new System.Drawing.Size(169, 28);
            this.ColumnLabel02.TabIndex = 21;
            this.ColumnLabel02.Text = "Case Used:";
            // 
            // ColumnLabel03
            // 
            this.ColumnLabel03.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ColumnLabel03.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnLabel03.Location = new System.Drawing.Point(12, 274);
            this.ColumnLabel03.Name = "ColumnLabel03";
            this.ColumnLabel03.Size = new System.Drawing.Size(169, 28);
            this.ColumnLabel03.TabIndex = 22;
            this.ColumnLabel03.Text = "Poly Code:";
            // 
            // ColumnLabel00
            // 
            this.ColumnLabel00.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ColumnLabel00.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnLabel00.Location = new System.Drawing.Point(12, 157);
            this.ColumnLabel00.Name = "ColumnLabel00";
            this.ColumnLabel00.Size = new System.Drawing.Size(169, 28);
            this.ColumnLabel00.TabIndex = 19;
            this.ColumnLabel00.Text = "Format:";
            // 
            // ColumnLabel01
            // 
            this.ColumnLabel01.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ColumnLabel01.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnLabel01.Location = new System.Drawing.Point(12, 196);
            this.ColumnLabel01.Name = "ColumnLabel01";
            this.ColumnLabel01.Size = new System.Drawing.Size(169, 28);
            this.ColumnLabel01.TabIndex = 20;
            this.ColumnLabel01.Text = "Pallet Size:";
            // 
            // ColumnData02
            // 
            this.ColumnData02.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ColumnData02.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnData02.Location = new System.Drawing.Point(207, 235);
            this.ColumnData02.Name = "ColumnData02";
            this.ColumnData02.Size = new System.Drawing.Size(191, 28);
            this.ColumnData02.TabIndex = 25;
            // 
            // ColumnData03
            // 
            this.ColumnData03.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ColumnData03.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnData03.Location = new System.Drawing.Point(207, 274);
            this.ColumnData03.Name = "ColumnData03";
            this.ColumnData03.Size = new System.Drawing.Size(191, 28);
            this.ColumnData03.TabIndex = 26;
            // 
            // ColumnData00
            // 
            this.ColumnData00.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ColumnData00.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnData00.Location = new System.Drawing.Point(207, 157);
            this.ColumnData00.Name = "ColumnData00";
            this.ColumnData00.Size = new System.Drawing.Size(191, 28);
            this.ColumnData00.TabIndex = 23;
            // 
            // ColumnData01
            // 
            this.ColumnData01.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ColumnData01.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnData01.Location = new System.Drawing.Point(207, 196);
            this.ColumnData01.Name = "ColumnData01";
            this.ColumnData01.Size = new System.Drawing.Size(191, 28);
            this.ColumnData01.TabIndex = 24;
            // 
            // ResourceLabel
            // 
            this.ResourceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResourceLabel.Location = new System.Drawing.Point(207, 118);
            this.ResourceLabel.Name = "ResourceLabel";
            this.ResourceLabel.Size = new System.Drawing.Size(191, 32);
            this.ResourceLabel.TabIndex = 28;
            // 
            // Remarks3TextBox
            // 
            this.Remarks3TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Remarks3TextBox.Location = new System.Drawing.Point(415, 184);
            this.Remarks3TextBox.MaxLength = 30;
            this.Remarks3TextBox.Name = "Remarks3TextBox";
            this.Remarks3TextBox.Size = new System.Drawing.Size(359, 26);
            this.Remarks3TextBox.TabIndex = 31;
            // 
            // Remarks2TextBox
            // 
            this.Remarks2TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Remarks2TextBox.Location = new System.Drawing.Point(415, 151);
            this.Remarks2TextBox.MaxLength = 30;
            this.Remarks2TextBox.Name = "Remarks2TextBox";
            this.Remarks2TextBox.Size = new System.Drawing.Size(359, 26);
            this.Remarks2TextBox.TabIndex = 30;
            // 
            // Remarks1TextBox
            // 
            this.Remarks1TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Remarks1TextBox.Location = new System.Drawing.Point(415, 118);
            this.Remarks1TextBox.MaxLength = 30;
            this.Remarks1TextBox.Name = "Remarks1TextBox";
            this.Remarks1TextBox.Size = new System.Drawing.Size(359, 26);
            this.Remarks1TextBox.TabIndex = 29;
            // 
            // RemarksLabel
            // 
            this.RemarksLabel.AutoSize = true;
            this.RemarksLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemarksLabel.Location = new System.Drawing.Point(541, 89);
            this.RemarksLabel.Name = "RemarksLabel";
            this.RemarksLabel.Size = new System.Drawing.Size(100, 26);
            this.RemarksLabel.TabIndex = 32;
            this.RemarksLabel.Text = "Remarks";
            // 
            // ButtonTimoutTimer
            // 
            this.ButtonTimoutTimer.Interval = 5000;
            this.ButtonTimoutTimer.Tick += new System.EventHandler(this.ButtonTimoutTimer_Tick);
            // 
            // Print_Manual_Ticket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(796, 450);
            this.Controls.Add(this.Remarks3TextBox);
            this.Controls.Add(this.Remarks2TextBox);
            this.Controls.Add(this.Remarks1TextBox);
            this.Controls.Add(this.RemarksLabel);
            this.Controls.Add(this.ResourceLabel);
            this.Controls.Add(this.ColumnData02);
            this.Controls.Add(this.ColumnData03);
            this.Controls.Add(this.ColumnData00);
            this.Controls.Add(this.ColumnData01);
            this.Controls.Add(this.ColumnLabel02);
            this.Controls.Add(this.ColumnLabel03);
            this.Controls.Add(this.ColumnLabel00);
            this.Controls.Add(this.ColumnLabel01);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CaseCount);
            this.Controls.Add(this.Printbut);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PrinterCombo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Print_Manual_Ticket";
            this.Text = "Print Manual Ticket";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Print_Manual_Ticket_FormClosed);
            this.Load += new System.EventHandler(this.Print_Manual_Ticket_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CaseCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox PrinterCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Printbut;
        private System.Windows.Forms.NumericUpDown CaseCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ColumnLabel02;
        private System.Windows.Forms.Label ColumnLabel03;
        private System.Windows.Forms.Label ColumnLabel00;
        private System.Windows.Forms.Label ColumnLabel01;
        private System.Windows.Forms.Label ColumnData02;
        private System.Windows.Forms.Label ColumnData03;
        private System.Windows.Forms.Label ColumnData00;
        private System.Windows.Forms.Label ColumnData01;
        private System.Windows.Forms.TextBox ResourceLabel;
        private System.Windows.Forms.TextBox Remarks3TextBox;
        private System.Windows.Forms.TextBox Remarks2TextBox;
        private System.Windows.Forms.TextBox Remarks1TextBox;
        private System.Windows.Forms.Label RemarksLabel;
        private System.Windows.Forms.Timer ButtonTimoutTimer;
    }
}