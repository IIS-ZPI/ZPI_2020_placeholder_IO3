using System.Runtime.InteropServices.ComTypes;

namespace USASales.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int TaxId { get; set; }
        public decimal NetPriceUsd { get; set; }
    }
}