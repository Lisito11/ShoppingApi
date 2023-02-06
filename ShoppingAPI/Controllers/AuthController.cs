using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Services.Contracts;
using System.Security.Claims;
using ShoppingAPI.Services;
using ShoppingAPI.DTOs.User;
using ShoppingAPI.DTOs.Brand;
using ShoppingAPI.Helpers;

namespace ShoppingAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
        {
            ResponseBase<UserAuthDTO> response = new ResponseBase<UserAuthDTO>();

            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _userService.Login(userLogin);

            if (response.StatusCode == 400) {
                return BadRequest(response);
            }

            if (response.StatusCode == 404){
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}

