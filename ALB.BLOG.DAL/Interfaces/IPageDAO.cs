using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.DAL.Interfaces
{
    public interface IPageDAO
    {
        /// <summary>
        /// Get the publication page.
        /// </summary>
        /// <returns></returns>
        Task<Page?> GetPagePost();

        /// <summary>
        /// Get the about page.
        /// </summary>
        /// <returns></returns>
        Task<Page?> GetPageAbout();

        /// <summary>
        /// Get the contact page.
        /// </summary>
        /// <returns></returns>
        Task<Page?> GetPageContact();

        /// <summary>
        /// Update of a page.
        /// </summary>
        /// <param name="Page"></param>
        /// <returns></returns>
        Task Update(Page Page);
    }
}