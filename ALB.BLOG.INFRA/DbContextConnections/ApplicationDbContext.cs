using ALB.BLOG.DOMAIN.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ALB.BLOG.INFRA.DbContextConnections
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Email>? Emails { get; set; }
        public DbSet<Page>? Pages { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<PostCategory>? PostsCategories { get; set; }
        public DbSet<Setting>? Settings { get; set; }
    }
}