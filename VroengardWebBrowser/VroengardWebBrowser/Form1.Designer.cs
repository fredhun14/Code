namespace VroengardWebBrowser
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
            this.Back = new System.Windows.Forms.Button();
            this.Forward = new System.Windows.Forms.Button();
            this.URL = new System.Windows.Forms.TextBox();
            this.Search = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Historybutton = new System.Windows.Forms.Button();
            this.VroengardWebBrowser = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(0, 8);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(59, 24);
            this.Back.TabIndex = 1;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Visible = false;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Forward
            // 
            this.Forward.Location = new System.Drawing.Point(65, 8);
            this.Forward.Name = "Forward";
            this.Forward.Size = new System.Drawing.Size(59, 24);
            this.Forward.TabIndex = 2;
            this.Forward.Text = "Forward";
            this.Forward.UseVisualStyleBackColor = true;
            this.Forward.Visible = false;
            this.Forward.Click += new System.EventHandler(this.Forward_Click);
            // 
            // URL
            // 
            this.URL.Location = new System.Drawing.Point(130, 11);
            this.URL.Name = "URL";
            this.URL.Size = new System.Drawing.Size(854, 20);
            this.URL.TabIndex = 3;
            this.URL.Text = "http://google.com";
            this.URL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.URL_KeyDown);
            // 
            // Search
            // 
            this.Search.Location = new System.Drawing.Point(990, 11);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(75, 23);
            this.Search.TabIndex = 1;
            this.Search.Text = "GO!";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(2, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Historybutton);
            this.splitContainer1.Panel1.Controls.Add(this.Back);
            this.splitContainer1.Panel1.Controls.Add(this.Search);
            this.splitContainer1.Panel1.Controls.Add(this.URL);
            this.splitContainer1.Panel1.Controls.Add(this.Forward);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.VroengardWebBrowser);
            this.splitContainer1.Size = new System.Drawing.Size(1160, 647);
            this.splitContainer1.SplitterDistance = 37;
            this.splitContainer1.TabIndex = 5;
            // 
            // Historybutton
            // 
            this.Historybutton.Location = new System.Drawing.Point(1071, 10);
            this.Historybutton.Name = "Historybutton";
            this.Historybutton.Size = new System.Drawing.Size(79, 25);
            this.Historybutton.TabIndex = 4;
            this.Historybutton.Text = "History";
            this.Historybutton.UseVisualStyleBackColor = true;
            this.Historybutton.Click += new System.EventHandler(this.Historybutton_Click);
            // 
            // VroengardWebBrowser
            // 
            this.VroengardWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VroengardWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.VroengardWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.VroengardWebBrowser.Name = "VroengardWebBrowser";
            this.VroengardWebBrowser.Size = new System.Drawing.Size(1160, 606);
            this.VroengardWebBrowser.TabIndex = 0;
            this.VroengardWebBrowser.Url = new System.Uri("http://www.google.com", System.UriKind.Absolute);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 643);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Vroengard Web Browser";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button Forward;
        private System.Windows.Forms.TextBox URL;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser VroengardWebBrowser;
        private System.Windows.Forms.Button Historybutton;
    }
}

