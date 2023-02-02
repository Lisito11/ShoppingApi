using System;
using System.Linq.Expressions;
using ShoppingAPI.Models;

namespace ShoppingAPI.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
    }
}

