using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReplacementForWestMark
{
    public partial class Get_Weight : Form
    {
        public Get_Weight()
        {
            InitializeComponent();
        }

        private void Okbut_Click(object sender, EventArgs e)
        {
            var pf = Application.OpenForms.OfType<Print_Manual_Ticket>().First();
            pf._weight = numericUpDown1.Value;
            pf._LineNumber = LineBox.SelectedItem.ToString();
        }

        private void cancelbut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
