using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Contracts;
using ShoppingAPI.DTOs.SuperMarketProductBrand;
using ShoppingAPI.Helpers;
using ShoppingAPI.Models;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Services
{
	public class SuperMarketProductBrandService : ISuperMarketProductBrandService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public SuperMarketProductBrandService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<SuperMarketProductBrandDTO>> Create(SuperMarketProductBrandCreationDTO entity)
        {
            ResponseBase<SuperMarketProductBrandDTO> response = new ResponseBase<SuperMarketProductBrandDTO>();

            try
            {
                var entityMapped = _mapper.Map<SuperMarketProductBrand>(entity);
                entityMapped.Status = true;
                await _repository.SuperMarketProductBrand.Create(entityMapped);
                await _repository.Save();
                var entityCreated = _mapper.Map<SuperMarketProductBrandDTO>(entityMapped);
                response = new ResponseBase<SuperMarketProductBrandDTO>(entityCreated);
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
                var superMarketProductBrand = await _repository.SuperMarketProductBrand.FindByCondition(p => p.SuperMarketProductBrandId.Equals(id)).FirstOrDefaultAsync();

                if (superMarketProductBrand is null)
                {
                    response.Succeeded = false;
                    return response;
                }
                superMarketProductBrand.Status = false;
                _repository.SuperMarketProductBrand.Update(superMarketProductBrand);
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

        public async Task<PaginationResponse<List<SuperMarketProductBrandDTO>>> GetAll(PaginationFilter filter)
        {

            try
            {

                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                var pagedData = await _repository.SuperMarketProductBrand.FindAll().Where(e => e.Status == true)
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .Include(s=> s.SuperMarket)
                    .Include(p=> p.ProductBrand)
                    .ThenInclude(p => p!.Product)
                    .Include(p => p.ProductBrand)
                    .ThenInclude(b => b!.Brand)                    
                    .ToListAsync();

                var totalRecords = _repository.SuperMarketProductBrand.FindAll().Where(e => e.Status == true).Count();

                List<SuperMarketProductBrandDTO> superMarketProductBrands = _mapper.Map<IEnumerable<SuperMarketProductBrandDTO>>(pagedData).ToList();

                return PaginationResponse<SuperMarketProductBrandDTO>.CreatePaginationReponse(superMarketProductBrands, validFilter, totalRecords);

            }
            catch (Exception ex)
            {
                return new PaginationResponse<List<SuperMarketProductBrandDTO>>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<ResponseBase<SuperMarketProductBrandDTO>> GetById(Guid id)
        {

            try
            {
                var superMarketProductBrand = await _repository.SuperMarketProductBrand.FindByCondition(p => p.SuperMarketProductBrandId.Equals(id) && p.Status == true).FirstOrDefaultAsync();

                if (superMarketProductBrand is null)
                {
                    return new ResponseBase<SuperMarketProductBrandDTO>() { Succeeded = false, Error = "Not found" };
                }

                var superMarketProductBrandResult = _mapper.Map<SuperMarketProductBrandDTO>(superMarketProductBrand);

                return new ResponseBase<SuperMarketProductBrandDTO>(superMarketProductBrandResult);

            }
            catch (Exception ex)
            {
                return new ResponseBase<SuperMarketProductBrandDTO>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<ResponseBase<bool>> Update(Guid id, SuperMarketProductBrandCreationDTO superMarketProductBrand)
        {
            var response = new ResponseBase<bool>();
            try
            {

                var superMarketProductBrandEntity = _repository.SuperMarketProductBrand.FindByCondition(p => p.SuperMarketProductBrandId.Equals(id)).FirstOrDefault();
                if (superMarketProductBrandEntity is null)
                {
                    response.Succeeded = false;
                    return response;
                }
                var superMarketProductBrandUpdated = _mapper.Map(superMarketProductBrand, superMarketProductBrandEntity!);
                _repository.SuperMarketProductBrand.Update(superMarketProductBrandEntity);
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

        public async Task<ResponseBase<List<SuperMarketProductBrandDTO>>> GetBySupermarket(PaginationFilter filter, Guid supermarketId)
        {
            try
            {

                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                var pagedData = await _repository.SuperMarketProductBrand.FindAll().Where(e => e.Status == true && e.SuperMarketId == supermarketId)
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .Include(s => s.SuperMarket)
                    .Include(p => p.ProductBrand)
                    .ThenInclude(p => p!.Product)
                    .Include(p => p.ProductBrand)
                    .ThenInclude(b => b!.Brand)
                    .ToListAsync();

                var totalRecords = _repository.SuperMarketProductBrand.FindAll().Where(e => e.Status == true && e.SuperMarketId == supermarketId).Count();

                List<SuperMarketProductBrandDTO> superMarketProductBrands = _mapper.Map<IEnumerable<SuperMarketProductBrandDTO>>(pagedData).ToList();

                return PaginationResponse<SuperMarketProductBrandDTO>.CreatePaginationReponse(superMarketProductBrands, validFilter, totalRecords);

            }
            catch (Exception ex)
            {
                return new PaginationResponse<List<SuperMarketProductBrandDTO>>() { Succeeded = false, Error = ex.Message };
            }
        }
    }
}

