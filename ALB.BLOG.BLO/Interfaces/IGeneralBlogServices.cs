using ALB.BLOG.BLO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ALB.BLOG.BLO.Interfaces
{
    public interface IGeneralBlogServices
    {
        /// <summary>
        /// Base method for sending emails.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool SendEmail(EmailVM emailVM);

        /// <summary>
        /// Image upload.
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        string UploadImage(IWebHostEnvironment webHostEnvironment, IFormFile file);

        /// <summary>
        /// Validation of the MAC address.
        /// </summary>
        /// <returns></returns>
        Task<string?> ValidateMac();
    }
}