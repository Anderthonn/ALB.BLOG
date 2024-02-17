using System.ComponentModel.DataAnnotations;

namespace ALB.BLOG.BLO.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Color { get; set; }
    }
}