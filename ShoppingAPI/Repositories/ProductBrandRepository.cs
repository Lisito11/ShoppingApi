using System;
using ShoppingAPI.Contracts;
using ShoppingAPI.Models;

namespace ShoppingAPI.Repositories
{
	public class ProductBrandRepository : RepositoryBase<ProductBrand>, IProductBrandRepository
    {
        public ProductBrandRepository(ShoppingDbContext shoppingContext) : base(shoppingContext)
        {

        }
    }
}

