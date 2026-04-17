using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forecasting.Sales.Entity
{
    [Table("sales")]
    public class Sale
    {
        [Key]
        [Column("sale_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("quantity", TypeName = "numeric(10,2)")]
        public decimal Quantity { get; set; }

        [Column("week")]
        public int Week { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        // Navigation property
        public required Product Product { get; set; }
    }
}
