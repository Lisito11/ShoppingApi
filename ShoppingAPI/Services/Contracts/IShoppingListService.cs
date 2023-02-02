using System;
using ShoppingAPI.DTOs.ShoppingList;
using ShoppingAPI.Helpers;

namespace ShoppingAPI.Services.Contracts
{
	public interface IShoppingListService
	{
        Task<PaginationResponse<List<ShoppingListDTO>>> GetAll(PaginationFilter filter);
        Task<ResponseBase<ShoppingListDTO>> GetById(Guid id);
        Task<ResponseBase<ShoppingListDTO>> Create(ShoppingListCreationDTO shoppingList);
        Task<ResponseBase<bool>> Update(Guid id, ShoppingListCreationDTO shoppingList);
        Task<ResponseBase<bool>> Delete(Guid id);
        Task<PaginationResponse<List<ShoppingListDTO>>> GetByUser(PaginationFilter filter, Guid userId);

    }
}

