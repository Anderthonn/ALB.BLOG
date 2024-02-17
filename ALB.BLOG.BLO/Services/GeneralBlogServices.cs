using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;

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
                MailMessage mail = new MailMessage();
                string senderEmail = _configuration.GetSection("ConnectionEmail").GetSection("EmailHost").Value;
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(new MailAddress(_configuration.GetSection("ConnectionEmail").GetSection("Email").Value));
                mail.Subject = emailVM.Subject;

                StringBuilder body = new StringBuilder();
                body.AppendLine("Contact name: " + emailVM.Name);
                body.AppendLine("Contact E-mail:" + emailVM.UserEmail);
                body.AppendLine("Subject: " + emailVM.Subject);
                body.AppendLine("Message: " + emailVM.Message);

                mail.Body = body.ToString();

                SmtpClient smtpClient = new SmtpClient(_configuration.GetSection("ConnectionEmail").GetSection("Host").Value);
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(senderEmail, _configuration.GetSection("ConnectionEmail").GetSection("Password").Value);
                smtpClient.Send(mail);

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