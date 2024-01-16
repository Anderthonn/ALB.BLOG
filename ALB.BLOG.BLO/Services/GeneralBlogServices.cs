using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.DOMAIN.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net.NetworkInformation;

namespace ALB.BLOG.BLO.Services
{
    public class GeneralBlogServices : IGeneralBlogServices
    {
        private readonly IConfiguration _configuration;

        public GeneralBlogServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Base method for sending emails.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool SendEmail(Email email)
        {
            try
            {
                MailMessage oEmail = new MailMessage();
                MailAddress sDe = new MailAddress(email.UserEmail);

                oEmail.To.Add(_configuration.GetSection("ConnectionEmail").GetSection("Email").Value);
                oEmail.From = sDe;
                oEmail.Priority = MailPriority.Normal;
                oEmail.IsBodyHtml = false;
                oEmail.Subject = email.Subject;
                oEmail.Body = "Contact name: " + email.Name + "Contact E-mail:" + email.UserEmail + "Subject: " + email.Subject + "Message: " + email.Message;

                SmtpClient oEnviar = new SmtpClient();

                oEnviar.Host = _configuration.GetSection("ConnectionEmail").GetSection("Host").Value;

                oEnviar.Credentials = new System.Net.NetworkCredential(_configuration.GetSection("ConnectionEmail").GetSection("Email").Value, _configuration.GetSection("ConnectionEmail").GetSection("Password").Value);
                oEnviar.Send(oEmail);
                oEmail.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validation of the MAC address.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string?> ValidateMac()
        {
            try
            {
                NetworkInterface[] networkInterfaces = await Task.Run(() => (NetworkInterface.GetAllNetworkInterfaces()));
                string macAddressValue = "";
                bool foundActiveInterface = false;

                foreach (NetworkInterface networkInterface in networkInterfaces)
                {
                    if (networkInterface.OperationalStatus == OperationalStatus.Up)
                    {
                        PhysicalAddress macAddress = networkInterface.GetPhysicalAddress();
                        byte[] macBytes = macAddress.GetAddressBytes();

                        macAddressValue = BitConverter.ToString(macBytes);
                        foundActiveInterface = true;

                        break;
                    }
                }

                if (foundActiveInterface)
                {
                    return macAddressValue;
                }
                else
                {
                    throw new Exception("Error validating Mac address!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error validating Mac address:" + ex.Message);
            }
        }
    }
}