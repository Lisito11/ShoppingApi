using System;
using ShoppingAPI.DTOs.ShoppingDetailList;
using ShoppingAPI.Helpers;

namespace ShoppingAPI.Services.Contracts
{
	public interface IShoppingDetailListService 
    {
        Task<PaginationResponse<List<ShoppingDetailListDTO>>> GetAll(PaginationFilter filter);
        Task<ResponseBase<ShoppingDetailListDTO>> GetById(Guid id);
        Task<ResponseBase<ShoppingDetailListDTO>> Create(ShoppingDetailListCreationDTO shoppingDetailList);
        Task<ResponseBase<bool>> Update(Guid id, ShoppingDetailListCreationDTO shoppingDetailList);
        Task<ResponseBase<bool>> Delete(Guid id);
        Task<PaginationResponse<List<ShoppingDetailListDTO>>> GetByShoppingList(PaginationFilter filter, Guid shoppingListId);

    }
}

