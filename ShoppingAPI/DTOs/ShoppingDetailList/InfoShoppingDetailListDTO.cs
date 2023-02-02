using System;
using ShoppingAPI.DTOs.SuperMarketProductBrand;

namespace ShoppingAPI.DTOs.ShoppingDetailList
{
	public class InfoShoppingDetailListDTO
	{

        public virtual InfoSuperMarketProductBrandDTO? SuperMarketProductBrand { get; set; }

        public string? Name { get; set; }

        public string? Brand { get; set; }

        public int? Quantity { get; set; }

        public double? Price { get; set; }
    }
}

