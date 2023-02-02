using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Models
{
    [Table("SuperMarketProductBrand")]
    public class SuperMarketProductBrand
	{
        [Key]
        public Guid SuperMarketProductBrandId { get; set; }

        [Required]
        public Guid? SuperMarketId { get; set; }

        [Required]
        public Guid? ProductBrandId { get; set; }

        [Required]
        public double? Price { get; set; }

        [Required]
        public bool Status { get; set; }

        public virtual SuperMarket? SuperMarket { get; set; }

        public virtual ProductBrand? ProductBrand { get; set; }

    }
}

