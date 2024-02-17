using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALB.BLOG.WEBUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostBLO _postBLO;
        public INotyfService _notification { get; }

        public PostController(IPostBLO postBLO, INotyfService notification)
        {
            _postBLO = postBLO;
            _notification = notification;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            try
            {
                return View(await _postBLO.GetIndexPost(User.Identity!.Name, page)); ;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve the post. Error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View(new CreatePostVM());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve the post. Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostVM createPostVM)
        {
            try
            {
                if (createPostVM.Title == null)
                    return View(createPostVM);

                await _postBLO.Create(User.Identity!.Name, createPostVM);

                _notification.Success("Post Created Successfully");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve the post. Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var post = await _postBLO.Delete(User.Identity!.Name, id);

                if (post == true)
                {
                    _notification.Success("Post Deleted Successfully");
                    return RedirectToAction("Index", "Post", new { area = "Admin" });
                }

                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve the post. Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var postVM = await _postBLO.GetEditPost(User.Identity!.Name, id);

                if (postVM == null)
                {
                    _notification.Error("Post not found");
                    return View();
                }

                return View(postVM);
            }
            catch (Exception ex)
            {
                if (ex.Message == "You are not authorized")
                {
                    _notification.Error("You are not authorized");
                    return RedirectToAction("Index");
                }

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve the post. Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreatePostVM createPostVM)
        {
            try
            {
                if (createPostVM.Title == null)
                    return View(createPostVM);

                var getPost = await _postBLO.Update(createPostVM);

                if (getPost == false)
                {
                    _notification.Error("Post not found");
                    return View();
                }

                _notification.Success("Post updated succesfully");
                return RedirectToAction("Index", "Post", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve the post. Error: {ex.Message}");
            }
        }
    }
}