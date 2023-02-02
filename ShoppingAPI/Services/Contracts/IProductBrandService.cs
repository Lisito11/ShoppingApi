using System;
using ShoppingAPI.DTOs.ProductBrand;
using ShoppingAPI.Helpers;

namespace ShoppingAPI.Services.Contracts
{
    public interface IProductBrandService
    {
        Task<PaginationResponse<List<ProductBrandDTO>>> GetAll(PaginationFilter filter);
        Task<ResponseBase<ProductBrandDTO>> GetById(Guid id);
        Task<ResponseBase<ProductBrandDTO>> Create(ProductBrandCreationDTO productBrand);
        Task<ResponseBase<bool>> Update(Guid id, ProductBrandCreationDTO productBrand);
        Task<ResponseBase<bool>> Delete(Guid id);
    }
}

