﻿using ALB.BLOG.DOMAIN.Models;
using ALB.BLOG.INFRA.DbContextConnections;
using Microsoft.AspNetCore.Identity;

namespace ALB.BLOG.INFRA.DbUtilites
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (!_roleManager.RoleExistsAsync(WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAuthor)).GetAwaiter().GetResult();
                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Super",
                    LastName = "Admin"
                }, "Admin@0011").Wait();

                var appUser = _context.ApplicationUsers!.FirstOrDefault(x => x.Email == "admin@gmail.com");

                if (appUser != null)
                {
                    _userManager.AddToRoleAsync(appUser, WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult();
                }

                var listOfPages = new List<Page>()
                {
                    new Page("Post", null, null, "post", null),

                    new Page("About Us", null, null, "about", null),

                    new Page("Contact Us", null, null, "contact", null),
                 };

                _context.Pages!.AddRange(listOfPages);

                var setting = new Setting("Site Name", "Site Title", "Short Description of site", null, null, null, null, null);

                _context.Settings!.Add(setting);
                _context.SaveChanges();
            }
        }
    }
}