using UrlShortener.Models;
using UrlShortner.Interfaces;

namespace UrlShortener.Endpoints;

public static class UrlShortenerEndpoints
{
    public static void MapUrlShortenerEndpoints(this WebApplication app)
    {
        // POST / -> Create short URL
        app.MapPost("/api/shortner", async (HttpContext context, ShortenRequest request, IUrlShortenerService urlService) =>
        {
            if (string.IsNullOrWhiteSpace(request.OriginalUrl))
                return Results.BadRequest("URL cannot be empty.");

            var originalUrlTrimmed = request.OriginalUrl.Trim();

            if (!Uri.TryCreate(originalUrlTrimmed, UriKind.Absolute, out var _))
                return Results.BadRequest("Invalid URL format.");

            var shortUrlKey = await urlService.ShortenUrlAsync(originalUrlTrimmed);

            var baseUrl = $"{context.Request.Scheme}://{context.Request.Host}";
            return Results.Ok(new { shortUrl = $"{baseUrl}/api/shortner/{shortUrlKey}" });
        });


        // GET /{shortUrl} -> Regirect original URL
        app.MapGet("/api/shortner/{shortUrl}", async (string shortUrl, IUrlShortenerService urlService) =>
        {
            var originalUrl = await urlService.GetOriginalUrlAsync(shortUrl);
            return originalUrl != null ? Results.Redirect(originalUrl) : Results.NotFound("URL not found.");
        });

        // GET /all URLs
        app.MapGet("/api/shortner/all", async (IUrlShortenerService urlService) =>
        {
            var allUrls = await urlService.GetAllUrlsAsync();
            return Results.Ok(allUrls.Select(u => new
            {
                u.ShortUrlKey,
                u.OriginalUrl,
                u.CreatedAt,
                u.ModifiedAt
            }));
        });
    }
}
