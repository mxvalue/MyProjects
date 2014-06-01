using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace avon
{
    public partial class Form1 : Form
    {
        AvonCalc avonCalc = new AvonCalc();
        Calc calc = new Calc();

        public Form1()
        {
            InitializeComponent();
            MaximumSize = new Size(427, 506);
            MinimumSize = new Size(427, 506);
            comboBoxHide();
        }

        public int calcID = 0;

        //Переносим данные форм в массивы
        public void SetArrays()
        {
            String[] texts = new string[10];
            texts[0] = textBox2.Text;
            texts[1] = textBox5.Text;
            texts[2] = textBox13.Text;
            texts[3] = textBox14.Text;
            texts[4] = textBox15.Text;
            texts[5] = textBox16.Text;
            texts[6] = textBox17.Text;
            texts[7] = textBox18.Text;
            texts[8] = textBox19.Text;
            texts[9] = textBox23.Text;
            avonCalc.TextStrings = texts;

            String[] combox = new string[10];
            combox[0] = comboBox1.Text;
            combox[1] = comboBox2.Text;
            combox[2] = comboBox3.Text;
            combox[3] = comboBox4.Text;
            combox[4] = comboBox5.Text;
            combox[5] = comboBox6.Text;
            combox[6] = comboBox7.Text;
            combox[7] = comboBox8.Text;
            combox[8] = comboBox9.Text;
            combox[9] = comboBox10.Text;
            avonCalc.ComboStrings = combox;

            double[] numericUp = new double[10];
            numericUp[0] = Convert.ToDouble(numericUpDown1.Value);
            numericUp[1] = Convert.ToDouble(numericUpDown2.Value);
            numericUp[2] = Convert.ToDouble(numericUpDown3.Value);
            numericUp[3] = Convert.ToDouble(numericUpDown4.Value);
            numericUp[4] = Convert.ToDouble(numericUpDown5.Value);
            numericUp[5] = Convert.ToDouble(numericUpDown6.Value);
            numericUp[6] = Convert.ToDouble(numericUpDown7.Value);
            numericUp[7] = Convert.ToDouble(numericUpDown8.Value);
            numericUp[8] = Convert.ToDouble(numericUpDown9.Value);
            numericUp[9] = Convert.ToDouble(numericUpDown10.Value);
            avonCalc.NumerUpDown = numericUp;
        }

        //кнопка посчитать
        private void button1_Click(object sender, EventArgs e)
        {
            SetArrays();
            avonCalc.result();
            label19.Text = Convert.ToString(avonCalc.TotalSumValue) + " тг";
            label20.Text = Convert.ToString(avonCalc.ProcentValue) + " тг";
            label21.Text = Convert.ToString(avonCalc.TotalSumWithDostavka) + " тг";
        }

        //кнопка очистить
        private void button2_Click(object sender, EventArgs e)
        {
            avonCalc.ResetBoxes(Controls);
            comboBoxHide();
            label19.Text = "";
            label20.Text = "";
            label21.Text = "";

        }

        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("(с)Жуков Константин");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Avon калькулятор v3.0");
        }

        //keydown отлавливает нажатие enter и delete
        private void enterKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetArrays();
                avonCalc.result();
                label19.Text = Convert.ToString(avonCalc.TotalSumValue) + " тг";
                label20.Text = Convert.ToString(avonCalc.ProcentValue) + " тг";
                label21.Text = Convert.ToString(avonCalc.TotalSumWithDostavka) + " тг";
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (sender == textBox2) { textBox2.Text = null; }
                if (sender == textBox5) { textBox5.Text = null; }
                if (sender == textBox13) { textBox13.Text = null; }
                if (sender == textBox14) { textBox14.Text = null; }
                if (sender == textBox15) { textBox15.Text = null; }
                if (sender == textBox16) { textBox16.Text = null; }
                if (sender == textBox17) { textBox17.Text = null; }
                if (sender == textBox18) { textBox18.Text = null; }
                if (sender == textBox19) { textBox19.Text = null; }
                if (sender == textBox23) { textBox23.Text = null; }
            }
        }

        //запрет на ввод символов за счет keypress
        private void keyChar(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar)&& !char.IsControl(e.KeyChar))
            {
                
                e.Handled = true;
            }
            if (sender == textBox2) { comboBox1.Enabled = true; } if (textBox2.Text == "") { comboBox1.Enabled = false; }
            if (sender == textBox5) { comboBox2.Enabled = true; } if (textBox5.Text == "") { comboBox2.Enabled = false; }
            if (sender == textBox13) { comboBox3.Enabled = true; } if (textBox13.Text == "") { comboBox3.Enabled = false; }
            if (sender == textBox14) { comboBox4.Enabled = true; } if (textBox14.Text == "") { comboBox4.Enabled = false; }
            if (sender == textBox15) { comboBox5.Enabled = true; } if (textBox15.Text == "") { comboBox5.Enabled = false; }
            if (sender == textBox16) { comboBox6.Enabled = true; } if (textBox16.Text == "") { comboBox6.Enabled = false; }
            if (sender == textBox17) { comboBox7.Enabled = true; } if (textBox17.Text == "") { comboBox7.Enabled = false; }
            if (sender == textBox18) { comboBox8.Enabled = true; } if (textBox18.Text == "") { comboBox8.Enabled = false; }
            if (sender == textBox19) { comboBox9.Enabled = true; } if (textBox19.Text == "") { comboBox9.Enabled = false; }
            if (sender == textBox23) { comboBox10.Enabled = true; } if (textBox23.Text == "") { comboBox10.Enabled = false; }
        }

        //Запись в файл из меню->экспорт.
        private void экспортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (label19.Text == "" || label19.Text == "0 тг"){MessageBox.Show("Нет данных для экспорта.");}

            else
            {
                string text = null;
                string text1 = null;
                string text2 = null;
                string text3 = null;
                string text4 = null;
                string text5 = null;
                string text6 = null;
                string text7 = null;
                string text8 = null;
                string text9 = null;
                string total;
                string header;

                header = "Наименование|" + "Стоимость|" + "Скидка" + Environment.NewLine + "" + Environment.NewLine;

                if (textBox2.Text != "") { text = textBox1.Text + " " + textBox2.Text + "тг " + comboBox1.Text + Environment.NewLine; }
                if (textBox5.Text != "") { text1 = textBox4.Text + " " + textBox5.Text + "тг " + comboBox2.Text + Environment.NewLine; }
                if (textBox13.Text != "") { text2 = textBox8.Text + " " + textBox13.Text + "тг " + comboBox3.Text + Environment.NewLine; }
                if (textBox14.Text != "") { text3 = textBox12.Text + " " + textBox14.Text + "тг " + comboBox4.Text + Environment.NewLine; }
                if (textBox15.Text != "") { text4 = textBox11.Text + " " + textBox15.Text + "тг " + comboBox5.Text + Environment.NewLine; }
                if (textBox16.Text != "") { text5 = textBox9.Text + " " + textBox16.Text + "тг " + comboBox6.Text + Environment.NewLine; }
                if (textBox17.Text != "") { text6 = textBox10.Text + " " + textBox17.Text + "тг " + comboBox7.Text + Environment.NewLine; }
                if (textBox18.Text != "") { text7 = textBox20.Text + " " + textBox18.Text + "тг " + comboBox8.Text + Environment.NewLine; }
                if (textBox19.Text != "") { text8 = textBox21.Text + " " + textBox19.Text + "тг " + comboBox9.Text + Environment.NewLine; }
                if (textBox23.Text != "") { text9 = textBox22.Text + " " + textBox23.Text + "тг " + comboBox10.Text + Environment.NewLine; }

                total = "-------------------------"
                    + Environment.NewLine + "Общая сумма: " + label19.Text + Environment.NewLine + "Скидка: " + label20.Text +
                    Environment.NewLine + "Итого к оплате: " + label21.Text + ".";

                System.IO.File.WriteAllText(@"C:\ExportFile.txt", header + text + text1 + text2 + text3 + text4 + text5 + text6 +
                    text7 + text8 + text9 + total);
                MessageBox.Show("Экспорт данных успешно завершен.");
            }
            }

        //Открыть/Скрыть Калькулятор из меню
        private void калькуляторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (calcID == 0)
            {
                calcID = 1;
                hideCalc(calcID);
            }
            else
            {
                calcID = 0;
                hideCalc(calcID);
            }
        }

        //скрыть калькулятор по кнопке
        private void button3_Click(object sender, EventArgs e)
        {
            MaximumSize = new Size(427, 506);
            MinimumSize = new Size(427, 506);
            textBox24.Clear();
            calcID = 0;
        }

        //скрытие калькулятора с использованием идентификатора
        public void hideCalc(int a)
        {
            int id = a;

            if (id == 1)
            {
                MaximumSize = new Size(739, 506);
                MinimumSize = new Size(739, 506);
            }
            else
            {
                MaximumSize = new Size(427, 506);
                MinimumSize = new Size(427, 506);
            }
        }

        //Кнопки со значениями
        #region Value
        private void button4_Click(object sender, EventArgs e)
        {
            textBox24.Text = textBox24.Text + button4.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox24.Text = textBox24.Text + button5.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox24.Text = textBox24.Text + button6.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox24.Text = textBox24.Text + button7.Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox24.Text = textBox24.Text + button8.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox24.Text = textBox24.Text + button9.Text;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox24.Text = textBox24.Text + button10.Text;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox24.Text = textBox24.Text + button11.Text;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox24.Text = textBox24.Text + button12.Text;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox24.Text = textBox24.Text + button13.Text;
        }
        #endregion

        //Операторы
        #region Operators
        private void button14_Click(object sender, EventArgs e)
        {
            emptyError();
            calc.id = 1;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            emptyError();
            calc.id = 2;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            emptyError();
            calc.id = 3;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            emptyError();
            calc.id = 4;
        }
        
        private void button18_Click(object sender, EventArgs e)
        {
            textBox24.Clear();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (textBox24.Text == "")
            {

            }
            else
            {
                calc.setSecondValue(textBox24.Text);
                textBox24.Text = Convert.ToString(calc.resultTotal());
            }
        }
        #endregion

        //Считывание клавиш для калькулятора, KeyDown
        private void keyEnt(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add)
            {
                emptyError();
                calc.id = 1;
            }
            if (e.KeyCode == Keys.Subtract)
            {
                emptyError();
                calc.id = 2;
            }
            if (e.KeyCode == Keys.Multiply)
            {
                emptyError();
                calc.id = 3;
            }
            if (e.KeyCode == Keys.Divide)
            {
                emptyError();
                calc.id = 4;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox24.Text == "")
                {

                }
                else
                {
                    calc.setSecondValue(textBox24.Text);
                    textBox24.Text = Convert.ToString(calc.resultTotal());
                }
            }
            if (e.KeyCode == Keys.Delete)
            {
                textBox24.Clear();
            }
        }

        
        //Открытие ссылки по клику на картинке
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://kz.avon.com/REPSuite/loginMain.page?not_ya=yes&timeout=yes&");
        }
        
        
        //Проверка на заполненность поля
        public void emptyError()
        {
            if (textBox24.Text == "")
            {

            }
            else
            {
                calc.setFirstValue(textBox24.Text);
                textBox24.Clear();
            }
        }

        //Скрыть комбобоксы
        public void comboBoxHide()
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            comboBox6.Enabled = false;
            comboBox7.Enabled = false;
            comboBox8.Enabled = false;
            comboBox9.Enabled = false;
            comboBox10.Enabled = false;
            numericUpDown1.Text = "1";
            numericUpDown2.Text = "1";
            numericUpDown3.Text = "1";
            numericUpDown4.Text = "1";
            numericUpDown5.Text = "1";
            numericUpDown6.Text = "1";
            numericUpDown7.Text = "1";
            numericUpDown8.Text = "1";
            numericUpDown9.Text = "1";
            numericUpDown10.Text = "1";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //открывает калькулятор
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("calc.exe");
        }

        private void button20_Click(object sender, EventArgs e)
        {

        }
    }
}
