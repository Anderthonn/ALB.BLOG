using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DOMAIN.Models;
using ALB.BLOG.INFRA.DbContextConnections;
using Microsoft.EntityFrameworkCore;

namespace ALB.BLOG.DAL.Querys
{
    public class PostCategoryDAO : IPostCategoryDAO
    {
        private readonly ApplicationDbContext _context;

        public PostCategoryDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a post category.
        /// </summary>
        /// <param name="postCategory"></param>
        /// <returns></returns>
        public async Task Create(PostCategory postCategory)
        {
            _context.PostsCategories.Add(postCategory);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a post category.
        /// </summary>
        /// <param name="postCategory"></param>
        /// <returns></returns>
        public async Task Delete(PostCategory postCategory)
        {
            _context.PostsCategories.Remove(postCategory);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all posts categories.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PostCategory>> GetAllPostCategory()
        {
            return await _context.PostsCategories.Include(c => c.Category).Include(c => c.Post).ToListAsync();
        }

        /// <summary>
        /// Get posts categories by id post.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PostCategory>> GetPostCategoryByIdPost(int id)
        {
            return await _context.PostsCategories.Include(c => c.Category).Where(c => c.PostId == id).ToListAsync();
        }

        /// <summary>
        /// Get posts categories by id post and category.
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<PostCategory?> GetPostCategoryByIdPostAndCategory(int postId, int categoryId)
        {
            return await _context.PostsCategories.Include(c => c.Category).Where(c => c.PostId == postId && c.CategoryId == categoryId).FirstOrDefaultAsync();
        }
    }
}