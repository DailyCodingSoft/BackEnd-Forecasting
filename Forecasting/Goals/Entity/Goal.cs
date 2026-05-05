using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forecasting.Goals.Entity
{
    [Table("goals")]
    public class Goal
    {
        [Key]
        [Column("goal_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GoalId { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Column("progress", TypeName = "numeric(5,2)")]
        public decimal Progress { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("status")]
        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        [Column("bonus", TypeName = "numeric(10,2)")]
        public decimal Bonus { get; set; }

        // Navigation
        public Category Category { get; set; }
    }
}