using DescriptorGeneratorAPI.Models;
using DescriptorGeneratorAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Models;

namespace DescriptorGeneratorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DescriptorController : ControllerBase
    {
        private readonly IDescriptorService _descriptorService;

        public DescriptorController(IDescriptorService descriptorService)
        {
            _descriptorService = descriptorService;
        }

        [SwaggerOperation(OperationId = "PostDescription")]
        [Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<ActionResult<ProductRepresentation>> PostDescriptionAsync([FromForm] MultipartFormData<ProductRepresentation> product)
        {
            var description = await _descriptorService.GenerateDescriptionAsync(product.Json, product.File);
            return Ok(description);
        }

        // post bulk description receiving file return ok
        [SwaggerOperation(OperationId = "PostBulkDescription")]
        [Consumes("multipart/form-data")]
        [HttpPost("bulk")]
        public async Task<ActionResult<IEnumerable<ProductRepresentation>>> PostBulkDescriptionAsync(IFormFile csvFile)
        {
            var bulkProducts = await _descriptorService.UpdateBulkDescriptionsAsync(csvFile);
            return Ok(bulkProducts);
        }
    }
}