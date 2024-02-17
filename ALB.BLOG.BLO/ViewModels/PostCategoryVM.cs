using System.ComponentModel.DataAnnotations;

namespace ALB.BLOG.BLO.ViewModels
{
    public class PostCategoryVM
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryColor { get; set; }
    }
}