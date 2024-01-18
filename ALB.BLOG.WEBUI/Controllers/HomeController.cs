using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ALB.BLOG.WEBUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPageBLO _pageBLO;

        public HomeController(IPageBLO pageBLO)
        {
            _pageBLO = pageBLO;
        }

        public async Task<IActionResult> Index(int? page, string? searchFilter = null)
        {
            try
            {
                return View(await _pageBLO.GetPageHome(page, searchFilter));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve the page. Error: {ex.Message}");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM(Activity.Current?.Id ?? HttpContext.TraceIdentifier));
        }
    }
}