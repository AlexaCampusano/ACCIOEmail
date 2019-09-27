using System.Collections.Generic;
using System.Threading.Tasks;
using Accio.Data.Models;

namespace Accio.Data.Repository
{
    public interface IPromotionsRepository
    {
        IEnumerable<Promotions> GetPromotions();
    }
}