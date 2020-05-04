namespace USASales.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal WholesalePrice { get; set; }
        public decimal GrossPrice { get; set; }
    }
}