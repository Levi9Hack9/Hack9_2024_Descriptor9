using CsvHelper;
using CsvHelper.Configuration;
using DescriptorGeneratorAPI.Models;
using System.Globalization;

namespace DescriptorGeneratorAPI.Utils
{
    public class FileManipulation
    {
        public static async Task<List<CsvProductData>> ParseCsvFileAsync(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";", // Set semicolon as delimiter
                HeaderValidated = null, // Ignore header validation errors
                MissingFieldFound = null // Ignore missing field errors
            };

            using (var stream = new StreamReader(csvFile.OpenReadStream()))
            using (var csv = new CsvReader(stream, config))
            {
                var records = await Task.Run(() => csv.GetRecords<CsvProductData>().ToList());

                return records;
            }
        }
    }
}
