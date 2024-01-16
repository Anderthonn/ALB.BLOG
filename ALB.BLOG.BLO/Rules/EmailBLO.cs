using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.BLO.Rules
{
    public class EmailBLO : IEmailBLO
    {
        private readonly IEmailDAO _emailDAO;
        private readonly IGeneralBlogServices _generalBlogServices;

        public EmailBLO(IEmailDAO emailDAO, IGeneralBlogServices generalBlogServices)
        {
            _emailDAO = emailDAO;
            _generalBlogServices = generalBlogServices;

        }

        #region Public Methods
        /// <summary>
        /// Email sending.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> SendEmail(Email email)
        {
            try
            {
                Email emailAdd;
                var macAddress = "";
                DateTime dateNow = DateTime.Now.AddHours(1);

                if (email == null)
                    throw new Exception("Fill in the details before sending the email!");

                var returnEnvioEmail = _generalBlogServices.SendEmail(email);

                if (returnEnvioEmail == false)
                    throw new Exception("Email was not sent, please try again in a few minutes!");

                var validateMacAddress = await _generalBlogServices.ValidateMac();

                var listMacAddress = await GetAllEmail();

                foreach (var itemMacAddress in listMacAddress)
                {
                    if (itemMacAddress.MacAddress == validateMacAddress && itemMacAddress.ShippingDate < dateNow)
                    {
                        throw new Exception("You have already sent an email, please wait 1 hour to send another!");
                    }
                }

                if (validateMacAddress != null)
                {
                    macAddress = validateMacAddress;
                }
                else
                {
                    throw new Exception("An error occurred, please try later!");
                }

                emailAdd = new Email(email.Name, email.UserEmail, email.Subject, email.Message, email.MacAddress, DateTime.Now);

                await Create(email);

                return "Email successfully sent!";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Listing of all emails already sent.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task Create(Email email)
        {
            try
            {
                if (email == null)
                    throw new Exception("Object is empty!");

                await _emailDAO.Create(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Creation of the email sending record in the email table.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<List<Email>> GetAllEmail()
        {
            try
            {
                return await _emailDAO.GetAllEmail();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}