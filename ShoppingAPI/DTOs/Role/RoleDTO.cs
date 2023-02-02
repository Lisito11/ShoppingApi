using System;
using ShoppingAPI.DTOs.User;

namespace ShoppingAPI.DTOs.Role
{
	public class RoleDTO : RoleCreationDTO
	{
        public Guid RoleId { get; set; }

        public string? Description { get; set; }

        public ICollection<InfoUserDTO>? Users { get; set; }
    }
}

