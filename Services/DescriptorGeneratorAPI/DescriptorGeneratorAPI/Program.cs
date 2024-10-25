using DescriptorGeneratorAPI.Services;
using DescriptorGeneratorAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Extensions;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Integrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductsDbContext>(options =>
    options.UseInMemoryDatabase("ProductDb"));
builder.Services.AddJsonMultipartFormDataSupport(JsonSerializerChoice.SystemText);

builder.Services.AddScoped<IDescriptorService, DescriptorService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IGenAIService, GenAIService>();
builder.Services.AddScoped<IBlobService, BlobService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.MapControllers();

app.Run();


