using System;
using AutoMapper;
using ShoppingAPI.Contracts;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.DTOs.Role;
using ShoppingAPI.Helpers;
using ShoppingAPI.Models;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Services
{
	public class RoleService : IRoleService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public RoleService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ResponseBase<RoleDTO>> Create(RoleCreationDTO entity)
        {
            ResponseBase<RoleDTO> response = new ResponseBase<RoleDTO>();

            try
            {
                var entityMapped = _mapper.Map<Role>(entity);
                entityMapped.Status = true;
                await _repository.Role.Create(entityMapped);
                await _repository.Save();
                var entityCreated = _mapper.Map<RoleDTO>(entityMapped);
                response = new ResponseBase<RoleDTO>(entityCreated);
                return response;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Error = ex.Message;
                return response;
            }
        }

        public async Task<PaginationResponse<List<RoleDTO>>> GetAll(PaginationFilter filter)
        {

            try
            {

                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                var pagedData = await _repository.Role.FindAll().Where(e => e.Status == true)
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();

                var totalRecords = _repository.Role.FindAll().Where(e => e.Status == true).Count();

                List<RoleDTO> roles = _mapper.Map<IEnumerable<RoleDTO>>(pagedData).ToList();

                return PaginationResponse<RoleDTO>.CreatePaginationReponse(roles, validFilter, totalRecords);

            }
            catch (Exception ex)
            {
                return new PaginationResponse<List<RoleDTO>>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<ResponseBase<RoleDTO>> GetById(Guid id)
        {

            try
            {
                var role = await _repository.Role.FindByCondition(p => p.RoleId.Equals(id) && p.Status == true).FirstOrDefaultAsync();

                if (role is null)
                {
                    return new ResponseBase<RoleDTO>() { Succeeded = false, Error = "Not found" };
                }

                var roleResult = _mapper.Map<RoleDTO>(role);

                return new ResponseBase<RoleDTO>(roleResult);

            }
            catch (Exception ex)
            {
                return new ResponseBase<RoleDTO>() { Succeeded = false, Error = ex.Message };
            }

        }

    }
}

