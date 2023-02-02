using System;

namespace ShoppingAPI.DTOs.User
{
	public class UserCreationDTO
	{
        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public Guid? RoleId { get; set; }
    }
}

