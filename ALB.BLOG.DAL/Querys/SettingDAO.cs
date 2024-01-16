using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DOMAIN.Models;
using ALB.BLOG.INFRA.DbContextConnections;
using Microsoft.EntityFrameworkCore;

namespace ALB.BLOG.DAL.Querys
{
    public class SettingDAO : ISettingDAO
    {
        private readonly ApplicationDbContext _context;

        public SettingDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a configuration.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public async Task Created(Setting setting)
        {
            await _context.Settings.AddAsync(setting);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a configuration.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public async Task Delete(Setting setting)
        {
            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Helper for updating a configuration.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="setting"></param>
        public void Entity(int id, Setting setting)
        {
            var existingEntity = _context.Settings.Local.FirstOrDefault(e => e.Id == id);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            _context.Attach(setting);
            _context.Entry(setting).State = EntityState.Modified;
        }

        /// <summary>
        /// Get all configurations.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Setting?>> GetAllSetting()
        {
            return await _context.Settings!.ToListAsync();
        }

        /// <summary>
        /// Get a configuration by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Setting?> GetSettingById(int id)
        {
            return await _context.Settings!.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Perform an update of a configuration.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public async Task Update(Setting setting)
        {
            _context.Settings.Update(setting);
            await _context.SaveChangesAsync();
        }
    }
}