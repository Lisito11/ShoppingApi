
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Models
{
    [Table("SuperMarket")]
    public class SuperMarket
	{
        [Key]
        public Guid SuperMarketId { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Name can't be longer than 250 characters")]
        public string? Name { get; set; }

        [StringLength(450, ErrorMessage = "Description can't be longer than 450 characters")]
        public string? Description { get; set; }

        [StringLength(300, ErrorMessage = "Description can't be longer than 300 characters")]
        public string? Slogan { get; set; }

        [Required]
        public bool Status { get; set; }

        public ICollection<ShoppingList>? ShoppingLists { get; set; }

        public ICollection<SuperMarketProductBrand>? SuperMarketProductBrands { get; set; }

    }
}

