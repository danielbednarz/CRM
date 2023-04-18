using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace CRM.Application.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public async Task SendConfirmationEmail(AppUser user, string token)
        {
            var encodedToken = HttpUtility.UrlEncode(token);

            string url = @$"http://localhost:9000/#/confirmEmail?id={user.Id}&token={encodedToken}";

            string htmlMessage = @$"
<html>
<head>
    <title>Potwierdzenie rejestracji</title>
</head>
<body>
    <h1>Witaj {user.FirstName} {user.LastName}!</h1>
    <p>Administrator systemu utworzył konto dla tego adresu email.</p>
    <p>Aby dokończyć proces rejestracji, kliknij w poniższy link:</p>
    <a href='{url}'>Potwierdź adres e-mail</a>
    <p>Życzymy udanej pracy!</p>
    <p>Zespół CRM System</p>
</body>
</html>
";

            string pwd = File.ReadAllText(@"D:\pwd.txt");

            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("daniel_bednarz@outlook.com", "CRM System");
                message.To.Add(user.Email);
                message.Subject = "Potwierdzenie rejestracji";
                message.Body = htmlMessage;
                message.IsBodyHtml = true;

                using var smtpClient = new SmtpClient();
                smtpClient.Host = "smtp-mail.outlook.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("daniel_bednarz@outlook.com", pwd);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                await smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Wystąpił błąd podczas wysyłania e-maila: {ex.Message}", ex);
            }
        }
    }
}
