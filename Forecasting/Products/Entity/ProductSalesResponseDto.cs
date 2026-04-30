// Serie de tiempo de una semana
public class SalePointDto
{
    public int Week { get; set; }
    public DateTime Date { get; set; }
    public decimal Quantity { get; set; }
}

// Producto con sus ventas agrupadas
public class ProductSalesDto
{
    public int ProductId { get; set; }
    public required string Identificator { get; set; }
    public required string ProductName { get; set; }
    public required List<SalePointDto> Sales { get; set; }
}

// Respuesta raíz
public class ProductSalesResponseDto
{
    public required List<ProductSalesDto> Products { get; set; }
}