using System.Text.Json.Serialization;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var urlStore = new Dictionary<string, string>();

async Task<string> GenerateShortUrlKeyAsync(int length = 6)
{
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var random = new Random();
    string shortUrlKey;

    await Task.Delay(10); // This can be used to simulate async work, for example with a DB query

    // Generate a key until it is unique
    do
    {
        shortUrlKey = new string(Enumerable.Repeat(chars, length)
                                       .Select(s => s[random.Next(s.Length)])
                                       .ToArray());
    } while (urlStore.ContainsKey(shortUrlKey)); // Ensure uniqueness by checking if it already exists

    return shortUrlKey;
}

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// POST 
app.MapPost("/generate", async (ShortenRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.OriginalUrl))
        return Results.BadRequest("URL cannot be empty.");

    var shortUrlKey = await GenerateShortUrlKeyAsync(); 
    urlStore[shortUrlKey] = request.OriginalUrl;

    return Results.Ok(new { shortUrl = $"{app.Urls.First()}/{shortUrlKey}" });
});

// GET 
app.MapGet("/{shortUrl}", (string shortUrl) =>
{
    if (urlStore.ContainsKey(shortUrl))
        return Results.Redirect(urlStore[shortUrl]);
    return Results.NotFound("URL not found.");
});

app.Run();

public record ShortenRequest(string OriginalUrl);
