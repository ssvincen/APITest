using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace APITest.BI
{
    public class EmailProvider : IEmailProvider
    {
        private static readonly string email = ConfigurationManager.AppSettings["APIemailAddress"];
        private static readonly string password = ConfigurationManager.AppSettings["APIPassword"];
        public async Task SendAsync(string destination, string subject, string body, int retryCount)
        {
            if (retryCount > 1)
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add(destination);
                    mailMessage.From = new MailAddress(email, "Call Login");
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.EnableSsl = false;
                        smtp.Host = "mail.test.com";
                        smtp.Port = 25;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(email, password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                        await smtp.SendMailAsync(mailMessage);
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    //throw ex;
                    await SendAsync(destination, subject, body, --retryCount);
                }
            }
        }
    }
}
