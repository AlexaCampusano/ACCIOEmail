using System.Collections.Generic;
using System.Threading.Tasks;
using Accio.Data.Models;

namespace Accio.BLL.Services
{
    public interface IPromotionsService
    {
        Task<IEnumerable<Promotions>> GetPromotions();
    }
}