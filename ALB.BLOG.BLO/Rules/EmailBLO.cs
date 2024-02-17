using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
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
        public async Task<string> SendEmail(EmailVM emailVM)
        {
            try
            {
                Email emailAdd;
                var macAddress = "";
                DateTime dateNow = DateTime.Now.AddHours(1);

                if (emailVM == null)
                    return "Preencha os dados antes de enviar o e-mail!";

                var validateMacAddress = await _generalBlogServices.ValidateMac();

                var listMacAddress = await GetAllEmail();

                foreach (var itemMacAddress in listMacAddress)
                {
                    if (itemMacAddress.MacAddress == validateMacAddress && itemMacAddress.ShippingDate < dateNow)
                    {
                        return "Você já enviou um e-mail, aguarde 1 hora para enviar outro!";
                    }
                }

                if (validateMacAddress != null)
                {
                    macAddress = validateMacAddress;
                }
                else
                {
                    return "Ocorreu um erro, tente mais tarde!";
                }

                var returnEnvioEmail = _generalBlogServices.SendEmail(emailVM);

                if (returnEnvioEmail == false)
                    return "O e-mail não foi enviado, tente novamente em alguns minutos!";

                emailAdd = new Email(emailVM.Name, emailVM.UserEmail, emailVM.Subject, emailVM.Message, macAddress, DateTime.Now);

                await Create(emailAdd);

                return "E-mail enviado com sucesso!";
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