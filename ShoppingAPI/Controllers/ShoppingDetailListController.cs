using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.DTOs.ShoppingDetailList;
using ShoppingAPI.Helpers;
using ShoppingAPI.Services;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ShoppingDetailListController : ControllerBase
    {
        private readonly IShoppingDetailListService _shoppingDetailListService;

        public ShoppingDetailListController(IShoppingDetailListService shoppingDetailListService)
        {
            _shoppingDetailListService = shoppingDetailListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {

            var response = await _shoppingDetailListService.GetAll(filter);
            return response.Succeeded is false ? StatusCode(500, response) : Ok(response);
        }

        [HttpGet("list/{id}")]
        public async Task<IActionResult> GetByShoppingList([FromQuery] PaginationFilter filter, Guid id)
        {
            var response = await _shoppingDetailListService.GetByShoppingList(filter, id);
            return response.Succeeded is false ? StatusCode(500, response) : Ok(response);
        }


        [HttpGet("{id}", Name = "ShoppingDetailListById")]
        public async Task<IActionResult> GetShoppingDetailListById(Guid id)
        {

            var response = await _shoppingDetailListService.GetById(id)!;
            return response.Succeeded is false ? NotFound(response) : Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShoppingDetailListCreationDTO shoppingDetailList)
        {
            ResponseBase<ShoppingDetailListDTO> response = new ResponseBase<ShoppingDetailListDTO>();

            if (shoppingDetailList is null)
            {
                response.Succeeded = false;
                response.Error = "ShoppingDetailList object is null";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _shoppingDetailListService.Create(shoppingDetailList);

            return CreatedAtRoute("ShoppingDetailListById", new { id = response.Data!.ShoppingDetailListId }, response);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ShoppingDetailListCreationDTO shoppingDetailList)
        {
            ResponseBase<bool> response = new ResponseBase<bool>();
            if (shoppingDetailList is null)
            {
                response.Succeeded = false;
                response.Error = "ShoppingDetailList object is null";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _shoppingDetailListService.Update(id, shoppingDetailList);

            return response.Succeeded == false ? NotFound(response) : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            ResponseBase<bool> response = await _shoppingDetailListService.Delete(id);

            return response.Succeeded == false ? NotFound(response) : NoContent();

        }

    }
}

