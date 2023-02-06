using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.DTOs.ProductBrand;
using ShoppingAPI.Helpers;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductBrandController : ControllerBase
    {
        private readonly IProductBrandService _productBrandService;

        public ProductBrandController(IProductBrandService productBrandService)
        {
            _productBrandService = productBrandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {

            var response = await _productBrandService.GetAll(filter);
            return response.Succeeded is false ? StatusCode(500, response) : Ok(response);
        }


        [HttpGet("{id}", Name = "ProductBrandById")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var response = await _productBrandService.GetById(id)!;
            return response.Succeeded is false ? NotFound(response) : Ok(response);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductBrandCreationDTO productBrand)
        {
            ResponseBase<ProductBrandDTO> response = new ResponseBase<ProductBrandDTO>();

            if (productBrand is null)
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

            response = await _productBrandService.Create(productBrand);

            return CreatedAtRoute("ProductBrandById", new { id = response.Data!.ProductBrandId }, response);

        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductBrandCreationDTO productBrand)
        {
            ResponseBase<bool> response = new ResponseBase<bool>();
            if (productBrand is null)
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

            response = await _productBrandService.Update(id, productBrand);

            return response.Succeeded == false ? NotFound(response) : NoContent();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            ResponseBase<bool> response = await _productBrandService.Delete(id);

            return response.Succeeded == false ? NotFound(response) : NoContent();

        }
    }
}

