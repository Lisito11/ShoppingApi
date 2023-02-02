using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Models
{
	[Table("Product")]
	public class Product
	{
		[Key]
		public Guid ProductId { get; set; }

		[Required]
		[StringLength(200, ErrorMessage = "Name can't be longer than 200 characters")]
		public string? Name { get; set; }

        [StringLength(450, ErrorMessage = "Description can't be longer than 450 characters")]
        public string? Description { get; set; }

        [Required]
        public bool Status { get; set; }

        public ICollection<ProductBrand>? ProductBrands { get; set; }

    }
}