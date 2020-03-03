using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicCipher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int key = Convert.ToInt32(Key.Text);
                int keymem = key;
                int[] i = new int[CipherText.Text.Length];
                char[] cipher = CipherText.Text.ToCharArray();
                char[] Capitals = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
                char[] Lower = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

                for (int c = 0; c < cipher.Length; c++)
                {
                    for (int a = 0; a < 26; a++)
                    {
                        if (Capitals[a] == cipher[c])
                        {
                            i[c] = a;
                        }
                        if (Lower[a] == cipher[c])
                        {
                            i[c] = a;
                        }
                    }
                    key = keymem;
                    if (i[c] + key > 25) { key -= 26; }
                    if (cipher[c] != ' ' && cipher[c] >= 65 && cipher[c] <= 90)
                    {
                        cipher[c] = Capitals[i[c] + key];
                    }
                    if (cipher[c] != ' ' && cipher[c] >= 97 && cipher[c] <= 122)
                    {
                        cipher[c] = Lower[i[c] + key];
                    }
                }

                string plainText = new string(cipher);
                PlainText.Text = plainText;
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        
        private void Encrypt_Click(object sender, EventArgs e)
        {
            try { 

            int key = Convert.ToInt32(Key.Text);
            int keymem = key;
            int[] i = new int[PlainText.Text.Length];
            char[] Plain = PlainText.Text.ToCharArray();
            char[] Capitals = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] Lower = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            for (int c = 0; c < Plain.Length; c++)
            {
                for (int a = 0; a < 26; a++)
                {
                    if (Capitals[a] == Plain[c])
                    {
                        i[c] = a;
                    }
                    if (Lower[a] == Plain[c])
                    {
                        i[c] = a;
                    }
                }
                key = keymem;
                if (i[c] - key <0) { key -= 26; }
                if (Plain[c] != ' ' && Plain[c] >= 65 && Plain[c] <= 90)
                {
                    Plain[c] = Capitals[i[c] - key];
                }
                if (Plain[c] != ' ' && Plain[c] >= 97 && Plain[c] <= 122)
                {
                    Plain[c] = Lower[i[c] - key];
                }
            }

            string plainText = new string(Plain);
            CipherText.Text = plainText;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
       }
    }
}
