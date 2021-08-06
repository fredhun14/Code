using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ReplacementForWestMark
{
    public partial class GsheetForm : Form
    {
        public GsheetForm()
        {
            InitializeComponent();
        }

        private void Okbutton_Click(object sender, EventArgs e)
        {
            string path5 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\GSheetString" + ".txt";
            path5 = path5.Replace("\r", "").Replace("\n", "");
            if (File.Exists(path5))
            {
                File.WriteAllText(path5, gsheetstringbox.Text);
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GsheetForm_Load(object sender, EventArgs e)
        {
            string path5 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\GSheetString" + ".txt";
            path5 = path5.Replace("\r", "").Replace("\n", "");
            if (File.Exists(path5))
            {
                gsheetstringbox.Text = File.ReadAllText(path5);
                gsheetstringbox.Text = gsheetstringbox.Text.Replace("\r", "").Replace("\n", "");
            }
        }
    }
}
