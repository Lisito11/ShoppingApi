using System;
using ShoppingAPI.DTOs.Brand;
using ShoppingAPI.DTOs.Product;

namespace ShoppingAPI.DTOs.ProductBrand
{
	public class InfoProductBrandDTO
	{
        public Guid ProductBrandId { get; set; }

        public virtual InfoProductDTO? Product { get; set; }

        public virtual InfoBrandDTO? Brand { get; set; }
    }
}

