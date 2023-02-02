using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Contracts;
using ShoppingAPI.DTOs.User;
using ShoppingAPI.Helpers;
using ShoppingAPI.Models;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Services
{
	public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserService(IRepositoryWrapper repository, IMapper mapper, ITokenService tokenService)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenService = tokenService;

        }


        public async Task<ResponseBase<UserDTO>> Create(UserCreationDTO entity)
        {
            ResponseBase<UserDTO> response = new ResponseBase<UserDTO>();

            try
            {
                var entityMapped = _mapper.Map<User>(entity);
                entityMapped.Status = true;
                entityMapped.Password = BCrypt.Net.BCrypt.HashPassword(entityMapped.Password);
                await _repository.User.Create(entityMapped);
                await _repository.Save();
                var entityCreated = _mapper.Map<UserDTO>(entityMapped);
                response = new ResponseBase<UserDTO>(entityCreated);
                return response;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Error = ex.Message;
                return response;
            }
        }

        public async Task<PaginationResponse<List<UserDTO>>> GetAll(PaginationFilter filter)
        {

            try
            {

                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                var pagedData = await _repository.User.FindAll().Where(e => e.Status == true)
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();

                var totalRecords = _repository.User.FindAll().Where(e => e.Status == true).Count();

                List<UserDTO> users = _mapper.Map<IEnumerable<UserDTO>>(pagedData).ToList();

                return PaginationResponse<UserDTO>.CreatePaginationReponse(users, validFilter, totalRecords);

            }
            catch (Exception ex)
            {
                return new PaginationResponse<List<UserDTO>>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<ResponseBase<UserDTO>> GetById(Guid id)
        {

            try
            {
                var user = await _repository.User.FindByCondition(p => p.UserId.Equals(id) && p.Status == true).FirstOrDefaultAsync();

                if (user is null)
                {
                    return new ResponseBase<UserDTO>() { Succeeded = false, Error = "Not found" };
                }

                var userResult = _mapper.Map<UserDTO>(user);

                return new ResponseBase<UserDTO>(userResult);

            }
            catch (Exception ex)
            {
                return new ResponseBase<UserDTO>() { Succeeded = false, Error = ex.Message };
            }

        }

        public async Task<ResponseBase<UserAuthDTO>> Login(UserLoginDTO userAuth)
        {

            try
            {
                var user = await _repository.User.FindByCondition(u => u.Email!.Equals(userAuth.Email) && u.Status == true).Include(x=> x.Role).FirstOrDefaultAsync();

                if (user is null)
                {
                    return new ResponseBase<UserAuthDTO>() { Succeeded = false, Error = "Not found", StatusCode = 404 };
                }

                bool verified = BCrypt.Net.BCrypt.Verify(userAuth.Password, user.Password);

                if (!verified) {

                    return new ResponseBase<UserAuthDTO>() { Succeeded = false, Error = "Password not correct", StatusCode = 400};
                }
                var token = _tokenService.GenerateJwt(user);

                var userResult = new UserAuthDTO() { Token = token, UserId = user.UserId, RoleId = user.RoleId, RoleName = user.Role!.Name, UserName = $"{user.Name} {user.LastName}" };

                return new ResponseBase<UserAuthDTO>(userResult);

            }
            catch (Exception ex)
            {
                return new ResponseBase<UserAuthDTO>() { Succeeded = false, Error = ex.Message, StatusCode = 500 };
            }
        }

    }
}

