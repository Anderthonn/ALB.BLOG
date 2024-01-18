using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALB.BLOG.WEBUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IApplicationUserBLO _applicationUserBLO;
        public INotyfService _notification { get; }

        public UserController(IApplicationUserBLO applicationUserBLO, INotyfService notification)
        {
            _applicationUserBLO = applicationUserBLO;
            _notification = notification;

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _applicationUserBLO.GetAllUserIndex());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve configuration. Error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string id)
        {
            try
            {
                var resetPasswordVM = await _applicationUserBLO.GetResetPassword(id);

                if (resetPasswordVM == null)
                {
                    _notification.Error("User doesnot exsits");
                    return View();
                }

                return View(resetPasswordVM);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve configuration. Error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            try
            {
                if (!ModelState.IsValid) { return View(resetPasswordVM); }

                var resetPassword = await _applicationUserBLO.ResetPassword(resetPasswordVM);

                if (resetPassword == true)
                {
                    _notification.Success("Password reset succuful");
                    return RedirectToAction(nameof(Index));
                }

                return View(resetPasswordVM);
            }
            catch (Exception ex)
            {
                if (ex.Message == "User doesnot exist!")
                {
                    _notification.Error("User doesnot exist!");
                    return View(resetPasswordVM);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve configuration. Error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Register()
        {
            try
            {
                return View(new RegisterVM());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve configuration. Error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            try
            {
                if (!ModelState.IsValid) { return View(registerVM); }

                var returnRegister = await _applicationUserBLO.Register(registerVM);

                if (returnRegister == true)
                {
                    _notification.Success("User registered successfully");
                    return RedirectToAction("Index", "User", new { area = "Admin" });
                }

                return View(registerVM);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Email already exists!")
                {
                    _notification.Error("Email already exists");
                    return View(registerVM);
                }

                if (ex.Message == "Username already exists!")
                {
                    _notification.Error("Username already exists");
                    return View(registerVM);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve configuration. Error: {ex.Message}");
            }
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            try
            {
                if (!HttpContext.User.Identity!.IsAuthenticated)
                {
                    return View(new LoginVM());
                }

                return RedirectToAction("Index", "Post", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve configuration. Error: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            try
            {
                if (!ModelState.IsValid) { return View(loginVM); }

                var existingUser = await _applicationUserBLO.Login(loginVM);

                if (existingUser == true)
                {
                    _notification.Success("Login Successful");
                    return RedirectToAction("Index", "Post", new { area = "Admin" });
                }

                return View(loginVM);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Username does not exist!")
                {
                    _notification.Error("Username does not exist!");
                    return View(loginVM);
                }
                if (ex.Message == "Password does not match!")
                {
                    _notification.Error("Password does not match");
                    return View(loginVM);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve configuration. Error: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Logout()
        {
            try
            {
                _applicationUserBLO.Logout();

                _notification.Success("You are logged out successfully");
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve configuration. Error: {ex.Message}");
            }
        }

        [HttpGet("AccessDenied")]
        [Authorize]
        public IActionResult AccessDenied()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve configuration. Error: {ex.Message}");
            }
        }
    }
}