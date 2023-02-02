using System;
using ShoppingAPI.DTOs.Role;
using ShoppingAPI.DTOs.ShoppingList;
using ShoppingAPI.Models;

namespace ShoppingAPI.DTOs.User
{
	public class UserDTO
	{
        public Guid UserId { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public Guid? RoleId { get; set; }

        public virtual InfoRoleDTO? Role { get; set; }

        public ICollection<InfoShoppingListDTO>? ShoppingLists { get; set; }
    }
}

