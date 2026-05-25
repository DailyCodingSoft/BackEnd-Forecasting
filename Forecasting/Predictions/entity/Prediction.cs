using Forecasting.Products.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forecasting.Predictions.entity
{
    [Table("predictions")]
    public class Prediction
    {
        [Key]
        [Column("prediction_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PredictionId { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("predicted_week")]
        public DateTime PredictedWeek { get; set; }

        [Column("predicted_sales")]
        public int PredictedSales { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public required Product Product { get; set; }
    }
}
