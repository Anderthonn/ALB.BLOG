using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.DAL.Interfaces
{
    public interface IApplicationUserDAO
    {
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        Task<List<ApplicationUser>> GetAllUser();

        /// <summary>
        /// Get the user by name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<ApplicationUser?> GetUserByNameIdentity(string userName);
    }
}