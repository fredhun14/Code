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
using NiceLabel.SDK;
using System.Reflection;
using System.Data.SqlClient;

namespace ReplacementForWestMark
{
    public partial class Print_Manual_Ticket : Form
    {
        public Print_Manual_Ticket()
        {
            InitializeComponent();
        }
        #region Weight/LineNumber
        public decimal weight = 0;
        public decimal _weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (weight != value)
                    weight = value;
            }
        }
        private string LineNumber = "00";
        public string _LineNumber
        {
            get
            {
                return LineNumber;
            }
            set
            {
                if (LineNumber != value)
                    LineNumber = value;
            }
        }
        #endregion
        #region Nice Label
        private void InitializePrintEngine()
        {
            try
            {
                string sdkFilesPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..\\SDKFiles");
                if (Directory.Exists(sdkFilesPath))
                {
                    PrintEngineFactory.SDKFilesPath = sdkFilesPath;
                }

                PrintEngineFactory.PrintEngine.Initialize();
            }
            catch (SDKException exception)
            {
                MessageBox.Show("Initialization of the SDK failed." + Environment.NewLine + Environment.NewLine + exception.ToString());
                Application.Exit();
            }
        }
        #endregion
        #region Form Load/Close
        private void Print_Manual_Ticket_Load(object sender, EventArgs e)
        {
            string path = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterConfigFile.txt";
            path = path.Replace("\r", "").Replace("\n", "");
            InitializePrintEngine();
            if (File.Exists(path))
            {
                string[] printers = File.ReadAllText(path).Split(',');
                for (int f = 0; f < printers.Length; f += 3)
                {
                    PrinterCombo.Items.Add(printers[f]);
                }
                PrinterCombo.SelectedIndex = 0;
            }
            
        }
        private void Print_Manual_Ticket_FormClosed(object sender, FormClosedEventArgs e)
        {
            PrintEngineFactory.PrintEngine.Shutdown();
        }
        #endregion
        #region Helper Methods
        string getdatafromgsheettext(int row, int column)
        {
            string path8 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Gsheet.txt";
            path8 = path8.Replace("\r", "").Replace("\n", "");
            string[] lines = File.ReadAllLines(path8);
            string[] items;
            string[,] data = new string[lines.Length, 66];
            for (int c = 0; c < lines.Length; c++)
            {
                items = lines[c].Split('╨');
                for (int r = 0; r < 65; r++)
                {
                    data[c, r] = items[r];
                }
            }
            return data[row + 1, column];
        }
        /// Helper method to convert Bytes to image.
        /// </summary>
        /// <param name="bytes">The image as a byte array.</param>
        /// <returns>Bitmap of the image.</returns>
        private Bitmap ByteToImage(byte[] bytes)
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(bytes, 0, Convert.ToInt32(bytes.Length));
            Bitmap bm = new Bitmap(memoryStream, false);
            memoryStream.Dispose();
            return bm;
        }
        #endregion
        #region Triggers/Buttons
        private void PrinterCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path2 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterHistory" + PrinterCombo.SelectedIndex.ToString() + ".txt";
            path2 = path2.Replace("\r", "").Replace("\n", "");
            string[] printhist = File.ReadAllText(path2).Split(',');
            ResourceLabel.Text = getdatafromgsheettext(Int32.Parse(printhist[0]), 0);
            ColumnData00.Text = getdatafromgsheettext(Int32.Parse(printhist[0]), 30);
            ColumnData01.Text = getdatafromgsheettext(Int32.Parse(printhist[0]), 26);
            ColumnData02.Text = getdatafromgsheettext(Int32.Parse(printhist[0]), 40);
            ColumnData03.Text = getdatafromgsheettext(Int32.Parse(printhist[0]), 41);
        }
        private void Print_Click(object sender, EventArgs e)
        {
            DisablePrint();
            ButtonTimoutTimer.Enabled = true;
            string path2 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterHistory" + PrinterCombo.SelectedIndex.ToString() + ".txt";
            path2 = path2.Replace("\r", "").Replace("\n", "");
            string[] printhist = File.ReadAllText(path2).Split(',');
            int p = Int32.Parse(printhist[0]);
            string path4 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\DateCodes" + ".txt";
            path4 = path4.Replace("\r", "").Replace("\n", "");
            string[] datecodedata = File.ReadAllText(path4).Split(',');
            string Path6 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Images\\";
            Path6 = Path6.Replace("\r", "").Replace("\n", "");
            string path8 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Gsheet.txt";
            path8 = path8.Replace("\r", "").Replace("\n", "");
            string[] header = File.ReadLines(path8).Skip(0).Take(1).First().Split('╨');
            string[] resource = File.ReadLines(path8).Skip(p + 1).Take(1).First().Split('╨');
            string Path3 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Formats\\" + "Manifest" + ".nlbl";
            Path3 = Path3.Replace("\r", "").Replace("\n", "");
            string path9 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\ManifestPrinterConfigFile.txt";
            path9 = path9.Replace("\r", "").Replace("\n", "");
            string[] printer = File.ReadAllText(path9).Split(','); //gets name of manifest printer
            string path10 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\SerialNumber.txt";
            path10 = path10.Replace("\r", "").Replace("\n", "");
            string path11 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\GeneralSettings.txt";
            path11 = path11.Replace("\r", "").Replace("\n", "");
            var pf = Application.OpenForms.OfType<PrimaryForm>().First();
            if (ResourceLabel.Text == getdatafromgsheettext(Int32.Parse(printhist[0]), 0))
            {
                #region SQL stuff
                //Data Source=SFFNT8;Initial Catalog=ReplacementDB;Persist Security Info=True;User ID=software;Password=***********
                #region Donotopen
                string connString = "Data Source=SFFNT8;Initial Catalog=ReplacementDB;User ID=software;Password=!Mk!03625;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                #endregion
                string cmdString = "INSERT INTO DBO.Case_Archive_Data (SERIAL_NUMBER, RES_NUMBER, SIZE_PRINT, ITEM_NUM_PRINT, BRAND_PRINT, LABEL_PRINT, GRADE_PRINT, PRODUCT_PRINT, IMG_PRINT1, IMG_PRINT2, IMG_PRINT3, IMG_PRINT4, IMG_PRINT5, NUT_IMAGE, ADDRESS1, ADDRESS2, ADDRESS3, CITY, STATE, ZIP, PRODUCT_ORGIN, ORGIN_SPARE, BARCODE, CODE, KOSHER, ORGANIC, HALAH, PALLET_SIZE, UPC_CODE, AS400_DESCRIPTION, UNIT_PER_CASE, FORMAT_NAME, STICKER_FRONT, STICKER_SIDE, DAY_CODE, PRE_PC, PACK_CODE, PRE_EC, EXP_CODE, POST_MONTH, POST_DAYS, CASE_USED, POLY_CODE, INSTR1, INSTR2, INSTR3, INSTR4, PRD_CORN, PRD_CORN_TYPE, PRD_PEAS, PRD_ONION, PRD_CARROTS, PRD_GARB, PRD_LIMAS, PRD_GBEAN, PRD_COB, PRD_SPARE3, PRD_SPARE4, PRD_DICE_SIZE, PRODUCT_CLASS,SIZE_CLASS, BRAND, LABEL) VALUES (@val0,@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13,@val14,@val15,@val16,@val17,@val18,@val19,@val20,@val21,@val22,@val23,@val24,@val25,@val26,@val27,@val28,@val29,@val30,@val31,@val32,@val33,@val34,@val35,@val36,@val37,@val38,@val39,@val40,@val41,@val42,@val43,@val44,@val45,@val46,@val47,@val48,@val49,@val50,@val51,@val52,@val53,@val54,@val55,@val56,@val57,@val58,@val59,@val60,@val61,@val62)";
                #region GetGsheetTextFile
                string[] lines = File.ReadAllLines(path8);
                string[] items;
                string[,] data = new string[lines.Length, 66];
                for (int c = 0; c < lines.Length - 1; c++)
                {
                    items = lines[c + 1].Split('╨');
                    for (int r = 0; r < 65; r++)
                    {
                        data[c, r] = items[r];
                    }
                }
                #endregion
                #endregion
                try
                {
                    ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(Path3);
                    label.PrintSettings.PrinterName = printer[3];
                    // SPECIAL FOR MANUAL TICKET CaseCount (Numericupdown) Will be assigned to Pallet_Size Variable on the label
                    for (int f = 0; f < header.Length - 1; f++)
                    {
                        switch (f)
                        {
                            case 7://img1
                                if (resource[7].ToString() != "")
                                {
                                    label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                }
                                break;
                            case 8://img2
                                if (resource[7].ToString() != "")
                                {
                                    label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                }
                                break;
                            case 9://img3
                                if (resource[7].ToString() != "")
                                {
                                    label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                }
                                break;
                            case 10://img4
                                if (resource[7].ToString() != "")
                                {
                                    label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                }
                                break;
                            case 11://img5
                                if (resource[7].ToString() != "")
                                {
                                    label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                }
                                break;
                            case 12://nut image
                                if (resource[7].ToString() != "")
                                {
                                    label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                }
                                break;
                            case 23://Kosher
                                if (resource[7].ToString() != "")
                                {
                                    label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                }
                                break;
                            case 24://organic
                                if (resource[7].ToString() != "")
                                {
                                    label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                }
                                break;
                            case 25://hallah
                                if (resource[7].ToString() != "")
                                {
                                    label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                }
                                break;
                            case 26://Case Count
                                label.Variables[header[f]].SetValue(CaseCount.Value.ToString());
                                break;
                            case 33://daycode
                                for (int g = 0; g < datecodedata.Length; g++)
                                {
                                    if (datecodedata[g] == resource[f])
                                    {
                                        if (g + 1 < datecodedata.Length)
                                        {
                                            label.Variables[header[f]].SetValue(CodeReturnGenerator(datecodedata[g + 1], resource[38]));
                                            g = g + datecodedata.Length;
                                        }
                                    }
                                    else
                                    {
                                        label.Variables[header[f]].SetValue(resource[f]);
                                    }
                                }
                                break;
                            case 37://expcode
                                for (int g = 0; g < datecodedata.Length; g++)
                                {
                                    if (datecodedata[g] == resource[f])
                                    {
                                        if (g + 1 < datecodedata.Length)
                                        {
                                            label.Variables[header[f]].SetValue(CodeReturnGenerator(datecodedata[g + 1], resource[38]));
                                            g = g + datecodedata.Length;
                                        }
                                    }
                                    else
                                    {
                                        label.Variables[header[f]].SetValue(resource[f]);
                                    }
                                }
                                break;
                            default:
                                label.Variables[header[f]].SetValue(resource[f].ToString());
                                break;
                        }
                    }
                    label.Variables["SERIAL_NUMBER"].SetValue(pf._SerialNumber);
                    //What actually sends the print command:
                    label.Print(1);
                    #region SQL Stuff
                    //for sql table
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        using (SqlCommand comm = new SqlCommand())
                        {
                            comm.Connection = conn;
                            comm.CommandText = cmdString;
                            if (data != null && data.Length > 0)
                            {
                                try
                                {
                                    comm.Parameters.AddWithValue("@Val0", pf._SerialNumber.PadLeft(7,'0'));
                                    comm.Parameters.AddWithValue("@val1", data[Int32.Parse(printhist[0]), 0]);
                                    comm.Parameters.AddWithValue("@val2", data[Int32.Parse(printhist[0]), 1]);
                                    comm.Parameters.AddWithValue("@val3", data[Int32.Parse(printhist[0]), 2]);
                                    comm.Parameters.AddWithValue("@val4", data[Int32.Parse(printhist[0]), 3]);
                                    comm.Parameters.AddWithValue("@val5", data[Int32.Parse(printhist[0]), 4]);
                                    comm.Parameters.AddWithValue("@val6", data[Int32.Parse(printhist[0]), 5]);
                                    comm.Parameters.AddWithValue("@val7", data[Int32.Parse(printhist[0]), 6]);
                                    comm.Parameters.AddWithValue("@val8", data[Int32.Parse(printhist[0]), 7]);
                                    comm.Parameters.AddWithValue("@val9", data[Int32.Parse(printhist[0]), 8]);
                                    comm.Parameters.AddWithValue("@val10", data[Int32.Parse(printhist[0]), 9]);
                                    comm.Parameters.AddWithValue("@val11", data[Int32.Parse(printhist[0]), 10]);
                                    comm.Parameters.AddWithValue("@val12", data[Int32.Parse(printhist[0]), 11]);
                                    comm.Parameters.AddWithValue("@val13", data[Int32.Parse(printhist[0]), 12]);
                                    comm.Parameters.AddWithValue("@val14", data[Int32.Parse(printhist[0]), 13]);
                                    comm.Parameters.AddWithValue("@val15", data[Int32.Parse(printhist[0]), 14]);
                                    comm.Parameters.AddWithValue("@val16", data[Int32.Parse(printhist[0]), 15]);
                                    comm.Parameters.AddWithValue("@val17", data[Int32.Parse(printhist[0]), 16]);
                                    comm.Parameters.AddWithValue("@val18", data[Int32.Parse(printhist[0]), 17]);
                                    comm.Parameters.AddWithValue("@val19", data[Int32.Parse(printhist[0]), 18]);
                                    comm.Parameters.AddWithValue("@val20", data[Int32.Parse(printhist[0]), 19]);
                                    comm.Parameters.AddWithValue("@val21", data[Int32.Parse(printhist[0]), 20]);
                                    comm.Parameters.AddWithValue("@val22", data[Int32.Parse(printhist[0]), 21]);
                                    comm.Parameters.AddWithValue("@val23", data[Int32.Parse(printhist[0]), 22]);
                                    comm.Parameters.AddWithValue("@val24", data[Int32.Parse(printhist[0]), 23]);
                                    comm.Parameters.AddWithValue("@val25", data[Int32.Parse(printhist[0]), 24]);
                                    comm.Parameters.AddWithValue("@val26", data[Int32.Parse(printhist[0]), 25]);
                                    comm.Parameters.AddWithValue("@val27", CaseCount.Value.ToString());
                                    comm.Parameters.AddWithValue("@val28", data[Int32.Parse(printhist[0]), 27]);
                                    comm.Parameters.AddWithValue("@val29", data[Int32.Parse(printhist[0]), 28]);
                                    comm.Parameters.AddWithValue("@val30", data[Int32.Parse(printhist[0]), 29]);
                                    comm.Parameters.AddWithValue("@val31", data[Int32.Parse(printhist[0]), 30]);
                                    comm.Parameters.AddWithValue("@val32", data[Int32.Parse(printhist[0]), 31]);
                                    comm.Parameters.AddWithValue("@val33", data[Int32.Parse(printhist[0]), 32]);
                                    comm.Parameters.AddWithValue("@val34", data[Int32.Parse(printhist[0]), 33]);
                                    comm.Parameters.AddWithValue("@val35", data[Int32.Parse(printhist[0]), 34]);
                                    comm.Parameters.AddWithValue("@val36", data[Int32.Parse(printhist[0]), 35]);
                                    comm.Parameters.AddWithValue("@val37", data[Int32.Parse(printhist[0]), 36]);
                                    comm.Parameters.AddWithValue("@val38", data[Int32.Parse(printhist[0]), 37]);
                                    comm.Parameters.AddWithValue("@val39", data[Int32.Parse(printhist[0]), 38]);
                                    comm.Parameters.AddWithValue("@val40", data[Int32.Parse(printhist[0]), 39]);
                                    comm.Parameters.AddWithValue("@val41", data[Int32.Parse(printhist[0]), 40]);
                                    comm.Parameters.AddWithValue("@val42", data[Int32.Parse(printhist[0]), 41]);
                                    comm.Parameters.AddWithValue("@val43", data[Int32.Parse(printhist[0]), 42]);
                                    comm.Parameters.AddWithValue("@val44", data[Int32.Parse(printhist[0]), 43]);
                                    comm.Parameters.AddWithValue("@val45", data[Int32.Parse(printhist[0]), 44]);
                                    comm.Parameters.AddWithValue("@val46", data[Int32.Parse(printhist[0]), 45]);
                                    comm.Parameters.AddWithValue("@val47", data[Int32.Parse(printhist[0]), 46]);
                                    comm.Parameters.AddWithValue("@val48", data[Int32.Parse(printhist[0]), 47]);
                                    comm.Parameters.AddWithValue("@val49", data[Int32.Parse(printhist[0]), 48]);
                                    comm.Parameters.AddWithValue("@val50", data[Int32.Parse(printhist[0]), 49]);
                                    comm.Parameters.AddWithValue("@val51", data[Int32.Parse(printhist[0]), 50]);
                                    comm.Parameters.AddWithValue("@val52", data[Int32.Parse(printhist[0]), 51]);
                                    comm.Parameters.AddWithValue("@val53", data[Int32.Parse(printhist[0]), 52]);
                                    comm.Parameters.AddWithValue("@val54", data[Int32.Parse(printhist[0]), 53]);
                                    comm.Parameters.AddWithValue("@val55", data[Int32.Parse(printhist[0]), 54]);
                                    comm.Parameters.AddWithValue("@val56", data[Int32.Parse(printhist[0]), 55]);
                                    comm.Parameters.AddWithValue("@val57", data[Int32.Parse(printhist[0]), 56]);
                                    comm.Parameters.AddWithValue("@val58", data[Int32.Parse(printhist[0]), 57]);
                                    comm.Parameters.AddWithValue("@val59", data[Int32.Parse(printhist[0]), 58]);
                                    comm.Parameters.AddWithValue("@val60", data[Int32.Parse(printhist[0]), 59]);
                                    comm.Parameters.AddWithValue("@val61", data[Int32.Parse(printhist[0]), 60]);
                                    comm.Parameters.AddWithValue("@val62", data[Int32.Parse(printhist[0]), 61]);
                                }
                                catch (Exception g)
                                {
                                    MessageBox.Show("Contact your system admin if you are seeing this message" + System.Environment.NewLine + g.ToString());
                                }
                                try
                                {
                                    conn.Open();
                                    comm.ExecuteNonQuery();
                                    string[] gsettings = File.ReadAllText(path11).Split(',');
                                    DateTime now = DateTime.Now;
                                    #region Truncate variables
                                    if (data[Int32.Parse(printhist[0]), 28].Length > 15)
                                    {
                                        data[Int32.Parse(printhist[0]), 28] = data[Int32.Parse(printhist[0]), 28].Substring(0, 15);
                                    }
                                    #endregion
                                    string NewLine = "\"Line "+ LineNumber.PadRight(3,' ') + "\",\"" + ("U/L").PadRight(18, ' ') + "\"," + CaseCount.Value.ToString().ToString().PadLeft(5, ' ') + "," +
                                    ("0").PadLeft(5, ' ') + "," + pf._SerialNumber.PadLeft(7, '0') + ",\"" + data[Int32.Parse(printhist[0]), 28].PadLeft(15, ' ') + "\",\"" + data[Int32.Parse(printhist[0]), 28].PadLeft(15, ' ') +
                                    "\"," + CaseCount.Value.ToString().PadLeft(7, ' ') + "," + gsettings[0].PadLeft(6, ' ') + ",\"" + NoAlphaDaycode().PadRight(10, ' ') + "\",\"" + now.ToString("HH:mm ") + now.ToString("tt").ToLower() + "\",\"" +
                                    now.ToString("MM/dd/yy") + "\",\"" + data[Int32.Parse(printhist[0]), 0].PadRight(15, ' ') + "\"," + pf._SerialNumber.PadLeft(8, '0') + "," + ("").PadRight(8, '0') + ",\"" +
                                    Remarks1TextBox.Text.PadRight(30, ' ') + "\",\"" + Remarks2TextBox.Text.PadRight(30, ' ') + "\",\"" + Remarks3TextBox.Text.PadRight(30, ' ') + "\",\"     " +
                                    "\",\"     " + "\"";
                                    string exppath = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\" + CodeReturnGenerator("dddyy", "0") + "REPACK.txt";
                                    exppath = exppath.Replace("\r", "").Replace("\n", "");
                                    using (StreamWriter w = File.AppendText(exppath))
                                    {
                                        w.WriteLine(NewLine);
                                    };
                                    pf._SerialNumber = (Int32.Parse(pf._SerialNumber) + 1).ToString().PadLeft(7, '0');
                                    File.WriteAllText(path10, pf._SerialNumber);
                                }
                                catch (SqlException g)
                                {
                                    MessageBox.Show("Contact your system admin if you are seeing this message" + System.Environment.NewLine + g.ToString());
                                }
                            }
                        }

                    }


                    #endregion
                }
                catch (Exception f)
                {
                    if (f.Message.Contains("An error occured during loading the file"))
                    {
                        MessageBox.Show("Label Format not found");
                    }
                    else
                    {
                        MessageBox.Show("Contact your system admin if you are seeing this message" + System.Environment.NewLine + f.ToString());
                    }
                }
            }
            else if (ResourceLabel.Text != getdatafromgsheettext(Int32.Parse(printhist[0]), 0))
            {
                Get_Weight config = new Get_Weight();
                var dialogresult = config.ShowDialog();
                if (dialogresult == DialogResult.OK)
                {
                    try
                    {
                        ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(Path3);
                        label.PrintSettings.PrinterName = printer[3];
                        // SPECIAL FOR MANUAL TICKET Weightwill be assigned to Pallet_Size Variable on the label and in the export file
                        // ALSO 0 WILL BE ASSIGNED THE CONTENTS OF RESOURCE NUMBER TEXT BOX
                        #region For Export
                        DateTime now = DateTime.Now;
                        string newline = weight.ToString().PadLeft(5, ' ') + ",00000," + ResourceLabel.Text.PadRight(16, ' ') + ",\"" + now.ToString("HH:mm") + " " + now.ToString("tt").ToLower() + "\",\"" + now.ToString("MM/dd/yy") + "\"," + LineNumber + ",\"" + daycodeGenerator() + "\"," + pf._SerialNumber.PadLeft(7, '0') + ",\"\",\"TOTECO    \",\"" + Remarks1TextBox.Text + "\",\"" + Remarks2TextBox.Text + "\",\"" + Remarks3TextBox.Text + "\",\"" + "\",\"\"";
                        string exppath = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\" + CodeReturnGenerator("dddyy","0") + "_Manual_RPK.txt";
                        exppath = exppath.Replace("\r", "").Replace("\n", "");
                        using (StreamWriter w = File.AppendText(exppath))
                        {
                            w.WriteLine(newline);
                        };
                        pf._SerialNumber = (Int32.Parse(pf._SerialNumber) + 1).ToString().PadLeft(7, '0');
                        File.WriteAllText(path10, pf._SerialNumber);
                        #endregion
                        for (int f = 0; f < header.Length - 1; f++)
                        {
                            switch (f)
                            {
                                case 0://Resource Number
                                    label.Variables[header[f]].SetValue(ResourceLabel.Text);
                                    break;
                                case 7://img1
                                    if (resource[7].ToString() != "")
                                    {
                                        label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                    }
                                    break;
                                case 8://img2
                                    if (resource[7].ToString() != "")
                                    {
                                        label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                    }
                                    break;
                                case 9://img3
                                    if (resource[7].ToString() != "")
                                    {
                                        label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                    }
                                    break;
                                case 10://img4
                                    if (resource[7].ToString() != "")
                                    {
                                        label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                    }
                                    break;
                                case 11://img5
                                    if (resource[7].ToString() != "")
                                    {
                                        label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                    }
                                    break;
                                case 12://nut image
                                    if (resource[7].ToString() != "")
                                    {
                                        label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                    }
                                    break;
                                case 23://Kosher
                                    if (resource[7].ToString() != "")
                                    {
                                        label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                    }
                                    break;
                                case 24://organic
                                    if (resource[7].ToString() != "")
                                    {
                                        label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                    }
                                    break;
                                case 25://hallah
                                    if (resource[7].ToString() != "")
                                    {
                                        label.Variables[header[f]].SetValue(Path6 + resource[f].ToString() + ".jpg");
                                    }
                                    break;
                                case 26://Weight
                                    label.Variables[header[f]].SetValue(weight.ToString());
                                    break;
                                case 33://daycode
                                    for (int g = 0; g < datecodedata.Length; g++)
                                    {
                                        if (datecodedata[g] == resource[f])
                                        {
                                            if (g + 1 < datecodedata.Length)
                                            {
                                                label.Variables[header[f]].SetValue(CodeReturnGenerator(datecodedata[g + 1], resource[38]));
                                                g = g + datecodedata.Length;
                                            }
                                        }
                                        else
                                        {
                                            label.Variables[header[f]].SetValue(resource[f]);
                                        }
                                    }
                                    break;
                                case 37://expcode
                                    for (int g = 0; g < datecodedata.Length; g++)
                                    {
                                        if (datecodedata[g] == resource[f])
                                        {
                                            if (g + 1 < datecodedata.Length)
                                            {
                                                label.Variables[header[f]].SetValue(CodeReturnGenerator(datecodedata[g + 1], resource[38]));
                                                g = g + datecodedata.Length;
                                            }
                                        }
                                        else
                                        {
                                            label.Variables[header[f]].SetValue(resource[f]);
                                        }
                                    }
                                    break;
                                default:
                                    label.Variables[header[f]].SetValue(resource[f].ToString());
                                    break;
                            }
                        }
                        label.Variables["SERIAL_NUMBER"].SetValue(pf._SerialNumber);
                        //What actually sends the print command:
                        label.Print(1);
                    }
                    catch (Exception f)
                    {
                        if (f.Message.Contains("An error occured during loading the file"))
                        {
                            MessageBox.Show("Label Format not found");
                        }
                        else
                        {
                            MessageBox.Show("Contact your system admin if you are seeing this message" + System.Environment.NewLine + f.ToString());
                        }
                    }
                }
            }
        
        }
        #endregion
        #region datecode generation
        string NoAlphaDaycode()
        {
            string thereturn = "";
            thereturn = thereturn + JulianDateGenerator();
            thereturn = thereturn + DateTime.Now.ToString("y").Substring(DateTime.Now.ToString("y").Length - 1);
            thereturn = thereturn + LineNumber;
            return thereturn;
        }
        string daycodeGenerator()
        {
            string thereturn = "";
            thereturn = twohouralpha();
            thereturn = thereturn + JulianDateGenerator();
            thereturn = thereturn + DateTime.Now.ToString("y").Substring(DateTime.Now.ToString("y").Length - 1);
            thereturn = thereturn + LineNumber;
            return thereturn;
        }
        public string CodeReturnGenerator(string s, string exp)
        {
            string thereturn = "";
            char[] code = s.ToCharArray();
            if (s != "")
            {
                if (s.Substring(s.Length - 1) != "E")
                {
                    for (int f = 0; f < code.Length; f++)
                    {
                        DateTime now = DateTime.Now;
                        if (now.Hour <= 7)
                        { now = now.AddDays(-1); }
                        #region Days
                        if (code[f] == 'd')
                        {
                            #region two d two digit day
                            if (f + 1 < code.Length)
                            {
                                if (code[f + 1] == 'd')
                                {
                                    #region Three d three digit day
                                    if (f + 2 < code.Length)
                                    {
                                        if (code[f + 2] == 'd')
                                        {
                                            thereturn = thereturn + JulianDateGenerator();
                                            f = f + 2;
                                        }
                                        #endregion
                                        else
                                        {
                                            if (now.Day.ToString().Length > 1)
                                            { thereturn = thereturn + now.Day.ToString(); }
                                            else { thereturn = thereturn + "0" + now.Day.ToString(); }
                                            f = f + 1;
                                        }
                                    }
                                    else
                                    {
                                        if (now.Day.ToString().Length > 1)
                                        { thereturn = thereturn + now.Day.ToString(); }
                                        else { thereturn = thereturn + "0" + now.Day.ToString(); }
                                        f = f + 1;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    thereturn = thereturn + DateTime.Now.ToString("dd").Substring(DateTime.Now.ToString("dd").Length - 1);
                                }
                            }
                            else
                            {
                                thereturn = thereturn + DateTime.Now.ToString("dd").Substring(DateTime.Now.ToString("dd").Length - 1);
                            }
                        }
                        #endregion
                        #region Years
                        else if (code[f] == 'y')
                        {
                            #region two y two digit year
                            if (f + 1 < code.Length)
                            {
                                if (code[f + 1] == 'y')
                                {
                                    #region Three y three digit year
                                    if (f + 2 < code.Length)
                                    {
                                        if (code[f + 2] == 'y')
                                        {
                                            #region four y four digit year
                                            if (f + 3 < code.Length)
                                            {
                                                if (code[f + 3] == 'y')
                                                {
                                                    thereturn = thereturn + DateTime.Now.ToString("yyyy");
                                                    f = f + 3;
                                                }
                                                #endregion
                                                else
                                                {
                                                    thereturn = thereturn + DateTime.Now.ToString("yyy").Substring(DateTime.Now.ToString("yyy").Length - 3);
                                                    f = f + 2;
                                                }
                                            }
                                            else
                                            {
                                                thereturn = thereturn + DateTime.Now.ToString("yyy").Substring(DateTime.Now.ToString("yyy").Length - 3);
                                                f = f + 2;
                                            }
                                        }
                                        #endregion
                                        else
                                        {
                                            thereturn = thereturn + DateTime.Now.ToString("yy");
                                            f = f + 1;
                                        }

                                    }
                                    else
                                    {
                                        thereturn = thereturn + DateTime.Now.ToString("yy");
                                        f = f + 1;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    thereturn = thereturn + DateTime.Now.ToString("y").Substring(DateTime.Now.ToString("y").Length - 1);
                                }
                            }
                            else
                            {
                                thereturn = thereturn + DateTime.Now.ToString("y").Substring(DateTime.Now.ToString("y").Length - 1);
                            }
                        }
                        #endregion
                        #region DayOfWeek
                        else if (code[f] == 'w')
                        {
                            #region Abbreviated day of the week
                            if (f + 1 < code.Length)
                            {
                                if (code[f + 1] == 'w')
                                {
                                    thereturn = thereturn + now.DayOfWeek.ToString().Substring(0, 3);
                                    f = f + 1;
                                }
                                #endregion
                                else
                                {
                                    thereturn = thereturn + now.DayOfWeek.ToString();
                                }
                            }
                            else
                            {
                                thereturn = thereturn + now.DayOfWeek.ToString();
                            }

                        }
                        #endregion
                        #region Month
                        #region Single digit month
                        else if (code[f] == 'm')
                        {
                            #region 2 digit month
                            if (f + 1 < code.Length)
                            {
                                if (code[f + 1] == 'm')
                                {
                                    #region 3 char abbreviation
                                    if (f + 2 < code.Length)
                                    {
                                        if (code[f + 2] == 'm')
                                        {
                                            thereturn = thereturn + now.ToString("MMM");
                                            f = f + 2;
                                        }
                                        #endregion
                                        else
                                        {
                                            if (now.Month.ToString().Length > 1)
                                            { thereturn = thereturn + now.Month.ToString(); }
                                            else { thereturn = thereturn + "0" + now.Month.ToString(); }
                                            f = f + 1;
                                        }
                                    }
                                    else
                                    {
                                        if (now.Month.ToString().Length > 1)
                                        { thereturn = thereturn + now.Month.ToString(); }
                                        else { thereturn = thereturn + "0" + now.Month.ToString(); }
                                        f = f + 1;
                                    }
                                }
                                #endregion
                                else
                                {
                                    thereturn = thereturn + now.Month.ToString().Substring(now.Month.ToString().Length - 1);
                                }
                            }
                            else
                            {
                                thereturn = thereturn + now.Month.ToString().Substring(now.Month.ToString().Length - 1);
                            }
                        }
                        #endregion
                        #region Two Char Month 
                        else if (code[f] == 'M')
                        {
                            thereturn = thereturn + twocharmonth();
                        }
                        #endregion
                        #endregion
                        #region  Two hour alpha code
                        else if (code[f] == 'a')
                        {
                            thereturn = thereturn + twohouralpha();
                        }
                        #endregion
                        else if (s[f] != 'E')
                        {
                            thereturn = thereturn + s[f];
                        }
                    }
                }
                else if (s.Substring(s.Length - 1) == "E")
                {
                    for (int f = 0; f < code.Length; f++)
                    {
                        DateTime now = DateTime.Now.AddMonths(Int32.Parse(exp));
                        if (now.Hour <= 7)
                        { now = now.AddDays(-1); }
                        #region Days
                        if (code[f] == 'd')
                        {
                            #region two d two digit day
                            if (f + 1 < code.Length)
                            {
                                if (code[f + 1] == 'd')
                                {
                                    #region Three d three digit day
                                    if (f + 2 < code.Length)
                                    {
                                        if (code[f + 2] == 'd')
                                        {
                                            thereturn = thereturn + JulianDateGenerator();
                                            f = f + 2;
                                        }
                                        #endregion
                                        else
                                        {
                                            if (now.Day.ToString().Length > 1)
                                            { thereturn = thereturn + now.Day.ToString(); }
                                            else { thereturn = thereturn + "0" + now.Day.ToString(); }
                                            f = f + 1;
                                        }
                                    }
                                    else
                                    {
                                        if (now.Day.ToString().Length > 1)
                                        { thereturn = thereturn + now.Day.ToString(); }
                                        else { thereturn = thereturn + "0" + now.Day.ToString(); }
                                        f = f + 1;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    thereturn = thereturn + DateTime.Now.ToString("dd").Substring(DateTime.Now.ToString("dd").Length - 1);
                                }
                            }
                            else
                            {
                                thereturn = thereturn + DateTime.Now.ToString("dd").Substring(DateTime.Now.ToString("dd").Length - 1);
                            }
                        }
                        #endregion
                        #region Years
                        else if (code[f] == 'y')
                        {
                            #region two y two digit year
                            if (f + 1 < code.Length)
                            {
                                if (code[f + 1] == 'y')
                                {
                                    #region Three y three digit year
                                    if (f + 2 < code.Length)
                                    {
                                        if (code[f + 2] == 'y')
                                        {
                                            #region four y four digit year
                                            if (f + 3 < code.Length)
                                            {
                                                if (code[f + 3] == 'y')
                                                {
                                                    thereturn = thereturn + DateTime.Now.AddMonths(24).ToString("yyyy");
                                                    f = f + 3;
                                                }
                                                #endregion
                                                else
                                                {
                                                    thereturn = thereturn + DateTime.Now.AddMonths(24).ToString("yyy").Substring(DateTime.Now.AddMonths(24).ToString("yyy").Length - 3);
                                                    f = f + 2;
                                                }
                                            }
                                            else
                                            {
                                                thereturn = thereturn + DateTime.Now.AddMonths(24).ToString("yyy").Substring(DateTime.Now.AddMonths(24).ToString("yyy").Length - 3);
                                                f = f + 2;
                                            }
                                        }
                                        #endregion
                                        else
                                        {
                                            thereturn = thereturn + DateTime.Now.AddMonths(24).ToString("yy");
                                            f = f + 1;
                                        }

                                    }
                                    else
                                    {
                                        thereturn = thereturn + DateTime.Now.AddMonths(24).ToString("yy");
                                        f = f + 1;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    thereturn = thereturn + DateTime.Now.AddMonths(24).ToString("y").Substring(DateTime.Now.AddMonths(24).ToString("y").Length - 1);
                                }
                            }
                            else
                            {
                                thereturn = thereturn + DateTime.Now.AddMonths(24).ToString("y").Substring(DateTime.Now.AddMonths(24).ToString("y").Length - 1);
                            }
                        }
                        #endregion
                        #region DayOfWeek
                        else if (code[f] == 'w')
                        {
                            #region Abbreviated day of the week
                            if (f + 1 < code.Length)
                            {
                                if (code[f + 1] == 'w')
                                {
                                    thereturn = thereturn + now.DayOfWeek.ToString().Substring(0, 3);
                                    f = f + 1;
                                }
                                #endregion
                                else
                                {
                                    thereturn = thereturn + now.DayOfWeek.ToString();
                                }
                            }
                            else
                            {
                                thereturn = thereturn + now.DayOfWeek.ToString();
                            }

                        }
                        #endregion
                        #region Month
                        #region Single digit month
                        else if (code[f] == 'm')
                        {
                            #region 2 digit month
                            if (f + 1 < code.Length)
                            {
                                if (code[f + 1] == 'm')
                                {
                                    #region 3 char abbreviation
                                    if (f + 2 < code.Length)
                                    {
                                        if (code[f + 2] == 'm')
                                        {
                                            thereturn = thereturn + now.ToString("MMM");
                                            f = f + 2;
                                        }
                                        #endregion
                                        else
                                        {
                                            if (now.Month.ToString().Length > 1)
                                            { thereturn = thereturn + now.Month.ToString(); }
                                            else { thereturn = thereturn + "0" + now.Month.ToString(); }
                                            f = f + 1;
                                        }
                                    }
                                    else
                                    {
                                        if (now.Month.ToString().Length > 1)
                                        { thereturn = thereturn + now.Month.ToString(); }
                                        else { thereturn = thereturn + "0" + now.Month.ToString(); }
                                        f = f + 1;
                                    }
                                }
                                #endregion
                                else
                                {
                                    thereturn = thereturn + now.Month.ToString().Substring(now.Month.ToString().Length - 1);
                                }
                            }
                            else
                            {
                                thereturn = thereturn + now.Month.ToString().Substring(now.Month.ToString().Length - 1);
                            }
                        }
                        #endregion
                        #region Two Char Month 
                        else if (code[f] == 'M')
                        {
                            thereturn = thereturn + twocharmonth();
                        }
                        #endregion
                        #endregion
                        #region  Two hour alpha code
                        else if (code[f] == 'a')
                        {
                            thereturn = thereturn + twohouralpha();
                        }
                        #endregion
                        else if (s[f] != 'E')
                        {
                            thereturn = thereturn + s[f];
                        }
                    }
                }

            }
            return thereturn;
        }
        public string JulianDateGenerator()
        {
            string Julian = "";
            int add;
            DateTime now = DateTime.Now;
            if (now.Hour <= 7)
            { now = now.AddDays(-1); }
            #region Not a leap Year
            if (DateTime.IsLeapYear(now.Year) == false)
            {
                if (now.Month == 1)
                {
                    Julian = now.Day.ToString();
                }
                else if (now.Month == 2)
                {
                    add = 31 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 3)
                {
                    add = 59 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 4)
                {
                    add = 90 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 5)
                {
                    add = 120 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 6)
                {
                    add = 151 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 7)
                {
                    add = 181 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 8)
                {
                    add = 212 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 9)
                {
                    add = 243 + now.Day;
                    Julian = add.ToString();

                }
                else if (now.Month == 10)
                {
                    add = 273 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 11)
                {
                    add = 304 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 12)
                {
                    add = 334 + now.Day;
                    Julian = add.ToString();
                }
            }
            #endregion
            #region Is a Leap Year
            else if (DateTime.IsLeapYear(now.Year) == true)
            {
                if (now.Month == 1)
                {
                    Julian = now.Day.ToString();
                }
                else if (now.Month == 2)
                {
                    add = 31 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 3)
                {
                    add = 60 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 4)
                {
                    add = 91 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 5)
                {
                    add = 121 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 6)
                {
                    add = 152 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 7)
                {
                    add = 182 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 8)
                {
                    add = 213 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 9)
                {
                    add = 244 + now.Day;
                    Julian = add.ToString();

                }
                else if (now.Month == 10)
                {
                    add = 274 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 11)
                {
                    add = 305 + now.Day;
                    Julian = add.ToString();
                }
                else if (now.Month == 12)
                {
                    add = 335 + now.Day;
                    Julian = add.ToString();
                }
            }
            #endregion
            return Julian;
        }
        public string twocharmonth()
        {
            DateTime now = DateTime.Now;
            string thereturn = "";
            if (now.Month == 1)
            {
                thereturn = "JA";
            }
            else if (now.Month == 2)
            {
                thereturn = "FE";
            }
            else if (now.Month == 3)
            {
                thereturn = "MR";
            }
            else if (now.Month == 4)
            {
                thereturn = "AL";
            }
            else if (now.Month == 5)
            {
                thereturn = "MA";
            }
            else if (now.Month == 6)
            {
                thereturn = "JN";
            }
            else if (now.Month == 7)
            {
                thereturn = "JL";
            }
            else if (now.Month == 8)
            {
                thereturn = "AU";
            }
            else if (now.Month == 9)
            {
                thereturn = "SE";
            }
            else if (now.Month == 10)
            {
                thereturn = "OC";
            }
            else if (now.Month == 11)
            {
                thereturn = "NO";
            }
            else if (now.Month == 12)
            {
                thereturn = "DE";
            }

            return thereturn;
        }
        public string twohouralpha()
        {
            string thereturn = "";
            DateTime now = DateTime.Now;
            if (now.Hour == 7 || now.Hour == 8)
            {
                thereturn = "A";
            }
            else if (now.Hour == 9 || now.Hour == 10)
            {
                thereturn = "B";
            }
            else if (now.Hour == 11 || now.Hour == 12)
            {
                thereturn = "C";
            }
            else if (now.Hour == 13 || now.Hour == 14)
            {
                thereturn = "D";
            }
            else if (now.Hour == 15 || now.Hour == 16)
            {
                thereturn = "E";
            }
            else if (now.Hour == 17 || now.Hour == 18)
            {
                thereturn = "F";
            }
            else if (now.Hour == 19 || now.Hour == 20)
            {
                thereturn = "G";
            }
            else if (now.Hour == 21 || now.Hour == 22)
            {
                thereturn = "H";
            }
            else if (now.Hour == 23 || now.Hour == 0)
            {
                thereturn = "J";
            }
            else if (now.Hour == 1 || now.Hour == 2)
            {
                thereturn = "K";
            }
            else if (now.Hour == 3 || now.Hour == 4)
            {
                thereturn = "L";
            }
            else if (now.Hour == 5 || now.Hour == 6)
            {
                thereturn = "M";
            }

            return thereturn;
        }
        #endregion
        #region Button timer
        void EnablePrint()
        {
            Printbut.Enabled = true;
        }
        void DisablePrint()
        {
            Printbut.Enabled = false;
        }
        private void ButtonTimoutTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                EnablePrint();
                ButtonTimoutTimer.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }
        #endregion
    }
}
