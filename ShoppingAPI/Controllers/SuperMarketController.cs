using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.DTOs.SuperMarket;
using ShoppingAPI.Helpers;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class SuperMarketController : ControllerBase
    {
        private readonly ISuperMarketService _superMarketService;

        public SuperMarketController(ISuperMarketService superMarketService)
        {
            _superMarketService = superMarketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {

            var response = await _superMarketService.GetAll(filter);
            return response.Succeeded is false ? StatusCode(500, response) : Ok(response);
        }


        [HttpGet("{id}", Name = "SuperMarketById")]
        public async Task<IActionResult> GetSuperMarketById(Guid id)
        {

            var response = await _superMarketService.GetById(id)!;
            return response.Succeeded is false ? NotFound(response) : Ok(response);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SuperMarketCreationDTO superMarket)
        {
            ResponseBase<SuperMarketDTO> response = new ResponseBase<SuperMarketDTO>();

            if (superMarket is null)
            {
                response.Succeeded = false;
                response.Error = "SuperMarket object is null";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _superMarketService.Create(superMarket);

            return CreatedAtRoute("SuperMarketById", new { id = response.Data!.SuperMarketId }, response);

        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SuperMarketCreationDTO superMarket)
        {
            ResponseBase<bool> response = new ResponseBase<bool>();
            if (superMarket is null)
            {
                response.Succeeded = false;
                response.Error = "SuperMarket object is null";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _superMarketService.Update(id, superMarket);

            return response.Succeeded is false ? NotFound(response) : NoContent();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            ResponseBase<bool> response = await _superMarketService.Delete(id);

            return response.Succeeded is false ? NotFound(response) : NoContent();

        }

    }
}

