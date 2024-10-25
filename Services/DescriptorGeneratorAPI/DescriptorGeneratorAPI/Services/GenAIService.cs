using Azure;
using Azure.AI.OpenAI;
using DescriptorGeneratorAPI.Models;
using DescriptorGeneratorAPI.Services.Interfaces;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace DescriptorGeneratorAPI.Services
{
    public class GenAIService : IGenAIService
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly AzureOpenAIClient _aiClient;
        private readonly HttpClient _httpClient;
        private readonly string _deploymentName = "gpt-4o";
        private readonly string _systemMessage = "You are an AI assistant designed to generate product descriptions based on textual inputs and accompanying product images. Your task is to analyze the provided image and the given text to create a compelling and informative product description that highlights key features, benefits, and any relevant details.";

        public GenAIService(IConfiguration configuration)
        {
            _apiKey = configuration["OpenAI:ApiKey"] ?? throw new ArgumentNullException(nameof(configuration), "OpenAI:ApiKey is null");
            _baseUrl = configuration["OpenAI:Endpoint"] ?? throw new ArgumentNullException(nameof(configuration), "OpenAI:BaseUrl is null");
            _aiClient = new(new Uri(_baseUrl), new ApiKeyCredential(_apiKey));
            _httpClient = new HttpClient();
        }
        
        public async Task<DescriptionTranslations> GenerateResponseFromImageAsync(ProductRepresentation product, IFormFile image)
        {
            // converting image to Base64 format
            string base64Image = string.Empty;
            if (image != null && image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    base64Image = Convert.ToBase64String(imageBytes);
                }
            }

            // settings up the request headers
            _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);

            var messageRequest = new
            {
                // creating a message object with system and user messages
                messages = new object[]
                {
                    new
                    {
                        role = "system",
                        content = new object[]
                        {
                            new
                            {
                                type = "text",
                                text = "You are an AI assistant that helps people to create description for provided product image and some product data." // Ovde će ići primer opisa koji će korisnik poslati
                            }
                        }
                    },
                    new
                    {
                        role = "user",
                        content = new object[]
                        {
                            new
                            {
                                type = "text",
                                text = $"Can you give me precise description for the product: {product.Title} of category: {product.Category} and provided image. It should be in one sentence, and if you can recognise some characteristics like material, dimensions..."
                            },
                            new
                            {
                                type = "image_url",
                                image_url = new
                                {
                                    url = $"data:{image?.ContentType};base64,{base64Image}"
                                }
                            },
                            new
                            {
                                type = "text",
                                text = @"Translate the description into the following languages and return the response in the format below:
                                {
                                    ""English"": [English description],
                                    ""Spanish"": [Spanish translation],
                                    ""Dutch"": [Dutch translation],
                                    ""Ukrainian"": [Ukrainian translation],
                                    ""Romanian"": [Romanian translation],
                                    ""Serbian"": [Serbian translation]
                                }"
                            },
                        }
                    }
                },
                temperature = 0.7,
                top_p = 0.95,
                max_tokens = 800,
                stream = false
            };

            // calling the OpenAI API
            var response = await _httpClient.PostAsync(_baseUrl, new StringContent(JsonSerializer.Serialize(messageRequest), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                // checking if the response is successful
                var responseData = await response.Content.ReadFromJsonAsync<GenAiResponse>();
                if (responseData is not null && responseData.Choices?.Length > 0)
                {
                    // getting the content of the response
                    var content = responseData.Choices[0]?.Message?.Content?.Replace("\n", "").Trim(); ;

                    if (!string.IsNullOrEmpty(content))
                    {
                        // Assuming the content is valid JSON that matches DescriptionTranslations structure
                        var translations = JsonSerializer.Deserialize<DescriptionTranslations>(content);
                        return translations ?? new DescriptionTranslations();
                    }
                }
            }
            else
            {
                var errorResponse = $"Error: {response.StatusCode}, {response.ReasonPhrase}";
                throw new Exception(errorResponse);
            }

            throw new Exception("Failed to generate description.");

        }

        public async Task<DescriptionTranslations> GenerateResponseFromCsvImportAsync(Product product, CsvProductData csvData)
        {
            var prompt = new StringBuilder();
            prompt.AppendLine($"Can you give more precise description: {product.DescriptionTranslations?.English} of product: {product.Title}.");
            prompt.AppendLine($"Additional Details:");

            if (!string.IsNullOrEmpty(csvData.Colors))
                prompt.AppendLine($"- Colors: {csvData.Colors}");

            if (!string.IsNullOrEmpty(csvData.Widths))
                prompt.AppendLine($"- Widths: {csvData.Widths}");

            if (!string.IsNullOrEmpty(csvData.Clips))
                prompt.AppendLine($"- Clips: {csvData.Clips}");

            if (!string.IsNullOrEmpty(csvData.MetalClosure))
                prompt.AppendLine($"- Metal Closure: {csvData.MetalClosure}");

            if (!string.IsNullOrEmpty(csvData.LogoPrint))
                prompt.AppendLine($"- Logo Print: {csvData.LogoPrint}");

            if (!string.IsNullOrEmpty(csvData.Logo))
                prompt.AppendLine($"- Logo: {csvData.Logo}");

            prompt.AppendLine("Please provide an engaging and informative description suitable for a product listing.");
            prompt.AppendLine("It should be in one sentence");

            // settings up the request headers
            _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);

            var messageRequest = new
            {
                // creating a message object with system and user messages
                messages = new object[]
                {
                    new
                    {
                        role = "system",
                        content = new object[]
                        {
                            new
                            {
                                type = "text",
                                text = "You are an AI assistant that helps people to create description for provided product image and some product data." // Ovde će ići primer opisa koji će korisnik poslati
                            }
                        }
                    },
                    new
                    {
                        role = "user",
                        content = new object[]
                        {
                            new
                            {
                                type = "text",
                                text = prompt.ToString()
                            },
                            new
                            {
                                type = "text",
                                text = @"Translate the description into the following languages and return the response in the format below:
                                {
                                    ""English"": [English description],
                                    ""Spanish"": [Spanish translation],
                                    ""Dutch"": [Dutch translation],
                                    ""Ukrainian"": [Ukrainian translation],
                                    ""Romanian"": [Romanian translation],
                                    ""Serbian"": [Serbian translation]
                                }"
                            },
                        }
                    }
                },
                temperature = 0.7,
                top_p = 0.95,
                max_tokens = 800,
                stream = false
            };

            // calling the OpenAI API
            var response = await _httpClient.PostAsync(_baseUrl, new StringContent(JsonSerializer.Serialize(messageRequest), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                // checking if the response is successful
                var responseData = await response.Content.ReadFromJsonAsync<GenAiResponse>();
                if (responseData is not null && responseData.Choices?.Length > 0)
                {
                    // getting the content of the response
                    var content = responseData.Choices[0]?.Message?.Content?.Replace("\n", "").Trim(); ;

                    if (!string.IsNullOrEmpty(content))
                    {
                        // Assuming the content is valid JSON that matches DescriptionTranslations structure
                        var translations = JsonSerializer.Deserialize<DescriptionTranslations>(content);
                        return translations ?? new DescriptionTranslations();
                    }
                }
            }
            else
            {
                var errorResponse = $"Error: {response.StatusCode}, {response.ReasonPhrase}";
                throw new Exception(errorResponse);
            }

            throw new Exception("Failed to generate description.");

        }
    }
}
