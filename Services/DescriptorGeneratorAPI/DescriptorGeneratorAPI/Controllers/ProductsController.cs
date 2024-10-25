using DescriptorGeneratorAPI.Models;
using DescriptorGeneratorAPI.Services;
using DescriptorGeneratorAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Models;
using System;

namespace DescriptorGeneratorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productService;

        public ProductsController(IProductsService productService)
        {
            _productService = productService;
        }

        [SwaggerOperation(OperationId = "GetAllProducts")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {

            var products = await _productService.GetAllProductsAsync();

            if (!products.Any())
            {
                return NotFound("No products found. Check if the database was seeded.");
            }

            return Ok(products);
        }

        [SwaggerOperation(OperationId = "AddProduct")]
        [Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromForm] MultipartFormData<ProductRepresentation> productRepresentation)
        {
            if (productRepresentation.Json is null)
            {
                return BadRequest("Invalid product data.");
            }

            if (productRepresentation.File is null || productRepresentation.File.Length == 0)
            {
                return BadRequest("Invalid file or there is not file.");
            }

            var product = await _productService.AddProductAsync(productRepresentation.Json,productRepresentation.File);
            return CreatedAtAction(nameof(GetAllProducts), new { id = product.Id }, product);
        }

        [SwaggerOperation(OperationId = "UpdateProducts")]
        [HttpPut]
        public async Task<IActionResult> UpdateProducts([FromBody] IEnumerable<ProductRepresentation> products)
        {
            if (products == null || !products.Any())
            {
                return BadRequest("Product list cannot be null or empty.");
            }

            try
            {
                var updatedProducts = await _productService.UpdateProductsAsync(products);
                return Ok(updatedProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
