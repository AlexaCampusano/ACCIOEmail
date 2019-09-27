using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accio.Data.Models;

namespace Accio.Data.Repository
{
    public class PromotionsRepository : IPromotionsRepository
    {
        public IEnumerable<Promotions> GetPromotions()
        {
            var promotions = new List<Promotions>();

            for (var i = 0; i <= 10; i++)
            {
                promotions.Add(new Promotions
                {
                    Id = i,
                    Name = $"Promotion {i}",
                    StartDate = DateTime.Now.AddDays(i),
                    EndDate = DateTime.Now.AddDays(i * 2)
                });
            }

            return promotions;
        }
    }
}