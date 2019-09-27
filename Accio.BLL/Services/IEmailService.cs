using System.Threading.Tasks;
using Accio.BLL.Models;

namespace Accio.BLL.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toAddress, EmailBody body = null, string ccAddress = null);
    }
}