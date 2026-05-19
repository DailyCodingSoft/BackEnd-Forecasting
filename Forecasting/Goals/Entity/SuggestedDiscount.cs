using Forecasting.Products.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forecasting.Goals.Entity
{
    [Table("suggested_discount")]
    public class SuggestedDiscount
    {
        [Key]
        [Column("suggested_discount_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SuggestedDiscountId { get; set; }

        [Required]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Required]
        [Column("goal_id")]
        public int GoalId { get; set; }

        [Column("minimum_price", TypeName = "numeric(10,2)")]
        public decimal MinimumPrice { get; set; }

        [Column("maximum_price", TypeName = "numeric(10,2)")]
        public decimal MaximumPrice { get; set; }

        //Navigation
        public Product? Product { get; set; }
        public Goal? Goal { get; set; }
    }
}