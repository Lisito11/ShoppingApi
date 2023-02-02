using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Contracts;
using ShoppingAPI.DTOs.Brand;
using ShoppingAPI.Helpers;
using ShoppingAPI.Models;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Services
{
	public class BrandService : IBrandService
	{
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public BrandService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<BrandDTO>> Create(BrandCreationDTO brand)
        {
            ResponseBase<BrandDTO> response = new ResponseBase<BrandDTO>(); 

            try
            {
                var brandMapped = _mapper.Map<Brand>(brand);
                brandMapped.Status = true;
                await _repository.Brand.Create(brandMapped);
                await _repository.Save();
                var brandCreated = _mapper.Map<BrandDTO>(brandMapped);
                response = new ResponseBase<BrandDTO>(brandCreated);
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
                var brand = await _repository.Brand.FindByCondition(p => p.BrandId.Equals(id)).FirstOrDefaultAsync();
                var productBrands = await _repository.ProductBrand.FindByCondition(p => p.BrandId.Equals(id)).ToListAsync();

                if (brand is null)
                {
                    response.Succeeded = false;
                    return response;
                }
                brand.Status = false;
                _repository.Brand.Update(brand);
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

        public async Task<PaginationResponse<List<BrandDTO>>> GetAll(PaginationFilter filter)
        {

            try
            {

                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                var pagedData = await _repository.Brand.FindAll().Where(e => e.Status == true)
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();

                var totalRecords = _repository.Brand.FindAll().Where(e => e.Status == true).Count();

                List<BrandDTO> brands = _mapper.Map<IEnumerable<BrandDTO>>(pagedData).ToList();

                return PaginationResponse<BrandDTO>.CreatePaginationReponse((List<BrandDTO>)brands, validFilter, totalRecords);

            }
            catch (Exception ex)
            {
                return new PaginationResponse<List<BrandDTO>>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<ResponseBase<BrandDTO>> GetById(Guid id)
        {

            try
            {
                var brand = await _repository.Brand.FindByCondition(p => p.BrandId.Equals(id) && p.Status == true)
                    .Include(p => p.ProductBrands)
                    .ThenInclude(p => p.Product)
                    .FirstOrDefaultAsync();

                if (brand is null)
                {
                    return new ResponseBase<BrandDTO>() { Succeeded = false, Error = "Not found" };
                }

                var brandResult = _mapper.Map<BrandDTO>(brand);

                return new ResponseBase<BrandDTO>(brandResult);

            }
            catch (Exception ex)
            {
                return new ResponseBase<BrandDTO>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<ResponseBase<bool>> Update(Guid id, BrandCreationDTO brand)
        {
            var response = new ResponseBase<bool>();
            try
            {

                var brandEntity = await _repository.Brand.FindByCondition(p => p.BrandId.Equals(id)).FirstOrDefaultAsync();
                if (brandEntity is null)
                {
                    response.Succeeded = false;
                    return response;
                }
                var brandUpdated = _mapper.Map(brand, brandEntity!);
                _repository.Brand.Update(brandUpdated);
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

