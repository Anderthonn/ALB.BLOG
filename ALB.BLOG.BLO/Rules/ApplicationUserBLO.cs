using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DOMAIN.Models;
using ALB.BLOG.INFRA.DbUtilites;
using Microsoft.AspNetCore.Identity;

namespace ALB.BLOG.BLO.Rules
{
    public class ApplicationUserBLO : IApplicationUserBLO
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationUserDAO _applicationUserDAO;

        public ApplicationUserBLO(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IApplicationUserDAO applicationUserDAO)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationUserDAO = applicationUserDAO;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<ApplicationUser>> GetAllUser()
        {
            try
            {
                return await _applicationUserDAO.GetAllUser();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Take all users and format the return in a View Model to populate a screen.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<UserVM>> GetAllUserIndex()
        {
            try
            {
                var users = await GetAllUser();

                var userVM = users.Select(x => new UserVM()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName,
                    Email = x.Email,
                }).ToList();

                foreach (var user in userVM)
                {
                    var singleUser = await _userManager.FindByIdAsync(user.Id);
                    var role = await _userManager.GetRolesAsync(singleUser);
                    user.Role = role.FirstOrDefault();
                }

                return userVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get the user by ID and format the return in the View Model to reset the password.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ResetPasswordVM> GetResetPassword(string id)
        {
            try
            {
                var existingUser = await _userManager.FindByIdAsync(id);

                if (existingUser == null)
                {
                    return new ResetPasswordVM();
                }

                var resetPasswordVM = new ResetPasswordVM()
                {
                    Id = existingUser.Id,
                    UserName = existingUser.UserName
                };

                return resetPasswordVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get the user by name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ApplicationUser?> GetUserByNameIdentity(string userName)
        {
            try
            {
                var user = await _applicationUserDAO.GetUserByNameIdentity(userName);

                if (user == null)
                {
                    throw new Exception("No user with that name was found!");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Perform user login.
        /// </summary>
        /// <param name="loginVM"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Login(LoginVM loginVM)
        {
            try
            {
                var existingUser = await GetUserByNameIdentity(loginVM.Username);

                if (existingUser == null)
                {
                    throw new Exception("Username does not exist!");
                }

                var verifyPassword = await _userManager.CheckPasswordAsync(existingUser, loginVM.Password);

                if (!verifyPassword)
                {
                    throw new Exception("Password does not match!");
                }

                await _signInManager.PasswordSignInAsync(loginVM.Username, loginVM.Password, loginVM.RememberMe, true);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Disconnect the user.
        /// </summary>
        public void Logout()
        {
            try
            {
                _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Reset the user's password.
        /// </summary>
        /// <param name="resetPasswordVM"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            try
            {
                var existingUser = await _userManager.FindByIdAsync(resetPasswordVM.Id);

                if (existingUser == null)
                {
                    throw new Exception("User doesnot exist!");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
                var result = await _userManager.ResetPasswordAsync(existingUser, token, resetPasswordVM.NewPassword);

                if (result.Succeeded)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="registerVM"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Register(RegisterVM registerVM)
        {
            try
            {
                var checkUserByEmail = await _userManager.FindByEmailAsync(registerVM.Email);

                if (checkUserByEmail != null)
                    throw new Exception("Email already exists!");

                var checkUserByUsername = await GetUserByNameIdentity(registerVM.UserName);

                if (checkUserByUsername != null)
                    throw new Exception("Username already exists!");

                var applicationUser = new ApplicationUser()
                {
                    Email = registerVM.Email,
                    UserName = registerVM.UserName,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName
                };

                var result = await _userManager.CreateAsync(applicationUser, registerVM.Password);

                if (result.Succeeded)
                {
                    if (registerVM.IsAdmin)
                    {
                        await _userManager.AddToRoleAsync(applicationUser, WebsiteRoles.WebsiteAdmin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(applicationUser, WebsiteRoles.WebsiteAuthor);
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}