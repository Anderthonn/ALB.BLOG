using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ALB.BLOG.DOMAIN.Models
{
    [Table(name: "tb_category")]
    public class Category
    {
        #region Properties
        [Key]
        [Column(name: "id")]
        public int Id { get; set; }

        [Column(name: "name")]
        public string? Name { get; set; }

        [Column(name: "color")]
        public string? Color { get; set; }

        [JsonIgnore]
        public List<PostCategory> PostCategories { get; private set; }
        #endregion

        #region Constructor
        public Category(int id, string name, string color)
        {
            Id = id;
            Name = name;
            Color = color;
        }

        public Category(string name, string color)
        {
            Name = name;
            Color = color;
        }
        #endregion
    }
}