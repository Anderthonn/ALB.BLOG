using ALB.BLOG.BLO.ViewModels;
using ALB.BLOG.DOMAIN.Models;
using X.PagedList;

namespace ALB.BLOG.BLO.Interfaces
{
    public interface IPostBLO
    {
        /// <summary>
        /// Create a post.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="createPostVM"></param>
        /// <returns></returns>
        Task Create(string userName, CreatePostVM createPostVM);

        /// <summary>
        /// Delete a post.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(string userName, int id);

        /// <summary>
        /// Get all posts.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<IPagedList<PostVM>> GetAllPost(int? page);

        /// <summary>
        /// Get all posts with a filter.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchFilter"></param>
        /// <returns></returns>
        Task<IPagedList<Post>> GetAllPostSearch(int? page, string? searchFilter = null);

        /// <summary>
        /// Get post by ID and validate the user by name has access.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CreatePostVM?> GetEditPost(string userName, int id);

        /// <summary>
        /// Take the list of posts, complete or by user, and add pagination.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<IPagedList<PostVM>> GetIndexPost(string userName, int? page);

        /// <summary>
        /// Get a post by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CreatePostVM?> GetPostByIdPost(int id);

        /// <summary>
        /// Get a post by User ID.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        Task<IPagedList<PostVM>> GetPostsByIdUser(int? page, string idUser);

        /// <summary>
        /// Get a post by slug.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        Task<BlogPostVM?> GetPostBySlug(string slug);

        /// <summary>
        /// Update a post.
        /// </summary>
        /// <param name="createPostVM"></param>
        /// <returns></returns>
        Task<bool> Update(CreatePostVM createPostVM);
    }
}