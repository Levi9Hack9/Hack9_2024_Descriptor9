namespace DescriptorGeneratorAPI.Models
{
    public class GenAiResponse
    {
        public Choice[]? Choices { get; set; }

        public class Choice
        {
            public Message? Message { get; set; }
        }

        public class Message
        {
            public string? Content { get; set; }
            public string? Role { get; set; }
        }
    }
}
