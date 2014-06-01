using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace avon
{
    public class Calc
    {
        private double firstValue;
        private double secondValue;
        public int id = 0;

        //TextBox from Form1
        string textBox24;

        public void textBox(string t1)
        {
            textBox24 = t1;
        }


        //КАЛЬКУЛЯТОР 
        public double getFirstValue()
        {
            return firstValue;
        }

        public void setFirstValue(string f)
        {
            firstValue += Convert.ToDouble(f);
        }

        public double getSecondValue()
        {
            return secondValue;
        }

        public void setSecondValue(string s)
        {
            secondValue = Convert.ToDouble(s);
        }

        //расчет (операторы)
        public double resultTotal()
        {
            double total = 0;

            switch (id)
            {

                case 1: total = getFirstValue() + getSecondValue();
                    break;
                case 2: total = getFirstValue() - getSecondValue();
                    break;
                case 3: total = getFirstValue() * getSecondValue();
                    break;
                case 4: total = getFirstValue() / getSecondValue();
                    break;

            }
            return total;
        }
    }
}
