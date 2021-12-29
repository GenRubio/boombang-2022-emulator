using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.mails.mailtrap
{
    class Mailtrap
    {
        public static void sendMessage(string subject, string body)
        {
            MailAddress to = new MailAddress(Config.MAILTRAP_TO);
            MailAddress from = new MailAddress(Config.MAILTRAP_FROM);

            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;

            SmtpClient client = new SmtpClient(Config.MAILTRAP_HOST, 2525)
            {
                Credentials = new NetworkCredential(Config.MAILTRAP_USERNAME, Config.MAILTRAP_PASSWORD),
                EnableSsl = true
            };
            // code in brackets above needed if authentication required

            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Log.error(ex);
            }
        }
    }
}
