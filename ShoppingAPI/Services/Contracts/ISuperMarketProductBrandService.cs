using System;
using ShoppingAPI.DTOs.SuperMarketProductBrand;
using ShoppingAPI.Helpers;

namespace ShoppingAPI.Services.Contracts
{
	public interface ISuperMarketProductBrandService
	{
        Task<PaginationResponse<List<SuperMarketProductBrandDTO>>> GetAll(PaginationFilter filter);
        Task<ResponseBase<SuperMarketProductBrandDTO>> GetById(Guid id);
        Task<ResponseBase<SuperMarketProductBrandDTO>> Create(SuperMarketProductBrandCreationDTO superMarketProductBrand);
        Task<ResponseBase<bool>> Update(Guid id, SuperMarketProductBrandCreationDTO superMarketProductBrand);
        Task<ResponseBase<bool>> Delete(Guid id);

        Task<ResponseBase<List<SuperMarketProductBrandDTO>>> GetBySupermarket(PaginationFilter filter, Guid supermarketId);

    }
}

