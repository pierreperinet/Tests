using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
namespace Email
{
    public class SimpleAsynchronousExample
    {
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
        public static void Main(string[] args)
        {
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient()
            {
               Host = "smtp-mail.outlook.com",
               Port = 587,
               Credentials = new NetworkCredential("mankala.game@outlook.com", "Mankala-74"),
               //UseDefaultCredentials = true,
               EnableSsl = true
            };
            MailAddress from = new MailAddress("mankala.game@outlook.com", "Mankala Game", System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress("pierre.perinet@outlook.com");
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            MailMessage message = new MailMessage(from, to)
            {
                Body = "This is a test e-mail message sent by an application. " + Environment.NewLine + someArrows,
                BodyEncoding = System.Text.Encoding.UTF8,
                Subject = "test message 1" + someArrows,
                SubjectEncoding = System.Text.Encoding.UTF8
            };
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            string userState = "test message1";
            client.SendAsync(message, userState);
            Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.");
            string answer = Console.ReadLine();
            if (answer.StartsWith("c") && mailSent == false)
            {
                client.SendAsyncCancel();
            }
            // Clean up.
            message.Dispose();
            Console.WriteLine("Goodbye.");
        }
    }
}