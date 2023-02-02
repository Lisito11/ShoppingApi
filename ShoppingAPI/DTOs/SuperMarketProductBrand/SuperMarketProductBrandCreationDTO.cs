using System;
namespace ShoppingAPI.DTOs.SuperMarketProductBrand
{
	public class SuperMarketProductBrandCreationDTO
	{
        public Guid? SuperMarketId { get; set; }

        public Guid? ProductBrandId { get; set; }

        public double? Price { get; set; }
    }
}

