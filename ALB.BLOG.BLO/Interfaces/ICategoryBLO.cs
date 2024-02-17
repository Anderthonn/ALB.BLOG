using ALB.BLOG.BLO.ViewModels;

namespace ALB.BLOG.BLO.Interfaces
{
    public interface ICategoryBLO
    {
        /// <summary>
        /// Create a category.
        /// </summary>
        /// <param name="categoryVM"></param>
        /// <returns></returns>
        Task Create(CategoryVM categoryVM);

        /// <summary>
        /// Delete a category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CategoryVM>> GetAllCategory();

        /// <summary>
        /// Get a category by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CategoryVM> GetCategoryByCategoryId(int id);

        /// <summary>
        /// Perform an update of a category.
        /// </summary>
        /// <param name="categoryVM"></param>
        /// <returns></returns>
        Task Update(CategoryVM categoryVM);
    }
}