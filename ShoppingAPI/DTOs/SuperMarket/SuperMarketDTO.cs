using System;
using ShoppingAPI.DTOs.ShoppingList;
using ShoppingAPI.DTOs.SuperMarketProductBrand;
using ShoppingAPI.Models;

namespace ShoppingAPI.DTOs.SuperMarket
{
	public class SuperMarketDTO : SuperMarketCreationDTO
	{
        public Guid SuperMarketId { get; set; }
        public ICollection<InfoShoppingListDTO>? ShoppingLists { get; set; }
        public ICollection<InfoSuperMarketProductBrandDTO>? SuperMarketProductBrands { get; set; }
    }
} 

