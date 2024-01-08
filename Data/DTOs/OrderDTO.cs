namespace Data.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int? ClientId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? OrderDate { get; set; }
        public bool? State { get; set; }
        public List<OrderLineDTO> OrderLines { get; set; }
    }

    public class OrderLineDTO
    {
        public int? OrderId { get; set; }
        public int OrderLineId { get; set; }
        public int? ToyCode { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public bool? Wrapped { get; set; }
    }
}
