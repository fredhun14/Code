
namespace ReplacementForWestMark
{
    partial class Get_Weight
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Get_Weight));
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.Okbut = new System.Windows.Forms.Button();
            this.cancelbut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LineBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(122, 16);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 26);
            this.numericUpDown1.TabIndex = 0;
            // 
            // Okbut
            // 
            this.Okbut.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Okbut.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Okbut.Location = new System.Drawing.Point(12, 83);
            this.Okbut.Name = "Okbut";
            this.Okbut.Size = new System.Drawing.Size(90, 38);
            this.Okbut.TabIndex = 1;
            this.Okbut.Text = "OK";
            this.Okbut.UseVisualStyleBackColor = true;
            this.Okbut.Click += new System.EventHandler(this.Okbut_Click);
            // 
            // cancelbut
            // 
            this.cancelbut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbut.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelbut.Location = new System.Drawing.Point(152, 83);
            this.cancelbut.Name = "cancelbut";
            this.cancelbut.Size = new System.Drawing.Size(90, 38);
            this.cancelbut.TabIndex = 2;
            this.cancelbut.Text = "Cancel";
            this.cancelbut.UseVisualStyleBackColor = true;
            this.cancelbut.Click += new System.EventHandler(this.cancelbut_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Weight:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Line:";
            // 
            // LineBox
            // 
            this.LineBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.LineBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.LineBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LineBox.FormattingEnabled = true;
            this.LineBox.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99"});
            this.LineBox.Location = new System.Drawing.Point(121, 49);
            this.LineBox.Name = "LineBox";
            this.LineBox.Size = new System.Drawing.Size(121, 28);
            this.LineBox.TabIndex = 5;
            // 
            // Get_Weight
            // 
            this.AcceptButton = this.Okbut;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.CancelButton = this.cancelbut;
            this.ClientSize = new System.Drawing.Size(254, 124);
            this.Controls.Add(this.LineBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelbut);
            this.Controls.Add(this.Okbut);
            this.Controls.Add(this.numericUpDown1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Get_Weight";
            this.Text = "Get Weight";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button Okbut;
        private System.Windows.Forms.Button cancelbut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox LineBox;
    }
}