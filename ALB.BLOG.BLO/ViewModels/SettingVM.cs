using Microsoft.AspNetCore.Http;

namespace ALB.BLOG.BLO.ViewModels
{
    public class SettingVM
    {
        public int Id { get; set; }
        public string? SiteName { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? GithubUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public IFormFile? Thumbnail { get; set; }
    }
}