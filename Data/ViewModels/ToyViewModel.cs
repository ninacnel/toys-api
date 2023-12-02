namespace Data.ViewModels
{
    public class ToyViewModel
    {
        public int code { get; set; }
        public string name { get; set; }
        public int? category_id { get; set; }
        public string description { get; set; }
        public string image_path { get; set; }
        //public byte[] toy_img { get; set; }
        public int stock { get; set; }
        public int stock_threshold { get; set; }
        public decimal price { get; set; }
        //public byte[] qr_code { get; set; }
    }
}
