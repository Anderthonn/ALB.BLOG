using ALB.BLOG.BLO.ViewModels;
using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.BLO.Interfaces
{
    public interface ISettingBLO
    {
        /// <summary>
        /// Create a setting.
        /// </summary>
        /// <returns></returns>
        Task<SettingVM?> Created();

        /// <summary>
        /// Get all settings.
        /// </summary>
        /// <returns></returns>
        Task<List<Setting?>> GetAllSetting();

        /// <summary>
        /// Get Core Configuration VM.
        /// </summary>
        /// <returns></returns>
        Task<SettingVM?> GetMainSettingVM();

        /// <summary>
        /// Get a setting by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Setting?> GetSettingById(int id);

        /// <summary>
        /// Update a setting.
        /// </summary>
        /// <param name="settingVM"></param>
        /// <returns></returns>
        Task<bool> Update(SettingVM settingVM);
    }
}