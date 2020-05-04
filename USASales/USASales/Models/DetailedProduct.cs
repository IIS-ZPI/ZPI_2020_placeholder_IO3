using System.Collections.Generic;

namespace USASales.Models
{
    public class DetailedProduct
    {
        public Product Product { get; set; }
        public IEnumerable<DetailedPrice> PriceInStates { get; set; }
    }
}
