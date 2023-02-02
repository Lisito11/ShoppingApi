using System;
using ShoppingAPI.DTOs.ShoppingList;
using ShoppingAPI.DTOs.SuperMarketProductBrand;

namespace ShoppingAPI.DTOs.ShoppingDetailList
{
	public class ShoppingDetailListDTO : ShoppingDetailListCreationDTO
	{
        public Guid ShoppingDetailListId { get; set; }

        public virtual InfoSuperMarketProductBrandDTO? SuperMarketProductBrand { get; set; }

        public virtual InfoShoppingListDTO? ShoppingList { get; set; }

    }
}

