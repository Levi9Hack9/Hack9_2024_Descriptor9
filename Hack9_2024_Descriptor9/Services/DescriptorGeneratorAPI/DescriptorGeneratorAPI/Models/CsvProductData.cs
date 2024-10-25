using CsvHelper.Configuration.Attributes;

namespace DescriptorGeneratorAPI.Models
{
    public class CsvProductData
    {
        public string? Title { get; set; }
        public string? Colors { get; set; }
        public string? Widths { get; set; }
        public string? Clips { get; set; }
        public string? MetalClosure { get; set; }
        public string? LogoPrint { get; set; }
        public string? Logo { get; set; }
    }
}
