using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALB.BLOG.DOMAIN.Models
{
    [Table(name: "tb_post_category")]
    public class PostCategory
    {
        #region Properties
        [Key]
        [Column(name: "id")]
        public int Id { get; set; }

        [Column(name: "post_id")]
        public int PostId { get; set; }

        [Column(name: "category_id")]
        public int CategoryId { get; set; }

        [ForeignKey(name: "PostId")]
        public Post Post { get; set; }

        [ForeignKey(name: "CategoryId")]
        public Category Category { get; set; }
        #endregion

        #region Constructor
        public PostCategory(int id, int postId, int categoryId)
        {
            Id = id;
            PostId = postId;
            CategoryId = categoryId;
        }

        public PostCategory(int postId, int categoryId)
        {
            PostId = postId;
            CategoryId = categoryId;
        }
        #endregion
    }
}