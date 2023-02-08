namespace ECOMM2023.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string Category { get; set; }

        public string Producer { get; set; }

        public byte[] Image { get; set; }

    }
}
