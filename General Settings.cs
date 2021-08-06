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
    public partial class General_Settings : Form
    {
        public General_Settings()
        {
            InitializeComponent();
        }

        private void OKbut_Click(object sender, EventArgs e)
        {
            string path11 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\GeneralSettings.txt";
            path11 = path11.Replace("\r", "").Replace("\n", "");
            string newline = GenFacilityUPDown.Value.ToString() + "," + SQLServerName.Text + "," + SQLDatabaseName.Text + "," + SQLTableName.Text + "," + SQLUserID.Text + "," + SQLUserPassword.Text + "," + SQLColumnCount.Value.ToString() + "," + GSGsheetString.Text + "," + EXPath1.Text + "," + EXPath2.Text + ",";
            //expand the above line for each item add a comma to the end should be simple to extend and add setting like this.
            //Remember to adjust the default settings in generateneccesaryfiles() in the primary form when adding items to the list so that things don't break.
            //Item List:
            /* 0 Facility Number
             * 1 SQL Server name
             * 2 SQL Database name
             * 3 SQL Table name
             * 4 SQL Username
             * 5 SQL User password
             * 6 SQL Column Count
             * 7 GS Gsheet String
             * 8 EX Export path 1
             * 9 EX Export path 2
             * 10 
             */
            File.WriteAllText(path11, newline);
            this.Close();
        }

        private void Cancelbut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void General_Settings_Load(object sender, EventArgs e)
        {
            string path11 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\GeneralSettings.txt";
            path11 = path11.Replace("\r", "").Replace("\n", "");
            string[] settings = File.ReadAllText(path11).Split(',');
            GenFacilityUPDown.Value = Int32.Parse(settings[0]);
            SQLServerName.Text = settings[1];
            SQLDatabaseName.Text = settings[2];
            SQLTableName.Text = settings[3];
            SQLUserID.Text = settings[4];
            SQLUserPassword.Text = settings[5];
            SQLColumnCount.Value = Int32.Parse(settings[6]);
            GSGsheetString.Text = settings[7];
            EXPath1.Text = settings[8];
            EXPath2.Text = settings[9];
        }
    }
}
