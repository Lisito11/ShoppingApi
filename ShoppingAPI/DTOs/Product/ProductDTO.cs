using System;
using ShoppingAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShoppingAPI.DTOs.ProductBrand;

namespace ShoppingAPI.DTOs.Product
{
	public class ProductDTO : ProductCreationDTO
	{
        
        public Guid ProductId { get; set; }

        public ICollection<ProductBrandDTO>? ProductBrands { get; set; }

    }
}

