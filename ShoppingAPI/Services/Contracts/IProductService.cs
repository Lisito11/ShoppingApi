using System;
using System.Net.Sockets;
using ShoppingAPI.DTOs.Product;
using ShoppingAPI.Helpers;
using ShoppingAPI.Models;

namespace ShoppingAPI.Services.Contracts
{
	public interface IProductService
	{
        Task<PaginationResponse<List<ProductDTO>>> GetAll(PaginationFilter filter);
        Task<ResponseBase<ProductDTO>> GetById(Guid id);
        Task<ResponseBase<ProductDTO>> Create(ProductCreationDTO product);
        Task<ResponseBase<bool>> Update(Guid id, ProductCreationDTO product);
        Task<ResponseBase<bool>> Delete(Guid id);
    }
}

