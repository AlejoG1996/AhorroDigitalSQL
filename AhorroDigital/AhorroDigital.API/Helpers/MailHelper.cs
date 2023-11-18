using AhorroDigital.Common.Response;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace AhorroDigital.API.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;

        public MailHelper(IConfiguration configuration)
        {
                _configuration = configuration;
        }
        Response IMailHelper.SendMail(string to, string subject, string body)
        {
            try
            {
                string from = _configuration["Mail:From"];
                string smtp = _configuration["Mail:Smtp"];
                string port = _configuration["Mail:Port"];
                string password = _configuration["Mail:Password"];

                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress(from));
                message.To.Add(new MailboxAddress(to));
                message.Subject = subject;

              

               



                BodyBuilder bodyBuilder = new BodyBuilder();
                
           
                 //var image = bodyBuilder.LinkedResources.Add("https://ahorrodigitalapi.azurewebsites.net/images/confirmacioncuentaemail.gif");
                 // image.ContentId = "imagen";


                bodyBuilder.HtmlBody = body;
                message.Body = bodyBuilder.ToMessageBody();
               
                using  (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                {
                    
                    client.Connect(smtp, int.Parse(port), false);
                    client.Authenticate(from, password);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return new Response { IsSuccess = true };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = ex
                };
            }

        }


    }
}
