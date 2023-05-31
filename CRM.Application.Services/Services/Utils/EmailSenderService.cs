using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace CRM.Application.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendConfirmationEmail(AppUser user, string token)
        {
            var encodedToken = HttpUtility.UrlEncode(token);

            string mailboxEmail = "daniel_bednarz@outlook.com";
            string mailboxName = "CRM System";

            string url = @$"http://localhost:9000/#/confirmEmail?id={user.Id}&token={encodedToken}";

            string style = @"    <style>
    .button {background - color: purple;
      color: white;
      padding: 10px 20px;
      text-align: center;
      text-decoration: none;
      display: inline-block;
      font-size: 16px;
      margin: 10px;
      cursor: pointer;
    }
</style>";

            string htmlMessage = @$"
<html>
<head>
    <title>Potwierdzenie rejestracji</title>
    {style}
</head>
<body>
    <h1>Witaj {user.FirstName} {user.LastName}!</h1>
    <p>Administrator systemu utworzył konto dla tego adresu email.</p>
    <p>Aby dokończyć proces rejestracji, kliknij w poniższy link:</p>
    <a class='button' href='{url}'>Potwierdź adres e-mail</a>
    <p>Życzymy udanej pracy!</p>
    <p>Zespół CRM System</p>
</body>
</html>
";

            string secret = _configuration["SMTP:Password"];

            try
            {
                var message = new MailMessage();
                message.From = new MailAddress(mailboxEmail, mailboxName);
                message.To.Add(user.Email);
                message.Subject = "Potwierdzenie rejestracji";
                message.Body = htmlMessage;
                message.IsBodyHtml = true;

                using var smtpClient = new SmtpClient();
                smtpClient.Host = "smtp-mail.outlook.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(mailboxEmail, secret);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                await smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while sending the email: {ex.Message}", ex);
            }
        }
    }
}
