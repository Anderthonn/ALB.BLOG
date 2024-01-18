using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ALB.BLOG.WEBUI.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageBLO _pageBLO;
        private readonly IEmailBLO _emailBLO;

        public PageController(IPageBLO pageBLO, IEmailBLO emailBLO)
        {
            _pageBLO = pageBLO;
            _emailBLO = emailBLO;
        }

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

        public async Task<IActionResult> ContactEmail(EmailVM emailVM)
        {
            try
            {
                return View(await _emailBLO.SendEmail(emailVM));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to recover the page. Error: {ex.Message}");
            }
        }
    }
}