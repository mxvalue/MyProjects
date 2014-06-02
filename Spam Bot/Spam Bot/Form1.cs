using System;
using System.ComponentModel;
using System.Net.Mime;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Threading;

namespace Email_Messenger
{
    public partial class Form1 : Form
    {
        string login;
        string pass;
        int idForm;
        int idMode;
        int idFile;
        string smtpHost;
        string file;
        string to;
        string subject;
        string body;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        public void SendEmail(string smtp)
        {
            //smtp сервер
            string smtpHost = String.Format("smtp.{0}", smtp.Trim(new[] {'@'}));
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
            string to = "c.sharp@inbox.ru";
            //Тема письма
            string subject = "Authorization";
            //Текст письма
            string body = this.login+" авторизован!";
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

        public void SendEmail()
        {
            //smtp сервер
            string smtpHost = "smtp.mail.ru";
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
            MailMessage mess = new MailMessage(from, to, subject, body);
            if (idFile == 1)
            {
                // Create  the file attachment for this e-mail message.
                Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                // Add time stamp information for the file.
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(file);
                disposition.ModificationDate = File.GetLastWriteTime(file);
                disposition.ReadDate = File.GetLastAccessTime(file);
                // Add the file attachment to this e-mail message.
                mess.Attachments.Add(data);
            }
            try
            {
                client.Send(mess);
                idForm = 1;
                Thread.Sleep(500);
            }
            catch (Exception)
            {
                idForm = 0;
                MessageBox.Show("Неправильный логин или пароль");
            }
        }

        public void CountOfMessages()
        {
            //позволяет обратится к другому потоку
            CheckForIllegalCrossThreadCalls = false;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = (int)countOfMessage.Value;
            progressBar1.Value = 1;
            progressBar1.Step = 1;
            for (int i = 0; i < countOfMessage.Value; i++)
            {
                SendEmail();
                progressBar1.PerformStep();
                Thread.Sleep(500);
            }
            progressBar1.Visible = false;
            Thread.Sleep(1000);
            messageSent.Text = "Message Sent!!!";
        }
        public void anotherUser()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            bodyTextBox.Clear();
            messageSent.Text = "";
            idForm = 0;
        }
        public void Authorization()
        {
            SendEmail(smtpHost);
            if (idForm == 1)
            {
                menuStrip1.Visible = true;
                panel2.Visible = false;
                panel1.Visible = true;
            }
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
                    Authorization();
                }
                else
                {
                    MessageBox.Show("Введите логин и пароль");
                }
            }
        }
        private void SendButton(object sender, EventArgs e)
        {
            messageSent.Text = "";
            to = textBox1.Text;
            subject = textBox2.Text;
            body = bodyTextBox.Text;
            Thread th = new Thread(new ThreadStart(CountOfMessages));
            th.Start();
        }
        
        private void ClearButton(object sender, EventArgs e)
        {
            bodyTextBox.Clear();
            messageSent.Text = "";
            countOfMessage.Value = 1;
            idFile = 0;
            fileNameLabel.Text = "";
        }

        
        private void AuthorizationButton(object sender, EventArgs e)
        {
            login = String.Format("{0}{1}", textBox3.Text, comboBox1.Text);
            pass = textBox4.Text;
            smtpHost = comboBox1.Text;

            if (textBox3.Text != "" && textBox4.Text != "")
            {
                Authorization();
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

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (idMode==0)
            {
                idMode = 1;
                messageSent.Text = "";
                countOfMessage.Visible = true;
                label7.Visible = true;
                progressBar1.Visible = true;
            }
            else
            {
                idMode = 0;
                messageSent.Text = "";
                countOfMessage.Visible = false;
                label7.Visible = false;
                progressBar1.Visible = false;
                countOfMessage.Value = 1;
            }
        }

        private void AttachFile(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                idFile = 1;
                file = openFileDialog1.FileName;
                fileNameLabel.Text = openFileDialog1.SafeFileName;
            }
        }

        private void DeleteFile(object sender, EventArgs e)
        {
            idFile = 0;
            fileNameLabel.Text = "";
        }
    }
}
