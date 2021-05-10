using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class formCalculator : Form
    {

        private const string initText = "0"; // nachalna stoynost

        private TextBox output() => this.textResult;
        private bool newResult = false;
        private bool memory_flag = true;
        private double value = 0;
        private string lastOp = "+";  
        double memory = 0;



        public formCalculator()
        {
            InitializeComponent();
            output().Text = initText;
        }

        public void AppendDecimal()
        {
            if (newResult)
            {
                newResult = false;
                output().Text = initText;
            }

            // ako veche ima decimal
            if (output().Text.Contains(","))
            {
                return;
            }
            else
            {
                output().AppendText(",");
            }
        }

        public void AppendNum(string num)
        {
            if (newResult)
            {
                newResult = false;
                output().Text = initText;
            }
            output().AppendText(num);

            // ako ima decimal
            if (output().Text.Contains(","))
            {
                return;
            }
            // premahvane na 0
            else
            {
                output().Text = Double.Parse(output().Text).ToString();
            }
        }

        public void Calculate(string op) // osnovni kalkulacii
        {
            output().Text.TrimEnd(',');
            textOperations.AppendText($" {output().Text} {op}");
            double operand = Double.Parse(output().Text);
            newResult = true;

            // ako / na 0
            if (lastOp == "/" && output().Text == "0")
            {
                MessageBox.Show("NA 0 NE SE DELI!");
                output().Text = initText;
                return;
            }

            if (lastOp == "+") { value += operand; }
            if (lastOp == "-") { value -= operand; }
            if (lastOp == "*") { value *= operand; }
            if (lastOp == "/") { value /= operand; }


            lastOp = op;
            output().Text = value.ToString();
        }

        public void Clear()
        {
            value = 0;
            textOperations.Clear();
            output().Text = initText;
            newResult = false;
        }

        public void ClearError()
        {
            output().Text = initText;
            newResult = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // chisla
        private void buttonNum_Click(object sender, EventArgs e) => AppendNum(((Button)sender).Text);

        // decimal-a
        private void buttonDecimal_Click(object sender, EventArgs e) => AppendDecimal();

        // clear buton i clear error buton
        private void buttonClear_Click(object sender, EventArgs e) => Clear();//Button clear (C)
        private void buttonClearError_Click(object sender, EventArgs e) => ClearError();//Button Clear Error (CE)
        private void buttonDelete_Click(object sender, EventArgs e)//Backspace button

        {
            output().Text = output().Text.Substring(0, output().Text.Length - 1);
            if (String.IsNullOrEmpty(output().Text)) { output().Text = initText; }
        }

        // osnovni i basic calculacii 
        private void buttonDivide_Click(object sender, EventArgs e) => Calculate("/");// /
        private void buttonMultiply_Click(object sender, EventArgs e) => Calculate("*");//*
        private void buttonSubtract_Click(object sender, EventArgs e) => Calculate("-");//-
        private void buttonAdd_Click(object sender, EventArgs e) => Calculate("+");//+
        private void buttonEquals_Click(object sender, EventArgs e)// button =
        {
            Calculate("+");

            string result = output().Text;
            Clear();
            newResult = true;
            output().Text = result;

        }

        private void buttonSign_Click(object sender, EventArgs e)
        {
            bool decimalPoint = output().Text.EndsWith(",");
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = "."; // opit za popravka na problema za operacii s decimal , pri koito "." dava greshka pri nqkoi pc
            output().Text.TrimEnd(',');
            output().Text = (Double.Parse(output().Text) * -1).ToString();
            if (decimalPoint)
            {
                output().AppendText(",");
            }
            decimalPoint.ToString(nfi);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(output().Text);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string txt = Clipboard.GetText();
            double num;

            if (Double.TryParse(txt, out num))
            {
                output().Text = txt;
            }
        }

        private void buttonPercent_Click(object sender, EventArgs e)// tova e square root operaciq
        {
            string result = output().Text;
            result = double.Parse(textResult.Text).ToString();
            double result2 = double.Parse(result);
            double v = Math.Sqrt(result2);

            textResult.Text = v.ToString();

        }

        private void buttonMequals_Click(object sender, EventArgs e)//Operaciq Memory Save(MS)
        {
            memory = Double.Parse(textResult.Text);
            buttonMR.Enabled = true;
            buttonMC.Enabled = true;
            memory_flag = true;
          
        }

        private void buttonMminus_Click(object sender, EventArgs e)//Operaciq M-
        {
            memory -= Double.Parse(textResult.Text);
        }

        private void buttonMR_Click(object sender, EventArgs e)//Operaciq Memory Read (MR)
        {
            textResult.Text = memory.ToString();
             memory_flag = true;
        }

        private void buttonMC_Click(object sender, EventArgs e)//Operaciq Memory Clear (MC)
        {
            textResult.Text = "0";
            memory = 0;
            buttonMC.Enabled = false;
            buttonMR.Enabled = false;
        }

        private void buttonMplus_Click(object sender, EventArgs e)//Operaciq M+
        {
            memory += Double.Parse(textResult.Text);ToString();
        }
    }
}
