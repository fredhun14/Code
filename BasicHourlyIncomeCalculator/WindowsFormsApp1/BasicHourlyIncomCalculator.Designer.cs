namespace WindowsFormsApp1
{
    partial class HourlyIncomeCalcualtor
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
            this.HourlyWage = new System.Windows.Forms.TextBox();
            this.Hoursworked = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CalculateIncome = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.GrossIncome = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RegularPay = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.OvertimePay = new System.Windows.Forms.Label();
            this.OvertimeHours = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.RegularHours = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.OvertimeWage = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // HourlyWage
            // 
            this.HourlyWage.Location = new System.Drawing.Point(12, 41);
            this.HourlyWage.Name = "HourlyWage";
            this.HourlyWage.Size = new System.Drawing.Size(100, 20);
            this.HourlyWage.TabIndex = 0;
            // 
            // Hoursworked
            // 
            this.Hoursworked.Location = new System.Drawing.Point(118, 41);
            this.Hoursworked.Name = "Hoursworked";
            this.Hoursworked.Size = new System.Drawing.Size(100, 20);
            this.Hoursworked.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Hourly Wage";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Hours worked";
            // 
            // CalculateIncome
            // 
            this.CalculateIncome.Location = new System.Drawing.Point(475, 41);
            this.CalculateIncome.Name = "CalculateIncome";
            this.CalculateIncome.Size = new System.Drawing.Size(75, 45);
            this.CalculateIncome.TabIndex = 13;
            this.CalculateIncome.Text = "Calculate Income";
            this.CalculateIncome.UseVisualStyleBackColor = true;
            this.CalculateIncome.Click += new System.EventHandler(this.CalculateIncome_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Gross Income";
            // 
            // GrossIncome
            // 
            this.GrossIncome.AutoSize = true;
            this.GrossIncome.Location = new System.Drawing.Point(12, 229);
            this.GrossIncome.Name = "GrossIncome";
            this.GrossIncome.Size = new System.Drawing.Size(0, 13);
            this.GrossIncome.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(153, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Regular Pay";
            // 
            // RegularPay
            // 
            this.RegularPay.AutoSize = true;
            this.RegularPay.Location = new System.Drawing.Point(153, 229);
            this.RegularPay.Name = "RegularPay";
            this.RegularPay.Size = new System.Drawing.Size(0, 13);
            this.RegularPay.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(268, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Overtime Pay";
            // 
            // OvertimePay
            // 
            this.OvertimePay.AutoSize = true;
            this.OvertimePay.Location = new System.Drawing.Point(268, 229);
            this.OvertimePay.Name = "OvertimePay";
            this.OvertimePay.Size = new System.Drawing.Size(0, 13);
            this.OvertimePay.TabIndex = 20;
            // 
            // OvertimeHours
            // 
            this.OvertimeHours.AutoSize = true;
            this.OvertimeHours.Location = new System.Drawing.Point(268, 154);
            this.OvertimeHours.Name = "OvertimeHours";
            this.OvertimeHours.Size = new System.Drawing.Size(0, 13);
            this.OvertimeHours.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(268, 129);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Overtime Hours";
            // 
            // RegularHours
            // 
            this.RegularHours.AutoSize = true;
            this.RegularHours.Location = new System.Drawing.Point(153, 154);
            this.RegularHours.Name = "RegularHours";
            this.RegularHours.Size = new System.Drawing.Size(0, 13);
            this.RegularHours.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(153, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Regular hours";
            // 
            // OvertimeWage
            // 
            this.OvertimeWage.AutoSize = true;
            this.OvertimeWage.Location = new System.Drawing.Point(370, 154);
            this.OvertimeWage.Name = "OvertimeWage";
            this.OvertimeWage.Size = new System.Drawing.Size(0, 13);
            this.OvertimeWage.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(370, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Overtime Wage";
            // 
            // HourlyIncomeCalcualtor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 507);
            this.Controls.Add(this.OvertimeWage);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.OvertimeHours);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.RegularHours);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.OvertimePay);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RegularPay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.GrossIncome);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CalculateIncome);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Hoursworked);
            this.Controls.Add(this.HourlyWage);
            this.Name = "HourlyIncomeCalcualtor";
            this.Text = "Hourly Income Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox HourlyWage;
        private System.Windows.Forms.TextBox Hoursworked;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CalculateIncome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label GrossIncome;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label RegularPay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label OvertimePay;
        private System.Windows.Forms.Label OvertimeHours;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label RegularHours;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label OvertimeWage;
        private System.Windows.Forms.Label label8;
    }
}

