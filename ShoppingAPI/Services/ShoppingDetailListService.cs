using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Contracts;
using ShoppingAPI.DTOs.ShoppingDetailList;
using ShoppingAPI.Helpers;
using ShoppingAPI.Models;
using ShoppingAPI.Services.Contracts;


namespace ShoppingAPI.Services
{
	public class ShoppingDetailListService : IShoppingDetailListService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ShoppingDetailListService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ResponseBase<ShoppingDetailListDTO>> Create(ShoppingDetailListCreationDTO entity)
        {
            ResponseBase<ShoppingDetailListDTO> response = new ResponseBase<ShoppingDetailListDTO>();

            try
            {
                var entityMapped = _mapper.Map<ShoppingDetailList>(entity);
                entityMapped.Status = true;
                await _repository.ShoppingDetailList.Create(entityMapped);
                await _repository.Save();
                var entityCreated = _mapper.Map<ShoppingDetailListDTO>(entityMapped);
                response = new ResponseBase<ShoppingDetailListDTO>(entityCreated);
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
                var shoppingDetailList = await _repository.ShoppingDetailList.FindByCondition(p => p.ShoppingDetailListId.Equals(id)).FirstOrDefaultAsync();

                if (shoppingDetailList is null)
                {
                    response.Succeeded = false;
                    return response;
                }
                shoppingDetailList.Status = false;
                _repository.ShoppingDetailList.Update(shoppingDetailList);
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

        public async Task<PaginationResponse<List<ShoppingDetailListDTO>>> GetAll(PaginationFilter filter)
        {

            try
            {

                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                var pagedData = await _repository.ShoppingDetailList.FindAll().Where(e => e.Status == true)
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();

                var totalRecords = _repository.ShoppingDetailList.FindAll().Where(e => e.Status == true).Count();

                List<ShoppingDetailListDTO> shoppingDetailLists = _mapper.Map<IEnumerable<ShoppingDetailListDTO>>(pagedData).ToList();

                return PaginationResponse<ShoppingDetailListDTO>.CreatePaginationReponse(shoppingDetailLists, validFilter, totalRecords);

            }
            catch (Exception ex)
            {
                return new PaginationResponse<List<ShoppingDetailListDTO>>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<ResponseBase<ShoppingDetailListDTO>> GetById(Guid id)
        {

            try
            {
                var shoppingDetailList = await _repository.ShoppingDetailList.FindByCondition(p => p.ShoppingDetailListId.Equals(id) && p.Status == true).FirstOrDefaultAsync();

                if (shoppingDetailList is null)
                {
                    return new ResponseBase<ShoppingDetailListDTO>() { Succeeded = false, Error = "Not found" };
                }

                var shoppingDetailListResult = _mapper.Map<ShoppingDetailListDTO>(shoppingDetailList);

                return new ResponseBase<ShoppingDetailListDTO>(shoppingDetailListResult);

            }
            catch (Exception ex)
            {
                return new ResponseBase<ShoppingDetailListDTO>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<PaginationResponse<List<ShoppingDetailListDTO>>> GetByShoppingList(PaginationFilter filter, Guid shoppingListId)
        {
            try
            {

                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                var pagedData = await _repository.ShoppingDetailList.FindAll().Where(e => e.Status == true && e.ShoppingListId == shoppingListId)
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();

                var totalRecords = _repository.ShoppingDetailList.FindAll().Where(e => e.Status == true && e.ShoppingListId == shoppingListId).Count();

                List<ShoppingDetailListDTO> shoppingDetailLists = _mapper.Map<IEnumerable<ShoppingDetailListDTO>>(pagedData).ToList();

                return PaginationResponse<ShoppingDetailListDTO>.CreatePaginationReponse(shoppingDetailLists, validFilter, totalRecords);

            }
            catch (Exception ex)
            {
                return new PaginationResponse<List<ShoppingDetailListDTO>>() { Succeeded = false, Error = ex.Message };
            }
        }

        public async Task<ResponseBase<bool>> Update(Guid id, ShoppingDetailListCreationDTO shoppingDetailList)
        {
            var response = new ResponseBase<bool>();
            try
            {

                var shoppingDetailListEntity = await _repository.ShoppingDetailList.FindByCondition(p => p.ShoppingDetailListId.Equals(id)).FirstOrDefaultAsync();
                if (shoppingDetailListEntity is null)
                {
                    response.Succeeded = false;
                    return response;
                }
                var shoppingDetailListUpdated = _mapper.Map(shoppingDetailList, shoppingDetailListEntity!);
                _repository.ShoppingDetailList.Update(shoppingDetailListUpdated);
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

