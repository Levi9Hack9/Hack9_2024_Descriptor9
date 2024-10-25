using DescriptorGeneratorAPI.Models;

namespace DescriptorGeneratorAPI.Services.Interfaces
{
    public interface IGenAIService
    {
        Task<DescriptionTranslations> GenerateResponseFromImageAsync(ProductRepresentation product, IFormFile image);
        Task<DescriptionTranslations> GenerateResponseFromCsvImportAsync(Product product, CsvProductData csvData);
    }
}
