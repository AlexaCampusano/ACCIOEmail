using System.Threading.Tasks;

namespace Accio.BLL.Services
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}