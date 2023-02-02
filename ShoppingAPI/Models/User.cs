using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI.Models
{
	public class User
	{
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public bool? Status { get; set; }

        [Required]
        public Guid? RoleId { get; set; }

        public virtual Role? Role { get; set; }

        public ICollection<ShoppingList>? ShoppingLists { get; set; }

    }
}

