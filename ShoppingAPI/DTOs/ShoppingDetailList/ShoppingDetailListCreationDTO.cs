using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI.DTOs.ShoppingDetailList
{
	public class ShoppingDetailListCreationDTO
	{
        public Guid? ShoppingListId { get; set; }

        public Guid? SuperMarketProductBrandId { get; set; }

        public string? Name { get; set; }

        public string? Brand { get; set; }

        public int? Quantity { get; set; }

        public double? Price { get; set; }
    }
}

