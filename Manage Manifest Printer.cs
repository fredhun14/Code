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
    public partial class Manage_Manifest_Printer : Form
    {
        public Manage_Manifest_Printer()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            string path9 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\ManifestPrinterConfigFile.txt";
            path9 = path9.Replace("\r", "").Replace("\n", "");
            string printer = PrinterName.Text + "," + IPAddress.Text + "," + Port.Text + ",";
            string printer2 = PrinterName2.Text + "," + IPAddress2.Text + "," + Port2.Text;
            File.WriteAllText(path9, printer);
            using (StreamWriter w = File.AppendText(path9))
            {
                w.WriteLine(printer2);
            }
        }

        private void Manage_Manifest_Printer_Load(object sender, EventArgs e)
        {
            string path9 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\ManifestPrinterConfigFile.txt";
            path9 = path9.Replace("\r", "").Replace("\n", "");
            if (File.Exists(path9))
            {
                string[] printer = File.ReadAllText(path9).Split(',');
                PrinterName.Text = printer[0];
                IPAddress.Text = printer[1];
                Port.Text = printer[2];
                PrinterName2.Text = printer[3];
                IPAddress2.Text = printer[4];
                Port2.Text = printer[5];
            }
            else
            {
                MessageBox.Show("Could not find ManifestPrinterConfigFile.txt");
            }
        }

        private void CancelBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
