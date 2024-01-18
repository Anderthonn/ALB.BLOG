using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ALB.BLOG.DAL.Querys
{
    public class ApplicationUserDAO : IApplicationUserDAO
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserDAO(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApplicationUser>> GetAllUser()
        {
            return await _userManager.Users.ToListAsync();
        }

        /// <summary>
        /// Get the user by name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<ApplicationUser?> GetUserByNameIdentity(string userName)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}