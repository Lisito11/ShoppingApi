using System;
using AutoMapper;
using ShoppingAPI.Contracts;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.DTOs.SuperMarket;
using ShoppingAPI.Helpers;
using ShoppingAPI.Models;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Services
{
	public class SuperMarketService : ISuperMarketService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public SuperMarketService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ResponseBase<SuperMarketDTO>> Create(SuperMarketCreationDTO entity)
        {
            ResponseBase<SuperMarketDTO> response = new ResponseBase<SuperMarketDTO>();

            try
            {
                var entityMapped = _mapper.Map<SuperMarket>(entity);
                entityMapped.Status = true;
                await _repository.SuperMarket.Create(entityMapped);
                await _repository.Save();
                var entityCreated = _mapper.Map<SuperMarketDTO>(entityMapped);
                response = new ResponseBase<SuperMarketDTO>(entityCreated);
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
                var superMarket = await _repository.SuperMarket.FindByCondition(p => p.SuperMarketId.Equals(id)).FirstOrDefaultAsync();

                if (superMarket is null)
                {
                    response.Succeeded = false;
                    return response;
                }
                superMarket.Status = false;
                _repository.SuperMarket.Update(superMarket);
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

        public async Task<PaginationResponse<List<SuperMarketDTO>>> GetAll(PaginationFilter filter)
        {

            try
            {

                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                var pagedData = await _repository.SuperMarket.FindAll().Where(e => e.Status == true)
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .Include(p=> p.SuperMarketProductBrands!.Where(s => s.Status == true))
                    .ToListAsync();

                var totalRecords = _repository.SuperMarket.FindAll().Where(e => e.Status == true).Count();

                List<SuperMarketDTO> superMarkets = _mapper.Map<IEnumerable<SuperMarketDTO>>(pagedData).ToList();

                return PaginationResponse<SuperMarketDTO>.CreatePaginationReponse(superMarkets, validFilter, totalRecords);

            }
            catch (Exception ex)
            {
                return new PaginationResponse<List<SuperMarketDTO>>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<ResponseBase<SuperMarketDTO>> GetById(Guid id)
        {

            try
            {
                var superMarket = await _repository.SuperMarket.FindByCondition(p => p.SuperMarketId.Equals(id) && p.Status == true).FirstOrDefaultAsync();
                if (superMarket is null)
                {
                    return new ResponseBase<SuperMarketDTO>() { Succeeded = false, Error = "Not found" };
                }

                var superMarketResult = _mapper.Map<SuperMarketDTO>(superMarket);

                return new ResponseBase<SuperMarketDTO>(superMarketResult);

            }
            catch (Exception ex)
            {
                return new ResponseBase<SuperMarketDTO>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<ResponseBase<bool>> Update(Guid id, SuperMarketCreationDTO superMarket)
        {
            var response = new ResponseBase<bool>();
            try
            {

                var superMarketEntity = await _repository.SuperMarket.FindByCondition(p => p.SuperMarketId.Equals(id) && p.Status == true).FirstOrDefaultAsync();
                if (superMarketEntity is null)
                {
                    response.Succeeded = false;
                    return response;
                }
                var superMarketUpdated = _mapper.Map(superMarket, superMarketEntity!);
                _repository.SuperMarket.Update(superMarketUpdated);
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

