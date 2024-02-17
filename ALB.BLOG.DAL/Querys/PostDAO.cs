using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DOMAIN.Models;
using ALB.BLOG.INFRA.DbContextConnections;
using Microsoft.EntityFrameworkCore;

namespace ALB.BLOG.DAL.Querys
{
    public class PostDAO : IPostDAO
    {
        private readonly ApplicationDbContext _context;

        public PostDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<Post> Created(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        /// <summary>
        /// Delete a post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task Delete(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Helper for updating a post.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        public void Entity(int id, Post post)
        {
            var existingEntity = _context.Posts.Local.FirstOrDefault(e => e.Id == id);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            _context.Attach(post);
            _context.Entry(post).State = EntityState.Modified;
        }

        /// <summary>
        /// Get all posts.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Post>> GetAllPost()
        {
            return await _context.Posts!.Include(x => x.ApplicationUser).ToListAsync();
        }

        /// <summary>
        /// Get all posts in descending order.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Post>> GetAllPostOrderByDescending()
        {
            return await _context.Posts!.Include(x => x.ApplicationUser).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        /// <summary>
        /// Get all posts with a filter.
        /// </summary>
        /// <param name="searchFilter"></param>
        /// <returns></returns>
        public async Task<List<Post>> GetAllPostSearch(string? searchFilter = null, int[]? categories = null)
        {
            var posts = await _context.Posts!
                                      .Include(x => x.ApplicationUser)
                                      .Include(x => x.PostCategories).ThenInclude(x => x.Category)
                                      .ToListAsync();

            if (!string.IsNullOrEmpty(searchFilter))
            {
                var filter = searchFilter.Trim().ToLowerInvariant();

                posts = posts.Where(p =>
                    p.Title.Trim().ToLower().Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                    p.ShortDescription.Trim().ToLower().Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                    p.Slug.Trim().ToLower().Contains(filter, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (categories != null && categories.Length > 0)
            {
                posts = posts.Where(p => p.PostCategories.Any(c => categories.Contains(c.CategoryId))).ToList();
            }

            return posts;
        }

        /// <summary>
        /// Get a post by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Post?> GetPostByIdPost(int id)
        {
            return await _context.Posts!.Include(x => x.ApplicationUser).Include(x => x.PostCategories).ThenInclude(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Get a post by User ID.
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public async Task<List<Post>> GetPostsByIdUser(string idUser)
        {
            return await _context.Posts!.Include(x => x.ApplicationUser).Where(x => x.ApplicationUser!.Id == idUser!).ToListAsync();
        }

        /// <summary>
        /// Get a post by slug.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public async Task<Post?> GetPostBySlug(string slug)
        {
            return await _context.Posts!.Include(x => x.ApplicationUser).Include(x => x.PostCategories).ThenInclude(x => x.Category).FirstOrDefaultAsync(x => x.Slug == slug);
        }

        /// <summary>
        /// Perform an update of a post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task Update(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }
    }
}