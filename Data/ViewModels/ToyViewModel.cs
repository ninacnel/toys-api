using Microsoft.AspNetCore.Http;

namespace Data.ViewModels
{
    public class ToyViewModel
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string Description { get; set; }
        //public string image_path { get; set; }
        public byte[]? image_path { get; set; }
        public int Stock { get; set; }
        public int StockThreshold { get; set; }
        public decimal Price { get; set; }

            public IFormFile? ImageFile { get; set; }
    }

    public class ToyPhotoViewModel
    {
        public int Code { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
