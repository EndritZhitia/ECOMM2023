namespace ECOMM2023.ViewModels
{
    public class CreateProductViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string Category { get; set; }

        public string Producer { get; set; }

        public IFormFile Image { get; set; }
    }
}
