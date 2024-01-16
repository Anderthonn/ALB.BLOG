using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALB.BLOG.DOMAIN.Models
{
    [Table(name: "tb_application_user")]
    public class ApplicationUser : IdentityUser
    {
        [Column(name: "first_name")]
        public string? FirstName { get; set; }
        [Column(name: "last_name")]
        public string? LastName { get; set; }

        public List<Post>? Posts { get; set; }
    }
}