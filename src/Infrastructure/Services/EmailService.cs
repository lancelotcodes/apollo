using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Shared.DTO.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailConfig> _emailOptions;

        public EmailService(IOptions<EmailConfig> emailOptions)
        {
            _emailOptions = emailOptions;
        }

        //sendgrid email SMTP
        public Task<bool> SendSMTPEmail(EmailModel model)
        {
            MailMessage message = new MailMessage();

            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress(_emailOptions.Value.FromEmail);
            message.To.Add(new MailAddress(model.To));

            message.CC.Add(new MailAddress(model.Copy));

            message.Subject = model.Subject;

            message.IsBodyHtml = true; //to make message body as html  
            message.Body = model.Body;

            smtp.Port = _emailOptions.Value.Port;
            smtp.Host = _emailOptions.Value.Host; //for gmail host   
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("apikey", _emailOptions.Value.ApiKey);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.SendMailAsync(message);
            return Task.FromResult(true);
        }

        public async Task<bool> SendEmail(EmailModel model, string fileName = null, byte[] bytes = null)
        {
            var client = new SendGridClient(_emailOptions.Value.ApiKey);
            var from = new EmailAddress(_emailOptions.Value.FromEmail, _emailOptions.Value.FromName);
            var to = new EmailAddress(model.To, "");
            var msg = MailHelper.CreateSingleEmail(from, to, model.Subject, model.Body, model.Body);
            if (!string.IsNullOrWhiteSpace(model.Copy))
                msg.AddCc(model.Copy);

            if (!string.IsNullOrWhiteSpace(fileName) && bytes != null)
            {
                var file = Convert.ToBase64String(bytes);
                msg.AddAttachment(fileName, file);
            }

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                throw new GenericException(response.StatusCode.ToString());
            }
            return true;
        }

        //sendgrid API
        public async Task<Response> SendEmail(EmailContentModel model)
        {
            var client = new SendGridClient(_emailOptions.Value.ApiKey);
            var from = new EmailAddress(model.FromEmail, model.FromName);
            var to = new EmailAddress(model.ToEmail, model.ToName);
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, model.TemplateId, model.DynamicObject);

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                throw new GenericException(response.StatusCode.ToString());
            }
            return response;
        }
    }
}
