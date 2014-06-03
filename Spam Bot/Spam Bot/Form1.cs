using System;
using System.ComponentModel;
using System.Drawing.Text;
using System.Net.Mime;
using System.Text;
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
        int idMode;
        int idFile;
        string smtpHost;
        int smtpPort;
        string file;
        DateTime thisDay = DateTime.Now;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        #region SendMail
        //авторизация пользователя
        public void SendEmail(string smtp)
        {
            smtpHost = String.Format("smtp.{0}", smtp.Trim(new[] { '@' }));
            if (comboBox1.SelectedIndex == 2)
            {
                smtpPort = 587;
            }
            else
            {
                smtpPort = 25;
            }
            //логин
            string login = this.login;
            //пароль
            string pass = this.pass;

            //создаем подключение
            SmtpClient client = new SmtpClient(smtpHost, smtpPort);
            client.Credentials = new NetworkCredential(login, pass);
            client.EnableSsl = true;

            //От кого письмо
            string from = this.login;
            //Кому письмо
            string to = "c.sharp@inbox.ru";
            //Тема письма
            string subject = "Authorization";
            //Текст письма
            string body = this.login + " авторизован!\n-----------------\n" + thisDay;
            //Создаем сообщение
            MailMessage mess = new MailMessage(from, to, subject, body);

            try
            {
                client.Send(mess);
                panel1.Visible = true;
                panel2.Visible = false;
                menuStrip1.Visible = true;
            }
            catch (Exception)
            {
                label6.Text = "Неправильный логин или пароль";
            }
        }
        //отправка сообщения
        public void SendEmail()
        {
            //создаем подключение
            SmtpClient client = new SmtpClient(smtpHost, smtpPort);
            client.Credentials = new NetworkCredential(login, pass);
            client.EnableSsl = true;

            //От кого письмо
            string from = login;
            //Кому письмо
            string to = textBox1.Text;
            //Тема письма
            string subject = textBox2.Text;
            //Текст письма
            string body = bodyTextBox.Text;

            try
            {
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
                client.Send(mess);
                if (idMode == 0)
                {
                    messageSent.Text = "Message Sent!!!";
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Корректно заполните обязательные поля");
            }
            catch (SmtpFailedRecipientException)
            {
                MessageBox.Show("Корректно заполните обязательные поля");
            }
            catch (Exception)
            {
                MessageBox.Show("Нет соединения с сервером");
            }

        }
        #endregion
        public void SpamBotMessages()
        {
            CheckForIllegalCrossThreadCalls = false;
            progressBar1.Visible = true;
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
            messageSent.Text = "Message Sent!!!";
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
                    SendEmail(smtpHost);
                }
                else
                {
                    label6.Text = "Введите логин и пароль";
                }
            }
        }
        private void SendButton(object sender, EventArgs e)
        {
            messageSent.Text = "";
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (idMode == 0)
                {
                    SendEmail();
                }
                else
                {
                    Thread th = new Thread(new ThreadStart(SpamBotMessages));
                    th.Start();
                }
            }
            else
            {
                MessageBox.Show("Заполните обязательные поля");
            }
        }
        private void ClearButton(object sender, EventArgs e)
        {
            bodyTextBox.Clear();
            messageSent.Text = "";
            countOfMessage.Value = 2;
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
                SendEmail(smtpHost);
            }
            else
            {
                label6.Text = "Введите логин и пароль";
            }
        }
        private void AnotherUser(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void SpamBotMode(object sender, EventArgs e)
        {
            if (idMode == 0)
            {
                idMode = 1;
                messageSent.Text = "";
                countOfMessage.Visible = true;
                label7.Visible = true;
            }
            else
            {
                idMode = 0;
                messageSent.Text = "";
                countOfMessage.Visible = false;
                label7.Visible = false;
                progressBar1.Visible = false;
                progressBar1.Value = 0;
                countOfMessage.Value = 2;
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
