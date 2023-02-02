using System;
using ShoppingAPI.Contracts;

namespace ShoppingAPI.Repositories
{
	public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ShoppingDbContext _shoppingContext;

        public RepositoryWrapper(ShoppingDbContext shoppingContext)
        {
            _shoppingContext = shoppingContext;
        }

        private IProductRepository? _product;
        private IProductBrandRepository? _productBrand;
        private IBrandRepository? _brand;
        private IRoleRepository? _role;
        private ISuperMarketRepository? _superMarket;
        private ISuperMarketProductBrandRepository? _superMarketProductBrand;
        private IUserRepository? _user;
        private IShoppingListRepository? _shoppingList;
        private IShoppingDetailListRepository? _shoppingDetailList;

        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_shoppingContext);
                }
                return _product;
            }
        }

        public IProductBrandRepository ProductBrand
        {
            get
            {
                if (_productBrand == null)
                {
                    _productBrand = new ProductBrandRepository(_shoppingContext);
                }
                return _productBrand;
            }
        }

        public IBrandRepository Brand
        {
            get
            {
                if (_brand == null)
                {
                    _brand = new BrandRepository(_shoppingContext);
                }
                return _brand;
            }
        }

        public IRoleRepository Role
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepository(_shoppingContext);
                }
                return _role;
            }
        }

        public ISuperMarketRepository SuperMarket
        {
            get
            {
                if (_superMarket == null)
                {
                    _superMarket = new SuperMarketRepository(_shoppingContext);
                }
                return _superMarket;
            }
        }

        public ISuperMarketProductBrandRepository SuperMarketProductBrand
        {
            get
            {
                if (_superMarketProductBrand == null)
                {
                    _superMarketProductBrand = new SuperMarketProductBrandRepository(_shoppingContext);
                }
                return _superMarketProductBrand;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_shoppingContext);
                }
                return _user;
            }
        }

        public IShoppingListRepository ShoppingList
        {
            get
            {
                if (_shoppingList == null)
                {
                    _shoppingList = new ShoppingListRepository(_shoppingContext);
                }
                return _shoppingList;
            }
        }

        public IShoppingDetailListRepository ShoppingDetailList
        {
            get
            {
                if (_shoppingDetailList == null)
                {
                    _shoppingDetailList = new ShoppingDetailListRepository(_shoppingContext);
                }
                return _shoppingDetailList;
            }
        }


        public async Task Save()
        {
           await _shoppingContext.SaveChangesAsync();
        }
    }
}

