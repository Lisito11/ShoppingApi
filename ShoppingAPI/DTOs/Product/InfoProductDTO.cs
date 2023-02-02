using System;
namespace ShoppingAPI.DTOs.Product
{
	public class InfoProductDTO
	{
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
}

