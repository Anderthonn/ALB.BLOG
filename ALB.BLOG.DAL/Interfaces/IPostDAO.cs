using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.DAL.Interfaces
{
    public interface IPostDAO
    {
        /// <summary>
        /// Create a post. 
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        Task Created(Post post);

        /// <summary>
        /// Delete a post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        Task Delete(Post post);

        /// <summary>
        /// Helper for updating a post.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        void Entity(int id, Post post);

        /// <summary>
        /// Get all posts.
        /// </summary>
        /// <returns></returns>
        Task<List<Post>> GetAllPost();

        /// <summary>
        /// Get all posts in descending order.
        /// </summary>
        /// <returns></returns>
        Task<List<Post>> GetAllPostOrderByDescending();

        /// <summary>
        /// Get all posts with a filter.
        /// </summary>
        /// <param name="searchFilter"></param>
        /// <returns></returns>
        Task<List<Post>> GetAllPostSearch(string? searchFilter = null);

        /// <summary>
        /// Get a post by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Post?> GetPostByIdPost(int id);

        /// <summary>
        /// Get a post by User ID.
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        Task<List<Post>> GetPostsByIdUser(string idUser);

        /// <summary>
        /// Get a post by slug.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        Task<Post?> GetPostBySlug(string slug);

        /// <summary>
        /// Perform an update of a post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        Task Update(Post post);
    }
}