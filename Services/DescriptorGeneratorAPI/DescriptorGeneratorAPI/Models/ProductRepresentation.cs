namespace DescriptorGeneratorAPI.Models
{
    public class ProductRepresentation
    {
        public string? Title { get; set; }
        public required DescriptionTranslations DescriptionTranslations { get; set; }
        public string? Category { get; set; }
    }
}
