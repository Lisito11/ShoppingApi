using System;
using ShoppingAPI.DTOs.User;
using ShoppingAPI.Models;

namespace ShoppingAPI.Services.Contracts
{
	public interface ITokenService
	{
        public string GenerateJwt(User user);
    }
}

