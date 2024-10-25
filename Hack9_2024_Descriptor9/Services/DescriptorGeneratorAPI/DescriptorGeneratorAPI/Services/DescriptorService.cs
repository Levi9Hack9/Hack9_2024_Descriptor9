
using DescriptorGeneratorAPI.Models;
using DescriptorGeneratorAPI.Services.Interfaces;
using DescriptorGeneratorAPI.Utils;

namespace DescriptorGeneratorAPI.Services
{
    public class DescriptorService : IDescriptorService
    {
        private readonly IGenAIService _genAIService;
        private readonly IProductsService _productsService;

        public DescriptorService(IGenAIService genAIService, IProductsService productsService)
        {
            _genAIService = genAIService;
            _productsService = productsService;
        }
        public async Task<ProductRepresentation> GenerateDescriptionAsync(ProductRepresentation product, IFormFile image)
        {
            product.DescriptionTranslations = await _genAIService.GenerateResponseFromImageAsync(product, image);

            return product;
        }

        public async Task<IEnumerable<ProductRepresentation>> UpdateBulkDescriptionsAsync(IFormFile csvFile)
        {
            var productForUpdate = await FileManipulation.ParseCsvFileAsync(csvFile);
            var allProducts = await _productsService.GetAllProductsAsync();

            var filteredProducts = allProducts.Where(product => productForUpdate
                                        .Any(csvData => csvData.Title == product.Title)).ToList();

            var tasks = filteredProducts.Select(async product =>
            {
                var csvData = productForUpdate.First(data => data.Title == product.Title); // Assuming Title matches
                product.DescriptionTranslations = await _genAIService.GenerateResponseFromCsvImportAsync(product, csvData);
                
                return new ProductRepresentation
                {
                    Title = product.Title,
                    DescriptionTranslations = product.DescriptionTranslations,
                    Category = product.Category,
                };
            });

            return await Task.WhenAll(tasks);
        }
    }
}
