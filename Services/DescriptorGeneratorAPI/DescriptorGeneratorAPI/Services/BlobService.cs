using Azure.Storage.Blobs;
using DescriptorGeneratorAPI.Services.Interfaces;

namespace DescriptorGeneratorAPI.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobService(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration.GetConnectionString("AzureStorageConnection"));
            _containerName = configuration["BlobStorage:ContainerName"] ?? throw new ArgumentNullException(nameof(configuration), "Container name cannot be null");
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file.Length == 0)
            {
                throw new ArgumentException("File is required.");
            }

            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync(); // Create the container if it doesn't exist

            var blobName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var blobClient = containerClient.GetBlobClient(blobName);
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString(); // Return the URI of the uploaded blob
        }
    }
}
