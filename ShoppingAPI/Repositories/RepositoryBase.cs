using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Contracts;

namespace ShoppingAPI.Repositories
{
	public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ShoppingDbContext ShoppingDbContext { get; set; }

        public RepositoryBase(ShoppingDbContext shoppingDbContext)
        {
            ShoppingDbContext = shoppingDbContext;
        }

        public IQueryable<T> FindAll() => ShoppingDbContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => ShoppingDbContext.Set<T>().Where(expression).AsNoTracking();

        public async Task Create(T entity) => await ShoppingDbContext.Set<T>().AddAsync(entity);

        public void Update(T entity) => ShoppingDbContext.Set<T>().Update(entity);


    }
}

