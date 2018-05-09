using System;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace KalkulatorLab01
{
    public partial class Kalkulator : Form
    {
        public Kalkulator()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }
        #region variables
        private string result;
        private string operandtype = "";
        private string partOne = string.Empty, partTwo = string.Empty;
        private bool bIsWorking = false;

        public string GetResult()
        {
            return result;
        }
       
        public void SetResult(string value)
        {
            result = value;
        }

        public string GetOperandtype()
        {
            return operandtype;
        }

        public void SetOperandtype(string value)
        {
            operandtype = value;
        }

        public string GetPartOne()
        {
            return partOne;
        }

        public void SetPartOne(string value)
        {
            partOne = value;
        }

        public string GetPartTwo()
        {
            return partTwo;
        }

        public void SetPartTwo(string value)
        {
            partTwo = value;
        }

        public bool GetBIsWorking()
        {
            return bIsWorking;
        }

        public void SetBIsWorking(bool value)
        {
            bIsWorking = value;
        }
        #endregion


        private void button_Click(object sender,EventArgs e)
        {
            if ((TextDisplay.Text == "0") || (bIsWorking))
            {
                TextDisplay.Text = "";
            }
            bIsWorking = false;
            try
            {
                Button butn = sender as Button;
                if (TextDisplay.Text.Length >= 14)
                {
                    MessageBox.Show("Nie można wpisać więcej znaków!");
                }
                else
                {
                    if (butn.Text == ".")
                    {
                        if (!TextDisplay.Text.Contains("."))
                        {
                            if (TextDisplay.Text == "")
                            {
                                TextDisplay.Text = "0" + butn.Text;
                            }
                            else
                            {
                                TextDisplay.Text = TextDisplay.Text + butn.Text;
                            }
                        }
                    }
                    else
                    {
                        TextDisplay.Text = TextDisplay.Text + butn.Text;
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show( "Error: "+error.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            TextDisplay.Text = "0";
            SetResult(string.Empty);
            partOne = string.Empty;
            textCurrentPart.Text = partOne;
        }

        private void buttonOperationType_Click(object sender, EventArgs e)
        {
            try
            {
                Button butn = sender as Button;
                double res;
                double.TryParse(GetResult(), out res);
                operandtype = butn.Text;
                partOne = TextDisplay.Text;
                textCurrentPart.Text = partOne + " " + operandtype;
                bIsWorking = true; 
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonBackspace_Click(object sender, EventArgs e)
        {
            if (TextDisplay.Text != "")
            {
                TextDisplay.Text = TextDisplay.Text.Remove(TextDisplay.Text.Length - 1);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            TextDisplay.Text = "0";
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            partTwo = TextDisplay.Text;
            double one, two;

            double.TryParse(partOne, out one);
            double.TryParse(partTwo, out two);

            switch (operandtype)
            {
                case "+":
                    SetResult((one + two).ToString());
                    break;
                case "-":
                    SetResult((one - two).ToString());
                    break;
                case "*":
                    SetResult((one * two).ToString());
                    break;
                case "/":
                    if (two == 0)
                    {
                        MessageBox.Show("Nie wolno dzielić przez 0!");
                    }
                    else
                    {
                        SetResult((one / two).ToString());
                    }
                    break;
                default:
                    break;
            }
            TextDisplay.Text = GetResult();
            partOne = GetResult();
            operandtype = "";
            textCurrentPart.Text = partOne;
        }

        private void buttonChangeSign_Click(object sender, EventArgs e)
        {
            if (TextDisplay.Text.Length <= 0 || TextDisplay.Text == "0")
            {
                return;
            }
            if (TextDisplay.Text[0] == '-')
            {
                TextDisplay.Text = TextDisplay.Text.Remove(0, 1);
            }
            else
            {
                TextDisplay.Text = TextDisplay.Text.Insert(0, "-");
            }
        }
    }
}
