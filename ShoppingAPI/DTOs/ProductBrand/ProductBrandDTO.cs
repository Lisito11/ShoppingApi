using System;
using ShoppingAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShoppingAPI.DTOs.Brand;
using ShoppingAPI.DTOs.Product;
using ShoppingAPI.DTOs.SuperMarketProductBrand;

namespace ShoppingAPI.DTOs.ProductBrand
{
	public class ProductBrandDTO
    {
        public Guid ProductBrandId { get; set; }

        public virtual InfoProductDTO? Product { get; set; }

        public virtual InfoBrandDTO? Brand { get; set; }

        public ICollection<InfoSuperMarketProductBrandDTO>? SuperMarketProductBrands { get; set; }

    }
}

