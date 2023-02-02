using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Models
{
    [Table("ShoppingDetailList")]
    public class ShoppingDetailList
	{
        [Key]
        public Guid ShoppingDetailListId { get; set; }

        public Guid? ShoppingListId { get; set; }

        public Guid? SuperMarketProductBrandId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Brand { get; set; }

        [Required]
        public int? Quantity { get; set; }

        [Required]
        public double? Price { get; set; }

        [Required]
        public bool Status { get; set; }

        public virtual SuperMarketProductBrand? SuperMarketProductBrand { get; set; }

        public virtual ShoppingList? ShoppingList { get; set; }

    }
}

