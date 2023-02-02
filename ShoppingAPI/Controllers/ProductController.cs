using System;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Contracts;
using ShoppingAPI.DTOs;
using ShoppingAPI.DTOs.Product;
using ShoppingAPI.Helpers;
using ShoppingAPI.Models;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : ControllerBase
	{
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            
            var response = await _productService.GetAll(filter);
            return response.Succeeded is false ? StatusCode(500, response) : Ok(response);
        }

        [HttpGet("{id}", Name = "ProductById")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            
            var response = await _productService.GetById(id)!;
            return response.Succeeded is false ? NotFound(response) : Ok(response);          
        }

        //[Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreationDTO product)
        {
            ResponseBase<ProductDTO> response = new ResponseBase<ProductDTO>();

            if (product is null)
            {
                response.Succeeded = false;
                response.Error = "Product object is null";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _productService.Create(product);

            return CreatedAtRoute("ProductById", new { id = response.Data!.ProductId }, response);
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductCreationDTO product)
        {
            ResponseBase<bool> response = new ResponseBase<bool>();
            if (product is null)
            {
                response.Succeeded = false;
                response.Error = "Product object is null";
                return BadRequest(response) ;
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Invalid model object";
                return BadRequest(response);
            }

            response = await _productService.Update(id, product);

            return response.Succeeded == false ? NotFound(response) : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            ResponseBase<bool> response = await _productService.Delete(id);

            return response.Succeeded == false ? NotFound(response) : NoContent();

        }

    }
}

