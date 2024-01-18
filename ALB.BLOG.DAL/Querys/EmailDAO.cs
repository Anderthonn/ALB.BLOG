using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DOMAIN.Models;
using ALB.BLOG.INFRA.DbContextConnections;
using Microsoft.EntityFrameworkCore;

namespace ALB.BLOG.DAL.Querys
{
    public class EmailDAO : IEmailDAO
    {
        private readonly ApplicationDbContext _context;

        public EmailDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creation of the email sending record in the email table.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task Create(Email email)
        {
            _context.Emails.Add(email);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Listing of all emails already sent.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Email>> GetAllEmail()
        {
            return await _context.Emails.ToListAsync();
        }
    }
}