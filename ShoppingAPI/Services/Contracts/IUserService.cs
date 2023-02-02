using System;
using ShoppingAPI.DTOs.User;
using ShoppingAPI.Helpers;

namespace ShoppingAPI.Services.Contracts
{
	public interface IUserService
	{
        Task<PaginationResponse<List<UserDTO>>> GetAll(PaginationFilter filter);
        Task<ResponseBase<UserDTO>> GetById(Guid id);
        Task<ResponseBase<UserDTO>> Create(UserCreationDTO User);

        Task<ResponseBase<UserAuthDTO>> Login(UserLoginDTO User);
    }
}

