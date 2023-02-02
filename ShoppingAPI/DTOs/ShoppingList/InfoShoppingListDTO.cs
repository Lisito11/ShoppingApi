using System;
using ShoppingAPI.DTOs.SuperMarket;
using ShoppingAPI.DTOs.User;

namespace ShoppingAPI.DTOs.ShoppingList
{
	public class InfoShoppingListDTO
	{
        public Guid ShoppingListId { get; set; }

        public virtual InfoSuperMarketDTO? SuperMarket { get; set; }

        public virtual InfoUserDTO? User { get; set; }
    }
}

