using System;
using ShoppingAPI.Contracts;
using ShoppingAPI.Models;

namespace ShoppingAPI.Repositories
{
	public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(ShoppingDbContext shoppingContext) : base(shoppingContext)
        {

        }
    }
}

