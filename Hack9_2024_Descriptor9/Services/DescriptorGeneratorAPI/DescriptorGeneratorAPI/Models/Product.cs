namespace DescriptorGeneratorAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DescriptionTranslations? DescriptionTranslations { get; set; }
        public string? Category { get; set; }
        public string? ImageUrl { get; set; }
    }
}
