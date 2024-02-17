using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ALB.BLOG.BLO.ViewModels
{
    public class PageVM
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public IFormFile? Thumbnail { get; set; }
        public string EmailResultMessage { get; set; }
    }
}