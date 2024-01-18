using ALB.BLOG.BLO.ViewModels;

namespace ALB.BLOG.BLO.Interfaces
{
    public interface IEmailBLO
    {
        /// <summary>
        /// Email sending.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<string> SendEmail(EmailVM emailVM);
    }
}