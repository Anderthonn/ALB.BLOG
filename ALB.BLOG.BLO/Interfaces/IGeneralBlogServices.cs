using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.BLO.Interfaces
{
    public interface IGeneralBlogServices
    {
        /// <summary>
        /// Base method for sending emails.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool SendEmail(Email email);

        /// <summary>
        /// Validation of the MAC address.
        /// </summary>
        /// <returns></returns>
        Task<string?> ValidateMac();
    }
}