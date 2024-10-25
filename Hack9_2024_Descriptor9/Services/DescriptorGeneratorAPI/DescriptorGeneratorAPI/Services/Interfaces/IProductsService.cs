using DescriptorGeneratorAPI.Models;

namespace DescriptorGeneratorAPI.Services.Interfaces
{
    public interface IProductsService
    {
        Task<Product> AddProductAsync(ProductRepresentation productRepresentation, IFormFile image);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<ProductRepresentation>> UpdateProductsAsync(IEnumerable<ProductRepresentation> products);
    }
}