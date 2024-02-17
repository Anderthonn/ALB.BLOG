using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.DAL.Interfaces
{
    public interface ICategoryDAO
    {
        /// <summary>
        /// Create a category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task Create(Category category);

        /// <summary>
        /// Delete a category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task Delete(Category category);

        /// <summary>
        /// Helper for updating a category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        void Entity(int id, Category category);

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetAllCategory();

        /// <summary>
        /// Get a category by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Category> GetCategoryByCategoryId(int id);

        /// <summary>
        /// Perform an update of a category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task Update(Category category);
    }
}