using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DOMAIN.Models;
using ALB.BLOG.INFRA.DbContextConnections;
using Microsoft.EntityFrameworkCore;

namespace ALB.BLOG.DAL.Querys
{
    public class CategoryDAO : ICategoryDAO
    {
        private readonly ApplicationDbContext _context;

        public CategoryDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task Create(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task Delete(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Helper for updating a category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        public void Entity(int id, Category category)
        {
            var existingEntity = _context.Categories.Local.FirstOrDefault(e => e.Id == id);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            _context.Categories.Attach(category);
            _context.Categories.Entry(category).State = EntityState.Modified;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await _context.Categories.ToListAsync();
        }

        /// <summary>
        /// Get a category by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Category> GetCategoryByCategoryId(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Perform an update of a category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task Update(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}