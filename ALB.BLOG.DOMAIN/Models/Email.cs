using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALB.BLOG.DOMAIN.Models
{
    [Table(name: "tb_email")]
    public class Email
    {
        #region Properties
        [Key]
        [Column(name: "id")]
        public int Id { get; set; }

        [Column(name: "user_name")]
        [Display(Name = "Name: ", ShortName = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter your name")]
        [MinLength(3, ErrorMessage = "Your name must have at least 3 characters")]
        [MaxLength(50, ErrorMessage = "Your name must have a maximum of 50 characters")]
        public string Name { get; set; }

        [Column(name: "user_email")]
        [Display(Name = "Email: ", ShortName = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter your email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string UserEmail { get; set; }

        [Column(name: "subject")]
        [Display(Name = "Subject: ", ShortName = "Subject")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter the subject of the email")]
        [MinLength(3, ErrorMessage = "The subject must have at least 15 characters")]
        [MaxLength(50, ErrorMessage = "Your name must have a maximum of 50 characters")]
        public string Subject { get; set; }

        [Column(name: "message")]
        [Display(Name = "Message: ", ShortName = "Message")]
        [MaxLength(200, ErrorMessage = "Your message must have a maximum of 200 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Write your message")]
        public string Message { get; set; }

        [Column(name: "mac_address")]
        [Display(Name = "MacAddress: ", ShortName = "MacAddress")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Write your message")]
        public string MacAddress { get; set; } = "";

        [Column(name: "shipping_date")]
        [Display(Name = "ShippingDate: ", ShortName = "ShippingDate")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Write your message")]
        public DateTime ShippingDate { get; set; }
        #endregion

        #region Constructor
        public Email(int id, string name, string userEmail, string subject, string message, string macAddress, DateTime shippingDate)
        {
            Id = id;
            Name = name;
            UserEmail = userEmail;
            Subject = subject;
            Message = message;
            MacAddress = macAddress;
            ShippingDate = shippingDate;
        }

        public Email(string name, string userEmail, string subject, string message, string macAddress, DateTime shippingDate)
        {
            Name = name;
            UserEmail = userEmail;
            Subject = subject;
            Message = message;
            MacAddress = macAddress;
            ShippingDate = shippingDate;
        }
        #endregion
    }
}