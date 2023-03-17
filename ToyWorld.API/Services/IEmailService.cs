using ToyWorld.API.Models;

namespace ToyWorld.API.Services
{
    public interface IEmailService
    {
        Task SendEmail(MailRequest request);
    }
}
