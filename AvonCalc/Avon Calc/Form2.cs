using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
namespace Avon_Calc
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public void SendMail(string log, string f1, string f2, string f3){
             Form2 form2 = new Form2();
            //smtp сервер
            string smtpHost = "smtp.mail.ru";
            //smtp порт
            int smtpPort = 25;
            //логин
            string login = "scbyy@mail.ru";
            //пароль
            string pass = "4relogeR4";

            //создаем подключение
            SmtpClient client = new SmtpClient(smtpHost, smtpPort);
            client.Credentials = new NetworkCredential(login, pass);

            //От кого письмо
            string from = "scbyy@mail.ru";
            //Кому письмо
            string to = log;
            //Тема письма
            string subject = "Ваш чек Avon";
            //Текст письма
            //string body = "Привет! \n\n\n Это тестовое письмо от C Sharp";
            string body = String.Format("Общая стоимость: {0} тг\nСкидка: {1} тг\nИтого к оплате: {2} тг",f1,f2,f3);
            //Создаем сообщение
            MailMessage mess = new MailMessage(from, to, subject, body);

            try
            {
                client.Send(mess);
                MessageBox.Show("Письмо отправлено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string f1 = Convert.ToString(Form1.totalSum);
            string f2 = Convert.ToString(Form1.totalDiscount);
            string f3 = Convert.ToString(Form1.totalPayment);
            SendMail(textBox1.Text,f1,f2,f3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            textBox1.Clear();
        }  
    }
}
