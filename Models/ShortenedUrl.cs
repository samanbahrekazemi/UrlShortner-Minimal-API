namespace UrlShortner.Models
{
    public class ShortenedUrl
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string OriginalUrl { get; set; } = null!;
        public string ShortUrlKey { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow; 
    }
}
