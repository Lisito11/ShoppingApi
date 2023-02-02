using System;
using ShoppingAPI.Contracts;
using ShoppingAPI.Models;

namespace ShoppingAPI.Repositories
{
	public class ShoppingDetailListRepository : RepositoryBase<ShoppingDetailList>, IShoppingDetailListRepository
    {
        public ShoppingDetailListRepository(ShoppingDbContext shoppingContext) : base(shoppingContext)
        {

        }
    }
}

