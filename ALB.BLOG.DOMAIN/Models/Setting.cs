using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALB.BLOG.DOMAIN.Models
{
    [Table(name: "tb_setting")]
    public class Setting
    {
        #region Properties
        [Key]
        [Column(name: "id")]
        public int Id { get; private set; }

        [Column(name: "site_name")]
        public string? SiteName { get; private set; }

        [Column(name: "title")]
        public string? Title { get; private set; }

        [Column(name: "short_description")]
        public string? ShortDescription { get; private set; }

        [Column(name: "thumbnail_url")]
        public string? ThumbnailUrl { get; private set; }

        [Column(name: "github_url")]
        public string? GithubUrl { get; private set; }

        [Column(name: "linkedIn_url")]
        public string? LinkedInUrl { get; private set; }

        [Column(name: "instagram_url")]
        public string? InstagramUrl { get; private set; }

        [Column(name: "twitter_url")]
        public string? TwitterUrl { get; private set; }
        #endregion

        #region Constructor
        public Setting(int id, string siteName, string title, string shortDescription, string thumbnailUrl, string githubUrl, string linkedInUrl, string instagramUrl, string twitterUrl)
        {
            Id = id;
            SiteName = siteName;
            Title = title;
            ShortDescription = shortDescription;
            ThumbnailUrl = thumbnailUrl;
            GithubUrl = githubUrl;
            LinkedInUrl = linkedInUrl;
            InstagramUrl = instagramUrl;
            TwitterUrl = twitterUrl;
        }

        public Setting(string siteName, string title, string shortDescription, string thumbnailUrl, string githubUrl, string linkedInUrl, string instagramUrl, string twitterUrl)
        {
            SiteName = siteName;
            Title = title;
            ShortDescription = shortDescription;
            ThumbnailUrl = thumbnailUrl;
            GithubUrl = githubUrl;
            LinkedInUrl = linkedInUrl;
            InstagramUrl = instagramUrl;
            TwitterUrl = twitterUrl;
        }
        #endregion
    }
}