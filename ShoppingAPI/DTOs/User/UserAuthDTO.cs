using System;
namespace ShoppingAPI.DTOs.User
{
	public class UserAuthDTO
    {
        public Guid UserId { get; set; }

        public Guid? RoleId { get; set; }

        public string? RoleName { get; set; }

        public string? UserName { get; set; }

        public string? Token { get; set; }
    }
}

