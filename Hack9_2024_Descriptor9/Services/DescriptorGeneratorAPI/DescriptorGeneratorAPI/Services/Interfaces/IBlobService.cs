namespace DescriptorGeneratorAPI.Services.Interfaces
{
    public interface IBlobService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}