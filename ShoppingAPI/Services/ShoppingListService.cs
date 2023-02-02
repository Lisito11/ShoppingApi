using System;
using AutoMapper;
using ShoppingAPI.Contracts;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.DTOs.ShoppingList;
using ShoppingAPI.Helpers;
using ShoppingAPI.Models;
using ShoppingAPI.Services.Contracts;
using ShoppingAPI.DTOs.SuperMarketProductBrand;

namespace ShoppingAPI.Services
{
	public class ShoppingListService : IShoppingListService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ShoppingListService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ResponseBase<ShoppingListDTO>> Create(ShoppingListCreationDTO entity)
        {
            ResponseBase<ShoppingListDTO> response = new ResponseBase<ShoppingListDTO>();

            try
            {
                var entityMapped = _mapper.Map<ShoppingList>(entity);
                entityMapped.Status = true;
                await _repository.ShoppingList.Create(entityMapped);
                await _repository.Save();
                var entityCreated = _mapper.Map<ShoppingListDTO>(entityMapped);
                response = new ResponseBase<ShoppingListDTO>(entityCreated);
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
                var shoppingList = await  _repository.ShoppingList.FindByCondition(p => p.ShoppingListId.Equals(id)).FirstOrDefaultAsync();

                if (shoppingList is null)
                {
                    response.Succeeded = false;
                    return response;
                }
                shoppingList.Status = false;
                _repository.ShoppingList.Update(shoppingList);
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

        public async Task<PaginationResponse<List<ShoppingListDTO>>> GetAll(PaginationFilter filter)
        {

            try
            {

                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                var pagedData = await _repository.ShoppingList.FindAll().Where(e => e.Status == true)
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();

                var totalRecords = _repository.ShoppingList.FindAll().Where(e => e.Status == true).Count();

                List<ShoppingListDTO> shoppingLists = _mapper.Map<IEnumerable<ShoppingListDTO>>(pagedData).ToList();

                return PaginationResponse<ShoppingListDTO>.CreatePaginationReponse(shoppingLists, validFilter, totalRecords);

            }
            catch (Exception ex)
            {
                return new PaginationResponse<List<ShoppingListDTO>>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<ResponseBase<ShoppingListDTO>> GetById(Guid id)
        {

            try
            {
                var shoppingList = await _repository.ShoppingList.FindByCondition(p => p.ShoppingListId.Equals(id) && p.Status == true).FirstOrDefaultAsync();

                if (shoppingList is null)
                {
                    return new ResponseBase<ShoppingListDTO>() { Succeeded = false, Error = "Not found" };
                }

                var shoppingListResult = _mapper.Map<ShoppingListDTO>(shoppingList);

                return new ResponseBase<ShoppingListDTO>(shoppingListResult);

            }
            catch (Exception ex)
            {
                return new ResponseBase<ShoppingListDTO>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<ResponseBase<bool>> Update(Guid id, ShoppingListCreationDTO shoppingList)
        {
            var response = new ResponseBase<bool>();
            try
            {

                var shoppingListEntity = await _repository.ShoppingList.FindByCondition(p => p.ShoppingListId.Equals(id)).FirstOrDefaultAsync();
                if (shoppingListEntity is null)
                {
                    response.Succeeded = false;
                    return response;
                }
                var shoppingListUpdated = _mapper.Map(shoppingList, shoppingListEntity!);
                _repository.ShoppingList.Update(shoppingListUpdated);
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

        public async Task<PaginationResponse<List<ShoppingListDTO>>> GetByUser(PaginationFilter filter, Guid userId)
        {
            try
            {

                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                var pagedData = await _repository.ShoppingList.FindAll().Where(e => e.Status == true && e.UserId == userId)
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .Include(x=> x.ShoppingDetailLists!.Where(s=> s.Status == true))
                    .Include(x => x.SuperMarket)
                    .ToListAsync();

                var totalRecords = _repository.ShoppingList.FindAll().Where(e => e.Status == true && e.UserId == userId).Count();

                List<ShoppingListDTO> shoppingLists = _mapper.Map<IEnumerable<ShoppingListDTO>>(pagedData).ToList();

                return PaginationResponse<ShoppingListDTO>.CreatePaginationReponse(shoppingLists, validFilter, totalRecords);

            }
            catch (Exception ex)
            {
                return new PaginationResponse<List<ShoppingListDTO>>() { Succeeded = false, Error = ex.Message };
            }
        }
    }
}

