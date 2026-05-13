using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forecasting.Goals.Entity
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [Column("category_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required]
        [Column("code")]
        [MaxLength(10)]
        public string Code { get; set; } = string.Empty; // Example: "0001"

        [Required]
        [Column("name")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public override string ToString()
        {
            return $"CategoryId: {CategoryId}, " +
                $"Code: {Code}, " +
                $"Name: {Name}";
        }
    }
}
