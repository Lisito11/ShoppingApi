using System;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.DTOs.SuperMarketProductBrand;
using ShoppingAPI.Helpers;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SuperMarketProductBrandController : ControllerBase
    {
        private readonly ISuperMarketProductBrandService _superMarketProductBrandService;

        public SuperMarketProductBrandController(ISuperMarketProductBrandService superMarketProductBrandService)
        {
            _superMarketProductBrandService = superMarketProductBrandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {

            var response = await _superMarketProductBrandService.GetAll(filter);
            return response.Succeeded is false ? StatusCode(500, response) : Ok(response);
        }

        [HttpGet("supermarket/{id}")]
        public async Task<IActionResult> GetBySupermarket([FromQuery] PaginationFilter filter, Guid id)
        {

            var response = await _superMarketProductBrandService.GetBySupermarket(filter, id);
            return response.Succeeded is false ? StatusCode(500, response) : Ok(response);
        }


        [HttpGet("{id}", Name = "SuperMarketProductBrandById")]
        public async Task<IActionResult> GetSuperMarketProductBrandById(Guid id)
        {

            var response = await _superMarketProductBrandService.GetById(id)!;
            return response.Succeeded is false ? NotFound(response) : Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SuperMarketProductBrandCreationDTO superMarketProductBrand)
        {
            ResponseBase<SuperMarketProductBrandDTO> response = new ResponseBase<SuperMarketProductBrandDTO>();

            if (superMarketProductBrand is null)
            {
                response.Succeeded = false;
                response.Error = "SuperMarketProductBrand object is null";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _superMarketProductBrandService.Create(superMarketProductBrand);

            return CreatedAtRoute("SuperMarketProductBrandById", new { id = response.Data!.SuperMarketProductBrandId }, response);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SuperMarketProductBrandCreationDTO superMarketProductBrand)
        {
            ResponseBase<bool> response = new ResponseBase<bool>();
            if (superMarketProductBrand is null)
            {
                response.Succeeded = false;
                response.Error = "SuperMarketProductBrand object is null";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _superMarketProductBrandService.Update(id, superMarketProductBrand);

            return response.Succeeded == false ? NotFound(response) : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            ResponseBase<bool> response = await _superMarketProductBrandService.Delete(id);

            return response.Succeeded == false ? NotFound(response) : NoContent();

        }

    }
}

