using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
test
namespace Email_Messenger
{
    public partial class Form1 : Form
    {
        string login;
        string pass;
        int idForm = 0;
        int idMode = 0;
        string smtpHost = "";
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        public void SendEmail(string a, string b, string c, string d)
        {
            //smtp сервер
            string smtpHost = String.Format("smtp.{0}", d.Trim(new[] { '@' } ));
            //smtp порт
            int smtpPort = 25;
            //логин
            string login = this.login;
            //пароль
            string pass = this.pass;

            //создаем подключение
            SmtpClient client = new SmtpClient(smtpHost, smtpPort);
            client.Credentials = new NetworkCredential(login, pass);

            //От кого письмо
            string from = this.login;
            //Кому письмо
            string to = a;
            //Тема письма
            string subject = b;
            //Текст письма
            string body = c;
            //Создаем сообщение
            MailMessage mess = new MailMessage(from, to, subject, body);

            try
            {
                client.Send(mess);
                idForm = 1;
            }
            catch (Exception)
            {
                idForm = 0;
                MessageBox.Show("Неправильный логин или пароль");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CountOfMessages();
            progressBar1.Visible = false;
            label6.Text = "Mail sent!!!"; 
        }
        public void anotherUser()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            richTextBox1.Clear();
            label6.Text = "";
            idForm = 0;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            label6.Text = "";
            numericUpDown1.Value = 1;
        }

        public void Authentication()
        {
           SendEmail("c.sharp@inbox.ru", login, pass, smtpHost);
            if (idForm == 1)
            {
                menuStrip1.Visible = true;
                panel2.Visible = false;
                panel1.Visible = true;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            login = String.Format("{0}{1}", textBox3.Text, comboBox1.Text);
            pass = textBox4.Text;
            smtpHost = comboBox1.Text;

            if (textBox3.Text != "" && textBox4.Text != "")
            {
                Authentication();
            }
            else
            {
                MessageBox.Show("Введите логин и пароль");
            }
        }

        private void сменитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            anotherUser();
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login = String.Format("{0}{1}", textBox3.Text, comboBox1.Text);
                pass = textBox4.Text;
                smtpHost = comboBox1.Text;

                if (textBox3.Text != "" && textBox4.Text != "")
                {
                    Authentication();
                }
                else
                {
                    MessageBox.Show("Введите логин и пароль");
                }
            }
        }

        public void CountOfMessages()
        {
            progressBar1.Visible = true;
            // Set Minimum to 1 to represent the first file being copied.
            progressBar1.Minimum = 1;
            // Set Maximum to the total number of files to copy.
            progressBar1.Maximum = (int)numericUpDown1.Value;
            // Set the initial value of the ProgressBar.
            progressBar1.Value = 1;
            // Set the Step property to a value of 1 to represent each file being copied.
            progressBar1.Step = 1;
            for (int i = 1; i <= numericUpDown1.Value; i++)
            {
                SendEmail(textBox1.Text, textBox2.Text, richTextBox1.Text, smtpHost);
                progressBar1.PerformStep();
                System.Threading.Thread.Sleep(500);
            }
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (idMode==0)
            {
                idMode = 1;
                label6.Text = "";
                numericUpDown1.Visible = true;
                label7.Visible = true;
            }
            else
            {
                idMode = 0;
                label6.Text = "";
                numericUpDown1.Visible = false;
                label7.Visible = false;
                progressBar1.Visible = false;
                numericUpDown1.Value = 1;
            }
        }
    }
}
