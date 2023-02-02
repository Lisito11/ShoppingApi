using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Models
{
    [Table("ShoppingList")]
    public class ShoppingList
	{
        [Key]
        public Guid ShoppingListId { get; set; }

        [Required]
        public Guid? SuperMarketId { get; set; }

        [Required]
        public Guid? UserId { get; set; }

        [Required]
        public DateTime? created { get; set; }

        [Required]
        public bool Status { get; set; }

        public virtual SuperMarket? SuperMarket { get; set; }

        public virtual User? User { get; set; }

        public ICollection<ShoppingDetailList>? ShoppingDetailLists { get; set; }
    }
}

