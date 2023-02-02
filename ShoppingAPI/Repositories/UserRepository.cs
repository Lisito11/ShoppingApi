using System;
using ShoppingAPI.Contracts;
using ShoppingAPI.Models;

namespace ShoppingAPI.Repositories
{
	public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ShoppingDbContext shoppingContext) : base(shoppingContext)
        {
        }

        
    }
}

