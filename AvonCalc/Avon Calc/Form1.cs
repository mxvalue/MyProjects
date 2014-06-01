using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Avon_Calc
{
    public partial class Form1 : Form
    {
        public static double totalSum = 0;
        public static double totalDiscount = 0;
        public static double totalPayment = 0;
        double[] sum = new double[15];
        double[] discount = new double[15];
        int i = 0;
        string a;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        public void totalAmount()
        {
            totalSum = sum[0] + sum[1] + sum[2] + sum[3] + sum[4] + sum[5] + sum[6] + sum[7] + sum[8] + sum[9] + sum[10] + sum[11] + sum[12] + sum[13] + sum[14];
            totalDiscount = discount[0] + discount[1] + discount[2] + discount[3] + discount[4] + discount[5] + discount[6] + discount[7] + discount[8] + discount[9]
                 + discount[10] + discount[11] + discount[12] + discount[13] + discount[14];
            if (totalSum < 9000) { totalPayment = (totalSum-totalDiscount) + 500; } else { totalPayment=totalSum-totalDiscount;}
            listBox1.Items.Add("-------------------------------------------------------------");
            listBox1.Items.Add("Общая стоимость: "+ totalSum);
            listBox1.Items.Add("Скидка: " + totalDiscount);
            listBox1.Items.Add("Итого к оплате: " + totalPayment);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox1.SelectedIndex = -1;
        }

        public void clearAll()
        {
            textBox1.Clear();
            textBox2.Clear();
            numericUpDown1.Value = 1;
            comboBox1.Text = "";
            totalDiscount = 0;
            comboBox1.SelectedIndex = 0;
        }

        public void result()
        {
            if (textBox2.Text!="")
            {
                if (i < 15)
                {
                    sum[i] = Convert.ToDouble(textBox2.Text) * Convert.ToDouble(numericUpDown1.Value);
                    listBox1.Items.Add(textBox1.Text+" "+textBox2.Text+"тг "+numericUpDown1.Value+"шт "+comboBox1.Text);
                    

                    if (comboBox1.Text == "") { } else if (comboBox1.Text == "10%") { discount[i] = Convert.ToDouble(sum[i]) * 0.10; } else if (comboBox1.Text == "15%") { discount[i] = Convert.ToDouble(sum[i]) * 0.15; }
                }
                else { MessageBox.Show("Превышено максимальное количество покупок"); }
                clearAll();
                i++;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            result();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            totalSum = 0;
            i = 0;
            Array.Clear(sum, 0, 10);
            Array.Clear(discount, 0, 10);
            clearAll();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (sum[0]!=0)
            {
                totalAmount();
            }
            a = listBox1.Text;
        }
       

        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Жуков Константин");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Avon калькулятор SE v1.0");
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void отправитьНаПочтуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}