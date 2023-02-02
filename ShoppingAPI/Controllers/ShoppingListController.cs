using System;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.DTOs.ShoppingList;
using ShoppingAPI.Helpers;
using ShoppingAPI.Services;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ShoppingListController : ControllerBase
    {
        private readonly IShoppingListService _shoppingListService;

        public ShoppingListController(IShoppingListService shoppingListService)
        {
            _shoppingListService = shoppingListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {

            var response = await _shoppingListService.GetAll(filter);
            return response.Succeeded is false ? StatusCode(500, response) : Ok(response);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByUser([FromQuery] PaginationFilter filter, Guid id)
        {
            var response = await _shoppingListService.GetByUser(filter, id);
            return response.Succeeded is false ? StatusCode(500, response) : Ok(response);
        }


        [HttpGet("{id}", Name = "ShoppingListById")]
        public async Task<IActionResult> GetShoppingListById(Guid id)
        {

            var response = await _shoppingListService.GetById(id)!;
            return response.Succeeded is false ? NotFound(response) : Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShoppingListCreationDTO shoppingList)
        {
            ResponseBase<ShoppingListDTO> response = new ResponseBase<ShoppingListDTO>();

            if (shoppingList is null)
            {
                response.Succeeded = false;
                response.Error = "ShoppingList object is null";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _shoppingListService.Create(shoppingList);

            return CreatedAtRoute("ShoppingListById", new { id = response.Data!.ShoppingListId }, response);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ShoppingListCreationDTO shoppingList)
        {
            ResponseBase<bool> response = new ResponseBase<bool>();
            if (shoppingList is null)
            {
                response.Succeeded = false;
                response.Error = "ShoppingList object is null";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _shoppingListService.Update(id, shoppingList);

            return response.Succeeded == false ? NotFound(response) : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            ResponseBase<bool> response = await _shoppingListService.Delete(id);

            return response.Succeeded == false ? NotFound(response) : NoContent();

        }

    }
}

