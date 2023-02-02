using System;
using ShoppingAPI.DTOs.Role;
using ShoppingAPI.Helpers;

namespace ShoppingAPI.Services.Contracts
{
	public interface IRoleService
	{
        Task<PaginationResponse<List<RoleDTO>>> GetAll(PaginationFilter filter);
        Task<ResponseBase<RoleDTO>> GetById(Guid id);
        Task<ResponseBase<RoleDTO>> Create(RoleCreationDTO Role);
    }
}

