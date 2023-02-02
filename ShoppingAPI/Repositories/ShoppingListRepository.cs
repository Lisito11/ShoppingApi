using System;
using ShoppingAPI.Contracts;
using ShoppingAPI.Models;

namespace ShoppingAPI.Repositories
{
	public class ShoppingListRepository : RepositoryBase<ShoppingList>, IShoppingListRepository
    {
        public ShoppingListRepository(ShoppingDbContext shoppingContext) : base(shoppingContext)
        {

        }
    }
}

