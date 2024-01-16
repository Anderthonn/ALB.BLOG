using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DOMAIN.Models;
using ALB.BLOG.INFRA.DbContextConnections;
using Microsoft.EntityFrameworkCore;

namespace ALB.BLOG.DAL.Querys
{
    public class PageDAO : IPageDAO
    {
        private readonly ApplicationDbContext _context;

        public PageDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the publication page.
        /// </summary>
        /// <returns></returns>
        public async Task<Page?> GetPagePost()
        {
            return await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "post");
        }

        /// <summary>
        /// Get the about page.
        /// </summary>
        /// <returns></returns>
        public async Task<Page?> GetPageAbout()
        {
            return await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "about");
        }

        /// <summary>
        /// Get the contact page.
        /// </summary>
        /// <returns></returns>
        public async Task<Page?> GetPageContact()
        {
            return await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "contact");
        }

        /// <summary>
        /// Update of a page.
        /// </summary>
        /// <param name="Page"></param>
        /// <returns></returns>
        public async Task Update(Page Page)
        {
            _context.Pages.Update(Page);
            await _context.SaveChangesAsync();
        }
    }
}