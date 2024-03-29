﻿using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using ToyWorld.API.Models;

namespace ToyWorld.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IToyRepository _repo;
        public EmailService(IConfiguration config, IToyRepository repo)
        {
            _config = config;
            _repo = repo;   
        }

        public async Task SendEmail(MailRequest request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = request.Subject;

            var toy = _repo.GetToy(request.ToyId).Result;

            var builder = new BodyBuilder();
            builder.TextBody = request.Body;
            if (toy != null)
            {
                builder.Attachments.Add($"{toy.Name}.png", toy.Image, new ContentType("image", "png"));
            }

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
