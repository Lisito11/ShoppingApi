using System;
using ShoppingAPI.Contracts;
using ShoppingAPI.Models;

namespace ShoppingAPI.Repositories
{
	public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ShoppingDbContext shoppingContext): base(shoppingContext)
        {

        }
    }
}

