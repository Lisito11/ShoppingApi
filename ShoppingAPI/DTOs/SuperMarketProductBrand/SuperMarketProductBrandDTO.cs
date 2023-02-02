using System;
using ShoppingAPI.DTOs.ProductBrand;
using ShoppingAPI.DTOs.SuperMarket;

namespace ShoppingAPI.DTOs.SuperMarketProductBrand
{
	public class SuperMarketProductBrandDTO : SuperMarketProductBrandCreationDTO
	{
        public Guid SuperMarketProductBrandId { get; set; }

        public virtual InfoSuperMarketDTO? SuperMarket { get; set; }

        public virtual InfoProductBrandDTO? ProductBrand { get; set; }
    }
}

