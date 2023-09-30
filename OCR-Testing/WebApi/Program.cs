using Microsoft.AspNetCore.Mvc;
using WebApi.Sevices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOCRService, OCRService>();  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/ocr", async (IOCRService ocrService, [FromForm] IFormFile imageFile) =>
{
    if (imageFile == null || imageFile.Length == 0)
    {
        return Results.BadRequest("Invalid file.");
    }

    var filePath = Path.GetTempFileName();
    using (var stream = File.Create(filePath))
    {
        await imageFile.CopyToAsync(stream);
    }

    var lines = ocrService.ProcessImage(filePath);
    File.Delete(filePath);

    return Results.Ok(lines);
});



app.Run();
