using System;
using System.Linq.Expressions;

namespace ShoppingAPI.Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        void Update(T entity);
    }
}

