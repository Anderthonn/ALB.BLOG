using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.DAL.Interfaces
{
    public interface IEmailDAO
    {
        /// <summary>
        /// Listing of all emails already sent.
        /// </summary>
        /// <returns></returns>
        Task<List<Email>> GetAllEmail();

        /// <summary>
        /// Creation of the email sending record in the email table.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task Create(Email email);
    }
}