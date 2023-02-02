using System;
using ShoppingAPI.DTOs.SuperMarket;
using ShoppingAPI.Helpers;

namespace ShoppingAPI.Services.Contracts
{
	public interface ISuperMarketService
	{
        Task<PaginationResponse<List<SuperMarketDTO>>> GetAll(PaginationFilter filter);
        Task<ResponseBase<SuperMarketDTO>> GetById(Guid id);
        Task<ResponseBase<SuperMarketDTO>> Create(SuperMarketCreationDTO superMarket);
        Task<ResponseBase<bool>> Update(Guid id, SuperMarketCreationDTO superMarket);
        Task<ResponseBase<bool>> Delete(Guid id);
    }
}

