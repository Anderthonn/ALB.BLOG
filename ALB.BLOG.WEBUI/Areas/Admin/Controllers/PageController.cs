using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALB.BLOG.WEBUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PageController : Controller
    {
        private readonly IPageBLO _pageBLO;
        public INotyfService _notification { get; }

        public PageController(IPageBLO pageBLO, INotyfService notification)
        {
            _pageBLO = pageBLO;
            _notification = notification;
        }

        [HttpGet]
        public async Task<IActionResult> About()
        {
            try
            {
                return View(await _pageBLO.GetPageAbout());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to recover the page. Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> About(PageVM pageVM)
        {
            try
            {
                if (!ModelState.IsValid) { return View(pageVM); }

                var pageAbout = await _pageBLO.Update("A", pageVM);

                if (pageAbout == false)
                {
                    _notification.Error("Page not found");
                    return View();
                }

                _notification.Success("About page updated succesfully");
                return RedirectToAction("About", "Page", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to recover the page. Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Contact()
        {
            try
            {
                return View(await _pageBLO.GetPageContact());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to recover the page. Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Contact(PageVM pageVM)
        {
            try
            {
                if (!ModelState.IsValid) { return View(pageVM); }

                var pageAbout = await _pageBLO.Update("C", pageVM);

                if (pageAbout == false)
                {
                    _notification.Error("Page not found");
                    return View();
                }

                _notification.Success("Contact page updated succesfully");
                return RedirectToAction("Contact", "Page", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to recover the page. Error: {ex.Message}");
            }
        }
    }
}