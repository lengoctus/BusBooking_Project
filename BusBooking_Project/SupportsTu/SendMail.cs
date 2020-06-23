using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BusBooking_Project.SupportsTu
{
    public class SendMail
    {
        private IConfiguration Configuration { get; }


        public SendMail(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public bool Send(string to, string Subject, string Content)
        {
            try
            {
                var username = Configuration["Gmail:Username"];
                var password = Configuration["Gmail:Password"];

                var smtp = new SmtpClient
                {
                    Host = Configuration["Gmail:Port"],
                    Port = Convert.ToInt32(Configuration["Gmail:Port"]),
                    EnableSsl = Convert.ToBoolean(Configuration["SMTP:starttls:enable"]),
                    Credentials = new NetworkCredential(username, password)
                };

                var mailMes = new MailMessage(username, to);
                mailMes.Subject = Subject;
                mailMes.Body = Content;
                mailMes.IsBodyHtml = true;

                smtp.Send(mailMes);

                return true;

            }
            catch (Exception e)
            {
                return false;
                throw;
            }
        }
    }
}
