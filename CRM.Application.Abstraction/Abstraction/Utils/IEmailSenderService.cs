using CRM.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Abstraction
{
    public interface IEmailSenderService
    {
        Task SendConfirmationEmail(AppUser user, string token);
    }
}
