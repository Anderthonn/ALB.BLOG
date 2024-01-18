using ALB.BLOG.BLO.ViewModels;

namespace ALB.BLOG.BLO.Interfaces
{
    public interface IPageBLO
    {
        /// <summary>
        /// Get the about page.
        /// </summary>
        /// <returns></returns>
        Task<PageVM?> GetPageAbout();

        /// <summary>
        /// Get the contact page.
        /// </summary>
        /// <returns></returns>
        Task<PageVM?> GetPageContact();

        /// <summary>
        /// Get the home page.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchFilter"></param>
        /// <returns></returns>
        Task<HomeVM?> GetPageHome(int? page, string? searchFilter = null);

        /// <summary>
        /// Get the post page.
        /// </summary>
        /// <returns></returns>
        Task<PageVM?> GetPagePost();

        /// <summary>
        /// Update the about or contact page.
        /// </summary>
        /// <param name="aboutOrContact"></param>
        /// <param name="pageVM"></param>
        /// <returns></returns>
        Task<bool> Update(string aboutOrContact, PageVM pageVM);
    }
}