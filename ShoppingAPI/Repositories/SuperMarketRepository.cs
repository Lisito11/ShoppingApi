using System;
using ShoppingAPI.Contracts;
using ShoppingAPI.Models;

namespace ShoppingAPI.Repositories
{
	public class SuperMarketRepository : RepositoryBase<SuperMarket>, ISuperMarketRepository
    {
        public SuperMarketRepository(ShoppingDbContext shoppingContext) : base(shoppingContext)
        {

        }
    }
}

