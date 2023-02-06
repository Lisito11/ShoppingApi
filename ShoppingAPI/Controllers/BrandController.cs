using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.DTOs.Brand;
using ShoppingAPI.Helpers;
using ShoppingAPI.Models;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class BrandController : ControllerBase
	{
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {

            var response = await _brandService.GetAll(filter);
            return response.Succeeded is false ? StatusCode(500, response) : Ok(response);
        }


        [HttpGet("{id}", Name = "BrandById")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var response = await _brandService.GetById(id)!;
            return response.Succeeded is false ? NotFound(response) : Ok(response);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BrandCreationDTO brand)
        {
            ResponseBase<BrandDTO> response = new ResponseBase<BrandDTO>();

            if (brand is null)
            {
                response.Succeeded = false;
                response.Error = "Object is null";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _brandService.Create(brand);

            return CreatedAtRoute("BrandById", new { id = response.Data!.BrandId }, response);

        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BrandCreationDTO brand)
        {
            ResponseBase<bool> response = new ResponseBase<bool>();
            if (brand is null)
            {
                response.Succeeded = false;
                response.Error = "Object is null";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _brandService.Update(id, brand);

            return response.Succeeded == false ? NotFound(response) : NoContent();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            ResponseBase<bool> response = await _brandService.Delete(id);

            return response.Succeeded == false ? NotFound(response) : NoContent();

        }
    }
}

