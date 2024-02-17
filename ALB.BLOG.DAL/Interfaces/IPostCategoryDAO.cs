using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.DAL.Interfaces
{
    public interface IPostCategoryDAO
    {
        /// <summary>
        /// Create a post category.
        /// </summary>
        /// <param name="postCategory"></param>
        /// <returns></returns>
        Task Create(PostCategory postCategory);

        /// <summary>
        /// Delete a post category.
        /// </summary>
        /// <param name="postCategory"></param>
        /// <returns></returns>
        Task Delete(PostCategory postCategory);

        /// <summary>
        /// Get all posts categories.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PostCategory>> GetAllPostCategory();

        /// <summary>
        /// Get posts categories by id post.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<PostCategory>> GetPostCategoryByIdPost(int id);

        /// <summary>
        /// Get posts categories by id post and category.
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<PostCategory?> GetPostCategoryByIdPostAndCategory(int postId, int categoryId);
    }
}