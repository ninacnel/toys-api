namespace Data.ViewModels
{
    public class OrderViewModel
    {
        public int order_id { get; set; }
        public int? client_id { get; set; }
        public decimal total_amount { get; set; }
        public DateTime? order_date { get; set; }
        public bool? state { get; set; }
        public List<OrderLineViewModel> order_lines { get; set; }
    }

    public class OrderLineViewModel
    {
        public int? order_id { get; set; }
        public int order_line_id { get; set; }
        public int? toy_code { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal sub_total { get; set; }
        public bool? wrapped { get; set; }
    }
}
