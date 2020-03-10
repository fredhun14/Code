namespace VroengardWebBrowser
{
    partial class HistoryForm
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
            this.HistorylistBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // HistorylistBox
            // 
            this.HistorylistBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.HistorylistBox.FormattingEnabled = true;
            this.HistorylistBox.Location = new System.Drawing.Point(3, -1);
            this.HistorylistBox.Name = "HistorylistBox";
            this.HistorylistBox.Size = new System.Drawing.Size(375, 316);
            this.HistorylistBox.TabIndex = 0;
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 312);
            this.Controls.Add(this.HistorylistBox);
            this.Name = "HistoryForm";
            this.Text = "Browser History";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox HistorylistBox;
    }
}