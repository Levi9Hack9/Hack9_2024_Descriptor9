using DescriptorGeneratorAPI.Models;

namespace DescriptorGeneratorAPI.Services.Interfaces
{
    public interface IDescriptorService
    {
        Task<ProductRepresentation> GenerateDescriptionAsync(ProductRepresentation product, IFormFile image);
        Task<IEnumerable<ProductRepresentation>> UpdateBulkDescriptionsAsync(IFormFile csvFile);
    }
}