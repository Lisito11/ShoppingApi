using System;
using ShoppingAPI.Contracts;
using ShoppingAPI.Models;

namespace ShoppingAPI.Repositories
{
	public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
    {
        public BrandRepository(ShoppingDbContext shoppingContext) : base(shoppingContext)
        {

        }
    }
}

