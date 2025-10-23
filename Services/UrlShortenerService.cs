using Microsoft.EntityFrameworkCore;
using System.Linq;
using UrlShortner.Data;
using UrlShortner.Interfaces;
using UrlShortner.Models;

namespace UrlShortener.Services;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly UrlShortenerDbContext _db;
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private readonly Random _random = new();

    public UrlShortenerService(UrlShortenerDbContext db)
    {
        _db = db;
    }

    public async Task<string> ShortenUrlAsync(string originalUrl)
    {
        var normalizedUrl = originalUrl.Trim().TrimEnd('/');

        var existing = await _db.ShortenedUrls
                                .AsNoTracking()
                                .FirstOrDefaultAsync(u =>
                                    u.OriginalUrl.ToLower().TrimEnd('/') == normalizedUrl.ToLower());

        if (existing != null)
            return existing.ShortUrlKey;

        string shortUrlKey;
        do
        {
            shortUrlKey = new string(Enumerable.Repeat(Chars, 6)
                                              .Select(s => s[_random.Next(s.Length)])
                                              .ToArray());
        } while (await _db.ShortenedUrls.AnyAsync(u => u.ShortUrlKey == shortUrlKey));

        var mapping = new ShortenedUrl
        {
            OriginalUrl = originalUrl.Trim().TrimEnd('/'), 
            ShortUrlKey = shortUrlKey,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        };

        _db.ShortenedUrls.Add(mapping);
        await _db.SaveChangesAsync();

        return shortUrlKey;
    }

    public async Task<string?> GetOriginalUrlAsync(string shortUrlKey)
    {
        var mapping = await _db.ShortenedUrls
                               .AsNoTracking()
                               .FirstOrDefaultAsync(u => u.ShortUrlKey == shortUrlKey);
        return mapping?.OriginalUrl;
    }

    public async Task<IEnumerable<ShortenedUrl>> GetAllUrlsAsync()
    {
        return await _db.ShortenedUrls
                        .AsNoTracking()
                        .OrderByDescending(u => u.CreatedAt)
                        .ToListAsync() ?? [];
    }
}
