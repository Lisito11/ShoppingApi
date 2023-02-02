using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Contracts;
using ShoppingAPI.DTOs.Product;
using ShoppingAPI.Helpers;
using ShoppingAPI.Models;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Services
{
	public class ProductService : IProductService
	{
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ResponseBase<ProductDTO>> Create(ProductCreationDTO entity)
        {
            ResponseBase<ProductDTO> response = new ResponseBase<ProductDTO>();

            try
            {
                var entityMapped = _mapper.Map<Product>(entity);
                entityMapped.Status = true;
                await _repository.Product.Create(entityMapped);
                await _repository.Save();
                var entityCreated = _mapper.Map<ProductDTO>(entityMapped);
                response = new ResponseBase<ProductDTO>(entityCreated);
                return response;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Error = ex.Message;
                return response;
            }
        }

        public async Task<ResponseBase<bool>> Delete(Guid id)
        {
            var response = new ResponseBase<bool>();
            try
            {
                var product = await _repository.Product.FindByCondition(p => p.ProductId.Equals(id)).FirstOrDefaultAsync();
                var productBrands = await _repository.ProductBrand.FindByCondition(p => p.ProductId.Equals(id)).ToListAsync();

                if (product is null)
                {
                    response.Succeeded = false;
                    return response;
                }
                product.Status = false;
                _repository.Product.Update(product);
                productBrands.ForEach(pb => {
                    pb!.Status = false;
                    _repository.ProductBrand.Update(pb!);
                });
                await _repository.Save();
                return response;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Error = ex.Message;
                return response;
            }
        }

        public async Task<PaginationResponse<List<ProductDTO>>> GetAll(PaginationFilter filter) {

            try
            {

                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                var pagedData = await _repository.Product.FindAll().Where(e => e.Status == true)
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();

                var totalRecords = _repository.Product.FindAll().Where(e => e.Status == true).Count();

                List<ProductDTO> products = _mapper.Map<IEnumerable<ProductDTO>>(pagedData).ToList();

                return PaginationResponse<ProductDTO>.CreatePaginationReponse(products, validFilter, totalRecords);

            }
            catch(Exception ex)
            {
                return new PaginationResponse<List<ProductDTO>>() { Succeeded = false, Error = ex.Message};
            }

        }

        public async Task<ResponseBase<ProductDTO>> GetById(Guid id) {

            try
            {
                var product = await _repository.Product.FindByCondition(p => p.ProductId.Equals(id) && p.Status == true).FirstOrDefaultAsync();

                if (product is null)
                {
                    return new ResponseBase<ProductDTO>() { Succeeded = false, Error = "Not found" };
                }

                var productResult = _mapper.Map<ProductDTO>(product);

                return new ResponseBase<ProductDTO>(productResult);
                
            }
            catch(Exception ex)
            {
                return new ResponseBase<ProductDTO>() { Succeeded = false, Error = ex.Message};
            }

        }

        public async Task<ResponseBase<bool>> Update(Guid id, ProductCreationDTO product)
        {
            var response = new ResponseBase<bool>();
            try
            {
                
                var productEntity = await _repository.Product.FindByCondition(p => p.ProductId.Equals(id)).FirstOrDefaultAsync();
                if (productEntity is null)
                {
                    response.Succeeded = false;
                    return response;
                }
                var productUpdated = _mapper.Map(product, productEntity!);
                _repository.Product.Update(productUpdated);
                await _repository.Save();
                return response;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Error = ex.Message;
                return response;
            }

        }

    }
}

