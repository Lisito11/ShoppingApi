using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI.DTOs.ProductBrand
{
	public class ProductBrandCreationDTO
    {
        public Guid? ProductId { get; set; }

        public Guid? BrandId { get; set; }
    }
}

