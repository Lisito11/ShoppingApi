using System;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.DTOs.User;
using ShoppingAPI.Helpers;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {

            var response = await _userService.GetAll(filter);
            return response.Succeeded is false ? StatusCode(500, response) : Ok(response);
        }


        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {

            var response = await _userService.GetById(id)!;
            return response.Succeeded is false ? NotFound(response) : Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreationDTO user)
        {
            ResponseBase<UserDTO> response = new ResponseBase<UserDTO>();

            if (user is null)
            {
                response.Succeeded = false;
                response.Error = "User object is null";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _userService.Create(user);

            return CreatedAtRoute("UserById", new { id = response.Data!.UserId }, response);

        }
    }
}

