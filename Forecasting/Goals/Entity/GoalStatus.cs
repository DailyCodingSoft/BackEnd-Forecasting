using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forecasting.Goals.Entity
{
    [Table("goal_status")]
    public class GoalStatus
    {
        [Key]
        [Column("goal_status_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }

        [Required]
        [Column("code")]
        [MaxLength(10)]
        public required string Code { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(100)]
        public required string Name { get; set; }
    }
}
