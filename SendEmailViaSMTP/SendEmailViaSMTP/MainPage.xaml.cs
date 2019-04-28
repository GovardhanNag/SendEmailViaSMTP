using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SendEmailViaSMTP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnSend_Clicked(object sender, EventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("govardhan.nagaraja", "********");

                MailAddress from = new MailAddress("govardhan.nagaraja@gmail.com", "govardhan.nagaraja");
                MailAddress to = new MailAddress(entryTo.Text);
                MailAddress cc = new MailAddress(entryCc.Text);

                MailMessage message = new MailMessage(from, to);
                message.CC.Add(cc);
                message.Body = entryBody.Text;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = entrySubject.Text;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                // Set the method that is called back when the send operation ends.
                client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                string userState = "Send Email via SMTP";
                client.SendAsync(message, userState);
                message.Dispose();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }
    }
}
