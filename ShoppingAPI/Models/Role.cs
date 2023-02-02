using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Models
{
    [Table("Role")]
    public class Role
	{
        [Key]
        public Guid RoleId { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public bool? Status { get; set; }

        public ICollection<User>? Users { get; set; }

    }
}

