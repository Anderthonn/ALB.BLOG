using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.BLO.Interfaces
{
    public interface IEmailBLO
    {
        /// <summary>
        /// Email sending.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<string> SendEmail(Email email);
    }
}