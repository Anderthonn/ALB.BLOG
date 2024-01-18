using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALB.BLOG.WEBUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingBLO _settingBLO;
        public INotyfService _notification { get; }

        public SettingController(ISettingBLO settingBLO, INotyfService notification)
        {
            _settingBLO = settingBLO;
            _notification = notification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _settingBLO.Created());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve configuration. Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(SettingVM settingVM)
        {
            try
            {
                if (!ModelState.IsValid) { return View(settingVM); }

                var setting = await _settingBLO.Update(settingVM);

                if (setting == false)
                {
                    _notification.Error("Something went wrong");
                    return View();
                }

                _notification.Success("Setting updated succesfully");
                return RedirectToAction("Index", "Setting", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve configuration. Error: {ex.Message}");
            }
        }
    }
}