using System;
using ShoppingAPI.DTOs.Brand;
using ShoppingAPI.Helpers;

namespace ShoppingAPI.Services.Contracts
{
	public interface IBrandService
	{
        Task<PaginationResponse<List<DTOs.Brand.BrandDTO>>> GetAll(PaginationFilter filter);
        Task<ResponseBase<DTOs.Brand.BrandDTO>> GetById(Guid id);
        Task<ResponseBase<DTOs.Brand.BrandDTO>> Create(BrandCreationDTO brand);
        Task<ResponseBase<bool>> Update(Guid id, BrandCreationDTO brand);
        Task<ResponseBase<bool>> Delete(Guid id);
    }
}

