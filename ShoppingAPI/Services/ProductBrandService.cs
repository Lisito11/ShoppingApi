using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Contracts;
using ShoppingAPI.DTOs.ProductBrand;
using ShoppingAPI.Helpers;
using ShoppingAPI.Models;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Services
{
	public class ProductBrandService : IProductBrandService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ProductBrandService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ResponseBase<ProductBrandDTO>> Create(ProductBrandCreationDTO entity)
        {
            ResponseBase<ProductBrandDTO> response = new ResponseBase<ProductBrandDTO>();

            try
            {
                var entityMapped = _mapper.Map<ProductBrand>(entity);
                entityMapped.Status = true;
                await _repository.ProductBrand.Create(entityMapped);
                await _repository.Save();
                var entityCreated = _mapper.Map<ProductBrandDTO>(entityMapped);
                response = new ResponseBase<ProductBrandDTO>(entityCreated);
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
                var productBrand = await _repository.ProductBrand.FindByCondition(p => p.ProductBrandId.Equals(id)).FirstOrDefaultAsync();

                if (productBrand is null)
                {
                    response.Succeeded = false;
                    return response;
                }
                productBrand.Status = false;
                _repository.ProductBrand.Update(productBrand);
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

        public async Task<PaginationResponse<List<ProductBrandDTO>>> GetAll(PaginationFilter filter)
        {

            try
            {

                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                var pagedData = await _repository.ProductBrand.FindAll().Where(e => e.Status == true)
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .Include(x=> x.Brand)
                    .Include(x=> x.Product)
                    .ToListAsync();

                

                var totalRecords = _repository.ProductBrand.FindAll().Where(e => e.Status == true).Count();

                List<ProductBrandDTO> productBrands = _mapper.Map<IEnumerable<ProductBrandDTO>>(pagedData).ToList();

                return PaginationResponse<ProductBrandDTO>.CreatePaginationReponse(productBrands, validFilter, totalRecords);

            }
            catch (Exception ex)
            {
                return new PaginationResponse<List<ProductBrandDTO>>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<ResponseBase<ProductBrandDTO>> GetById(Guid id)
        {

            try
            {
                var productBrand = await _repository.ProductBrand.FindByCondition(p => p.ProductBrandId.Equals(id) && p.Status == true)
                    .Include(x => x.Brand)
                    .Include(x => x.Product)
                    .FirstOrDefaultAsync();

                if (productBrand is null)
                {
                    return new ResponseBase<ProductBrandDTO>() { Succeeded = false, Error = "Not found" };
                }

                var productBrandResult = _mapper.Map<ProductBrandDTO>(productBrand);

                return new ResponseBase<ProductBrandDTO>(productBrandResult);

            }
            catch (Exception ex)
            {
                return new ResponseBase<ProductBrandDTO>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<ResponseBase<bool>> Update(Guid id, ProductBrandCreationDTO productBrand)
        {
            var response = new ResponseBase<bool>();
            try
            {

                var productBrandEntity = await _repository.ProductBrand.FindByCondition(p => p.ProductBrandId.Equals(id)).FirstOrDefaultAsync();
                if (productBrandEntity is null)
                {
                    response.Succeeded = false;
                    return response;
                }
                var productBrandUpdated = _mapper.Map(productBrand, productBrandEntity!);
                _repository.ProductBrand.Update(productBrandUpdated);
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

