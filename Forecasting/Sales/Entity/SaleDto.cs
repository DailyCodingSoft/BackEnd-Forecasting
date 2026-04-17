using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forecasting.Sales.Entity
{
    public class SaleDto
    {
        public required string ProductName { get; set; }

        public required string Identificator { get; set; }

        public decimal Quantity { get; set; }

        public int Week { get; set; }

        public DateTime Date { get; set; }
    }
}
