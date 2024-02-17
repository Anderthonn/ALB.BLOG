using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALB.BLOG.WEBUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryBLO _categoryBLO;

        public CategoryController(ICategoryBLO categoryBLO)
        {
            _categoryBLO = categoryBLO;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CategoryVM categoryVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(categoryVM);

                await _categoryBLO.Create(categoryVM);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to create the tag: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryBLO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to delete the tag: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                return Ok(await _categoryBLO.GetAllCategory());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve tags: {ex.Message}");
            }
        }
    }
}