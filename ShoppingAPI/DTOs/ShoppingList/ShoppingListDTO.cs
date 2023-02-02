using System;
using ShoppingAPI.DTOs.ShoppingDetailList;
using ShoppingAPI.DTOs.SuperMarket;
using ShoppingAPI.DTOs.User;

namespace ShoppingAPI.DTOs.ShoppingList
{
	public class ShoppingListDTO : ShoppingListCreationDTO
	{
        public Guid ShoppingListId { get; set; }

        public virtual InfoSuperMarketDTO? SuperMarket { get; set; }

        public virtual InfoUserDTO? User { get; set; }

        public ICollection<InfoShoppingDetailListDTO>? ShoppingDetailLists { get; set; }
    }
}

