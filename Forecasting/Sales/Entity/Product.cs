using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forecasting.Sales.Entity
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("product_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [MaxLength(10)]
        [Column("identificator")]
        public required string Identificator { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("product_name")]
        public required string ProductName { get; set; }

        [Column("price", TypeName = "numeric(10,2)")]
        public decimal ProductPrice { get; set; }
        public required List<Sale> Sales { get; set; }
    }
}
