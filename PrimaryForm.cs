using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Data.SqlClient;
using NiceLabel.SDK;
using System.Reflection;
using PcapDotNet.Packets;
using PcapDotNet.Base;
using PcapDotNet.Core;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Icmp;
using PcapDotNet.Packets.Transport;
//connection string: Data Source=SFFNT8;User ID=software;Password=********;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
// ╨ is ALT + 2256

namespace ReplacementForWestMark
{
    public partial class PrimaryForm : Form
    {
        #region PCAP TESTS
        void testpacket(int row)
        {
            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;
            PacketDevice selectedDevice = null;
            for (int x = 0; x < allDevices.Count; x++)
            {
                if (allDevices[x].Name == "Ethernet2")
                {
                    selectedDevice = allDevices[x];
                }
            }
            using (PacketCommunicator communicator = selectedDevice.Open(100, // name of the device
                                                                         PacketDeviceOpenAttributes.Promiscuous, // promiscuous mode
                                                                         1000)) // read timeout
            {
                #region Pcap examples
                //// Supposing to be on ethernet, set mac source to 01:01:01:01:01:01
                //MacAddress source = new MacAddress("01:01:01:01:01:01");

                //// set mac destination to 02:02:02:02:02:02
                //MacAddress destination = new MacAddress("02:02:02:02:02:02");

                //// Create the packets layers

                //// Ethernet Layer
                //EthernetLayer ethernetLayer = new EthernetLayer
                //{
                //    Source = source,
                //    Destination = destination
                //};

                //// IPv4 Layer
                //IpV4Layer ipV4Layer = new IpV4Layer
                //{
                //    Source = new IpV4Address("1.2.3.4"),
                //    Ttl = 128,
                //    // The rest of the important parameters will be set for each packet
                //};

                //// ICMP Layer
                //IcmpEchoLayer icmpLayer = new IcmpEchoLayer();

                //// Create the builder that will build our packets
                //PacketBuilder builder = new PacketBuilder(ethernetLayer, ipV4Layer, icmpLayer);

                //// Send 100 Pings to different destination with different parameters
                //for (int i = 0; i != 100; ++i)
                //{
                //    // Set IPv4 parameters
                //    ipV4Layer.CurrentDestination = new IpV4Address("2.3.4." + i);
                //    ipV4Layer.Identification = (ushort)i;

                //    // Set ICMP parameters
                //    icmpLayer.SequenceNumber = (ushort)i;
                //    icmpLayer.Identifier = (ushort)i;

                //    // Build the packet
                //    Packet packet = builder.Build(DateTime.Now);

                //    // Send down the packet
                //    communicator.SendPacket(packet);
                //}
                #endregion
                communicator.SendPacket(BuildEthernetPacket(getdatafromgsheettext(row,41)));
            }
        }
        private static Packet BuildEthernetPacket(string jobname)
        {
            byte[] ba = Encoding.Default.GetBytes(jobname);
            var hexString = BitConverter.ToString(ba);
            hexString = hexString.Replace("-", " ");
            EthernetLayer ethernetLayer =
                new EthernetLayer
                {
                    Source = new MacAddress("01:01:01:01:01:01"),
                    Destination = new MacAddress("02:02:02:02:02:02"),
                    EtherType = EthernetType.IpV4,
                };
            //// IPv4 Layer
            //IpV4Layer ipV4Layer = new IpV4Layer
            //{
            //    Source = new IpV4Address("1.2.3.4"),
            //    Ttl = 128,
            //    // The rest of the important parameters will be set for each packet
            //};
            PayloadLayer payloadLayer =
                new PayloadLayer
                {
                    Data = new Datagram(Encoding.ASCII.GetBytes("A0h (02h) " + hexString + " (03h)")),
                };

            PacketBuilder builder = new PacketBuilder(ethernetLayer, payloadLayer);

            return builder.Build(DateTime.Now);
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
        #region For Security
        private int SecLevel = 0;
        public int _SecLevel
        {
            get
            {
                return SecLevel;
            }
            set
            {
                if (SecLevel != value)
                    SecLevel = value;
            }
        }
        #endregion
        #region Pallet Serial#
        private string SerialNumber = "0";
        public string _SerialNumber
        {
            get
            {
                return SerialNumber;
            }
            set
            {
                if (SerialNumber != value)
                    SerialNumber = value;
            }
        }
        #endregion
        #region For google API to work
        // stuff to make google api work
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "Form1";
        public string gsheetstring = "";
        #endregion
        #region For readloop
        private volatile bool _shouldStop;
        public PrimaryForm()
        {
            InitializeComponent();
        }
        public void RequestStop()
        {
            _shouldStop = true;
        }
        public void ResetStop()
        {
            _shouldStop = false;
        }
        #endregion
        #region Main Form Load

        private void login()
        {
            Login login = new Login();
            var dialogresult = login.ShowDialog();
                loginsettings();
        }
        private void ReplacementForWestMark_Load(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                MessageBox.Show("The program is already running");
                this.Close();
            }
            InitializePrintEngine();
            this.WindowState = FormWindowState.Maximized;
            GenerateNesecaryFiles();
            string path10 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\SerialNumber.txt";
            path10 = path10.Replace("\r", "").Replace("\n", "");
            SerialNumber = File.ReadAllText(path10);
            getGsheet();
            displayprinters();
            displayfinalpreview();
            string path = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterConfigFile.txt";
            path = path.Replace("\r", "").Replace("\n", "");
            int printercount = CountLines(path);
            for (int p = 0; p < printercount; p++)
            {
                APIClient(p);
            }
            login();
        }
        private void PrimaryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            PrintEngineFactory.PrintEngine.Shutdown();
        }

        #endregion
        #region PrinterAPI
        public void APIClient(int printer)
        {
            try
            {
                string path = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterConfigFile.txt";
                path = path.Replace("\r", "").Replace("\n", "");
                string[] printers = File.ReadAllText(path).Split(',');
                string host = printers[printer*3+1];
                int port = int.Parse(printers[printer * 3 + 2]);
                string command = "{\"method\": \"api.notifications.add\"," +
                                 "\"params\": " +
                                     "{\"name\": \"sequence.state.changed\"," +
                                     "\"options\": " +
                                         "{\"include_print_record\": true,\"" +
                                             "include_layout\": true}" +
                                         "}," +
                                         "\"id\": 0," +
                                         "\"jsonrpc\": \"2.0\"" +
                             "}";
                TcpClient tcpClient;
                tcpClient = new TcpClient();
                //tcpClient.Connect(host, port);
                if (!tcpClient.ConnectAsync(host, port).Wait(1500))
                {
                    MessageBox.Show(printers[printer * 3] + " did not connect!");
                    this.Controls["panel" + printer.ToString()].BackColor = System.Drawing.Color.FromArgb(255, 0, 0);
                    this.Controls["panel" + printer.ToString()].Controls["Detailsbutton" + printer.ToString()].Enabled = false;
                    this.Controls["panel" + printer.ToString()].Controls["PrintButton" + printer.ToString()].Enabled = false;
                }
                else
                {
                    NetworkStream netstream = tcpClient.GetStream();
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
                    netstream.Write(data, 0, data.Length);

                    new Thread(delegate ()
                    {
                        readloop(new StreamReader(netstream), printer);
                    }).Start();
                }
            }catch(Exception e)
            {
                if(e.Message == "A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 10.10.50.63:50000")
                {
                    outputtestbox.Text = e.Message;
                }
                else if(e.Message.Contains("A connection attempt failed because the connected party did not properly respond after a period of time"))
                {
                    string path = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterConfigFile.txt";
                    path = path.Replace("\r", "").Replace("\n", "");
                    string[] printers = File.ReadAllText(path).Split(',');
                    string host = printers[printer * 3 + 1];
                    if (e.Message.Contains(host))
                    {
                       MessageBox.Show(printers[printer * 3] + " did not connect!");
                        this.Controls["panel" + printer.ToString()].BackColor = System.Drawing.Color.FromArgb(255,0,0);
                        this.Controls["panel" + printer.ToString()].Controls["Detailsbutton"].Enabled = false;
                        this.Controls["panel" + printer.ToString()].Controls["PrintButton"].Enabled = false;
                    }
                }
                else
                {   
                    MessageBox.Show("Contact your system admin if you are seeing this message" + System.Environment.NewLine + e.ToString());
                }
            }
        }

        void readloop(StreamReader reader, int printer)
        {
            try
            {
                Thread.CurrentThread.IsBackground = true;
                reader.BaseStream.ReadTimeout = 200;
                string HistPath = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterHistory" +printer.ToString() + ".txt";
                HistPath = HistPath.Replace("\r", "").Replace("\n", "");
                string[] PrintHist = File.ReadAllText(HistPath).Split(',');
                string path11 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\GeneralSettings.txt";
                path11 = path11.Replace("\r", "").Replace("\n", "");
                //removes any carrige returns form the printer data
                for (int f = 0; f < PrintHist.Count(); f++)
                {
                    PrintHist[f] = PrintHist[f].Replace("\r", "").Replace("\n", "");
                }
                string HistJoin = "";
                string responseData = string.Empty;
                string newline = string.Empty;
                #region SQL stuff
                //Data Source=SFFNT8;Initial Catalog=ReplacementDB;Persist Security Info=True;User ID=software;Password=***********
                #region Donotopen
                string connString = "Data Source=SFFNT8;Initial Catalog=ReplacementDB;User ID=software;Password=!Mk!03625;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                #endregion
                string cmdString = "INSERT INTO DBO.Case_Archive_Data (SERIAL_NUMBER, RES_NUMBER, SIZE_PRINT, ITEM_NUM_PRINT, BRAND_PRINT, LABEL_PRINT, GRADE_PRINT, PRODUCT_PRINT, IMG_PRINT1, IMG_PRINT2, IMG_PRINT3, IMG_PRINT4, IMG_PRINT5, NUT_IMAGE, ADDRESS1, ADDRESS2, ADDRESS3, CITY, STATE, ZIP, PRODUCT_ORGIN, ORGIN_SPARE, BARCODE, CODE, KOSHER, ORGANIC, HALAH, PALLET_SIZE, UPC_CODE, AS400_DESCRIPTION, UNIT_PER_CASE, FORMAT_NAME, STICKER_FRONT, STICKER_SIDE, DAY_CODE, PRE_PC, PACK_CODE, PRE_EC, EXP_CODE, POST_MONTH, POST_DAYS, CASE_USED, POLY_CODE, INSTR1, INSTR2, INSTR3, INSTR4, PRD_CORN, PRD_CORN_TYPE, PRD_PEAS, PRD_ONION, PRD_CARROTS, PRD_GARB, PRD_LIMAS, PRD_GBEAN, PRD_COB, PRD_SPARE3, PRD_SPARE4, PRD_DICE_SIZE, PRODUCT_CLASS,SIZE_CLASS, BRAND, LABEL) VALUES (@val0,@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13,@val14,@val15,@val16,@val17,@val18,@val19,@val20,@val21,@val22,@val23,@val24,@val25,@val26,@val27,@val28,@val29,@val30,@val31,@val32,@val33,@val34,@val35,@val36,@val37,@val38,@val39,@val40,@val41,@val42,@val43,@val44,@val45,@val46,@val47,@val48,@val49,@val50,@val51,@val52,@val53,@val54,@val55,@val56,@val57,@val58,@val59,@val60,@val61,@val62)";
                #endregion
                // int newcount = Int32.Parse(this.Controls["panel" + printer.ToString()].Controls["Count"].Text);
                while (!_shouldStop)
                {
                    try { newline = reader.ReadLine(); } catch (Exception e)
                    { if (e.Message.Contains("Unable to read data from the transport connection: A connection attempt failed because the connected party did not properly respond after a period of time,"))
                        { }
                        else { MessageBox.Show("Contact your system admin if you are seeing this message" + System.Environment.NewLine + e.ToString()); }
                    }
                    if (newline != responseData)
                    {
                        responseData = newline;
                        if (responseData != null)
                        {
                            // if (responseData.Contains("\"state\": \"finished\", ") || responseData.Contains("{\"print_records\": [{\"success\": true,"))
                            if (responseData.Contains("{\"state\": \"idle\"}"))
                            {
                                //newcount++; 
                                #region GetGsheetTextFile
                                string path8 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Gsheet.txt";
                                path8 = path8.Replace("\r", "").Replace("\n", "");
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
                                PrintHist = File.ReadAllText(HistPath).Split(',');
                                //removes any carrige returns from the printer data
                                for (int f = 0; f < PrintHist.Count(); f++)
                                {
                                    PrintHist[f] = PrintHist[f].Replace("\r", "").Replace("\n", "");
                                }
                                PrintHist[1] = (Int32.Parse(PrintHist[1]) + 1).ToString();
                                for (int f = 0; f < PrintHist.Count(); f++)
                                {
                                    HistJoin = HistJoin + PrintHist[f] + ",";
                                }
                                HistJoin = HistJoin.Remove(HistJoin.Length - 1, 1);
                                if (File.Exists(HistPath))
                                {
                                    File.WriteAllText(HistPath, HistJoin);
                                }
                                HistJoin = "";
                                #region SQL Stuff
                                if (Int32.Parse(PrintHist[1]) % Int32.Parse(data[Int32.Parse(PrintHist[0]), 26]) == 0)
                                {
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
                                                    comm.Parameters.AddWithValue("@Val0", SerialNumber.PadLeft(7,'0'));
                                                    comm.Parameters.AddWithValue("@val1", data[Int32.Parse(PrintHist[0]), 0]);
                                                    comm.Parameters.AddWithValue("@val2", data[Int32.Parse(PrintHist[0]), 1]);
                                                    comm.Parameters.AddWithValue("@val3", data[Int32.Parse(PrintHist[0]), 2]);
                                                    comm.Parameters.AddWithValue("@val4", data[Int32.Parse(PrintHist[0]), 3]);
                                                    comm.Parameters.AddWithValue("@val5", data[Int32.Parse(PrintHist[0]), 4]);
                                                    comm.Parameters.AddWithValue("@val6", data[Int32.Parse(PrintHist[0]), 5]);
                                                    comm.Parameters.AddWithValue("@val7", data[Int32.Parse(PrintHist[0]), 6]);
                                                    comm.Parameters.AddWithValue("@val8", data[Int32.Parse(PrintHist[0]), 7]);
                                                    comm.Parameters.AddWithValue("@val9", data[Int32.Parse(PrintHist[0]), 8]);
                                                    comm.Parameters.AddWithValue("@val10", data[Int32.Parse(PrintHist[0]), 9]);
                                                    comm.Parameters.AddWithValue("@val11", data[Int32.Parse(PrintHist[0]), 10]);
                                                    comm.Parameters.AddWithValue("@val12", data[Int32.Parse(PrintHist[0]), 11]);
                                                    comm.Parameters.AddWithValue("@val13", data[Int32.Parse(PrintHist[0]), 12]);
                                                    comm.Parameters.AddWithValue("@val14", data[Int32.Parse(PrintHist[0]), 13]);
                                                    comm.Parameters.AddWithValue("@val15", data[Int32.Parse(PrintHist[0]), 14]);
                                                    comm.Parameters.AddWithValue("@val16", data[Int32.Parse(PrintHist[0]), 15]);
                                                    comm.Parameters.AddWithValue("@val17", data[Int32.Parse(PrintHist[0]), 16]);
                                                    comm.Parameters.AddWithValue("@val18", data[Int32.Parse(PrintHist[0]), 17]);
                                                    comm.Parameters.AddWithValue("@val19", data[Int32.Parse(PrintHist[0]), 18]);
                                                    comm.Parameters.AddWithValue("@val20", data[Int32.Parse(PrintHist[0]), 19]);
                                                    comm.Parameters.AddWithValue("@val21", data[Int32.Parse(PrintHist[0]), 20]);
                                                    comm.Parameters.AddWithValue("@val22", data[Int32.Parse(PrintHist[0]), 21]);
                                                    comm.Parameters.AddWithValue("@val23", data[Int32.Parse(PrintHist[0]), 22]);
                                                    comm.Parameters.AddWithValue("@val24", data[Int32.Parse(PrintHist[0]), 23]);
                                                    comm.Parameters.AddWithValue("@val25", data[Int32.Parse(PrintHist[0]), 24]);
                                                    comm.Parameters.AddWithValue("@val26", data[Int32.Parse(PrintHist[0]), 25]);
                                                    comm.Parameters.AddWithValue("@val27", data[Int32.Parse(PrintHist[0]), 26]);
                                                    comm.Parameters.AddWithValue("@val28", data[Int32.Parse(PrintHist[0]), 27]);
                                                    comm.Parameters.AddWithValue("@val29", data[Int32.Parse(PrintHist[0]), 28]);
                                                    comm.Parameters.AddWithValue("@val30", data[Int32.Parse(PrintHist[0]), 29]);
                                                    comm.Parameters.AddWithValue("@val31", data[Int32.Parse(PrintHist[0]), 30]);
                                                    comm.Parameters.AddWithValue("@val32", data[Int32.Parse(PrintHist[0]), 31]);
                                                    comm.Parameters.AddWithValue("@val33", data[Int32.Parse(PrintHist[0]), 32]);
                                                    comm.Parameters.AddWithValue("@val34", data[Int32.Parse(PrintHist[0]), 33]);
                                                    comm.Parameters.AddWithValue("@val35", data[Int32.Parse(PrintHist[0]), 34]);
                                                    comm.Parameters.AddWithValue("@val36", data[Int32.Parse(PrintHist[0]), 35]);
                                                    comm.Parameters.AddWithValue("@val37", data[Int32.Parse(PrintHist[0]), 36]);
                                                    comm.Parameters.AddWithValue("@val38", data[Int32.Parse(PrintHist[0]), 37]);
                                                    comm.Parameters.AddWithValue("@val39", data[Int32.Parse(PrintHist[0]), 38]);
                                                    comm.Parameters.AddWithValue("@val40", data[Int32.Parse(PrintHist[0]), 39]);
                                                    comm.Parameters.AddWithValue("@val41", data[Int32.Parse(PrintHist[0]), 40]);
                                                    comm.Parameters.AddWithValue("@val42", data[Int32.Parse(PrintHist[0]), 41]);
                                                    comm.Parameters.AddWithValue("@val43", data[Int32.Parse(PrintHist[0]), 42]);
                                                    comm.Parameters.AddWithValue("@val44", data[Int32.Parse(PrintHist[0]), 43]);
                                                    comm.Parameters.AddWithValue("@val45", data[Int32.Parse(PrintHist[0]), 44]);
                                                    comm.Parameters.AddWithValue("@val46", data[Int32.Parse(PrintHist[0]), 45]);
                                                    comm.Parameters.AddWithValue("@val47", data[Int32.Parse(PrintHist[0]), 46]);
                                                    comm.Parameters.AddWithValue("@val48", data[Int32.Parse(PrintHist[0]), 47]);
                                                    comm.Parameters.AddWithValue("@val49", data[Int32.Parse(PrintHist[0]), 48]);
                                                    comm.Parameters.AddWithValue("@val50", data[Int32.Parse(PrintHist[0]), 49]);
                                                    comm.Parameters.AddWithValue("@val51", data[Int32.Parse(PrintHist[0]), 50]);
                                                    comm.Parameters.AddWithValue("@val52", data[Int32.Parse(PrintHist[0]), 51]);
                                                    comm.Parameters.AddWithValue("@val53", data[Int32.Parse(PrintHist[0]), 52]);
                                                    comm.Parameters.AddWithValue("@val54", data[Int32.Parse(PrintHist[0]), 53]);
                                                    comm.Parameters.AddWithValue("@val55", data[Int32.Parse(PrintHist[0]), 54]);
                                                    comm.Parameters.AddWithValue("@val56", data[Int32.Parse(PrintHist[0]), 55]);
                                                    comm.Parameters.AddWithValue("@val57", data[Int32.Parse(PrintHist[0]), 56]);
                                                    comm.Parameters.AddWithValue("@val58", data[Int32.Parse(PrintHist[0]), 57]);
                                                    comm.Parameters.AddWithValue("@val59", data[Int32.Parse(PrintHist[0]), 58]);
                                                    comm.Parameters.AddWithValue("@val60", data[Int32.Parse(PrintHist[0]), 59]);
                                                    comm.Parameters.AddWithValue("@val61", data[Int32.Parse(PrintHist[0]), 60]);
                                                    comm.Parameters.AddWithValue("@val62", data[Int32.Parse(PrintHist[0]), 61]);
                                                }
                                                catch (Exception e)
                                                {
                                                    MessageBox.Show("Contact your system admin if you are seeing this message" + System.Environment.NewLine + e.ToString());
                                                }
                                                try
                                                {
                                                    string path10 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\SerialNumber.txt";
                                                    path10 = path10.Replace("\r", "").Replace("\n", "");
                                                    PrintManifest(printer);
                                                    string[] gsettings = File.ReadAllText(path11).Split(',');
                                                    conn.Open();
                                                    comm.ExecuteNonQuery();
                                                    DateTime now = DateTime.Now;
                                                    #region Truncate variables
                                                    if(data[Int32.Parse(PrintHist[0]), 28].Length >15)
                                                    {
                                                        data[Int32.Parse(PrintHist[0]), 28] = data[Int32.Parse(PrintHist[0]), 28].Substring(0, 15);
                                                    }
                                                    #endregion
                                                    string NewLine = "\"Line " + printer.ToString().PadRight(3, ' ') + "\",\"" + data[Int32.Parse(PrintHist[0]), 4].PadRight(18, ' ') + "\"," + data[Int32.Parse(PrintHist[0]), 26].PadLeft(5, ' ') + "," +
                                                    ("0").PadLeft(5, ' ') + "," + SerialNumber.PadLeft(7, '0') + ",\"" + data[Int32.Parse(PrintHist[0]), 28].PadLeft(15, ' ') + "\",\"" + data[Int32.Parse(PrintHist[0]), 28].PadLeft(15, ' ') +
                                                    "\"," + data[Int32.Parse(PrintHist[0]), 26].PadLeft(7, ' ') + "," + gsettings[0].PadLeft(6, ' ') + ",\"" + NoAlphaDaycode(printer).PadRight(10, ' ') + "\",\"" + now.ToString("HH:mm ") + now.ToString("tt").ToLower() + "\",\"" +
                                                    now.ToString("MM/dd/yy") + "\",\"" + data[Int32.Parse(PrintHist[0]), 0].PadRight(15, ' ') + "\"," + SerialNumber.PadLeft(8, '0') + "," + ("").PadRight(8, '0') + ",\"" +
                                                    ("").PadRight(30, ' ') + "\",\"" + ("").PadRight(30, ' ') + "\",\"" + ("").PadRight(30, ' ') + "\",\"     " +
                                                    "\",\"     " + "\"";
                                                    string exppath = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\" + CodeReturnGenerator("dddyy", "0") + "REPACK.txt";
                                                    exppath = exppath.Replace("\r", "").Replace("\n", "");
                                                    using (StreamWriter w = File.AppendText(exppath))
                                                    {
                                                        w.WriteLine(NewLine);
                                                    };
                                                    SerialNumber = (Int32.Parse(SerialNumber) + 1).ToString().PadLeft(7, '0');
                                                    File.WriteAllText(path10, SerialNumber);
                                                }
                                                catch (SqlException e)
                                                {
                                                    MessageBox.Show("Contact your system admin if you are seeing this message" + System.Environment.NewLine + e.ToString());
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                            #endregion
                            Invoke(new Action(() =>
                            {
                                if (this.Controls.ContainsKey("panel" + printer.ToString()))
                                {
                                    this.Controls["panel" + printer.ToString()].Controls["Count"].Text = PrintHist[1];
                                }
                                outputtestbox.AppendText(responseData + System.Environment.NewLine);
                                outputtestbox.AppendText("printer" + printer + System.Environment.NewLine);
                            }));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (e.Message == "Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host.")
                {
                    string path = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterConfigFile.txt";
                    path = path.Replace("\r", "").Replace("\n", "");
                    int printercount = CountLines(path);
                    for (int p = 0; p < printercount; p++)
                    {
                        APIClient(p);
                    }
                }
                else if (e.Message.Contains("because it is being used by another process."))
                { }
                else if (e.Message.Contains("Unable to read data from the transport connection: A connection attempt failed because the connected party did not properly respond after a period of time,")) 
                { }
                else
                {
                    MessageBox.Show("Contact your system admin if you are seeing this message" + System.Environment.NewLine + e.ToString());
                }
            };
        }
        #endregion
        #region Menu strip Actions
        private void generalSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General_Settings gsettings = new General_Settings();
            gsettings.Show();
        }
        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageUsers mnguser = new ManageUsers();
            mnguser.Show();
        }
        private void changeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            var dialogresult = login.ShowDialog();
            if(dialogresult == DialogResult.OK)
            {
                loginsettings();
            }
        }
        private void forceGsheetUpdateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            resetresourcenumbers();
            getGsheet();
        }
        private void reconnectToLabelPrintersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterConfigFile.txt";
            path = path.Replace("\r", "").Replace("\n", "");
            int printercount = CountLines(path);
            //stop tcp connection threads for reset
            RequestStop();
            wait(500);
            ResetStop();
            //threads reset.
            for (int p = 0; p < printercount; p++)
            {
                APIClient(p);
            }
        }
        private void configureDayCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DayCodeGenerator daycodegen = new DayCodeGenerator();
            daycodegen.Show();
        }
        private void configureManifestPrintersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_Manifest_Printer config = new Manage_Manifest_Printer();
            var dialogresult = config.ShowDialog();
        }
        private void configurePolyPrintersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void printManualTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print_Manual_Ticket config = new Print_Manual_Ticket();
            var dialogresult = config.ShowDialog();
        }
        private void configureLabelPrintersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrinterConfig config = new PrinterConfig();
            string path = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterConfigFile.txt";
            path = path.Replace("\r", "").Replace("\n", "");
            int printercount = CountLines(path);
            for (int pc = 0; pc < printercount; pc++)
            {
                this.Controls["panel" + pc.ToString()].Dispose();
            }
            var dialogresult = config.ShowDialog();
            //stop tcp connection threads for reset
            RequestStop();
            wait(500);
            ResetStop();
            //threads reset.
            displayprinters();
            displayfinalpreview();
            printercount = CountLines(path);
            for (int p = 0; p < printercount; p++)
            {
                APIClient(p);
            }
        }
        private void selectConfigurationPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + @"\ConfigurationPathFile.txt"))
            {
                string filename = "C:";
                using (var fbd = new FolderBrowserDialog())

                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        filename = fbd.SelectedPath.ToString();
                        File.WriteAllText(Application.StartupPath + @"\ConfigurationPathFile.txt", filename);
                        GenerateNesecaryFiles();
                    }

            }
        }
        private void configureGsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GsheetForm gconfig = new GsheetForm();
            var dialogresult = gconfig.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                string path5 = "";
                path5 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\GSheetString" + ".txt";
                path5 = path5.Replace("\r", "").Replace("\n", "");
                if (File.Exists(path5))
                {
                    gsheetstring = File.ReadAllText(path5);
                    gsheetstring = gsheetstring.Replace("\r", "").Replace("\n", "");
                    resetresourcenumbers();
                }
            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void moveWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.FormBorderStyle != FormBorderStyle.Sizable)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
            else if (this.FormBorderStyle == FormBorderStyle.Sizable)
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }
        }
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SecLevel = 0;
            loginsettings();
        }
        #endregion
        #region Generate Neccesary Files
        void GenerateNesecaryFiles()
        {
            //Creates the file that points to the configuration folder and has the user select that location
            //This should only happen the first time the program is run unless the file is deleted or the exe is moved

            if (!File.Exists(Application.StartupPath + @"\ConfigurationPathFile.txt"))
            {
                string filename = "C:";
                using (var fbd = new FolderBrowserDialog())

                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        filename = fbd.SelectedPath.ToString();
                    }

                using (StreamWriter w = File.AppendText(Application.StartupPath + @"\ConfigurationPathFile.txt"))
                {
                    w.WriteLine(filename);
                };
            }

            //Creates the Printer configuration file in the correct location and gives it a default single printer.
            string path = "";
            path = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterConfigFile.txt";
            path = path.Replace("\r", "").Replace("\n", "");
            if (!File.Exists(path))
            {
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine("Printer1" + "," + "127.0.0.1" + "," + "50000");
                };
            }
            //creates printer history files for last used resource and printer count
            string path2 = "";
            for (int g = 0; g < CountLines(path); g++)
            { 
                path2 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterHistory" + g.ToString() + ".txt";
                path2 = path2.Replace("\r", "").Replace("\n", "");
                if (!File.Exists(path2))
                {
                    using (StreamWriter w = File.AppendText(path2))
                    {
                        w.WriteLine("1" + "," + "0");
                    }
                }
            }
            //creates a folder to store and look for the bartender label formates
            string Path3 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Formats";
            Path3 = Path3.Replace("\r", "").Replace("\n", "");
            if (!System.IO.Directory.Exists(Path3))
            {
                System.IO.Directory.CreateDirectory(Path3);
            }
            //creates a file to save datecodes in
            string path4 = "";
            path4 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\DateCodes" + ".txt";
            path4 = path4.Replace("\r", "").Replace("\n", "");
            if (!File.Exists(path4))
            {
                using (StreamWriter w = File.AppendText(path4))
                {
                    w.WriteLine("01-SmithCode, adddyy,");
                }
            }
            //saves the string for which we will use to access the google sheet
            string path5 = "";
            path5 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\GSheetString" + ".txt";
            path5 = path5.Replace("\r", "").Replace("\n", "");
            if (!File.Exists(path5))
            {
                using (StreamWriter w = File.AppendText(path5))
                {
                    w.WriteLine("1YQJXtqhzqPAUtksRX7gaGunCU7z6MQ_LPEUE5-8_PVI");
                }
            }
            gsheetstring = File.ReadAllText(path5);
            gsheetstring = gsheetstring.Replace("\r", "").Replace("\n", "");

            //creates a folder to store and to look for images that will be used in the bartender labels
            string Path6 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Images";
            Path6 = Path6.Replace("\r", "").Replace("\n", "");
            if (!System.IO.Directory.Exists(Path6))
            {
                System.IO.Directory.CreateDirectory(Path6);
            }
            string path7 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\security.txt";
            path7 = path7.Replace("\r", "").Replace("\n", "");
            if (!File.Exists(path7))
            {
                using (StreamWriter w = File.AppendText(path7))
                {   
                    //Password defaults
                    //admin,Admin
                    //user.user
                    //manager,manager
                    w.WriteLine("firns,e3afed0047b08059d0fada10f400c1e5,10");//Admin
                    w.WriteLine(",zxjw,ee11cbb19052e40b07aac0ca060c23ee,1");//user
                    w.WriteLine(",rfsfljw,1d0258c2440a8d19e716292b231e3190,5");//manager
                }
            }
            string path8 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Gsheet.txt";
            path8 = path8.Replace("\r", "").Replace("\n", "");
            if(!File.Exists(path8))
            {
                File.WriteAllText(path8, "");
            }
            
            string path9 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\ManifestPrinterConfigFile.txt";
            path9 = path9.Replace("\r", "").Replace("\n", "");
            if (!File.Exists(path9))
            {
                using (StreamWriter w = File.AppendText(path9))
                {
                    w.WriteLine("Printer1" + "," + "127.0.0.1" + "," + "50000" + ",");
                    w.WriteLine("Printer2" + "," + "127.0.0.1" + "," + "50000");
                };
            }
            string path10 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\SerialNumber.txt";
            path10 = path10.Replace("\r", "").Replace("\n", "");
            if (!File.Exists(path10))
            {
                File.WriteAllText(path10, "0000000");
            }
            string path11 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\GeneralSettings.txt";
            path11 = path11.Replace("\r", "").Replace("\n", "");
            if (!File.Exists(path11))
            {
                File.WriteAllText(path11, @"1,SFFNT8,ReplacementDB,DBO.Case_Archive_Data,software,!Mk!03625,65,1YQJXtqhzqPAUtksRX7gaGunCU7z6MQ_LPEUE5-8_PVI,C:\,C:\,");
            }
        }
        #endregion
        #region Helper Methods
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
        void getGsheet()
        {
            string path8 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Gsheet.txt";
            path8 = path8.Replace("\r", "").Replace("\n", "");
            
            #region Gsheet Access
            UserCredential credential;
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.  
            //int index = ResourceNumberBox.SelectedIndex + 2;
            //int index = this.Controls["panel" + p.ToString()].Controls["PrinterComboBox" + p.ToString()].SelectedIndex + 2;

            String spreadsheetId = gsheetstring;
            String range = "SHEET1!A" + ":BM";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);
            // Prints the resource number and Label Print of products in a westmarkdata spreadsheet:
            // https://docs.google.com/spreadsheets/d/1YQJXtqhzqPAUtksRX7gaGunCU7z6MQ_LPEUE5-8_PVI/edit#gid=1781707169
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            string[,] data = new string[values.Count, 65];
            string[] newline = new string[values.Count];
            int c = 0;
            #endregion
            for (int r = 0; r < data.GetLength(0); r++)
            {
                foreach (var col in values[r])
                {
                    data[r,c] = col.ToString();
                    c++;
                }
                c = 0;
            }
            for (int f = 0; f < data.GetLength(0); f++)
            {
                for (int w = 0; w < data.GetLength(1); w++)
                {
                    newline[f] = newline[f] + data[f, w] + "╨";
                }
            }
            File.WriteAllText(path8, "");
            using (StreamWriter z = File.AppendText(path8))
            {
                for (int l = 0; l < newline.Length; l++)
                {
                    z.WriteLine(newline[l]);
                }
            }
        }
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
        //Shows or hides controls based on _seclevel
        void loginsettings()
        {
            switch(SecLevel)
            {
                case 10:
                    //file
                    moveWindowToolStripMenuItem.Visible = true;
                    //options
                    optionsToolStripMenuItem.Visible = true;
                    configurePrintersToolStripMenuItem.Visible = true;
                    configureGsheetToolStripMenuItem.Visible = true;
                    selectConfigurationPathToolStripMenuItem.Visible = true;
                    configureDayCodesToolStripMenuItem.Visible = true;
                    updatesToolStripMenuItem1.Visible = true;
                    manageUsersToolStripMenuItem.Visible = true;
                    //debugging
                    outputtestbox.Visible = true;
                    foreach (Control ctrl in this.Controls)
                    {
                        if (ctrl is Panel)
                        {
                            ctrl.Visible = true;
                        }
                    }
                    DetailPanel9.Visible = false;
                    break;
                case 5:
                    //file
                    moveWindowToolStripMenuItem.Visible = true;
                    //options
                    optionsToolStripMenuItem.Visible = true;
                    configurePrintersToolStripMenuItem.Visible = false;
                    configureGsheetToolStripMenuItem.Visible = false;
                    selectConfigurationPathToolStripMenuItem.Visible = false;
                    configureDayCodesToolStripMenuItem.Visible = true;
                    updatesToolStripMenuItem1.Visible = true;
                    manageUsersToolStripMenuItem.Visible = true;
                    //debugging
                    outputtestbox.Visible = false;
                    foreach (Control ctrl in this.Controls)
                    {
                        if (ctrl is Panel)
                        {
                            ctrl.Visible = true;
                        }
                    }
                    DetailPanel9.Visible = false;
                    break;
                case 1:
                    //file
                    moveWindowToolStripMenuItem.Visible = true;
                    //options
                    optionsToolStripMenuItem.Visible = false;
                    configurePrintersToolStripMenuItem.Visible = false;
                    configureGsheetToolStripMenuItem.Visible = false;
                    selectConfigurationPathToolStripMenuItem.Visible = false;
                    configureDayCodesToolStripMenuItem.Visible = false;
                    updatesToolStripMenuItem1.Visible = false;
                    manageUsersToolStripMenuItem.Visible = false;
                    //debugging
                    outputtestbox.Visible = false;
                    foreach (Control ctrl in this.Controls)
                    {
                        if (ctrl is Panel)
                        {
                            ctrl.Visible = true;
                        }
                    }
                    DetailPanel9.Visible = false;
                    break;
                case 0:
                    //file
                    moveWindowToolStripMenuItem.Visible = false;
                    //options
                    optionsToolStripMenuItem.Visible = false;
                    configurePrintersToolStripMenuItem.Visible = false;
                    configureGsheetToolStripMenuItem.Visible = false;
                    selectConfigurationPathToolStripMenuItem.Visible = false;
                    configureDayCodesToolStripMenuItem.Visible = false;
                    updatesToolStripMenuItem1.Visible = false;
                    manageUsersToolStripMenuItem.Visible = false;
                    //debugging
                    outputtestbox.Visible = false;
                    foreach (Control ctrl in this.Controls)
                    {
                        if (ctrl is Panel)
                        {
                            ctrl.Visible = false;
                        }
                    }
                    break;
            }

        }
        //used to fix bug where final print preview does not load when initially loading printer panels 
        void displayfinalpreview()
        {
            string path = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterConfigFile.txt";
            path = path.Replace("\r", "").Replace("\n", "");
            int printercount = CountLines(path) - 1;
            DisplayPrintPreview(printercount);
        }
        //gets neccesary referenced data from gsheet
        void printerhelper(int p)
        {
            this.Controls["panel" + p.ToString()].Controls["Format"].Text = gsheetgetdata(28);
        }
        //counts the number of lines in a file
        public int CountLines(string file)
        {
            int count = 0;
            count = File.ReadLines(file).Count();
            return count;
        }
        //waits a number of milliseconds 
        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
        //returns a specific cell based on a referenced column number and the tag on the current active control
        string gsheetgetdata(int column)
        {
            string returnvalue = "";
            #region Gsheet Access
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            #endregion
            // Define request parameters.
            int index = Int32.Parse(this.ActiveControl.Tag.ToString()) + 2;
            // GET INDEX
            #region Gsheet Access
            String spreadsheetId = gsheetstring;
            String range = "SHEET1!A" + index + ":BM" + index;
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the resource number and Label Print of products in a westmarkdata spreadsheet:
            // https://docs.google.com/spreadsheets/d/1YQJXtqhzqPAUtksRX7gaGunCU7z6MQ_LPEUE5-8_PVI/edit#gid=1781707169
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            string[,] data = new string[values.Count, 62];

            if (values != null && values.Count > 0)
            {
                #endregion
                //in here is where we put what we want from gsheet
                //currently displaying several columns for the give resource into a selection of label controls.
                foreach (var row in values)
                {
                    returnvalue = row[column].ToString();
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
            return returnvalue;
        }
        //resets the list of resource numbers to match the current online list in the gsheet
        void resetresourcenumbers()
        {
            string path = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterConfigFile.txt";
            path = path.Replace("\r", "").Replace("\n", "");
            if (File.Exists(path))
            {
                for (int f = 0; f < CountLines(path); f++)
                {
                    Control[] cs = this.Controls.Find("PrinterComboBox" + f, true);
                    ComboBox cb = cs[0] as ComboBox;
                    cb.Items.Clear();
                    cb.Items.AddRange(resourcenumbers());
                }
            }

            
        }
        #endregion
        #region Resource Numbers
        //used to return the full liost of resource numbers from gsheet
        public string[] resourcenumbers()
        {
            string[] numbers;
            #region Gsheet Access
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = gsheetstring;
            String range = "SHEET1!A1:BM662";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the resource number and Label Print of products in a westmarkdata spreadsheet:
            // https://docs.google.com/spreadsheets/d/1YQJXtqhzqPAUtksRX7gaGunCU7z6MQ_LPEUE5-8_PVI/edit#gid=1781707169
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            #endregion
            int flag = 0;
            numbers = new string[values.Count() - 1];

            if (values != null && values.Count > 0)
            {
                //in here is where we put what we want from gsheet
                foreach (var row in values.Skip(1))
                {
                    numbers[flag] = row[0].ToString();
                    flag++;
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }

            return numbers;
        }
        #endregion
        #region Display Printers
        //displays the printer panels and sets up the event triggers for changing resource numbers and the print and details buttons
        void displayprinters()
        {
            string path = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterConfigFile.txt";
            path = path.Replace("\r", "").Replace("\n", "");
            int printercount = CountLines(path);
            string[] printers = File.ReadAllText(path).Split(',');

            //removes any carrige returns form the printer data
            for (int f = 0;f < printers.Count();f++)
            {
                printers[f] = printers[f].Replace("\r", "").Replace("\n", "");
            }

            //creates the panels with appropriate buttons and labels for each printer listed in the printer config file.
            for (int pc = 0; pc < printercount; pc++)
            {
                string HistPath = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\PrinterHistory" + pc.ToString() + ".txt";
                HistPath = HistPath.Replace("\r", "").Replace("\n", "");
                string[] PrintHist = File.ReadAllText(HistPath).Split(',');
                for (int f = 0; f < PrintHist.Count(); f++)
                {
                    PrintHist[f] = PrintHist[f].Replace("\r", "").Replace("\n", "");
                }
                Label PrinterName = new Label();
                Label Format = new Label();
                Label Count = new Label();
                Label FormatLabel = new Label();
                Label PrintedLabel = new Label();
                Label ResourceLabel = new Label();
                Button Detailsbutton = new Button();
                Button PrintButton = new Button();
                PictureBox LabelImageBox = new PictureBox();
                Panel panel1 = new Panel();
                ComboBox PrinterComboBox = new ComboBox();
                // 
                // PrinterName
                // 
                PrinterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                PrinterName.Location = new System.Drawing.Point(11, 9);
                PrinterName.Name = "PrinterName";
                PrinterName.Size = new System.Drawing.Size(205, 28);
                PrinterName.TabIndex = 5;
                PrinterName.Text = printers[pc * 3];
                // 
                // Format
                // 
                Format.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Format.Location = new System.Drawing.Point(195, 51);
                Format.Name = "Format";
                Format.Size = new System.Drawing.Size(169, 28);
                Format.TabIndex = 7;
                Format.Text = "Description";
                // 
                // Count
                // 
                Count.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Count.Location = new System.Drawing.Point(370, 51);
                Count.Name = "Count";
                Count.Size = new System.Drawing.Size(109, 28);
                Count.TabIndex = 8;
                Count.Text = PrintHist[1];
                // 
                // PrinterComboBox
                // 
                PrinterComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                PrinterComboBox.FormattingEnabled = true;
                PrinterComboBox.Location = new System.Drawing.Point(16, 66);
                PrinterComboBox.Name = "PrinterComboBox" + pc.ToString();
                PrinterComboBox.Size = new System.Drawing.Size(157, 33);
                PrinterComboBox.TabIndex = 10;
                PrinterComboBox.Items.AddRange(resourcenumbers());
                PrinterComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                PrinterComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                PrinterComboBox.SelectedIndexChanged += (s, e) => 
                {
                    if (this.ActiveControl is ComboBox)
                    {
                        #region Gsheet Access
                        UserCredential credential;
                        PrinterComboBox.Tag = PrinterComboBox.SelectedIndex;
                        using (var stream =
                            new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                        {
                            // The file token.json stores the user's access and refresh tokens, and is created
                            // automatically when the authorization flow completes for the first time.
                            string credPath = "token.json";
                            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                                GoogleClientSecrets.FromStream(stream).Secrets,
                                Scopes,
                                "user",
                                CancellationToken.None,
                                new FileDataStore(credPath, true)).Result;
                            Console.WriteLine("Credential file saved to: " + credPath);
                        }

                        // Create Google Sheets API service.
                        var service = new SheetsService(new BaseClientService.Initializer()
                        {
                            HttpClientInitializer = credential,
                            ApplicationName = ApplicationName,
                        });

                        // Define request parameters.  
                        //int index = ResourceNumberBox.SelectedIndex + 2;
                        //int index = this.Controls["panel" + p.ToString()].Controls["PrinterComboBox" + p.ToString()].SelectedIndex + 2;
                        #endregion
                        int index = PrinterComboBox.SelectedIndex + 2;
                        #region Gsheet Access
                        String spreadsheetId = gsheetstring;
                        String range = "SHEET1!A" + index + ":BM" + index;
                        SpreadsheetsResource.ValuesResource.GetRequest request =
                                service.Spreadsheets.Values.Get(spreadsheetId, range);

                        // Prints the resource number and Label Print of products in a westmarkdata spreadsheet:
                        // https://docs.google.com/spreadsheets/d/1YQJXtqhzqPAUtksRX7gaGunCU7z6MQ_LPEUE5-8_PVI/edit#gid=1781707169
                        ValueRange response = request.Execute();
                        IList<IList<Object>> values = response.Values;

                        string[,] data = new string[values.Count, 62];

                        if (values != null && values.Count > 0)
                        {
                            #endregion
                            string[] PrintHistfresh = File.ReadAllText(HistPath).Split(',');
                            for (int f = 0; f < PrintHist.Count(); f++)
                            {
                                PrintHistfresh[f] = PrintHistfresh[f].Replace("\r", "").Replace("\n", "");
                            }
                            string HistJoin = "";
                            int c = Int32.Parse(this.ActiveControl.Name[this.ActiveControl.Name.Length - 1].ToString());

                            PrintHistfresh[0] = PrinterComboBox.SelectedIndex.ToString();
                            for (int f = 0; f < PrintHistfresh.Count(); f++)
                            {
                                HistJoin = HistJoin + PrintHistfresh[f] + ",";
                            }
                            HistJoin = HistJoin.Remove(HistJoin.Length - 1, 1); //removes extra 
                            if (File.Exists(HistPath))
                            {
                                File.WriteAllText(HistPath, HistJoin);
                            }
                            if (DetailPanel9.Visible == true)
                            {
                                PopulateDetails(Int32.Parse(this.ActiveControl.Name.Substring(this.ActiveControl.Name.Length - 1)));
                            }
                                DisplayPrintPreview(Int32.Parse(this.ActiveControl.Name.Substring(this.ActiveControl.Name.Length - 1)));
                        }
                        else
                        {
                            Console.WriteLine("No data found.");
                        }
                        PrinterComboBox.Tag = PrinterComboBox.SelectedIndex;
                    }
                };
                PrinterComboBox.SelectionChangeCommitted += (s, e) => 
                {
                    string[] PrintHistfresh = File.ReadAllText(HistPath).Split(',');
                    for (int f = 0; f < PrintHist.Count(); f++)
                    {
                        PrintHistfresh[f] = PrintHistfresh[f].Replace("\r", "").Replace("\n", "");
                    }
                    string HistJoin = "";
                    int c = Int32.Parse(this.ActiveControl.Name[this.ActiveControl.Name.Length - 1].ToString());

                    PrintHistfresh[1] = "0";
                    Count.Text = "0";
                    for (int f = 0; f < PrintHistfresh.Count(); f++)
                    {
                        HistJoin = HistJoin + PrintHistfresh[f] + ",";
                    }
                    HistJoin = HistJoin.Remove(HistJoin.Length - 1, 1); //removes extra 
                    if (File.Exists(HistPath))
                    {
                        File.WriteAllText(HistPath, HistJoin);
                    }
                    PrinterComboBox.Tag = PrinterComboBox.SelectedIndex;
                    RequestStop();
                    wait(500);
                    ResetStop();
                    //threads reset.
                    printercount = CountLines(path);
                    for (int p = 0; p < printercount; p++)
                    {
                        APIClient(p);
                    }
                };
                // 
                // Detailsbutton
                // 
                Detailsbutton.Location = new System.Drawing.Point(485, 60);
                Detailsbutton.Name = "Detailsbutton" + pc.ToString();
                Detailsbutton.Size = new System.Drawing.Size(91, 45);
                Detailsbutton.TabIndex = 11;
                Detailsbutton.Text = "Datails";
                Detailsbutton.UseVisualStyleBackColor = true;
                Detailsbutton.Click += (s, e) =>
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (DetailPanel9.Visible == false)
                    {
                        DetailPanel9.Visible = true;
                        DetailPanel9.Enabled = true;
                    }
                    Detailsbutton.Tag = PrinterComboBox.SelectedIndex;
                    PopulateDetails(Int32.Parse(this.ActiveControl.Name.Substring(this.ActiveControl.Name.Length - 1)));
                    DisplayPrintPreview(Int32.Parse(this.ActiveControl.Name.Substring(this.ActiveControl.Name.Length - 1)));
                    Cursor.Current = Cursors.Default;
                };
                // 
                // PrintButton
                // 
                PrintButton.Location = new System.Drawing.Point(485, 9);
                PrintButton.Name = "PrintButton" + pc.ToString();
                PrintButton.Size = new System.Drawing.Size(91, 45);
                PrintButton.TabIndex = 9;
                PrintButton.Text = "Print";
                PrintButton.UseVisualStyleBackColor = true;
                PrintButton.Click += (s, e) =>
                {
                    print(Int32.Parse(this.ActiveControl.Name.Substring(this.ActiveControl.Name.Length - 1)));
                };
               // 
               // FormatLabel
               // 
                FormatLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                FormatLabel.Location = new System.Drawing.Point(195, 23);
                FormatLabel.Name = "FormatLabel";
                FormatLabel.Size = new System.Drawing.Size(169, 28);
                FormatLabel.TabIndex = 12;
                FormatLabel.Text = "Description:";
                // 
                // PrintedLabel
                // 
                PrintedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                PrintedLabel.Location = new System.Drawing.Point(370, 23);
                PrintedLabel.Name = "PrintedLabel";
                PrintedLabel.Size = new System.Drawing.Size(109, 28);
                PrintedLabel.TabIndex = 13;
                PrintedLabel.Text = "Printed:";
                // 
                // ResourceLabel
                // 
                ResourceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ResourceLabel.Location = new System.Drawing.Point(20, 37);
                ResourceLabel.Name = "ResourceLabel";
                ResourceLabel.Size = new System.Drawing.Size(169, 28);
                ResourceLabel.TabIndex = 14;
                ResourceLabel.Text = "Resoruce:";
                // 
                // LabelImageBox
                // 
                LabelImageBox.Location = new System.Drawing.Point(589, 8);
                LabelImageBox.Name = "LabelImageBox" + pc.ToString();
                LabelImageBox.Size = new System.Drawing.Size(210, 107);
                LabelImageBox.TabIndex = 15;
                LabelImageBox.TabStop = false;
                LabelImageBox.BackColor = System.Drawing.Color.White;
                LabelImageBox.SizeMode = PictureBoxSizeMode.StretchImage;
                // 
                // panel1
                // 
                panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
                panel1.Controls.Add(LabelImageBox);
                panel1.Controls.Add(ResourceLabel);
                panel1.Controls.Add(PrintedLabel);
                panel1.Controls.Add(FormatLabel);
                panel1.Controls.Add(Detailsbutton);
                panel1.Controls.Add(PrinterComboBox);
                panel1.Controls.Add(PrintButton);
                panel1.Controls.Add(Format);
                panel1.Controls.Add(Count);
                panel1.Controls.Add(PrinterName);
                panel1.Location = new System.Drawing.Point(25, 50 + 150*pc);
                panel1.Name = "panel" + pc.ToString();
                panel1.Size = new System.Drawing.Size(815, 126);
                panel1.TabIndex = 9;
                panel1.Tag = panel1.Name;
                this.Controls.Add(panel1);
                PrinterComboBox.SelectedIndex = Int32.Parse(PrintHist[0]);
                PrinterComboBox.Tag = PrinterComboBox.SelectedIndex;
                PrinterComboBox.Focus();
                printerhelper(pc);
            }
        }
        #endregion
        #region Detail Panel
        //sets the printer name in the detail panel and then calls the function needed to populate the rest of the detail panel
        void PopulateDetails(int num)
        {
            DetailPanel9.Controls["PrinterNameDetails"].Text = this.Controls["panel" + num.ToString()].Controls["PrinterName"].Text;
            gsheetgetdataDetails();
        }
        //populates the detail panel other than the printer name
        void gsheetgetdataDetails()
        {
            #region Gsheet Access
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            #endregion
            // Define request parameters.
            int index = Int32.Parse(this.ActiveControl.Tag.ToString()) + 2;
            // GET INDEX
            #region Gsheet Access
            String spreadsheetId = gsheetstring;
            String range = "SHEET1!A" + index + ":BM" + index;
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the resource number and Label Print of products in a westmarkdata spreadsheet:
            // https://docs.google.com/spreadsheets/d/1YQJXtqhzqPAUtksRX7gaGunCU7z6MQ_LPEUE5-8_PVI/edit#gid=1781707169
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            string[,] data = new string[values.Count, 62];

            if (values != null && values.Count > 0)
            {
                #endregion
                //in here is where we put what we want from gsheet
                //currently displaying several columns for the give resource into a selection of label controls.
                foreach (var row in values)
                {
                    DetailPanel9.Controls["ColumnData00"].Text = row[30].ToString();
                    DetailPanel9.Controls["ColumnData01"].Text = row[26].ToString();
                    DetailPanel9.Controls["ColumnData02"].Text = row[40].ToString();
                    DetailPanel9.Controls["ColumnData03"].Text = row[41].ToString();
                    DetailPanel9.Controls["ColumnData04"].Text = row[42].ToString();
                    DetailPanel9.Controls["ColumnData05"].Text = row[43].ToString();
                    DetailPanel9.Controls["ColumnData06"].Text = row[44].ToString();
                    DetailPanel9.Controls["ColumnData07"].Text = row[45].ToString();
                }
            }
        }

        //hides and disabels the detail panel
        private void HideDetailPanel_Click(object sender, EventArgs e)
        {
            DetailPanel9.Visible = false;
            DetailPanel9.Enabled = false;
        }
        #endregion
        #region Print Preview
        //Does the prep work to generate the print preview based on the active control's tag property and starts the background worker that will do the displaying
        void DisplayPrintPreview(int p)
        {
            Control[] ct = this.Controls.Find("LabelImageBox" + this.ActiveControl.Name[this.ActiveControl.Name.Length - 1].ToString(), true);
            PictureBox pb = ct[0] as PictureBox;
            Control[] cs = this.Controls.Find("PrinterComboBox" + p.ToString(), true);
            ComboBox cb = cs[0] as ComboBox;
            p = cb.SelectedIndex;
            string path4 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\DateCodes" + ".txt";
            path4 = path4.Replace("\r", "").Replace("\n", "");
            string[] datecodedata = File.ReadAllText(path4).Split(',');
            string Path6 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Images\\";
            Path6 = Path6.Replace("\r", "").Replace("\n", "");
            string path8 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Gsheet.txt";
            path8 = path8.Replace("\r", "").Replace("\n", "");
            string[] header = File.ReadLines(path8).Skip(0).Take(1).First().Split('╨');
            string[] resource = File.ReadLines(path8).Skip(p + 1).Take(1).First().Split('╨');
            string Path3 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Formats\\" + resource[30] + ".nlbl";
            Path3 = Path3.Replace("\r", "").Replace("\n", "");
            try
            {
                ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(Path3);

                for (int f = 0; f < header.Length - 1; f++)
                {
                    //switch for assigning variabels add a case for each variable that needs special instructions else it will goto default
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
                label.Variables["SERIAL_NUMBER"].SetValue(SerialNumber);
                ILabelPreviewSettings labelPreviewSettings = new LabelPreviewSettings();

                labelPreviewSettings.PreviewToFile = false;     // if true, result will be the file name, if false, result will be a byte array.
                labelPreviewSettings.ImageFormat = "jpg";                                    // file format of graphics.  Valid formats: JPG, PNG, BMP.
                labelPreviewSettings.Width = this.DetailsPictureBox.Width;                   // Width of image to generate
                labelPreviewSettings.Height = this.DetailsPictureBox.Height;                 // Height of image to generate
                //labelPreviewSettings.Destination = this.textBoxFileName.Text;                // If PrintToFile is true, this is the name of the file that will be generated.
                labelPreviewSettings.FormatPreviewSide = FormatPreviewSide.FrontSide;              // Which label side(s) to generate the image for.  
                                                                                                   // Generate Preview File
                object imageObj = label.GetLabelPreview(labelPreviewSettings);

                // Display image in UI
                if (imageObj is byte[])
                {
                    // When PrintToFiles = false
                    // Convert byte[] to Bitmap and set as image source for PictureBox control
                    this.DetailsPictureBox.Image = this.ByteToImage((byte[])imageObj);
                    pb.Image = this.ByteToImage((byte[])imageObj);
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("An error occured during loading the file"))
                {
                    MessageBox.Show("Label Format not found");
                    this.DetailsPictureBox.Image = null;
                    pb.Image = null;
                }
                else
                {
                    MessageBox.Show("Contact your system admin if you are seeing this message" + System.Environment.NewLine + e.ToString());
                }
            }
        }
        #endregion
        #region Print
        //sends a single print job to the named printer; printer name must match the printer name in windows.
        void print(int p)
        {
            Control[] cs = this.Controls.Find("PrinterComboBox" + p.ToString(), true);
            ComboBox cb = cs[0] as ComboBox;
            Control cs2 = this.Controls["panel" + p.ToString()].Controls["PrinterName"];
            Label lb = cs2 as Label;
            string printername = lb.Text;
            p = cb.SelectedIndex;
            string path4 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\DateCodes" + ".txt";
            path4 = path4.Replace("\r", "").Replace("\n", "");
            string[] datecodedata = File.ReadAllText(path4).Split(',');
            string Path6 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Images\\";
            Path6 = Path6.Replace("\r", "").Replace("\n", "");
            string path8 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Gsheet.txt";
            path8 = path8.Replace("\r", "").Replace("\n", "");
            string[] header = File.ReadLines(path8).Skip(0).Take(1).First().Split('╨');
            string[] resource = File.ReadLines(path8).Skip(p+1).Take(1).First().Split('╨');
            string Path3 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + "\\Formats\\" + resource[30] +".nlbl";
            Path3 = Path3.Replace("\r", "").Replace("\n", "");
            try
            {
                ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(Path3);
                label.PrintSettings.PrinterName = printername;
                for(int f = 0; f <header.Length-1; f++)
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
                label.Variables["SERIAL_NUMBER"].SetValue(SerialNumber);
                //What actually sends the print command:
                label.Print(1);
            }catch(Exception e)
            {
                if (e.Message.Contains("An error occured during loading the file"))
                {
                    MessageBox.Show("Label Format not found");
                }
                else
                {
                    MessageBox.Show("Contact your system admin if you are seeing this message" + System.Environment.NewLine + e.ToString());
                }
            }
        }
        void PrintManifest(int p)
        {
            Control[] cs = this.Controls.Find("PrinterComboBox" + p.ToString(), true);
            ComboBox cb = cs[0] as ComboBox;
            string path9 = File.ReadAllText(Application.StartupPath + @"\ConfigurationPathFile.txt") + @"\ManifestPrinterConfigFile.txt";
            path9 = path9.Replace("\r", "").Replace("\n", "");
            string[] mprinters = File.ReadAllText(path9).Split(',');
            string printername = mprinters[0];
            p = Convert.ToInt32(cb.Tag);
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
            try
            {
                ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(Path3);
                label.PrintSettings.PrinterName = printername;
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
                label.Variables["SERIAL_NUMBER"].SetValue(SerialNumber);
                //What actually sends the print command:
                label.Print(1);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("An error occured during loading the file"))
                {
                    MessageBox.Show("Label Format not found");
                }
                else
                {
                    MessageBox.Show("Contact your system admin if you are seeing this message" + System.Environment.NewLine + e.ToString());
                }
            }
        }
        #endregion
        #region datecode generation
        string NoAlphaDaycode(int LineNumber)
        {
            string thereturn = "";
            thereturn = thereturn + JulianDateGenerator();
            thereturn = thereturn + DateTime.Now.ToString("y").Substring(DateTime.Now.ToString("y").Length - 1);
            thereturn = thereturn + "0" + LineNumber.ToString();
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
        #region Debugging
        private void test_Click(object sender, EventArgs e)
        {
            testpacket(123);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        #endregion


    }
}