using ALB.BLOG.BLO.ViewModels;
using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.BLO.Interfaces
{
    public interface IPostCategoryBLO
    {
        /// <summary>
        /// Create a post category.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="postCategoryVM"></param>
        /// <returns></returns>
        Task Create(PostCategory postCategory);

        /// <summary>
        /// Delete a post category.
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task Delete(int? postId = null, int? categoryId = null);

        /// <summary>
        /// Get all posts categories.
        /// </summary>
        /// <returns></returns>
        Task<List<PostCategoryVM>> GetAllPostCategory();

        /// <summary>
        /// Get posts categories by id post.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<PostCategoryVM>> GetPostCategoryByIdPost(int id);

        /// <summary>
        /// Get Categories by ID Post.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<int>> GetCategoriesByIdPost(int id);

        /// <summary>
        /// Get posts categories by id post and category.
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<PostCategoryVM> GetPostCategoryByIdPostAndCategory(int postId, int categoryId);
    }
}