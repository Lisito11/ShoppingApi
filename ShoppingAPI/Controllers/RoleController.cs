using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.DTOs.Role;
using ShoppingAPI.Helpers;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {

            var response = await _roleService.GetAll(filter);
            return response.Succeeded is false ? StatusCode(500, response) : Ok(response);
        }


        [HttpGet("{id}", Name = "RoleById")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {

            var response = await _roleService.GetById(id)!;
            return response.Succeeded is false ? NotFound(response) : Ok(response);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleCreationDTO role)
        {
            ResponseBase<RoleDTO> response = new ResponseBase<RoleDTO>();

            if (role is null)
            {
                response.Succeeded = false;
                response.Error = "Role object is null";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _roleService.Create(role);

            return CreatedAtRoute("RoleById", new { id = response.Data!.RoleId }, response);

        }
    }
}

