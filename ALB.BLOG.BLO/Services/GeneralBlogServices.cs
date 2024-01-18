using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        public bool SendEmail(EmailVM emailVM)
        {
            try
            {
                MailMessage oEmail = new MailMessage();
                MailAddress sDe = new MailAddress(emailVM.UserEmail);

                oEmail.To.Add(_configuration.GetSection("ConnectionEmail").GetSection("Email").Value);
                oEmail.From = sDe;
                oEmail.Priority = MailPriority.Normal;
                oEmail.IsBodyHtml = false;
                oEmail.Subject = emailVM.Subject;
                oEmail.Body = "Contact name: " + emailVM.Name + "Contact E-mail:" + emailVM.UserEmail + "Subject: " + emailVM.Subject + "Message: " + emailVM.Message;

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
        /// Image upload.
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string UploadImage(IWebHostEnvironment webHostEnvironment, IFormFile file)
        {
            try
            {
                string uniqueFileName = "";

                var folderPath = Path.Combine(webHostEnvironment.WebRootPath, "thumbnails");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(folderPath, uniqueFileName);

                using (FileStream fileStream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fileStream);
                }

                return uniqueFileName;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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