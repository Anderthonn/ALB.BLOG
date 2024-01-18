using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.DAL.Interfaces
{
    public interface ISettingDAO
    {
        /// <summary>
        /// Create a configuration.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        Task Created(Setting setting);

        /// <summary>
        /// Helper for updating a configuration.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="setting"></param>
        void Entity(int id, Setting setting);

        /// <summary>
        /// Get all configurations.
        /// </summary>
        /// <returns></returns>
        Task<List<Setting?>> GetAllSetting();

        /// <summary>
        /// Get a configuration by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Setting?> GetSettingById(int id);

        /// <summary>
        /// Perform an update of a configuration.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        Task Update(Setting setting);
    }
}