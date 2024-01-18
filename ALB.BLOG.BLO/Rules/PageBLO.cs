using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DOMAIN.Models;
using Microsoft.AspNetCore.Hosting;

namespace ALB.BLOG.BLO.Rules
{
    public class PageBLO : IPageBLO
    {
        private readonly IGeneralBlogServices _generalBlogServices;
        private readonly IPageDAO _pageDAO;
        private readonly IPostBLO _postBLO;
        private readonly ISettingBLO _settingBLO;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PageBLO(IGeneralBlogServices generalBlogServices, IPageDAO pageDAO, IPostBLO postBLO, ISettingBLO settingBLO, IWebHostEnvironment webHostEnvironment)
        {
            _generalBlogServices = generalBlogServices;
            _pageDAO = pageDAO;
            _postBLO = postBLO;
            _settingBLO = settingBLO;
            _webHostEnvironment = webHostEnvironment;
        }

        #region Public Methods
        /// <summary>
        /// Get the about page.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<PageVM?> GetPageAbout()
        {
            try
            {
                var page = await _pageDAO.GetPageAbout();

                var pageVM = new PageVM()
                {
                    Title = page!.Title,
                    ShortDescription = page.ShortDescription,
                    Description = page.Description,
                    ThumbnailUrl = page.ThumbnailUrl,
                };

                return pageVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get the contact page.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<PageVM?> GetPageContact()
        {
            try
            {
                var page = await _pageDAO.GetPageContact();

                var pageVM = new PageVM()
                {
                    Title = page!.Title,
                    ShortDescription = page.ShortDescription,
                    Description = page.Description,
                    ThumbnailUrl = page.ThumbnailUrl,
                };

                return pageVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get the home page.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<HomeVM?> GetPageHome(int? page, string? searchFilter = null)
        {
            try
            {
                var homeVM = new HomeVM();

                var setting = await _settingBLO.GetAllSetting();

                homeVM.Title = setting[0].Title;
                homeVM.ShortDescription = setting[0].ShortDescription;
                homeVM.ThumbnailUrl = setting[0].ThumbnailUrl;

                homeVM.Posts = await _postBLO.GetAllPostSearch(page, searchFilter);

                return homeVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get the post page.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<PageVM?> GetPagePost()
        {
            try
            {
                var page = await _pageDAO.GetPagePost();

                var pageVM = new PageVM()
                {
                    Title = page!.Title,
                    ShortDescription = page.ShortDescription,
                    Description = page.Description,
                    ThumbnailUrl = page.ThumbnailUrl,
                };

                return pageVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update the about or contact page.
        /// </summary>
        /// <param name="aboutOrContact"></param>
        /// <param name="pageVM"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(string aboutOrContact, PageVM pageVM)
        {
            try
            {
                Page page;
                Page pageAboutOrContact;
                string thumbnailUrl = null;

                if (aboutOrContact == "A")
                {
                    pageAboutOrContact = await _pageDAO.GetPageAbout();
                }
                else if (aboutOrContact == "C")
                {
                    pageAboutOrContact = await _pageDAO.GetPageContact();
                }
                else
                {
                    throw new Exception("The chosen option is not valid, choose between (A - About) or (C - Contact)!");
                }

                if (pageAboutOrContact == null)
                    return false;

                if (pageVM.Thumbnail != null)
                {
                    thumbnailUrl = _generalBlogServices.UploadImage(_webHostEnvironment, pageVM.Thumbnail);
                }

                page = new DOMAIN.Models.Page(pageAboutOrContact.Id, pageVM.Title, pageVM.ShortDescription, pageVM.Description ?? "", pageAboutOrContact.Slug, thumbnailUrl);

                Entity(pageAboutOrContact.Id, page);
                await _pageDAO.Update(page);

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
        /// Helper for updating a page.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <exception cref="Exception"></exception>
        private void Entity(int id, Page page)
        {
            try
            {
                if (page == null)
                    throw new Exception("Fill the object, it cannot be empty!");

                _pageDAO.Entity(id, page);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}