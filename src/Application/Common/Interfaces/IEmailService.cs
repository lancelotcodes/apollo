using apollo.Application.Common.Models;
using SendGrid;
using System.Threading.Tasks;

namespace apollo.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendSMTPEmail(EmailModel model);
        Task<Response> SendEmail(EmailContentModel model);
        Task<bool> SendEmail(EmailModel model, string fileName = null, byte[] bytes = null);
    }
}
