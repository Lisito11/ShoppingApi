using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Models
{
    [Table("ProductBrand")]
    public class ProductBrand
	{
        [Key]
        public Guid ProductBrandId { get; set; }

        [Required]
        public Guid? ProductId { get; set; }

        [Required]
        public Guid? BrandId { get; set; }

        [Required]
        public bool Status { get; set; }

        public virtual Product? Product { get; set; }

        public virtual Brand? Brand { get; set; }

        public ICollection<SuperMarketProductBrand>? SuperMarketProductBrands { get; set; }
    }
}

