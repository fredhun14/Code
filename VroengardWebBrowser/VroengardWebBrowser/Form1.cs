using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VroengardWebBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string[] history = { "http://www.google.com","Empty","Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" };
        public int historyflag = 0;
        private void Search_Click(object sender, EventArgs e)
        {
            try {
                history[historyflag] = VroengardWebBrowser.Url.ToString();
                historyflag++;
                URL.Text = "http://" + URL.Text+ ".com";
                VroengardWebBrowser.Url = new System.Uri(URL.Text);
                history[historyflag] = URL.Text;

                //Below this is for back and forward appearing and disappearing 
                if (historyflag != 14)
                {
                    if (history[historyflag + 1] == "Empty") { Forward.Visible = false; }
                    else { Forward.Visible = true; }
                }
                else if (historyflag == 14) { Forward.Visible = false; }

                if (historyflag != 0)
                {
                    if (history[historyflag - 1] == null) { Back.Visible = false; }
                    else { Back.Visible = true; }
                }
                else if (historyflag == 0) { Back.Visible = false; }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            try
            {
                history[historyflag] = VroengardWebBrowser.Url.ToString();
                historyflag--;
                VroengardWebBrowser.Url = new System.Uri(history[historyflag]);
                URL.Text = history[historyflag];
                //Below this is for back and forward appearing and disappearing 
                if (historyflag != 14)
                {
                    if (history[historyflag + 1] == "Empty") { Forward.Visible = false; }
                    else { Forward.Visible = true; }
                }
                else if (historyflag == 14) { Forward.Visible = false; }

                if (historyflag != 0)
                {
                    if (history[historyflag - 1] == null) { Back.Visible = false; }
                    else { Back.Visible = true; }
                }
                else if (historyflag == 0) { Back.Visible = false; }
                
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void Forward_Click(object sender, EventArgs e)
        {
            try
            {
                historyflag++;
                VroengardWebBrowser.Url = new System.Uri(history[historyflag]);
                URL.Text = history[historyflag];
                //Below this is for back and forward appearing and disappearing 
                if (historyflag != 14)
                {
                    if (history[historyflag + 1] == "Empty") { Forward.Visible = false; }
                    else { Forward.Visible = true; }
                }
                else if (historyflag == 14) { Forward.Visible = false; }

                if (historyflag != 0)
                {
                    if (history[historyflag - 1] == null) { Back.Visible = false; }
                    else { Back.Visible = true; }
                }
                else if (historyflag == 0) { Back.Visible = false; }
            }
            catch(Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void Historybutton_Click(object sender, EventArgs e)
        {
            HistoryForm popup = new HistoryForm();
            for (int f = 0; f < history.Length; f++)
            {
                popup.HistorylistBox.Items.Add(history[f]);

            }
            DialogResult dialogresult = popup.ShowDialog();
            
            
            popup.Dispose();
        }

        private void URL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Click(sender, e);
            }
        }
    }
}
