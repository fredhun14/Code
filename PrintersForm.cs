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
    public partial class PrinterConfig : Form
    {
        public PrinterConfig()
        {
            InitializeComponent();
        }
        public int Addedprinter = 0;
        public void AddedPrinter()
        {
            Addedprinter++;    
        }
        private void NewPrinterButton_Click(object sender, EventArgs e)
        {
            AddedPrinter();
            AddPrinter("Printer", "127.0.0.1", "50000");

            //This is how to access a specific dynamically created control!
            //this.Controls["textbox" + PrinterCount.Text].Text = "somethingelse";
        }
        void AddPrinter(string Name, string IP, string Port)
        {
            TextBox textBox12 = new TextBox();
            TextBox textBox22 = new TextBox();
            TextBox textBox32 = new TextBox();
            Button button = new Button();
            int textboxcount = Int32.Parse(PrinterCount.Text) * 3;
            // 
            // Name textBox
            // 
            textBox32.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox32.Location = new System.Drawing.Point(57, 93 + (45 * Int32.Parse(PrinterCount.Text)));
            textBox32.Name = "textBox" + textboxcount.ToString();
            textBox32.Size = new System.Drawing.Size(150, 29);
            textBox32.TabIndex = 5;
            textBox32.Text = Name;
            textboxcount++;
            // 
            // IP textBox
            // 
            textBox12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox12.Location = new System.Drawing.Point(213, 93 + (45 * Int32.Parse(PrinterCount.Text)));
            textBox12.Name = "textBox" + textboxcount.ToString();
            textBox12.Size = new System.Drawing.Size(150, 29);
            textBox12.TabIndex = 2;
            textBox12.Text = IP;
            textboxcount++;
            // 
            // Port textBox
            // 
            textBox22.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox22.Location = new System.Drawing.Point(372, 93 + (45 * Int32.Parse(PrinterCount.Text)));
            textBox22.Name = "textBox" + textboxcount.ToString();
            textBox22.Size = new System.Drawing.Size(150, 29);
            textBox22.TabIndex = 3;
            textBox22.Text = Port;
            textboxcount++; 
            // 
            // Delete printer Button
            // 
            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            button.Location = new System.Drawing.Point(10, 93 + (45 * Int32.Parse(PrinterCount.Text)));
            button.Name = "button" + PrinterCount.Text;
            button.Size = new System.Drawing.Size(31, 29);
            button.TabIndex = 6;
            button.Text = "X";
            button.UseVisualStyleBackColor = true;
            button.Click += (s, e) => { DeletePrinter(); };

            this.Controls.Add(textBox12);
            this.Controls.Add(textBox22);
            this.Controls.Add(textBox32);
            this.Controls.Add(button);
            
            int count;
            Int32.TryParse(PrinterCount.Text, out count);
            count++;
            PrinterCount.Text = count.ToString();
        }
        void LoadConfig()
        {
            string path;
            path = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterConfigFile.txt";
            path = path.Replace("\r", "").Replace("\n", "");
            if (!File.Exists(path))
            {
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine("Printer1" + "," + "50000" + "," + "127.0.0.1");
                };
            }
            int pc = CountLines(path), counter = 0;
            if (pc != 0)
            {
                string whole = File.ReadAllText(path);
                whole = whole.Replace("\r", "").Replace("\n", "");
                string[] split = whole.Split(',');
                while (counter < pc)
                {
                    AddPrinter(split[counter * 3 + 0], split[counter * 3 + 1], split[counter * 3 + 2]);
                    counter++;
                }
            }

        }
        public int CountLines(string file)
        {
            int count = 0;
            count = File.ReadLines(file).Count();
            return count;
        }

        private void PrinterConfig_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Controls["textbox" + PrinterCount.Text].Text = "somethingelse";
                string path = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterConfigFile.txt";
                path = path.Replace("\r", "").Replace("\n", "");
                string TextboxName, printerinfo = "";
                int TextboxNumber = 0;
                File.WriteAllText(path, String.Empty);
                for (int pc = 0; pc < Int32.Parse(PrinterCount.Text); pc++)
                {
                    for (int r = 0; r <= 2; r++)
                    {
                            TextboxNumber = pc * 3 + r;
                        if (checkdeleted(TextboxNumber))//checks if a textbox was one that was deleted
                        {
                            TextboxName = "textBox" + TextboxNumber.ToString();
                            printerinfo = printerinfo + this.Controls[TextboxName].Text + ",";
                        }
                    }

                    using (StreamWriter w = File.AppendText(path))
                    {
                        w.WriteLine(printerinfo);
                    };
                    printerinfo = "";
                }

                //removes extra lines from deleted printers.
                var lines = File.ReadAllLines(path).Where(arg => !string.IsNullOrWhiteSpace(arg));
                File.WriteAllLines(path, lines);

                // All for print history file correction

                //for deleting
                string HistPath = "", HistPath2 = "";
                if (Deleted.Text != "")
                { Deleted.Text = Deleted.Text.Remove(Deleted.Text.Length - 1, 1); }
                
                string[] deleted = Deleted.Text.Split(',');

                for (int x = 0; x < Int32.Parse(PrinterCount.Text)*3; x = x + 3)
                {
                    if (deleted.Count() != 1)
                    {
                        for (int c = deleted.Count() -1; c >= 0; c--)
                        {
                            if (Int32.Parse(deleted[c]) == x)
                            {
                                HistPath = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterHistory" + (x/3).ToString() + ".txt";
                                HistPath = HistPath.Replace("\r", "").Replace("\n", "");
                                File.Delete(HistPath); 
                            }
                        }
                    }
                }
                if (Deleted.Text != "")
                {
                    for (int z = 0; z < CountLines(path) + (deleted.Count() / 3); z++)
                    {
                        for (int y = 0; y < CountLines(path) + (deleted.Count() / 3); y++)
                        {
                            HistPath = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterHistory" + y.ToString() + ".txt";
                            HistPath = HistPath.Replace("\r", "").Replace("\n", "");
                            HistPath2 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterHistory" + (y + 1).ToString() + ".txt";
                            HistPath2 = HistPath2.Replace("\r", "").Replace("\n", "");
                            if (!File.Exists(HistPath) && File.Exists(HistPath2))
                            {
                                System.IO.File.Move(HistPath2, HistPath);
                            }
                        }
                    }
                }
                //for adding
                string path2 = "";
                int flag = Addedprinter, flag2 = 0; //flag is number of added printers if any flag2 increments everytime a file is generated
                                                    //This should lead to adding a new print hist file for each added printer or none if 
                for (int g = 0; flag2 < flag ; g++)
                {
                    path2 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterHistory" + g.ToString() + ".txt";
                    path2 = path2.Replace("\r", "").Replace("\n", "");
                    if (!File.Exists(path2))
                    {
                        using (StreamWriter w = File.AppendText(path2))
                        {
                            w.WriteLine("1" + "," + "0");
                        }
                        flag2++;
                    }
                }
                //End of print history file correction

                //closes printerconfig window
                this.Close(); 
            }
            catch(Exception ex)
            {
                MessageBox.Show("Contact your system admin if you are seeing this message" + System.Environment.NewLine + ex.ToString());
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void DeletePrinter()
        {
            int textboxnumber = Int32.Parse(this.ActiveControl.Name.Substring(this.ActiveControl.Name.Length - 1)) * 3 ;
            this.Controls["textbox" + textboxnumber.ToString()].Dispose();
            Deleted.Text = Deleted.Text + textboxnumber.ToString() + ",";
            textboxnumber++;
            this.Controls["textbox" + textboxnumber.ToString()].Dispose();
            Deleted.Text = Deleted.Text + textboxnumber.ToString() + ",";
            textboxnumber++;
            this.Controls["textbox" + textboxnumber.ToString()].Dispose();
            Deleted.Text = Deleted.Text + textboxnumber.ToString() + ",";
            //int d = Int32.Parse(this.ActiveControl.Name.Substring(this.ActiveControl.Name.Length - 1)) + 1;//for debugging
            //MessageBox.Show("You have pressed delete button. For printer #:" + d.ToString()); //for debugging

            

            this.ActiveControl.Dispose();
        }
        bool checkdeleted(int number)
        {
            string[] deleted = Deleted.Text.Split(',');
            if (deleted.Count() != 1)
            {
                for (int c = deleted.Count() - 2; c >= 0; c--)
                {
                    if (Int32.Parse(deleted[c]) == number)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
