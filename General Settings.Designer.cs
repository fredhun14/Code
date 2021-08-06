
namespace ReplacementForWestMark
{
    partial class General_Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(General_Settings));
            this.OKbut = new System.Windows.Forms.Button();
            this.Cancelbut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.GenFacilityUPDown = new System.Windows.Forms.NumericUpDown();
            this.SettingTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.SQLColumnCount = new System.Windows.Forms.NumericUpDown();
            this.SQLTableName = new System.Windows.Forms.TextBox();
            this.SQLUserPassword = new System.Windows.Forms.TextBox();
            this.SQLUserID = new System.Windows.Forms.TextBox();
            this.SQLDatabaseName = new System.Windows.Forms.TextBox();
            this.SQLServerName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.GSGsheetString = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.EXPath2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.EXPath1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SQLOnOff = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GenFacilityUPDown)).BeginInit();
            this.SettingTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SQLColumnCount)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // OKbut
            // 
            this.OKbut.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKbut.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKbut.Location = new System.Drawing.Point(12, 457);
            this.OKbut.Name = "OKbut";
            this.OKbut.Size = new System.Drawing.Size(115, 32);
            this.OKbut.TabIndex = 0;
            this.OKbut.Text = "OK";
            this.OKbut.UseVisualStyleBackColor = true;
            this.OKbut.Click += new System.EventHandler(this.OKbut_Click);
            // 
            // Cancelbut
            // 
            this.Cancelbut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancelbut.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancelbut.Location = new System.Drawing.Point(545, 457);
            this.Cancelbut.Name = "Cancelbut";
            this.Cancelbut.Size = new System.Drawing.Size(115, 32);
            this.Cancelbut.TabIndex = 1;
            this.Cancelbut.Text = "Cancel";
            this.Cancelbut.UseVisualStyleBackColor = true;
            this.Cancelbut.Click += new System.EventHandler(this.Cancelbut_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Facility Number:";
            // 
            // GenFacilityUPDown
            // 
            this.GenFacilityUPDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenFacilityUPDown.Location = new System.Drawing.Point(176, 13);
            this.GenFacilityUPDown.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.GenFacilityUPDown.Name = "GenFacilityUPDown";
            this.GenFacilityUPDown.Size = new System.Drawing.Size(47, 32);
            this.GenFacilityUPDown.TabIndex = 3;
            this.GenFacilityUPDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SettingTabControl
            // 
            this.SettingTabControl.Controls.Add(this.tabPage1);
            this.SettingTabControl.Controls.Add(this.tabPage2);
            this.SettingTabControl.Controls.Add(this.tabPage3);
            this.SettingTabControl.Controls.Add(this.tabPage4);
            this.SettingTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingTabControl.Location = new System.Drawing.Point(-5, 0);
            this.SettingTabControl.Name = "SettingTabControl";
            this.SettingTabControl.SelectedIndex = 0;
            this.SettingTabControl.Size = new System.Drawing.Size(681, 443);
            this.SettingTabControl.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.DimGray;
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.GenFacilityUPDown);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(673, 410);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.DimGray;
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.SQLOnOff);
            this.tabPage2.Controls.Add(this.SQLColumnCount);
            this.tabPage2.Controls.Add(this.SQLTableName);
            this.tabPage2.Controls.Add(this.SQLUserPassword);
            this.tabPage2.Controls.Add(this.SQLUserID);
            this.tabPage2.Controls.Add(this.SQLDatabaseName);
            this.tabPage2.Controls.Add(this.SQLServerName);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(673, 410);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SQL";
            // 
            // SQLColumnCount
            // 
            this.SQLColumnCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SQLColumnCount.Location = new System.Drawing.Point(184, 177);
            this.SQLColumnCount.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.SQLColumnCount.Name = "SQLColumnCount";
            this.SQLColumnCount.Size = new System.Drawing.Size(206, 32);
            this.SQLColumnCount.TabIndex = 14;
            this.SQLColumnCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SQLTableName
            // 
            this.SQLTableName.Location = new System.Drawing.Point(184, 79);
            this.SQLTableName.Name = "SQLTableName";
            this.SQLTableName.Size = new System.Drawing.Size(206, 26);
            this.SQLTableName.TabIndex = 13;
            // 
            // SQLUserPassword
            // 
            this.SQLUserPassword.Location = new System.Drawing.Point(184, 144);
            this.SQLUserPassword.Name = "SQLUserPassword";
            this.SQLUserPassword.Size = new System.Drawing.Size(206, 26);
            this.SQLUserPassword.TabIndex = 12;
            this.SQLUserPassword.UseSystemPasswordChar = true;
            // 
            // SQLUserID
            // 
            this.SQLUserID.Location = new System.Drawing.Point(184, 112);
            this.SQLUserID.Name = "SQLUserID";
            this.SQLUserID.Size = new System.Drawing.Size(206, 26);
            this.SQLUserID.TabIndex = 11;
            // 
            // SQLDatabaseName
            // 
            this.SQLDatabaseName.Location = new System.Drawing.Point(184, 47);
            this.SQLDatabaseName.Name = "SQLDatabaseName";
            this.SQLDatabaseName.Size = new System.Drawing.Size(206, 26);
            this.SQLDatabaseName.TabIndex = 10;
            // 
            // SQLServerName
            // 
            this.SQLServerName.Location = new System.Drawing.Point(184, 15);
            this.SQLServerName.Name = "SQLServerName";
            this.SQLServerName.Size = new System.Drawing.Size(206, 26);
            this.SQLServerName.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(158, 26);
            this.label6.TabIndex = 8;
            this.label6.Text = "Column Count:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 26);
            this.label7.TabIndex = 7;
            this.label7.Text = "Table Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 26);
            this.label4.TabIndex = 6;
            this.label4.Text = "User Password:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 26);
            this.label5.TabIndex = 5;
            this.label5.Text = "User ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "Database Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server Name:";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.DimGray;
            this.tabPage3.Controls.Add(this.GSGsheetString);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(673, 410);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Google Sheet";
            // 
            // GSGsheetString
            // 
            this.GSGsheetString.Location = new System.Drawing.Point(183, 6);
            this.GSGsheetString.Name = "GSGsheetString";
            this.GSGsheetString.Size = new System.Drawing.Size(343, 26);
            this.GSGsheetString.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 26);
            this.label8.TabIndex = 10;
            this.label8.Text = "Gsheet String:";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.DimGray;
            this.tabPage4.Controls.Add(this.EXPath2);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.EXPath1);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(673, 410);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Export";
            // 
            // EXPath2
            // 
            this.EXPath2.Location = new System.Drawing.Point(182, 40);
            this.EXPath2.Name = "EXPath2";
            this.EXPath2.Size = new System.Drawing.Size(396, 26);
            this.EXPath2.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 26);
            this.label10.TabIndex = 12;
            this.label10.Text = "Export Path 2:";
            // 
            // EXPath1
            // 
            this.EXPath1.Location = new System.Drawing.Point(182, 4);
            this.EXPath1.Name = "EXPath1";
            this.EXPath1.Size = new System.Drawing.Size(396, 26);
            this.EXPath1.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(150, 26);
            this.label9.TabIndex = 10;
            this.label9.Text = "Export Path 1:";
            // 
            // SQLOnOff
            // 
            this.SQLOnOff.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SQLOnOff.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.SQLOnOff.FlatAppearance.BorderSize = 10;
            this.SQLOnOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SQLOnOff.Location = new System.Drawing.Point(184, 215);
            this.SQLOnOff.Name = "SQLOnOff";
            this.SQLOnOff.Size = new System.Drawing.Size(44, 33);
            this.SQLOnOff.TabIndex = 15;
            this.SQLOnOff.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(8, 222);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(132, 26);
            this.label11.TabIndex = 16;
            this.label11.Text = "SQL On/ Off";
            // 
            // General_Settings
            // 
            this.AcceptButton = this.OKbut;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.CancelButton = this.Cancelbut;
            this.ClientSize = new System.Drawing.Size(672, 501);
            this.Controls.Add(this.SettingTabControl);
            this.Controls.Add(this.Cancelbut);
            this.Controls.Add(this.OKbut);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "General_Settings";
            this.Text = "General Settings";
            this.Load += new System.EventHandler(this.General_Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GenFacilityUPDown)).EndInit();
            this.SettingTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SQLColumnCount)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OKbut;
        private System.Windows.Forms.Button Cancelbut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown GenFacilityUPDown;
        private System.Windows.Forms.TabControl SettingTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox SQLTableName;
        private System.Windows.Forms.TextBox SQLUserPassword;
        private System.Windows.Forms.TextBox SQLUserID;
        private System.Windows.Forms.TextBox SQLDatabaseName;
        private System.Windows.Forms.TextBox SQLServerName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.NumericUpDown SQLColumnCount;
        private System.Windows.Forms.TextBox GSGsheetString;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox EXPath2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox EXPath1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton SQLOnOff;
    }
}