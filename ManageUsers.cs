using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace ReplacementForWestMark
{
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }
        private void ManageUsers_Load(object sender, EventArgs e)
        {
            var pf = Application.OpenForms.OfType<PrimaryForm>().First();
            string path7 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\security.txt";
            path7 = path7.Replace("\r", "").Replace("\n", "");
            string fullfile = File.ReadAllText(path7);
            string[] split = fullfile.Split(',');
            for(int n = 0; n < split.Length; n++)
            {
                split[n] = split[n].Replace("\r", "").Replace("\n", "");
            }
            for (int f = 0; f < split.Length; f = f + 3)
            {
                if(f + 2 < split.Length)
                {
                    Control[] cs = this.Controls.Find("Privilege" + f / 3, true);
                    ComboBox pv = cs[0] as ComboBox;
                    this.Controls["Username" + f / 3].Text = Decryptusername(split[f]);
                    this.Controls["Password" + f / 3].Text = split[f + 1];
                    switch(split[f+2])
                    {
                        case "10":
                            pv.SelectedIndex = 2;
                            if (pf._SecLevel != 10)
                            {
                                pv.Enabled = false;
                                this.Controls["Username" + f / 3].Enabled = false;
                                this.Controls["Password" + f / 3].Enabled = false;
                            }
                            break;
                        case "5":
                            pv.SelectedIndex = 1;
                            break;
                        case "1":
                            pv.SelectedIndex = 0;
                            break;
                    }
                }
            }


        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            string path7 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\security.txt";
            path7 = path7.Replace("\r", "").Replace("\n", "");
            string user = "";
            File.WriteAllText(path7, "");
            for(int f = 0; f < 25; f ++)
            {
                Control[] cs = this.Controls.Find("Privilege" + f, true);
                ComboBox pv = cs[0] as ComboBox;
                user = Encryptusername(this.Controls["Username" + f].Text) + ",";
                user = user + this.Controls["Password" + f].Text + ",";
                switch (pv.SelectedIndex)
                {
                    case 2:
                        user = user + "10,";
                        break;
                    case 1:
                        user = user + "5,";
                        break;
                    case 0:
                        user = user + "1,";
                        break;
                    default:
                        user = user + ",";
                        break;
                }
                using (StreamWriter w = File.AppendText(path7))
                {
                    w.WriteLine(user);
                };
            }
            this.Close();
        }
        string Encryptpassword(string password)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
        string Encryptusername(string username)
        {
            username.ToLower();
            char[] cusername = username.ToCharArray();
            for (int f = 0; f < cusername.Length; f++)
            {
                cusername[f] += (char)5;
            }
            string eusername = new string(cusername);
            return eusername;
        }
        string Decryptusername(string username)
        {
            char[] cusername = username.ToCharArray();
            for (int f = 0; f < cusername.Length; f++)
            {
                cusername[f] -= (char)5;
            }
            string dusername = new string(cusername);
            return dusername;
        }

        private void Password_Comitted(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = Encryptpassword(tb.Text);
        }
        private void Password_Entered(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = "";
        }
        private void Changed_user_Privilege(object sender, EventArgs e)
        {
            var pf = Application.OpenForms.OfType<PrimaryForm>().First();
            ComboBox cb = (ComboBox)sender;
            if (pf._SecLevel != 10 && cb.SelectedIndex == 2)
            {
                cb.SelectedIndex = 0;
            }
        }
    }
}
