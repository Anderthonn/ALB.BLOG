using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALB.BLOG.DOMAIN.Models
{
    [Table(name: "tb_post")]
    public class Post
    {
        #region Properties
        [Key]
        [Column(name: "id")]
        public int Id { get; private set; }

        [Column(name: "title")]
        [Display(Name = "Title: ", ShortName = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Title")]
        [MinLength(50, ErrorMessage = "Your title must have at least 50 characters")]
        [MaxLength(150, ErrorMessage = "Your title must have a maximum of 150 characters")]
        public string? Title { get; private set; }

        [Column(name: "short_description")]
        [Display(Name = "Short Description: ", ShortName = "Short Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter short description")]
        [MinLength(100, ErrorMessage = "Your short description must have at least 100 characters")]
        [MaxLength(300, ErrorMessage = "Your short description must have a maximum of 250 characters")]
        public string? ShortDescription { get; private set; }

        [Column(name: "application_user_id")]
        public string? ApplicationUserId { get; private set; }

        [Column(name: "created_date")]
        [Display(Name = "Created Date: ", ShortName = "Created Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter created date")]
        public DateTime CreatedDate { get; private set; } = DateTime.Now;

        [Column(name: "description")]
        [Display(Name = "Description: ", ShortName = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter description")]
        public string? Description { get; private set; }

        [Column(name: "slug")]
        [Display(Name = "Slug: ", ShortName = "Slug")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Slug")]
        public string? Slug { get; private set; }

        [Column(name: "thumbnail_url")]
        [Display(Name = "Thumbnail Url: ", ShortName = "Thumbnail Url")]
        public string? ThumbnailUrl { get; private set; }

        public List<PostCategory> PostCategories { get; private set; }

        [ForeignKey(name: "ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; private set; }
        #endregion

        #region Constructor
        public Post(int id, string title, string shortDescription, string applicationUserId, DateTime createdDate, string description, string slug, string thumbnailUrl)
        {
            Id = id;
            Title = title;
            ShortDescription = shortDescription;
            ApplicationUserId = applicationUserId;
            CreatedDate = createdDate;
            Description = description;
            Slug = slug;
            ThumbnailUrl = thumbnailUrl;
        }

        public Post(string title, string shortDescription, string applicationUserId, DateTime createdDate, string description, string slug, string thumbnailUrl)
        {
            Title = title;
            ShortDescription = shortDescription;
            ApplicationUserId = applicationUserId;
            CreatedDate = createdDate;
            Description = description;
            Slug = slug;
            ThumbnailUrl = thumbnailUrl;
        }
        #endregion
    }
}