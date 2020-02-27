using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class HourlyIncomeCalcualtor : Form
    {
        public HourlyIncomeCalcualtor()
        {
            InitializeComponent();
        }

        private void CalculateIncome_Click(object sender, EventArgs e)
        {
            double wage, hours, total, regpay, overtimepay, overtimehours, overtimewage;

            wage = Convert.ToDouble(HourlyWage.Text);
            hours = Convert.ToDouble(Hoursworked.Text);

            OvertimeHours.Text = " ";
            RegularPay.Text = " ";
            OvertimeWage.Text = " ";
            OvertimePay.Text = " ";
            RegularHours.Text = " ";
            GrossIncome.Text = " ";

            if (hours <= 40) // when no overtime has happened
            {
                total = wage * hours;

                GrossIncome.Text = "$" + Convert.ToString(total);
                RegularPay.Text = "$" + Convert.ToString(total);
                RegularHours.Text = Convert.ToString(hours) + " Hours";
                OvertimeHours.Text = "0 Hours";
                OvertimePay.Text = "$0.00";
                OvertimeWage.Text = "$"+Convert.ToString(wage * 1.5);
            }
            else if (hours > 40) //when there was overtime
            {
                overtimehours = hours - 40;
                regpay = wage * 40;
                overtimewage = wage * 1.5;
                overtimepay = overtimewage * overtimehours;
                total = regpay + overtimepay;
                OvertimeHours.Text = Convert.ToString(overtimehours)+" Hours";
                RegularPay.Text = "$"+Convert.ToString(regpay);
                OvertimeWage.Text = "$"+Convert.ToString(overtimewage);
                OvertimePay.Text = "$"+Convert.ToString(overtimepay);
                RegularHours.Text = "40 Hours";
                GrossIncome.Text = "$"+Convert.ToString(total);
            }
        }
    }
}
