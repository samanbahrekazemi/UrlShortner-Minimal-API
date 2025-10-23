using UrlShortner.Models;

namespace UrlShortner.Interfaces
{

    public interface IUrlShortenerService
    {
        Task<string> ShortenUrlAsync(string originalUrl);
        Task<string?> GetOriginalUrlAsync(string shortUrlKey);
        Task<IEnumerable<ShortenedUrl>> GetAllUrlsAsync();
    }
}
