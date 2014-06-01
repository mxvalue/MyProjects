using System;
using System.Windows.Forms;

namespace avon
{
    public class AvonCalc
    {
        public double TotalSumWithDostavka;
        public double TotalSumValue;
        public double ProcentValue;
        public double SumSkidka;
        public string[] TextStrings = new string[10];
        public string[] ComboStrings = new string[10];
        public double[] SumStrings = new double[10];
        public double[] NumerUpDown = new double[10];

        //расчет суммы
        public double SumTotal(string[] calc)
        {
            double total = 0;

            if (calc[0] == "") { } else { SumStrings[0] = Convert.ToDouble(calc[0]) * NumerUpDown[0]; total += SumStrings[0]; }
            if (calc[1] == "") { } else { SumStrings[1] = Convert.ToDouble(calc[1]) * NumerUpDown[1]; total += SumStrings[1]; }
            if (calc[2] == "") { } else { SumStrings[2] = Convert.ToDouble(calc[2]) * NumerUpDown[2]; total += SumStrings[2]; }
            if (calc[3] == "") { } else { SumStrings[3] = Convert.ToDouble(calc[3]) * NumerUpDown[3]; total += SumStrings[3]; }
            if (calc[4] == "") { } else { SumStrings[4] = Convert.ToDouble(calc[4]) * NumerUpDown[4]; total += SumStrings[4]; }
            if (calc[5] == "") { } else { SumStrings[5] = Convert.ToDouble(calc[5]) * NumerUpDown[5]; total += SumStrings[5]; }
            if (calc[6] == "") { } else { SumStrings[6] = Convert.ToDouble(calc[6]) * NumerUpDown[6]; total += SumStrings[6]; }
            if (calc[7] == "") { } else { SumStrings[7] = Convert.ToDouble(calc[7]) * NumerUpDown[7]; total += SumStrings[7]; }
            if (calc[8] == "") { } else { SumStrings[8] = Convert.ToDouble(calc[8]) * NumerUpDown[8]; total += SumStrings[8]; }
            if (calc[9] == "") { } else { SumStrings[9] = Convert.ToDouble(calc[9]) * NumerUpDown[9]; total += SumStrings[9]; }
            
            return total;
        }

        //расчет суммы с учетом процентов
        public double procentTotal(string[] proc)
        {
            double total = 0;

            if (SumStrings[0] == 0) { } else { if (proc[0] == "10%") { total += (SumStrings[0] * 0.10); } else if (proc[0] == "15%") { total += (SumStrings[0] * 0.15); } }
            if (SumStrings[1] == 0) { } else { if (proc[1] == "10%") { total += (SumStrings[1] * 0.10); } else if (proc[1] == "15%") { total += (SumStrings[1] * 0.15); } }
            if (SumStrings[2] == 0) { } else { if (proc[2] == "10%") { total += (SumStrings[2] * 0.10); } else if (proc[2] == "15%") { total += (SumStrings[2] * 0.15); } }
            if (SumStrings[3] == 0) { } else { if (proc[3] == "10%") { total += (SumStrings[3] * 0.10); } else if (proc[3] == "15%") { total += (SumStrings[3] * 0.15); } }
            if (SumStrings[4] == 0) { } else { if (proc[4] == "10%") { total += (SumStrings[4] * 0.10); } else if (proc[4] == "15%") { total += (SumStrings[4] * 0.15); } }
            if (SumStrings[5] == 0) { } else { if (proc[5] == "10%") { total += (SumStrings[5] * 0.10); } else if (proc[5] == "15%") { total += (SumStrings[5] * 0.15); } }
            if (SumStrings[6] == 0) { } else { if (proc[6] == "10%") { total += (SumStrings[6] * 0.10); } else if (proc[6] == "15%") { total += (SumStrings[6] * 0.15); } }
            if (SumStrings[7] == 0) { } else { if (proc[7] == "10%") { total += (SumStrings[7] * 0.10); } else if (proc[7] == "15%") { total += (SumStrings[7] * 0.15); } }
            if (SumStrings[8] == 0) { } else { if (proc[8] == "10%") { total += (SumStrings[8] * 0.10); } else if (proc[8] == "15%") { total += (SumStrings[8] * 0.15); } }
            if (SumStrings[9] == 0) { } else { if (proc[9] == "10%") { total += (SumStrings[9] * 0.10); } else if (proc[9] == "15%") { total += (SumStrings[9] * 0.15); } }

            return total;
        }

        //расчет скидки
        public double sumSkidka(double total, double skidka)
        {
            double result = total - skidka;

            return result;
        }

        //расчет доставки
        public double Dostavka(double b)
        {
            double total = 0;

            if (TotalSumValue > 0 && TotalSumValue < 9000) { total = b + 500; } else { total = b; }

            return total;

        }

        //рекурсия, очистить все чексбоксы и комбобоксы
        public void ResetBoxes(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                TextBox tb = c as TextBox;
                if (tb != null)
                {
                    tb.Text = string.Empty;
                }
                ResetBoxes(c.Controls);
            }
            foreach (Control b in controls)
            {
                ComboBox cb = b as ComboBox;
                if (cb != null)
                {
                    cb.Text = string.Empty;
                }
                ResetBoxes(b.Controls);
            }
        }

        //Итоговые расчеты
        public void result()
        {
            TotalSumValue = SumTotal(TextStrings);
            ProcentValue = procentTotal(ComboStrings);
            SumSkidka = sumSkidka(TotalSumValue, ProcentValue);
            TotalSumWithDostavka = Dostavka(SumSkidka);
        }
    }
}