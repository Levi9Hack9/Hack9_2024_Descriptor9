using DescriptorGeneratorAPI.Models;
using DescriptorGeneratorAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DescriptorGeneratorAPI.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ProductsDbContext _context;
        private readonly IBlobService _blobService;

        public ProductsService(ProductsDbContext context, IBlobService blobService)
        {
            _context = context;
            _context.Database.EnsureCreated();
            _blobService = blobService;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> AddProductAsync(ProductRepresentation productRepresentation, IFormFile image)
        {
            var product = new Product
            {
                Id = _context.Products.Max(p => p.Id) + 1, // Generate new ID
                Title = productRepresentation.Title,
                ImageUrl = await _blobService.UploadFileAsync(image),
                Category = productRepresentation.Category,
                DescriptionTranslations = new DescriptionTranslations
                {
                    English = productRepresentation.DescriptionTranslations.English,
                    Spanish = productRepresentation.DescriptionTranslations.Spanish,
                    Dutch = productRepresentation.DescriptionTranslations.Dutch,
                    Ukrainian = productRepresentation.DescriptionTranslations.Ukrainian,
                    Romanian = productRepresentation.DescriptionTranslations.Romanian,
                    Serbian = productRepresentation.DescriptionTranslations.Serbian,
                }
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<ProductRepresentation>> UpdateProductsAsync(IEnumerable<ProductRepresentation> products)
        {
            foreach (var product in products)
            {
                var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Title == product.Title);
                if (existingProduct != null)
                {
                    existingProduct.Category = product.Category;
                    existingProduct.DescriptionTranslations = new DescriptionTranslations
                    {
                        English = product.DescriptionTranslations.English,
                        Spanish = product.DescriptionTranslations.Spanish,
                        Dutch = product.DescriptionTranslations.Dutch,
                        Ukrainian = product.DescriptionTranslations.Ukrainian,
                        Romanian = product.DescriptionTranslations.Romanian,
                        Serbian = product.DescriptionTranslations.Serbian,
                    };
                    existingProduct.Title = product.Title;
                }
            }
            await _context.SaveChangesAsync();
            return products;
        }
    }
}
