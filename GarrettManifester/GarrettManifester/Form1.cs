//Written by Michael Hunsaker Waiting for deployment and testing 12/22/2020
//To replace the old Manifest program currently running on a Windows 2000 Computer
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
namespace GarrettManifester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists("SerialNumber.txt"))
                {
                    using (StreamWriter w = File.AppendText("SerialNumber.txt"))
                    {
                        w.WriteLine("00000000");
                    };
                }
                String SerialNumber = System.IO.File.ReadAllText("SerialNumber.txt");
                SerialNumberLabel.Text = SerialNumber;
                if (File.Exists("SaveTemp.txt"))
                {
                    load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line1PrintButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Add the weight entered to the total weight.
                int cases = Convert.ToInt32(CasesTextBox.Value); int weight = Convert.ToInt32(WeightTextBox.Value); int totalcases = int.Parse(TotalCasesTextBox.Text); int totalweight = int.Parse(TotalWeightTextBox.Text);
                totalcases = totalcases + cases;
                totalweight = totalweight + weight;
                TotalCasesTextBox.Text = totalcases.ToString();
                TotalWeightTextBox.Text = totalweight.ToString();

                //Set the time
                DateTime now = DateTime.Now;
                //Creates the file/appends information to the dates file
                // FIX THIS NAME FOR FINAL VERSION
                string path = now.ToString(@"MM-dd-yy") + "_Garrett" + ".txt";
                string newline = "\"Line 1  \",\"" + BuyerTextBox.Text.PadRight(18, ' ') + "\"," + TotalWeightTextBox.Text.PadLeft(5, ' ') + "," +
                    TotalCasesTextBox.Text.PadLeft(5, ' ') + "," + SerialNumberLabel.Text + ",\"" + DescriptionTextBox.Text.PadLeft(15, ' ') + "\",\"" + LineTextBox.Text.PadLeft(15, ' ') +
                    "\"," + TotalCasesTextBox.Text.PadLeft(7, ' ') + "," + "     3,\"" + DayCodeTextBox.Text.PadRight(10, ' ') + "\",\"" + now.ToString("HH:mm tt") + "\",\"" +
                    now.ToString("MM/dd/yy") + "\",\"" + ResNoTextBox.Text.PadRight(15, ' ') + "\"," + SerialNumberLabel.Text + "," + SerialNumberLabel.Text + ",\"" +
                    Remarks1TextBox.Text.PadRight(30, ' ') + "\",\"" + Remarks2TextBox.Text.PadRight(30, ' ') + "\",\"" + Remarks3TextBox.Text.PadRight(30, ' ') + "\",\"     " +
                    "\",\"     " + "\"";

                //This will append to an exsisting file or create it if it doesnt exsist and right the first line
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine(newline);
                };
                //*************************************************************************************Start Print Function********************************************************

                // the path to the print database and the correct data in the correct format for bartender to use it as a datasource
                string path2 = "PrintDatabase.txt";
                string newline2 = "\"" + DayCodeTextBox.Text + "\",\"" + now.ToString("HH:mm") + "\",\"" + now.ToString("MM/dd/yy") + "\",\"" + BuyerTextBox.Text + "\",\"" +
                    ResNoTextBox.Text + "\",\"" + DescriptionTextBox.Text + "\"," + TotalCasesTextBox.Text + "," + TotalWeightTextBox.Text + "," + OrigSerNoTextBox.Text + "," +
                    CompSerNoTextBox.Text + ",\"" + Remarks1TextBox.Text + "\",\"" + Remarks2TextBox.Text + "\",\"" + Remarks3TextBox.Text + "\"";

                //if the file exsists overwrite it with newline if not create it and then overwrite it with new 
                //Instantly (fast enough you never see the file in file explorer) Commander will send the data along and delete the file

                FileStream fcreate = File.Open(path2, FileMode.Create);
                fcreate.Close();
                System.IO.File.WriteAllText(path2, newline2);

                //**********************************************************************************************End Print function
                //Increment the serial Number for each printed label
                int serialnumber = int.Parse(SerialNumberLabel.Text);
                SerialNumberLabel.Text = (serialnumber + 1).ToString();
                //Ensure the Serial number stays a consistant length currently 9 must be changed on the if and the while if needed to be changed.
                do
                {
                    if (SerialNumberLabel.Text.Length < 8) { SerialNumberLabel.Text = '0' + SerialNumberLabel.Text; }
                }
                while (SerialNumberLabel.Text.Length < 8);
                //Overwrite the serial number file so that each time the program is opened it starts with the next serial number in line.
                System.IO.File.WriteAllText("SerialNumber.txt", SerialNumberLabel.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            try {
            //sets filename
            DateTime now = DateTime.Now;
            string filename = now.ToString(@"MM-dd-yy") + "_Garrett" + ".txt";
            //makes sure file exsists
            if (!File.Exists(filename))
            {
                using (StreamWriter w = File.AppendText(filename)) { w.WriteLine(""); };
            }
            //source and destination paths
            string SourcePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string TargetPath = @"\\sffnt4\Everyone\Repack Manifest\garrett";
            
            //source and destination file paths
            String SourceFile = System.IO.Path.Combine(SourcePath, filename);
            string DestFile = System.IO.Path.Combine(TargetPath, filename);

            // Create a new target folder.
            // If the directory already exists, this method does not create a new directory.
            System.IO.Directory.CreateDirectory(TargetPath);

            //Copy that day's file to the correct directory
            System.IO.File.Copy(SourceFile, DestFile, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line1ClearButton_Click(object sender, EventArgs e)
        {
            try { 
            //This should clear all data from the associated line's text/combo and number boxes
            LineTextBox.Text = "";
            DescriptionTextBox.Text = "";
            ResNoTextBox.Text = "";
            CasesTextBox.Value = 0;
            WeightTextBox.Value = 0;
            DayCodeTextBox.Text = "";
            OrigSerNoTextBox.Text = "";
            YUMProdComboBox.Text = "No";
            BuyerTextBox.Text = "";
            TypeComboBox.Text = "Tote";
            Remarks1TextBox.Text = "";
            Remarks2TextBox.Text = "";
            Remarks3TextBox.Text = "";
            CompSerNoTextBox.Text = "";
            TotalCasesTextBox.Text = "0";
            TotalWeightTextBox.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line2ClearButton_Click(object sender, EventArgs e)
        {
            try { 
            //This should clear all data from the associated line's text/combo and number boxes
            LineTextBox2.Text = "";
            DescriptionTextBox2.Text = "";
            ResNoTextBox2.Text = "";
            CasesTextBox2.Value = 0;
            WeightTextBox2.Value = 0;
            DayCodeTextBox2.Text = "";
            OrigSerNoTextBox2.Text = "";
            YUMProdComboBox2.Text = "No";
            BuyerTextBox2.Text = "";
            TypeComboBox2.Text = "Tote";
            Remarks1TextBox2.Text = "";
            Remarks2TextBox2.Text = "";
            Remarks3TextBox2.Text = "";
            CompSerNoTextBox2.Text = "";
            TotalCasesTextBox2.Text = "0";
            TotalWeightTextBox2.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line3ClearButton_Click(object sender, EventArgs e)
        {
            try { 
            //This should clear all data from the associated line's text/combo and number boxes
            LineTextBox3.Text = "";
            DescriptionTextBox3.Text = "";
            ResNoTextBox3.Text = "";
            CasesTextBox3.Value = 0;
            WeightTextBox3.Value = 0;
            DayCodeTextBox3.Text = "";
            OrigSerNoTextBox3.Text = "";
            YUMProdComboBox3.Text = "No";
            BuyerTextBox3.Text = "";
            TypeComboBox3.Text = "Tote";
            Remarks1TextBox3.Text = "";
            Remarks2TextBox3.Text = "";
            Remarks3TextBox3.Text = "";
            CompSerNoTextBox3.Text = "";
            TotalCasesTextBox3.Text = "0";
            TotalWeightTextBox3.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line4ClearButton_Click(object sender, EventArgs e)
        {
            try { 
            //This should clear all data from the associated line's text/combo and number boxes
            LineTextBox4.Text = "";
            DescriptionTextBox4.Text = "";
            ResNoTextBox4.Text = "";
            CasesTextBox4.Value = 0;
            WeightTextBox4.Value = 0;
            DayCodeTextBox4.Text = "";
            OrigSerNoTextBox4.Text = "";
            YUMProdComboBox4.Text = "No";
            BuyerTextBox4.Text = "";
            TypeComboBox4.Text = "Tote";
            Remarks1TextBox4.Text = "";
            Remarks2TextBox4.Text = "";
            Remarks3TextBox4.Text = "";
            CompSerNoTextBox4.Text = "";
            TotalCasesTextBox4.Text = "0";
            TotalWeightTextBox4.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line5ClearButton_Click(object sender, EventArgs e)
        {
            try { 
            //This should clear all data from the associated line's text/combo and number boxes
            LineTextBox5.Text = "";
            DescriptionTextBox5.Text = "";
            ResNoTextBox5.Text = "";
            CasesTextBox5.Value = 0;
            WeightTextBox5.Value = 0;
            DayCodeTextBox5.Text = "";
            OrigSerNoTextBox5.Text = "";
            YUMProdComboBox5.Text = "No";
            BuyerTextBox5.Text = "";
            TypeComboBox5.Text = "Tote";
            Remarks1TextBox5.Text = "";
            Remarks2TextBox5.Text = "";
            Remarks3TextBox5.Text = "";
            CompSerNoTextBox5.Text = "";
            TotalCasesTextBox5.Text = "0";
            TotalWeightTextBox5.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line6ClearButton_Click(object sender, EventArgs e)
        {
            try { 
            //This should clear all data from the associated line's text/combo and number boxes
            LineTextBox6.Text = "";
            DescriptionTextBox6.Text = "";
            ResNoTextBox6.Text = "";
            CasesTextBox6.Value = 0;
            WeightTextBox6.Value = 0;
            DayCodeTextBox6.Text = "";
            OrigSerNoTextBox6.Text = "";
            YUMProdComboBox6.Text = "No";
            BuyerTextBox6.Text = "";
            TypeComboBox6.Text = "Tote";
            Remarks1TextBox6.Text = "";
            Remarks2TextBox6.Text = "";
            Remarks3TextBox6.Text = "";
            CompSerNoTextBox6.Text = "";
            TotalCasesTextBox6.Text = "0";
            TotalWeightTextBox6.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line2PrintButtonMain_Click(object sender, EventArgs e)
        {
            try { 
            //Add the weight entered to the total weight.
            int cases = Convert.ToInt32(CasesTextBox2.Value); int weight = Convert.ToInt32(WeightTextBox2.Value); int totalcases = int.Parse(TotalCasesTextBox2.Text); int totalweight = int.Parse(TotalWeightTextBox2.Text);
            totalcases = totalcases + cases;
            totalweight = totalweight + weight;
            TotalCasesTextBox2.Text = totalcases.ToString();
            TotalWeightTextBox2.Text = totalweight.ToString();

            //Set the time
            DateTime now = DateTime.Now;
            //Creates the file/appends information to the dates file
            // FIX THIS NAME FOR FINAL VERSION
            string path = now.ToString(@"MM-dd-yy") + "_Garrett" + ".txt";
            string newline = "\"Line 1  \",\"" + BuyerTextBox2.Text.PadRight(18, ' ') + "\"," + TotalWeightTextBox2.Text.PadLeft(5, ' ') + "," +
                TotalCasesTextBox2.Text.PadLeft(5, ' ') + "," + SerialNumberLabel.Text + ",\"" + DescriptionTextBox2.Text.PadLeft(15, ' ') + "\",\"" + LineTextBox2.Text.PadLeft(15, ' ') +
                "\"," + TotalCasesTextBox2.Text.PadLeft(7, ' ') + "," + "     3,\"" + DayCodeTextBox2.Text.PadRight(10, ' ') + "\",\"" + now.ToString("HH:mm tt") + "\",\"" +
                now.ToString("MM/dd/yy") + "\",\"" + ResNoTextBox2.Text.PadRight(15, ' ') + "\"," + SerialNumberLabel.Text + "," + SerialNumberLabel.Text + ",\"" +
                Remarks1TextBox2.Text.PadRight(30, ' ') + "\",\"" + Remarks2TextBox2.Text.PadRight(30, ' ') + "\",\"" + Remarks3TextBox2.Text.PadRight(30, ' ') + "\",\"     " +
                "\",\"     " + "\"";

            //This will append to an exsisting file or create it if it doesnt exsist and right the first line
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine(newline);
            };
            //*************************************************************************************Start Print Function********************************************************

            // the path to the print database and the correct data in the correct format for bartender to use it as a datasource
            string path2 = "PrintDatabase.txt";
            string newline2 = "\"" + DayCodeTextBox2.Text + "\",\"" + now.ToString("HH:mm") + "\",\"" + now.ToString("MM/dd/yy") + "\",\"" + BuyerTextBox2.Text + "\",\"" +
                ResNoTextBox2.Text + "\",\"" + DescriptionTextBox2.Text + "\"," + TotalCasesTextBox2.Text + "," + TotalWeightTextBox2.Text + "," + OrigSerNoTextBox2.Text + "," +
                CompSerNoTextBox2.Text + ",\"" + Remarks1TextBox2.Text + "\",\"" + Remarks2TextBox2.Text + "\",\"" + Remarks3TextBox2.Text + "\"";

            //if the file exsists overwrite it with newline if not create it and then overwrite it with new 
            //Instantly (fast enough you never see the file in file explorer) Commander will send the data along and delete the file

            FileStream fcreate = File.Open(path2, FileMode.Create);
            fcreate.Close();
            System.IO.File.WriteAllText(path2, newline2);

            //**********************************************************************************************End Print function
            //Increment the serial Number for each printed label
            int serialnumber = int.Parse(SerialNumberLabel.Text);
            SerialNumberLabel.Text = (serialnumber + 1).ToString();
            //Ensure the Serial number stays a consistant length currently 9 must be changed on the if and the while if needed to be changed.
            do
            {
                if (SerialNumberLabel.Text.Length < 8) { SerialNumberLabel.Text = '0' + SerialNumberLabel.Text; }
            }
            while (SerialNumberLabel.Text.Length < 8);
            //Overwrite the serial number file so that each time the program is opened it starts with the next serial number in line.
            System.IO.File.WriteAllText("SerialNumber.txt", SerialNumberLabel.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line3PrintButtonMain_Click(object sender, EventArgs e)
        {
            try { 
            //Add the weight entered to the total weight.
            int cases = Convert.ToInt32(CasesTextBox3.Value); int weight = Convert.ToInt32(WeightTextBox3.Value); int totalcases = int.Parse(TotalCasesTextBox3.Text); int totalweight = int.Parse(TotalWeightTextBox3.Text);
            totalcases = totalcases + cases;
            totalweight = totalweight + weight;
            TotalCasesTextBox3.Text = totalcases.ToString();
            TotalWeightTextBox3.Text = totalweight.ToString();

            //Set the time
            DateTime now = DateTime.Now;
            //Creates the file/appends information to the dates file
            // FIX THIS NAME FOR FINAL VERSION
            string path = now.ToString(@"MM-dd-yy") + "_Garrett" + ".txt";
            string newline = "\"Line 1  \",\"" + BuyerTextBox3.Text.PadRight(18, ' ') + "\"," + TotalWeightTextBox3.Text.PadLeft(5, ' ') + "," +
                TotalCasesTextBox3.Text.PadLeft(5, ' ') + "," + SerialNumberLabel.Text + ",\"" + DescriptionTextBox3.Text.PadLeft(15, ' ') + "\",\"" + LineTextBox3.Text.PadLeft(15, ' ') +
                "\"," + TotalCasesTextBox3.Text.PadLeft(7, ' ') + "," + "     3,\"" + DayCodeTextBox3.Text.PadRight(10, ' ') + "\",\"" + now.ToString("HH:mm tt") + "\",\"" +
                now.ToString("MM/dd/yy") + "\",\"" + ResNoTextBox3.Text.PadRight(15, ' ') + "\"," + SerialNumberLabel.Text + "," + SerialNumberLabel.Text + ",\"" +
                Remarks1TextBox3.Text.PadRight(30, ' ') + "\",\"" + Remarks2TextBox3.Text.PadRight(30, ' ') + "\",\"" + Remarks3TextBox3.Text.PadRight(30, ' ') + "\",\"     " +
                "\",\"     " + "\"";

            //This will append to an exsisting file or create it if it doesnt exsist and right the first line
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine(newline);
            };
            //*************************************************************************************Start Print Function********************************************************

            // the path to the print database and the correct data in the correct format for bartender to use it as a datasource
            string path2 = "PrintDatabase.txt";
            string newline2 = "\"" + DayCodeTextBox3.Text + "\",\"" + now.ToString("HH:mm") + "\",\"" + now.ToString("MM/dd/yy") + "\",\"" + BuyerTextBox3.Text + "\",\"" +
                ResNoTextBox3.Text + "\",\"" + DescriptionTextBox3.Text + "\"," + TotalCasesTextBox3.Text + "," + TotalWeightTextBox3.Text + "," + OrigSerNoTextBox3.Text + "," +
                CompSerNoTextBox3.Text + ",\"" + Remarks1TextBox3.Text + "\",\"" + Remarks2TextBox3.Text + "\",\"" + Remarks3TextBox3.Text + "\"";

            //if the file exsists overwrite it with newline if not create it and then overwrite it with new 
            //Instantly (fast enough you never see the file in file explorer) Commander will send the data along and delete the file

            FileStream fcreate = File.Open(path2, FileMode.Create);
            fcreate.Close();
            System.IO.File.WriteAllText(path2, newline2);

            //**********************************************************************************************End Print function
            //Increment the serial Number for each printed label
            int serialnumber = int.Parse(SerialNumberLabel.Text);
            SerialNumberLabel.Text = (serialnumber + 1).ToString();
            //Ensure the Serial number stays a consistant length currently 9 must be changed on the if and the while if needed to be changed.
            do
            {
                if (SerialNumberLabel.Text.Length < 8) { SerialNumberLabel.Text = '0' + SerialNumberLabel.Text; }
            }
            while (SerialNumberLabel.Text.Length < 8);
            //Overwrite the serial number file so that each time the program is opened it starts with the next serial number in line.
            System.IO.File.WriteAllText("SerialNumber.txt", SerialNumberLabel.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line4PrintButtonMain_Click(object sender, EventArgs e)
        {
            try { 
            //Add the weight entered to the total weight.
            int cases = Convert.ToInt32(CasesTextBox4.Value); int weight = Convert.ToInt32(WeightTextBox4.Value); int totalcases = int.Parse(TotalCasesTextBox4.Text); int totalweight = int.Parse(TotalWeightTextBox4.Text);
            totalcases = totalcases + cases;
            totalweight = totalweight + weight;
            TotalCasesTextBox4.Text = totalcases.ToString();
            TotalWeightTextBox4.Text = totalweight.ToString();

            //Set the time
            DateTime now = DateTime.Now;
            //Creates the file/appends information to the dates file
            // FIX THIS NAME FOR FINAL VERSION
            string path = now.ToString(@"MM-dd-yy") + "_Garrett" + ".txt";
            string newline = "\"Line 1  \",\"" + BuyerTextBox4.Text.PadRight(18, ' ') + "\"," + TotalWeightTextBox4.Text.PadLeft(5, ' ') + "," +
                TotalCasesTextBox4.Text.PadLeft(5, ' ') + "," + SerialNumberLabel.Text + ",\"" + DescriptionTextBox4.Text.PadLeft(15, ' ') + "\",\"" + LineTextBox4.Text.PadLeft(15, ' ') +
                "\"," + TotalCasesTextBox4.Text.PadLeft(7, ' ') + "," + "     3,\"" + DayCodeTextBox4.Text.PadRight(10, ' ') + "\",\"" + now.ToString("HH:mm tt") + "\",\"" +
                now.ToString("MM/dd/yy") + "\",\"" + ResNoTextBox4.Text.PadRight(15, ' ') + "\"," + SerialNumberLabel.Text + "," + SerialNumberLabel.Text + ",\"" +
                Remarks1TextBox4.Text.PadRight(30, ' ') + "\",\"" + Remarks2TextBox4.Text.PadRight(30, ' ') + "\",\"" + Remarks3TextBox4.Text.PadRight(30, ' ') + "\",\"     " +
                "\",\"     " + "\"";

            //This will append to an exsisting file or create it if it doesnt exsist and right the first line
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine(newline);
            };
            //*************************************************************************************Start Print Function********************************************************

            // the path to the print database and the correct data in the correct format for bartender to use it as a datasource
            string path2 = "PrintDatabase.txt";
            string newline2 = "\"" + DayCodeTextBox4.Text + "\",\"" + now.ToString("HH:mm") + "\",\"" + now.ToString("MM/dd/yy") + "\",\"" + BuyerTextBox4.Text + "\",\"" +
                ResNoTextBox4.Text + "\",\"" + DescriptionTextBox4.Text + "\"," + TotalCasesTextBox4.Text + "," + TotalWeightTextBox4.Text + "," + OrigSerNoTextBox4.Text + "," +
                CompSerNoTextBox4.Text + ",\"" + Remarks1TextBox4.Text + "\",\"" + Remarks2TextBox4.Text + "\",\"" + Remarks3TextBox4.Text + "\"";

            //if the file exsists overwrite it with newline if not create it and then overwrite it with new 
            //Instantly (fast enough you never see the file in file explorer) Commander will send the data along and delete the file

            FileStream fcreate = File.Open(path2, FileMode.Create);
            fcreate.Close();
            System.IO.File.WriteAllText(path2, newline2);

            //**********************************************************************************************End Print function
            //Increment the serial Number for each printed label
            int serialnumber = int.Parse(SerialNumberLabel.Text);
            SerialNumberLabel.Text = (serialnumber + 1).ToString();
            //Ensure the Serial number stays a consistant length currently 9 must be changed on the if and the while if needed to be changed.
            do
            {
                if (SerialNumberLabel.Text.Length < 8) { SerialNumberLabel.Text = '0' + SerialNumberLabel.Text; }
            }
            while (SerialNumberLabel.Text.Length < 8);
            //Overwrite the serial number file so that each time the program is opened it starts with the next serial number in line.
            System.IO.File.WriteAllText("SerialNumber.txt", SerialNumberLabel.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line5PrintButtonMain_Click(object sender, EventArgs e)
        {
            try { 
            //Add the weight entered to the total weight.
            int cases = Convert.ToInt32(CasesTextBox5.Value); int weight = Convert.ToInt32(WeightTextBox5.Value); int totalcases = int.Parse(TotalCasesTextBox5.Text); int totalweight = int.Parse(TotalWeightTextBox5.Text);
            totalcases = totalcases + cases;
            totalweight = totalweight + weight;
            TotalCasesTextBox5.Text = totalcases.ToString();
            TotalWeightTextBox5.Text = totalweight.ToString();

            //Set the time
            DateTime now = DateTime.Now;
            //Creates the file/appends information to the dates file
            // FIX THIS NAME FOR FINAL VERSION
            string path = now.ToString(@"MM-dd-yy") + "_Garrett" + ".txt";
            string newline = "\"Line 1  \",\"" + BuyerTextBox5.Text.PadRight(18, ' ') + "\"," + TotalWeightTextBox5.Text.PadLeft(5, ' ') + "," +
                TotalCasesTextBox5.Text.PadLeft(5, ' ') + "," + SerialNumberLabel.Text + ",\"" + DescriptionTextBox5.Text.PadLeft(15, ' ') + "\",\"" + LineTextBox5.Text.PadLeft(15, ' ') +
                "\"," + TotalCasesTextBox5.Text.PadLeft(7, ' ') + "," + "     3,\"" + DayCodeTextBox5.Text.PadRight(10, ' ') + "\",\"" + now.ToString("HH:mm tt") + "\",\"" +
                now.ToString("MM/dd/yy") + "\",\"" + ResNoTextBox5.Text.PadRight(15, ' ') + "\"," + SerialNumberLabel.Text + "," + SerialNumberLabel.Text + ",\"" +
                Remarks1TextBox5.Text.PadRight(30, ' ') + "\",\"" + Remarks2TextBox5.Text.PadRight(30, ' ') + "\",\"" + Remarks3TextBox5.Text.PadRight(30, ' ') + "\",\"     " +
                "\",\"     " + "\"";

            //This will append to an exsisting file or create it if it doesnt exsist and right the first line
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine(newline);
            };
            //*************************************************************************************Start Print Function********************************************************

            // the path to the print database and the correct data in the correct format for bartender to use it as a datasource
            string path2 = "PrintDatabase.txt";
            string newline2 = "\"" + DayCodeTextBox5.Text + "\",\"" + now.ToString("HH:mm") + "\",\"" + now.ToString("MM/dd/yy") + "\",\"" + BuyerTextBox5.Text + "\",\"" +
                ResNoTextBox5.Text + "\",\"" + DescriptionTextBox5.Text + "\"," + TotalCasesTextBox5.Text + "," + TotalWeightTextBox5.Text + "," + OrigSerNoTextBox5.Text + "," +
                CompSerNoTextBox5.Text + ",\"" + Remarks1TextBox5.Text + "\",\"" + Remarks2TextBox5.Text + "\",\"" + Remarks3TextBox5.Text + "\"";

            //if the file exsists overwrite it with newline if not create it and then overwrite it with new 
            //Instantly (fast enough you never see the file in file explorer) Commander will send the data along and delete the file

            FileStream fcreate = File.Open(path2, FileMode.Create);
            fcreate.Close();
            System.IO.File.WriteAllText(path2, newline2);

            //**********************************************************************************************End Print function
            //Increment the serial Number for each printed label
            int serialnumber = int.Parse(SerialNumberLabel.Text);
            SerialNumberLabel.Text = (serialnumber + 1).ToString();
            //Ensure the Serial number stays a consistant length currently 9 must be changed on the if and the while if needed to be changed.
            do
            {
                if (SerialNumberLabel.Text.Length < 8) { SerialNumberLabel.Text = '0' + SerialNumberLabel.Text; }
            }
            while (SerialNumberLabel.Text.Length < 8);
            //Overwrite the serial number file so that each time the program is opened it starts with the next serial number in line.
            System.IO.File.WriteAllText("SerialNumber.txt", SerialNumberLabel.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line6PrintButtonMain_Click(object sender, EventArgs e)
        {
            try { 
            //Add the weight entered to the total weight.
            int cases = Convert.ToInt32(CasesTextBox6.Value); int weight = Convert.ToInt32(WeightTextBox6.Value); int totalcases = int.Parse(TotalCasesTextBox6.Text); int totalweight = int.Parse(TotalWeightTextBox6.Text);
            totalcases = totalcases + cases;
            totalweight = totalweight + weight;
            TotalCasesTextBox6.Text = totalcases.ToString();
            TotalWeightTextBox6.Text = totalweight.ToString();

            //Set the time
            DateTime now = DateTime.Now;
            //Creates the file/appends information to the dates file
            // FIX THIS NAME FOR FINAL VERSION
            string path = now.ToString(@"MM-dd-yy") + "_Garrett" + ".txt";
            string newline = "\"Line 1  \",\"" + BuyerTextBox5.Text.PadRight(18, ' ') + "\"," + TotalWeightTextBox6.Text.PadLeft(5, ' ') + "," +
                TotalCasesTextBox6.Text.PadLeft(5, ' ') + "," + SerialNumberLabel.Text + ",\"" + DescriptionTextBox6.Text.PadLeft(15, ' ') + "\",\"" + LineTextBox6.Text.PadLeft(15, ' ') +
                "\"," + TotalCasesTextBox6.Text.PadLeft(7, ' ') + "," + "     3,\"" + DayCodeTextBox6.Text.PadRight(10, ' ') + "\",\"" + now.ToString("HH:mm tt") + "\",\"" +
                now.ToString("MM/dd/yy") + "\",\"" + ResNoTextBox6.Text.PadRight(15, ' ') + "\"," + SerialNumberLabel.Text + "," + SerialNumberLabel.Text + ",\"" +
                Remarks1TextBox6.Text.PadRight(30, ' ') + "\",\"" + Remarks2TextBox6.Text.PadRight(30, ' ') + "\",\"" + Remarks3TextBox6.Text.PadRight(30, ' ') + "\",\"     " +
                "\",\"     " + "\"";

            //This will append to an exsisting file or create it if it doesnt exsist and right the first line
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine(newline);
            };
            //*************************************************************************************Start Print Function********************************************************

            // the path to the print database and the correct data in the correct format for bartender to use it as a datasource
            string path2 = "PrintDatabase.txt";
            string newline2 = "\"" + DayCodeTextBox6.Text + "\",\"" + now.ToString("HH:mm") + "\",\"" + now.ToString("MM/dd/yy") + "\",\"" + BuyerTextBox6.Text + "\",\"" +
                ResNoTextBox6.Text + "\",\"" + DescriptionTextBox6.Text + "\"," + TotalCasesTextBox6.Text + "," + TotalWeightTextBox6.Text + "," + OrigSerNoTextBox6.Text + "," +
                CompSerNoTextBox6.Text + ",\"" + Remarks1TextBox6.Text + "\",\"" + Remarks2TextBox6.Text + "\",\"" + Remarks3TextBox6.Text + "\"";

            //if the file exsists overwrite it with newline if not create it and then overwrite it with new 
            //Instantly (fast enough you never see the file in file explorer) Commander will send the data along and delete the file

            FileStream fcreate = File.Open(path2, FileMode.Create);
            fcreate.Close();
            System.IO.File.WriteAllText(path2, newline2);

            //**********************************************************************************************End Print function
            //Increment the serial Number for each printed label
            int serialnumber = int.Parse(SerialNumberLabel.Text);
            SerialNumberLabel.Text = (serialnumber + 1).ToString();
            //Ensure the Serial number stays a consistant length currently 9 must be changed on the if and the while if needed to be changed.
            do
            {
                if (SerialNumberLabel.Text.Length < 8) { SerialNumberLabel.Text = '0' + SerialNumberLabel.Text; }
            }
            while (SerialNumberLabel.Text.Length < 8);
            //Overwrite the serial number file so that each time the program is opened it starts with the next serial number in line.
            System.IO.File.WriteAllText("SerialNumber.txt", SerialNumberLabel.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line1PrintButtonRemote_Click(object sender, EventArgs e)
        {
            try { 
            //Add the weight entered to the total weight.
            int cases = Convert.ToInt32(CasesTextBox.Value); int weight = Convert.ToInt32(WeightTextBox.Value); int totalcases = int.Parse(TotalCasesTextBox.Text); int totalweight = int.Parse(TotalWeightTextBox.Text);
            totalcases = totalcases + cases;
            totalweight = totalweight + weight;
            TotalCasesTextBox.Text = totalcases.ToString();
            TotalWeightTextBox.Text = totalweight.ToString();

            //Set the time
            DateTime now = DateTime.Now;
            //Creates the file/appends information to the dates file
            // FIX THIS NAME FOR FINAL VERSION
            string path = now.ToString(@"MM-dd-yy") + "_Garrett" + ".txt";
            string newline = "\"Line 1  \",\"" + BuyerTextBox.Text.PadRight(18, ' ') + "\"," + TotalWeightTextBox.Text.PadLeft(5, ' ') + "," +
                TotalCasesTextBox.Text.PadLeft(5, ' ') + "," + SerialNumberLabel.Text + ",\"" + DescriptionTextBox.Text.PadLeft(15, ' ') + "\",\"" + LineTextBox.Text.PadLeft(15, ' ') +
                "\"," + TotalCasesTextBox.Text.PadLeft(7, ' ') + "," + "     3,\"" + DayCodeTextBox.Text.PadRight(10, ' ') + "\",\"" + now.ToString("HH:mm tt") + "\",\"" +
                now.ToString("MM/dd/yy") + "\",\"" + ResNoTextBox.Text.PadRight(15, ' ') + "\"," + SerialNumberLabel.Text + "," + SerialNumberLabel.Text + ",\"" +
                Remarks1TextBox.Text.PadRight(30, ' ') + "\",\"" + Remarks2TextBox.Text.PadRight(30, ' ') + "\",\"" + Remarks3TextBox.Text.PadRight(30, ' ') + "\",\"     " +
                "\",\"     " + "\"";

            //This will append to an exsisting file or create it if it doesnt exsist and right the first line
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine(newline);
            };
            //*************************************************************************************Start Print Function********************************************************

            // the path to the print database and the correct data in the correct format for bartender to use it as a datasource
            string path2 = "PrintDatabase2.txt";
            string newline2 = "\"" + DayCodeTextBox.Text + "\",\"" + now.ToString("HH:mm") + "\",\"" + now.ToString("MM/dd/yy") + "\",\"" + BuyerTextBox.Text + "\",\"" +
                ResNoTextBox.Text + "\",\"" + DescriptionTextBox.Text + "\"," + TotalCasesTextBox.Text + "," + TotalWeightTextBox.Text + "," + OrigSerNoTextBox.Text + "," +
                CompSerNoTextBox.Text + ",\"" + Remarks1TextBox.Text + "\",\"" + Remarks2TextBox.Text + "\",\"" + Remarks3TextBox.Text + "\"";

            //if the file exsists overwrite it with newline if not create it and then overwrite it with new 
            //Instantly (fast enough you never see the file in file explorer) Commander will send the data along and delete the file

            FileStream fcreate = File.Open(path2, FileMode.Create);
            fcreate.Close();
            System.IO.File.WriteAllText(path2, newline2);

            //**********************************************************************************************End Print function
            //Increment the serial Number for each printed label
            int serialnumber = int.Parse(SerialNumberLabel.Text);
            SerialNumberLabel.Text = (serialnumber + 1).ToString();
            //Ensure the Serial number stays a consistant length currently 9 must be changed on the if and the while if needed to be changed.
            do
            {
                if (SerialNumberLabel.Text.Length < 8) { SerialNumberLabel.Text = '0' + SerialNumberLabel.Text; }
            }
            while (SerialNumberLabel.Text.Length < 8);
            //Overwrite the serial number file so that each time the program is opened it starts with the next serial number in line.
            System.IO.File.WriteAllText("SerialNumber.txt", SerialNumberLabel.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line2PrintButtonRemote_Click(object sender, EventArgs e)
        {
            try { 
            //Add the weight entered to the total weight.
            int cases = Convert.ToInt32(CasesTextBox2.Value); int weight = Convert.ToInt32(WeightTextBox2.Value); int totalcases = int.Parse(TotalCasesTextBox2.Text); int totalweight = int.Parse(TotalWeightTextBox2.Text);
            totalcases = totalcases + cases;
            totalweight = totalweight + weight;
            TotalCasesTextBox2.Text = totalcases.ToString();
            TotalWeightTextBox2.Text = totalweight.ToString();

            //Set the time
            DateTime now = DateTime.Now;
            //Creates the file/appends information to the dates file
            // FIX THIS NAME FOR FINAL VERSION
            string path = now.ToString(@"MM-dd-yy") + "_Garrett" + ".txt";
            string newline = "\"Line 1  \",\"" + BuyerTextBox2.Text.PadRight(18, ' ') + "\"," + TotalWeightTextBox2.Text.PadLeft(5, ' ') + "," +
                TotalCasesTextBox2.Text.PadLeft(5, ' ') + "," + SerialNumberLabel.Text + ",\"" + DescriptionTextBox2.Text.PadLeft(15, ' ') + "\",\"" + LineTextBox2.Text.PadLeft(15, ' ') +
                "\"," + TotalCasesTextBox2.Text.PadLeft(7, ' ') + "," + "     3,\"" + DayCodeTextBox2.Text.PadRight(10, ' ') + "\",\"" + now.ToString("HH:mm tt") + "\",\"" +
                now.ToString("MM/dd/yy") + "\",\"" + ResNoTextBox2.Text.PadRight(15, ' ') + "\"," + SerialNumberLabel.Text + "," + SerialNumberLabel.Text + ",\"" +
                Remarks1TextBox2.Text.PadRight(30, ' ') + "\",\"" + Remarks2TextBox2.Text.PadRight(30, ' ') + "\",\"" + Remarks3TextBox2.Text.PadRight(30, ' ') + "\",\"     " +
                "\",\"     " + "\"";

            //This will append to an exsisting file or create it if it doesnt exsist and right the first line
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine(newline);
            };
            //*************************************************************************************Start Print Function********************************************************

            // the path to the print database and the correct data in the correct format for bartender to use it as a datasource
            string path2 = "PrintDatabase2.txt";
            string newline2 = "\"" + DayCodeTextBox2.Text + "\",\"" + now.ToString("HH:mm") + "\",\"" + now.ToString("MM/dd/yy") + "\",\"" + BuyerTextBox2.Text + "\",\"" +
                ResNoTextBox2.Text + "\",\"" + DescriptionTextBox2.Text + "\"," + TotalCasesTextBox2.Text + "," + TotalWeightTextBox2.Text + "," + OrigSerNoTextBox2.Text + "," +
                CompSerNoTextBox2.Text + ",\"" + Remarks1TextBox2.Text + "\",\"" + Remarks2TextBox2.Text + "\",\"" + Remarks3TextBox2.Text + "\"";

            //if the file exsists overwrite it with newline if not create it and then overwrite it with new 
            //Instantly (fast enough you never see the file in file explorer) Commander will send the data along and delete the file

            FileStream fcreate = File.Open(path2, FileMode.Create);
            fcreate.Close();
            System.IO.File.WriteAllText(path2, newline2);

            //**********************************************************************************************End Print function
            //Increment the serial Number for each printed label
            int serialnumber = int.Parse(SerialNumberLabel.Text);
            SerialNumberLabel.Text = (serialnumber + 1).ToString();
            //Ensure the Serial number stays a consistant length currently 9 must be changed on the if and the while if needed to be changed.
            do
            {
                if (SerialNumberLabel.Text.Length < 8) { SerialNumberLabel.Text = '0' + SerialNumberLabel.Text; }
            }
            while (SerialNumberLabel.Text.Length < 8);
            //Overwrite the serial number file so that each time the program is opened it starts with the next serial number in line.
            System.IO.File.WriteAllText("SerialNumber.txt", SerialNumberLabel.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line3PrintButtonRemote_Click(object sender, EventArgs e)
        {
            try { 
            //Add the weight entered to the total weight.
            int cases = Convert.ToInt32(CasesTextBox3.Value); int weight = Convert.ToInt32(WeightTextBox3.Value); int totalcases = int.Parse(TotalCasesTextBox3.Text); int totalweight = int.Parse(TotalWeightTextBox3.Text);
            totalcases = totalcases + cases;
            totalweight = totalweight + weight;
            TotalCasesTextBox3.Text = totalcases.ToString();
            TotalWeightTextBox3.Text = totalweight.ToString();

            //Set the time
            DateTime now = DateTime.Now;
            //Creates the file/appends information to the dates file
            // FIX THIS NAME FOR FINAL VERSION
            string path = now.ToString(@"MM-dd-yy") + "_Garrett" + ".txt";
            string newline = "\"Line 1  \",\"" + BuyerTextBox3.Text.PadRight(18, ' ') + "\"," + TotalWeightTextBox3.Text.PadLeft(5, ' ') + "," +
                TotalCasesTextBox3.Text.PadLeft(5, ' ') + "," + SerialNumberLabel.Text + ",\"" + DescriptionTextBox3.Text.PadLeft(15, ' ') + "\",\"" + LineTextBox3.Text.PadLeft(15, ' ') +
                "\"," + TotalCasesTextBox3.Text.PadLeft(7, ' ') + "," + "     3,\"" + DayCodeTextBox3.Text.PadRight(10, ' ') + "\",\"" + now.ToString("HH:mm tt") + "\",\"" +
                now.ToString("MM/dd/yy") + "\",\"" + ResNoTextBox3.Text.PadRight(15, ' ') + "\"," + SerialNumberLabel.Text + "," + SerialNumberLabel.Text + ",\"" +
                Remarks1TextBox3.Text.PadRight(30, ' ') + "\",\"" + Remarks2TextBox3.Text.PadRight(30, ' ') + "\",\"" + Remarks3TextBox3.Text.PadRight(30, ' ') + "\",\"     " +
                "\",\"     " + "\"";

            //This will append to an exsisting file or create it if it doesnt exsist and right the first line
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine(newline);
            };
            //*************************************************************************************Start Print Function********************************************************

            // the path to the print database and the correct data in the correct format for bartender to use it as a datasource
            string path2 = "PrintDatabase2.txt";
            string newline2 = "\"" + DayCodeTextBox3.Text + "\",\"" + now.ToString("HH:mm") + "\",\"" + now.ToString("MM/dd/yy") + "\",\"" + BuyerTextBox3.Text + "\",\"" +
                ResNoTextBox3.Text + "\",\"" + DescriptionTextBox3.Text + "\"," + TotalCasesTextBox3.Text + "," + TotalWeightTextBox3.Text + "," + OrigSerNoTextBox3.Text + "," +
                CompSerNoTextBox3.Text + ",\"" + Remarks1TextBox3.Text + "\",\"" + Remarks2TextBox3.Text + "\",\"" + Remarks3TextBox3.Text + "\"";

            //if the file exsists overwrite it with newline if not create it and then overwrite it with new 
            //Instantly (fast enough you never see the file in file explorer) Commander will send the data along and delete the file

            FileStream fcreate = File.Open(path2, FileMode.Create);
            fcreate.Close();
            System.IO.File.WriteAllText(path2, newline2);

            //**********************************************************************************************End Print function
            //Increment the serial Number for each printed label
            int serialnumber = int.Parse(SerialNumberLabel.Text);
            SerialNumberLabel.Text = (serialnumber + 1).ToString();
            //Ensure the Serial number stays a consistant length currently 9 must be changed on the if and the while if needed to be changed.
            do
            {
                if (SerialNumberLabel.Text.Length < 8) { SerialNumberLabel.Text = '0' + SerialNumberLabel.Text; }
            }
            while (SerialNumberLabel.Text.Length < 8);
            //Overwrite the serial number file so that each time the program is opened it starts with the next serial number in line.
            System.IO.File.WriteAllText("SerialNumber.txt", SerialNumberLabel.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line4PrintButtonRemote_Click(object sender, EventArgs e)
        {
            try { 
            //Add the weight entered to the total weight.
            int cases = Convert.ToInt32(CasesTextBox4.Value); int weight = Convert.ToInt32(WeightTextBox4.Value); int totalcases = int.Parse(TotalCasesTextBox4.Text); int totalweight = int.Parse(TotalWeightTextBox4.Text);
            totalcases = totalcases + cases;
            totalweight = totalweight + weight;
            TotalCasesTextBox4.Text = totalcases.ToString();
            TotalWeightTextBox4.Text = totalweight.ToString();

            //Set the time
            DateTime now = DateTime.Now;
            //Creates the file/appends information to the dates file
            // FIX THIS NAME FOR FINAL VERSION
            string path = now.ToString(@"MM-dd-yy") + "_Garrett" + ".txt";
            string newline = "\"Line 1  \",\"" + BuyerTextBox4.Text.PadRight(18, ' ') + "\"," + TotalWeightTextBox4.Text.PadLeft(5, ' ') + "," +
                TotalCasesTextBox4.Text.PadLeft(5, ' ') + "," + SerialNumberLabel.Text + ",\"" + DescriptionTextBox4.Text.PadLeft(15, ' ') + "\",\"" + LineTextBox4.Text.PadLeft(15, ' ') +
                "\"," + TotalCasesTextBox4.Text.PadLeft(7, ' ') + "," + "     3,\"" + DayCodeTextBox4.Text.PadRight(10, ' ') + "\",\"" + now.ToString("HH:mm tt") + "\",\"" +
                now.ToString("MM/dd/yy") + "\",\"" + ResNoTextBox4.Text.PadRight(15, ' ') + "\"," + SerialNumberLabel.Text + "," + SerialNumberLabel.Text + ",\"" +
                Remarks1TextBox4.Text.PadRight(30, ' ') + "\",\"" + Remarks2TextBox4.Text.PadRight(30, ' ') + "\",\"" + Remarks3TextBox4.Text.PadRight(30, ' ') + "\",\"     " +
                "\",\"     " + "\"";

            //This will append to an exsisting file or create it if it doesnt exsist and right the first line
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine(newline);
            };
            //*************************************************************************************Start Print Function********************************************************

            // the path to the print database and the correct data in the correct format for bartender to use it as a datasource
            string path2 = "PrintDatabase2.txt";
            string newline2 = "\"" + DayCodeTextBox4.Text + "\",\"" + now.ToString("HH:mm") + "\",\"" + now.ToString("MM/dd/yy") + "\",\"" + BuyerTextBox4.Text + "\",\"" +
                ResNoTextBox4.Text + "\",\"" + DescriptionTextBox4.Text + "\"," + TotalCasesTextBox4.Text + "," + TotalWeightTextBox4.Text + "," + OrigSerNoTextBox4.Text + "," +
                CompSerNoTextBox4.Text + ",\"" + Remarks1TextBox4.Text + "\",\"" + Remarks2TextBox4.Text + "\",\"" + Remarks3TextBox4.Text + "\"";

            //if the file exsists overwrite it with newline if not create it and then overwrite it with new 
            //Instantly (fast enough you never see the file in file explorer) Commander will send the data along and delete the file

            FileStream fcreate = File.Open(path2, FileMode.Create);
            fcreate.Close();
            System.IO.File.WriteAllText(path2, newline2);

            //**********************************************************************************************End Print function
            //Increment the serial Number for each printed label
            int serialnumber = int.Parse(SerialNumberLabel.Text);
            SerialNumberLabel.Text = (serialnumber + 1).ToString();
            //Ensure the Serial number stays a consistant length currently 9 must be changed on the if and the while if needed to be changed.
            do
            {
                if (SerialNumberLabel.Text.Length < 8) { SerialNumberLabel.Text = '0' + SerialNumberLabel.Text; }
            }
            while (SerialNumberLabel.Text.Length < 8);
            //Overwrite the serial number file so that each time the program is opened it starts with the next serial number in line.
            System.IO.File.WriteAllText("SerialNumber.txt", SerialNumberLabel.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line5PrintButtonRemote_Click(object sender, EventArgs e)
        {
            try { 
            //Add the weight entered to the total weight.
            int cases = Convert.ToInt32(CasesTextBox5.Value); int weight = Convert.ToInt32(WeightTextBox5.Value); int totalcases = int.Parse(TotalCasesTextBox5.Text); int totalweight = int.Parse(TotalWeightTextBox5.Text);
            totalcases = totalcases + cases;
            totalweight = totalweight + weight;
            TotalCasesTextBox5.Text = totalcases.ToString();
            TotalWeightTextBox5.Text = totalweight.ToString();

            //Set the time
            DateTime now = DateTime.Now;
            //Creates the file/appends information to the dates file
            // FIX THIS NAME FOR FINAL VERSION
            string path = now.ToString(@"MM-dd-yy") + "_Garrett" + ".txt";
            string newline = "\"Line 1  \",\"" + BuyerTextBox5.Text.PadRight(18, ' ') + "\"," + TotalWeightTextBox5.Text.PadLeft(5, ' ') + "," +
                TotalCasesTextBox5.Text.PadLeft(5, ' ') + "," + SerialNumberLabel.Text + ",\"" + DescriptionTextBox5.Text.PadLeft(15, ' ') + "\",\"" + LineTextBox5.Text.PadLeft(15, ' ') +
                "\"," + TotalCasesTextBox5.Text.PadLeft(7, ' ') + "," + "     3,\"" + DayCodeTextBox5.Text.PadRight(10, ' ') + "\",\"" + now.ToString("HH:mm tt") + "\",\"" +
                now.ToString("MM/dd/yy") + "\",\"" + ResNoTextBox5.Text.PadRight(15, ' ') + "\"," + SerialNumberLabel.Text + "," + SerialNumberLabel.Text + ",\"" +
                Remarks1TextBox5.Text.PadRight(30, ' ') + "\",\"" + Remarks2TextBox5.Text.PadRight(30, ' ') + "\",\"" + Remarks3TextBox5.Text.PadRight(30, ' ') + "\",\"     " +
                "\",\"     " + "\"";

            //This will append to an exsisting file or create it if it doesnt exsist and right the first line
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine(newline);
            };
            //*************************************************************************************Start Print Function********************************************************

            // the path to the print database and the correct data in the correct format for bartender to use it as a datasource
            string path2 = "PrintDatabase2.txt";
            string newline2 = "\"" + DayCodeTextBox5.Text + "\",\"" + now.ToString("HH:mm") + "\",\"" + now.ToString("MM/dd/yy") + "\",\"" + BuyerTextBox5.Text + "\",\"" +
                ResNoTextBox5.Text + "\",\"" + DescriptionTextBox5.Text + "\"," + TotalCasesTextBox5.Text + "," + TotalWeightTextBox5.Text + "," + OrigSerNoTextBox5.Text + "," +
                CompSerNoTextBox5.Text + ",\"" + Remarks1TextBox5.Text + "\",\"" + Remarks2TextBox5.Text + "\",\"" + Remarks3TextBox5.Text + "\"";

            //if the file exsists overwrite it with newline if not create it and then overwrite it with new 
            //Instantly (fast enough you never see the file in file explorer) Commander will send the data along and delete the file

            FileStream fcreate = File.Open(path2, FileMode.Create);
            fcreate.Close();
            System.IO.File.WriteAllText(path2, newline2);

            //**********************************************************************************************End Print function
            //Increment the serial Number for each printed label
            int serialnumber = int.Parse(SerialNumberLabel.Text);
            SerialNumberLabel.Text = (serialnumber + 1).ToString();
            //Ensure the Serial number stays a consistant length currently 9 must be changed on the if and the while if needed to be changed.
            do
            {
                if (SerialNumberLabel.Text.Length < 8) { SerialNumberLabel.Text = '0' + SerialNumberLabel.Text; }
            }
            while (SerialNumberLabel.Text.Length < 8);
            //Overwrite the serial number file so that each time the program is opened it starts with the next serial number in line.
            System.IO.File.WriteAllText("SerialNumber.txt", SerialNumberLabel.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void Line6PrintButtonRemote_Click(object sender, EventArgs e)
        {
            try { 
            //Add the weight entered to the total weight.
            int cases = Convert.ToInt32(CasesTextBox5.Value); int weight = Convert.ToInt32(WeightTextBox5.Value); int totalcases = int.Parse(TotalCasesTextBox5.Text); int totalweight = int.Parse(TotalWeightTextBox5.Text);
            totalcases = totalcases + cases;
            totalweight = totalweight + weight;
            TotalCasesTextBox5.Text = totalcases.ToString();
            TotalWeightTextBox5.Text = totalweight.ToString();

            //Set the time
            DateTime now = DateTime.Now;
            //Creates the file/appends information to the dates file
            // FIX THIS NAME FOR FINAL VERSION
            string path = now.ToString(@"MM-dd-yy") + "_Garrett" + ".txt";
            string newline = "\"Line 1  \",\"" + BuyerTextBox5.Text.PadRight(18, ' ') + "\"," + TotalWeightTextBox5.Text.PadLeft(5, ' ') + "," +
                TotalCasesTextBox5.Text.PadLeft(5, ' ') + "," + SerialNumberLabel.Text + ",\"" + DescriptionTextBox5.Text.PadLeft(15, ' ') + "\",\"" + LineTextBox5.Text.PadLeft(15, ' ') +
                "\"," + TotalCasesTextBox5.Text.PadLeft(7, ' ') + "," + "     3,\"" + DayCodeTextBox5.Text.PadRight(10, ' ') + "\",\"" + now.ToString("HH:mm tt") + "\",\"" +
                now.ToString("MM/dd/yy") + "\",\"" + ResNoTextBox5.Text.PadRight(15, ' ') + "\"," + SerialNumberLabel.Text + "," + SerialNumberLabel.Text + ",\"" +
                Remarks1TextBox5.Text.PadRight(30, ' ') + "\",\"" + Remarks2TextBox5.Text.PadRight(30, ' ') + "\",\"" + Remarks3TextBox5.Text.PadRight(30, ' ') + "\",\"     " +
                "\",\"     " + "\"";

            //This will append to an exsisting file or create it if it doesnt exsist and right the first line
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine(newline);
            };
            //*************************************************************************************Start Print Function********************************************************

            // the path to the print database and the correct data in the correct format for bartender to use it as a datasource
            string path2 = "PrintDatabase2.txt";
            string newline2 = "\"" + DayCodeTextBox5.Text + "\",\"" + now.ToString("HH:mm") + "\",\"" + now.ToString("MM/dd/yy") + "\",\"" + BuyerTextBox5.Text + "\",\"" +
                ResNoTextBox5.Text + "\",\"" + DescriptionTextBox5.Text + "\"," + TotalCasesTextBox5.Text + "," + TotalWeightTextBox5.Text + "," + OrigSerNoTextBox5.Text + "," +
                CompSerNoTextBox5.Text + ",\"" + Remarks1TextBox5.Text + "\",\"" + Remarks2TextBox5.Text + "\",\"" + Remarks3TextBox5.Text + "\"";

            //if the file exsists overwrite it with newline if not create it and then overwrite it with new 
            //Instantly (fast enough you never see the file in file explorer) Commander will send the data along and delete the file

            FileStream fcreate = File.Open(path2, FileMode.Create);
            fcreate.Close();
            System.IO.File.WriteAllText(path2, newline2);

            //**********************************************************************************************End Print function
            //Increment the serial Number for each printed label
            int serialnumber = int.Parse(SerialNumberLabel.Text);
            SerialNumberLabel.Text = (serialnumber + 1).ToString();
            //Ensure the Serial number stays a consistant length currently 9 must be changed on the if and the while if needed to be changed.
            do
            {
                if (SerialNumberLabel.Text.Length < 8) { SerialNumberLabel.Text = '0' + SerialNumberLabel.Text; }
            }
            while (SerialNumberLabel.Text.Length < 8);
            //Overwrite the serial number file so that each time the program is opened it starts with the next serial number in line.
            System.IO.File.WriteAllText("SerialNumber.txt", SerialNumberLabel.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void save()
        {
            try
            {
                string savepath = "SaveTemp.txt";
                if (!File.Exists(savepath))
                {
                    using (StreamWriter w = File.AppendText(savepath))
                    {
                        w.WriteLine(LineTextBox.Text + "," + DayCodeTextBox.Text + "," + TypeComboBox.Text + "," + Remarks1TextBox.Text + "," + DescriptionTextBox.Text + "," + BuyerTextBox.Text + "," + Remarks2TextBox.Text + "," + ResNoTextBox.Text + "," + YUMProdComboBox.Text + "," + Remarks3TextBox.Text + "," + CasesTextBox.Value + "," + WeightTextBox.Value + "," + OrigSerNoTextBox.Text + "," + CompSerNoTextBox.Text + "," + TotalWeightTextBox.Text + "," + TotalCasesTextBox.Text);
                        w.WriteLine(LineTextBox2.Text + "," + DayCodeTextBox2.Text + "," + TypeComboBox2.Text + "," + Remarks1TextBox2.Text + "," + DescriptionTextBox2.Text + "," + BuyerTextBox2.Text + "," + Remarks2TextBox2.Text + "," + ResNoTextBox2.Text + "," + YUMProdComboBox2.Text + "," + Remarks3TextBox2.Text + "," + CasesTextBox2.Value + "," + WeightTextBox2.Value + "," + OrigSerNoTextBox2.Text + "," + CompSerNoTextBox2.Text + "," + TotalWeightTextBox2.Text + "," + TotalCasesTextBox2.Text + ",");
                        w.WriteLine(LineTextBox3.Text + "," + DayCodeTextBox3.Text + "," + TypeComboBox3.Text + "," + Remarks1TextBox3.Text + "," + DescriptionTextBox3.Text + "," + BuyerTextBox3.Text + "," + Remarks2TextBox3.Text + "," + ResNoTextBox3.Text + "," + YUMProdComboBox3.Text + "," + Remarks3TextBox3.Text + "," + CasesTextBox3.Value + "," + WeightTextBox3.Value + "," + OrigSerNoTextBox3.Text + "," + CompSerNoTextBox3.Text + "," + TotalWeightTextBox3.Text + "," + TotalCasesTextBox3.Text + ",");
                        w.WriteLine(LineTextBox4.Text + "," + DayCodeTextBox4.Text + "," + TypeComboBox4.Text + "," + Remarks1TextBox4.Text + "," + DescriptionTextBox4.Text + "," + BuyerTextBox4.Text + "," + Remarks2TextBox4.Text + "," + ResNoTextBox4.Text + "," + YUMProdComboBox4.Text + "," + Remarks3TextBox4.Text + "," + CasesTextBox4.Value + "," + WeightTextBox4.Value + "," + OrigSerNoTextBox4.Text + "," + CompSerNoTextBox4.Text + "," + TotalWeightTextBox4.Text + "," + TotalCasesTextBox4.Text + ",");
                        w.WriteLine(LineTextBox5.Text + "," + DayCodeTextBox5.Text + "," + TypeComboBox5.Text + "," + Remarks1TextBox5.Text + "," + DescriptionTextBox5.Text + "," + BuyerTextBox5.Text + "," + Remarks2TextBox5.Text + "," + ResNoTextBox5.Text + "," + YUMProdComboBox5.Text + "," + Remarks3TextBox5.Text + "," + CasesTextBox5.Value + "," + WeightTextBox5.Value + "," + OrigSerNoTextBox5.Text + "," + CompSerNoTextBox5.Text + "," + TotalWeightTextBox5.Text + "," + TotalCasesTextBox5.Text + ",");
                        w.WriteLine(LineTextBox6.Text + "," + DayCodeTextBox6.Text + "," + TypeComboBox6.Text + "," + Remarks1TextBox6.Text + "," + DescriptionTextBox6.Text + "," + BuyerTextBox6.Text + "," + Remarks2TextBox6.Text + "," + ResNoTextBox6.Text + "," + YUMProdComboBox6.Text + "," + Remarks3TextBox6.Text + "," + CasesTextBox6.Value + "," + WeightTextBox6.Value + "," + OrigSerNoTextBox6.Text + "," + CompSerNoTextBox6.Text + "," + TotalWeightTextBox6.Text + "," + TotalCasesTextBox6.Text);
                    };
                }
                else
                {
                    string savethis = LineTextBox.Text + "," + DayCodeTextBox.Text + "," + TypeComboBox.Text + "," + Remarks1TextBox.Text + "," + DescriptionTextBox.Text + "," + BuyerTextBox.Text + "," + Remarks2TextBox.Text + "," + ResNoTextBox.Text + "," + YUMProdComboBox.Text + "," + Remarks3TextBox.Text + "," + CasesTextBox.Value + "," + WeightTextBox.Value + "," + OrigSerNoTextBox.Text + "," + CompSerNoTextBox.Text + "," + TotalWeightTextBox.Text + "," + TotalCasesTextBox.Text + ","
                    + LineTextBox2.Text + "," + DayCodeTextBox2.Text + "," + TypeComboBox2.Text + "," + Remarks1TextBox2.Text + "," + DescriptionTextBox2.Text + "," + BuyerTextBox2.Text + "," + Remarks2TextBox2.Text + "," + ResNoTextBox2.Text + "," + YUMProdComboBox2.Text + "," + Remarks3TextBox2.Text + "," + CasesTextBox2.Value + "," + WeightTextBox2.Value + "," + OrigSerNoTextBox2.Text + "," + CompSerNoTextBox2.Text + "," + TotalWeightTextBox2.Text + "," + TotalCasesTextBox2.Text + ","
                    + LineTextBox3.Text + "," + DayCodeTextBox3.Text + "," + TypeComboBox3.Text + "," + Remarks1TextBox3.Text + "," + DescriptionTextBox3.Text + "," + BuyerTextBox3.Text + "," + Remarks2TextBox3.Text + "," + ResNoTextBox3.Text + "," + YUMProdComboBox3.Text + "," + Remarks3TextBox3.Text + "," + CasesTextBox3.Value + "," + WeightTextBox3.Value + "," + OrigSerNoTextBox3.Text + "," + CompSerNoTextBox3.Text + "," + TotalWeightTextBox3.Text + "," + TotalCasesTextBox3.Text + ","
                    + LineTextBox4.Text + "," + DayCodeTextBox4.Text + "," + TypeComboBox4.Text + "," + Remarks1TextBox4.Text + "," + DescriptionTextBox4.Text + "," + BuyerTextBox4.Text + "," + Remarks2TextBox4.Text + "," + ResNoTextBox4.Text + "," + YUMProdComboBox4.Text + "," + Remarks3TextBox4.Text + "," + CasesTextBox4.Value + "," + WeightTextBox4.Value + "," + OrigSerNoTextBox4.Text + "," + CompSerNoTextBox4.Text + "," + TotalWeightTextBox4.Text + "," + TotalCasesTextBox4.Text + ","
                    + LineTextBox5.Text + "," + DayCodeTextBox5.Text + "," + TypeComboBox5.Text + "," + Remarks1TextBox5.Text + "," + DescriptionTextBox5.Text + "," + BuyerTextBox5.Text + "," + Remarks2TextBox5.Text + "," + ResNoTextBox5.Text + "," + YUMProdComboBox5.Text + "," + Remarks3TextBox5.Text + "," + CasesTextBox5.Value + "," + WeightTextBox5.Value + "," + OrigSerNoTextBox5.Text + "," + CompSerNoTextBox5.Text + "," + TotalWeightTextBox5.Text + "," + TotalCasesTextBox5.Text + ","
                    + LineTextBox6.Text + "," + DayCodeTextBox6.Text + "," + TypeComboBox6.Text + "," + Remarks1TextBox6.Text + "," + DescriptionTextBox6.Text + "," + BuyerTextBox6.Text + "," + Remarks2TextBox6.Text + "," + ResNoTextBox6.Text + "," + YUMProdComboBox6.Text + "," + Remarks3TextBox6.Text + "," + CasesTextBox6.Value + "," + WeightTextBox6.Value + "," + OrigSerNoTextBox6.Text + "," + CompSerNoTextBox6.Text + "," + TotalWeightTextBox6.Text + "," + TotalCasesTextBox6.Text;

                    FileStream fcreate = File.Open(savepath, FileMode.Create);
                    fcreate.Close();
                    System.IO.File.WriteAllText(savepath, savethis);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }
        }

        private void load()
        {
            try {
                string savepath = "SaveTemp.txt";
                if (File.Exists(savepath))
                {
                    String SavedData = System.IO.File.ReadAllText(savepath);
                    string[] Splitdata = SavedData.Split(',');
                    //line1
                    LineTextBox.Text = Splitdata[0];
                    DayCodeTextBox.Text = Splitdata[1];
                    TypeComboBox.Text = Splitdata[2];
                    Remarks1TextBox.Text = Splitdata[3];
                    DescriptionTextBox.Text = Splitdata[4];
                    BuyerTextBox.Text = Splitdata[5];
                    Remarks2TextBox.Text = Splitdata[6];
                    ResNoTextBox.Text = Splitdata[7];
                    YUMProdComboBox.Text = Splitdata[8];
                    Remarks3TextBox.Text = Splitdata[9];
                    CasesTextBox.Value = Int32.Parse(Splitdata[10]);
                    WeightTextBox.Value = Int32.Parse(Splitdata[11]);
                    OrigSerNoTextBox.Text = Splitdata[12];
                    CompSerNoTextBox.Text = Splitdata[13];
                    TotalWeightTextBox.Text = Splitdata[14];
                    TotalCasesTextBox.Text = Splitdata[15];

                    //line2
                    LineTextBox2.Text = Splitdata[16];
                    DayCodeTextBox2.Text = Splitdata[17];
                    TypeComboBox2.Text = Splitdata[18];
                    Remarks1TextBox2.Text = Splitdata[19];
                    DescriptionTextBox2.Text = Splitdata[20];
                    BuyerTextBox2.Text = Splitdata[21];
                    Remarks2TextBox2.Text = Splitdata[22];
                    ResNoTextBox2.Text = Splitdata[23];
                    YUMProdComboBox2.Text = Splitdata[24];
                    Remarks3TextBox2.Text = Splitdata[25];
                    CasesTextBox2.Value = Int32.Parse(Splitdata[26]);
                    WeightTextBox2.Value = Int32.Parse(Splitdata[27]);
                    OrigSerNoTextBox2.Text = Splitdata[28];
                    CompSerNoTextBox2.Text = Splitdata[29];
                    TotalWeightTextBox2.Text = Splitdata[30];
                    TotalCasesTextBox2.Text = Splitdata[31];

                    //line3
                    LineTextBox3.Text = Splitdata[32];
                    DayCodeTextBox3.Text = Splitdata[33];
                    TypeComboBox3.Text = Splitdata[34];
                    Remarks1TextBox3.Text = Splitdata[35];
                    DescriptionTextBox3.Text = Splitdata[36];
                    BuyerTextBox3.Text = Splitdata[37];
                    Remarks2TextBox3.Text = Splitdata[38];
                    ResNoTextBox3.Text = Splitdata[39];
                    YUMProdComboBox3.Text = Splitdata[40];
                    Remarks3TextBox3.Text = Splitdata[41];
                    CasesTextBox3.Value = Int32.Parse(Splitdata[42]);
                    WeightTextBox3.Value = Int32.Parse(Splitdata[43]);
                    OrigSerNoTextBox3.Text = Splitdata[44];
                    CompSerNoTextBox3.Text = Splitdata[45];
                    TotalWeightTextBox3.Text = Splitdata[46];
                    TotalCasesTextBox3.Text = Splitdata[47];

                    //Line4
                    LineTextBox4.Text = Splitdata[48];
                    DayCodeTextBox4.Text = Splitdata[49];
                    TypeComboBox4.Text = Splitdata[50];
                    Remarks1TextBox4.Text = Splitdata[51];
                    DescriptionTextBox4.Text = Splitdata[52];
                    BuyerTextBox4.Text = Splitdata[53];
                    Remarks2TextBox4.Text = Splitdata[54];
                    ResNoTextBox4.Text = Splitdata[55];
                    YUMProdComboBox4.Text = Splitdata[56];
                    Remarks3TextBox4.Text = Splitdata[57];
                    CasesTextBox4.Value = Int32.Parse(Splitdata[58]);
                    WeightTextBox4.Value = Int32.Parse(Splitdata[59]);
                    OrigSerNoTextBox4.Text = Splitdata[60];
                    CompSerNoTextBox4.Text = Splitdata[61];
                    TotalWeightTextBox4.Text = Splitdata[62];
                    TotalCasesTextBox4.Text = Splitdata[63];

                    //line5
                    LineTextBox5.Text = Splitdata[64];
                    DayCodeTextBox5.Text = Splitdata[65];
                    TypeComboBox5.Text = Splitdata[66];
                    Remarks1TextBox5.Text = Splitdata[67];
                    DescriptionTextBox5.Text = Splitdata[67];
                    BuyerTextBox5.Text = Splitdata[69];
                    Remarks2TextBox5.Text = Splitdata[70];
                    ResNoTextBox5.Text = Splitdata[71];
                    YUMProdComboBox5.Text = Splitdata[72];
                    Remarks3TextBox5.Text = Splitdata[73];
                    CasesTextBox5.Value = Int32.Parse(Splitdata[74]);
                    WeightTextBox5.Value = Int32.Parse(Splitdata[75]);
                    OrigSerNoTextBox5.Text = Splitdata[76];
                    CompSerNoTextBox5.Text = Splitdata[77];
                    TotalWeightTextBox5.Text = Splitdata[78];
                    TotalCasesTextBox5.Text = Splitdata[79];

                    //line6
                    LineTextBox6.Text = Splitdata[80];
                    DayCodeTextBox6.Text = Splitdata[81];
                    TypeComboBox6.Text = Splitdata[82];
                    Remarks1TextBox6.Text = Splitdata[83];
                    DescriptionTextBox6.Text = Splitdata[84];
                    BuyerTextBox6.Text = Splitdata[85];
                    Remarks2TextBox6.Text = Splitdata[86];
                    ResNoTextBox6.Text = Splitdata[87];
                    YUMProdComboBox6.Text = Splitdata[88];
                    Remarks3TextBox6.Text = Splitdata[89];
                    CasesTextBox6.Value = Int32.Parse(Splitdata[90]);
                    WeightTextBox6.Value = Int32.Parse(Splitdata[91]);
                    OrigSerNoTextBox6.Text = Splitdata[92];
                    CompSerNoTextBox6.Text = Splitdata[93];
                    TotalWeightTextBox6.Text = Splitdata[94];
                    TotalCasesTextBox6.Text = Splitdata[95];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tell the IT department you have seen this error\n" + ex.ToString());
            }


}

        private void LineTextBox_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void DayCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TypeComboBox_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks1TextBox_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void BuyerTextBox_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks2TextBox_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TotalWeightTextBox_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TotalCasesTextBox_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void ResNoTextBox_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void YUMProdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks3TextBox_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void CasesTextBox_ValueChanged(object sender, EventArgs e)
        {
            save();
        }

        private void WeightTextBox_ValueChanged(object sender, EventArgs e)
        {
            save();
        }

        private void OrigSerNoTextBox_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void CompSerNoTextBox_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void LineTextBox2_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void DayCodeTextBox2_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TypeComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks1TextBox2_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void DescriptionTextBox2_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void BuyerTextBox2_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks2TextBox2_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void ResNoTextBox2_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void YUMProdComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks3TextBox2_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void CasesTextBox2_ValueChanged(object sender, EventArgs e)
        {
            save();
        }

        private void WeightTextBox2_ValueChanged(object sender, EventArgs e)
        {
            save();
        }

        private void OrigSerNoTextBox2_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void CompSerNoTextBox2_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void LineTextBox3_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void DayCodeTextBox3_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TypeComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks1TextBox3_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void DescriptionTextBox3_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void BuyerTextBox3_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks2TextBox3_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void ResNoTextBox3_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void YUMProdComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks3TextBox3_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void CasesTextBox3_ValueChanged(object sender, EventArgs e)
        {
            save();
        }

        private void WeightTextBox3_ValueChanged(object sender, EventArgs e)
        {
            save();
        }

        private void OrigSerNoTextBox3_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void CompSerNoTextBox3_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void LineTextBox4_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void DayCodeTextBox4_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TypeComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks1TextBox4_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void DescriptionTextBox4_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void BuyerTextBox4_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks2TextBox4_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void ResNoTextBox4_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void YUMProdComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks3TextBox4_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void CasesTextBox4_ValueChanged(object sender, EventArgs e)
        {
            save();
        }

        private void WeightTextBox4_ValueChanged(object sender, EventArgs e)
        {
            save();
        }

        private void OrigSerNoTextBox4_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void CompSerNoTextBox4_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void LineTextBox5_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void DayCodeTextBox5_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TypeComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks1TextBox5_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void DescriptionTextBox5_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void BuyerTextBox5_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks2TextBox5_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void ResNoTextBox5_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void YUMProdComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks3TextBox5_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void CasesTextBox5_ValueChanged(object sender, EventArgs e)
        {
            save();
        }

        private void WeightTextBox5_ValueChanged(object sender, EventArgs e)
        {
            save();
        }

        private void OrigSerNoTextBox5_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void CompSerNoTextBox5_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void LineTextBox6_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void DayCodeTextBox6_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TypeComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks1TextBox6_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void DescriptionTextBox6_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void BuyerTextBox6_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks2TextBox6_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void ResNoTextBox6_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void YUMProdComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            save();
        }

        private void Remarks3TextBox6_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void CasesTextBox6_ValueChanged(object sender, EventArgs e)
        {
            save();
        }

        private void WeightTextBox6_ValueChanged(object sender, EventArgs e)
        {
            save();
        }

        private void OrigSerNoTextBox6_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void CompSerNoTextBox6_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TotalWeightTextBox2_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TotalCasesTextBox2_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TotalWeightTextBox3_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TotalCasesTextBox3_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TotalWeightTextBox4_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TotalCasesTextBox4_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TotalWeightTextBox5_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TotalCasesTextBox5_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TotalWeightTextBox6_TextChanged(object sender, EventArgs e)
        {
            save();
        }

        private void TotalCasesTextBox6_TextChanged(object sender, EventArgs e)
        {
            save();
        }
    }
}