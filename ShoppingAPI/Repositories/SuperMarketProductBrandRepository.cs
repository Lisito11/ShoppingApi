using System;
using ShoppingAPI.Contracts;
using ShoppingAPI.Models;

namespace ShoppingAPI.Repositories
{
	public class SuperMarketProductBrandRepository : RepositoryBase<SuperMarketProductBrand>, ISuperMarketProductBrandRepository
    {
        public SuperMarketProductBrandRepository(ShoppingDbContext shoppingContext) : base(shoppingContext)
        {

        }
    }
}

