using System;
namespace ShoppingAPI.Contracts
{
	public interface IRepositoryWrapper
	{
        IProductRepository Product { get; }
        IProductBrandRepository ProductBrand { get; }
        IBrandRepository Brand { get; }
        IRoleRepository Role { get; }
        ISuperMarketRepository SuperMarket { get; }
        ISuperMarketProductBrandRepository SuperMarketProductBrand { get; }
        IUserRepository User { get; }
        IShoppingListRepository ShoppingList { get; }
        IShoppingDetailListRepository ShoppingDetailList { get; }

        Task Save();
    }
}

