using ALB.BLOG.BLO.ViewModels;
using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.BLO.Interfaces
{
    public interface IApplicationUserBLO
    {
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        Task<List<ApplicationUser>> GetAllUser();

        /// <summary>
        /// Take all users and format the return in a View Model to populate a screen.
        /// </summary>
        /// <returns></returns>
        Task<List<UserVM>> GetAllUserIndex();

        /// <summary>
        /// Get the user by ID and format the return in the View Model to reset the password.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResetPasswordVM> GetResetPassword(string id);

        /// <summary>
        /// Get the user by name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<ApplicationUser?> GetUserByNameIdentity(string userName);

        /// <summary>
        /// Perform user login.
        /// </summary>
        /// <param name="loginVM"></param>
        /// <returns></returns>
        Task<bool> Login(LoginVM loginVM);

        /// <summary>
        /// Disconnect the user.
        /// </summary>
        void Logout();

        /// <summary>
        /// Reset the user's password.
        /// </summary>
        /// <param name="resetPasswordVM"></param>
        /// <returns></returns>
        Task<bool> ResetPassword(ResetPasswordVM resetPasswordVM);

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="registerVM"></param>
        /// <returns></returns>
        Task<bool> Register(RegisterVM registerVM);
    }
}