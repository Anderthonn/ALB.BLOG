using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DOMAIN.Models;
using Microsoft.AspNetCore.Hosting;

namespace ALB.BLOG.BLO.Rules
{
    public class SettingBLO : ISettingBLO
    {
        private readonly ISettingDAO _settingDAO;
        private readonly IGeneralBlogServices _generalBlogServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SettingBLO(ISettingDAO settingDAO, IGeneralBlogServices generalBlogServices, IWebHostEnvironment webHostEnvironment)
        {
            _settingDAO = settingDAO;
            _generalBlogServices = generalBlogServices;
            _webHostEnvironment = webHostEnvironment;
        }

        #region Public Methods
        /// <summary>
        /// Create a setting.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<SettingVM?> Created()
        {
            try
            {
                var settings = await _settingDAO.GetAllSetting();

                if (settings.Count >= 0)
                {
                    var settingVM = new SettingVM()
                    {
                        Id = settings[0].Id,
                        SiteName = settings[0].SiteName,
                        Title = settings[0].Title,
                        ShortDescription = settings[0].ShortDescription,
                        ThumbnailUrl = settings[0].ThumbnailUrl,
                        GithubUrl = settings[0].GithubUrl,
                        LinkedInUrl = settings[0].LinkedInUrl,
                        InstagramUrl = settings[0].InstagramUrl,
                        TwitterUrl = settings[0].TwitterUrl
                    };

                    return settingVM;
                }

                Setting setting = new Setting(null, "Demo Name", null, null, null, null, null, null);

                await _settingDAO.Created(setting);

                var createdSettings = await _settingDAO.GetAllSetting();

                var createdSettingsVM = new SettingVM()
                {
                    Id = createdSettings[0].Id,
                    SiteName = createdSettings[0].SiteName,
                    Title = createdSettings[0].Title,
                    ShortDescription = createdSettings[0].ShortDescription,
                    ThumbnailUrl = createdSettings[0].ThumbnailUrl,
                    GithubUrl = settings[0].GithubUrl,
                    LinkedInUrl = settings[0].LinkedInUrl,
                    InstagramUrl = settings[0].InstagramUrl,
                    TwitterUrl = settings[0].TwitterUrl
                };

                return createdSettingsVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all settings.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<Setting?>> GetAllSetting()
        {
            try
            {
                return await _settingDAO.GetAllSetting();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Core Configuration VM.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<SettingVM?> GetMainSettingVM()
        {
            try
            {
                var settingVM = new SettingVM();

                var setting = await _settingDAO.GetSettingById(1);

                if (setting != null)
                {
                    settingVM.SiteName = setting.SiteName;
                    settingVM.GithubUrl = setting.GithubUrl;
                    settingVM.LinkedInUrl = setting.LinkedInUrl;
                    settingVM.InstagramUrl = setting.InstagramUrl;
                    settingVM.TwitterUrl = setting.TwitterUrl;
                }

                return settingVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get a setting by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Setting?> GetSettingById(int id)
        {
            try
            {
                var setting = await _settingDAO.GetSettingById(id);

                if (setting == null)
                    throw new Exception("No configuration with the given ID was found!");

                return setting;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update a setting.
        /// </summary>
        /// <param name="settingVM"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(SettingVM settingVM)
        {
            try
            {
                Setting setting;
                string thumbnailUrl = null;

                var getSetting = await _settingDAO.GetSettingById(1);

                if (getSetting == null)
                {
                    return false;
                }

                if (settingVM.Thumbnail != null)
                {
                    thumbnailUrl = _generalBlogServices.UploadImage(_webHostEnvironment, settingVM.Thumbnail);
                }

                setting = new Setting(1, settingVM.SiteName, settingVM.Title, settingVM.ShortDescription, thumbnailUrl, settingVM.GithubUrl, settingVM.LinkedInUrl, settingVM.InstagramUrl, settingVM.TwitterUrl);

                Entity(1, setting);
                await _settingDAO.Update(setting);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Helper for updating a setting.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="setting"></param>
        /// <exception cref="Exception"></exception>
        private void Entity(int id, Setting setting)
        {
            try
            {
                if (setting == null)
                    throw new Exception("Fill the object, it cannot be empty!");

                _settingDAO.Entity(id, setting);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}