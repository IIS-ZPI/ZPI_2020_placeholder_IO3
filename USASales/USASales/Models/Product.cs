namespace USASales.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public double WholesalePrice { get; set; }
        public double GrossPrice { get; set; }
    }
}