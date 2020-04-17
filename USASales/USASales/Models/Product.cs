using System.Runtime.InteropServices.ComTypes;

namespace USASales.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int TaxId { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal GrossPrice { get; set; }
    }
}