using System;
using ShoppingAPI.DTOs.ProductBrand;
using ShoppingAPI.Models;

namespace ShoppingAPI.DTOs.Brand
{
	public class BrandDTO : BrandCreationDTO
	{
        public Guid BrandId { get; set; }

        public ICollection<InfoProductBrandDTO>? ProductBrands { get; set; }

    }
}

