namespace Data.DTOs
{
    public class ToyDTO
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] ToyImg { get; set; }
        public int Stock { get; set; }
        public int StockThreshold { get; set; }
        public bool? State { get; set; }
        public decimal Price { get; set; }
        public List<PriceDTO> PriceHistory { get; set; }
    }
}