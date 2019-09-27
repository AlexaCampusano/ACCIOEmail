using System.Collections.Generic;
using System.Threading.Tasks;
using Accio.Data.Models;
using Accio.Data.Repository;

namespace Accio.BLL.Services
{
    public class PromotionsService : IPromotionsService
    {
        private readonly IPromotionsRepository _repository;
        public PromotionsService(IPromotionsRepository repository)
        {
            _repository = repository;

        }
        public Task<IEnumerable<Promotions>> GetPromotions()
        {
            return Task.Run(() => _repository.GetPromotions());
        }
    }
}