using ALB.BLOG.BLO.Interfaces;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ALB.BLOG.WEBUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostBLO _postBLO;
        public INotyfService _notification { get; }

        public BlogController(IPostBLO postBLO, INotyfService notification)
        {
            _postBLO = postBLO;
            _notification = notification;
        }

        [HttpGet("[controller]/{slug}")]
        public async Task<IActionResult> Post(string slug)
        {
            try
            {
                var blogPostVM = await _postBLO.GetPostBySlug(slug);

                if (blogPostVM == null)
                {
                    _notification.Error("Post not found");
                    return View();
                }

                return View(blogPostVM);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve the post. Error: {ex.Message}");
            }
        }
    }
}