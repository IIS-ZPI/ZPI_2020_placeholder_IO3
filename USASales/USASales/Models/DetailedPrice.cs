namespace USASales.Models
{
    public class DetailedPrice
    {
        public double WholesalePrice { get; set; }
        public double Margin { get; set; }
        public double NetPrice => WholesalePrice + Margin;
        public double TaxPercentage { get; set; }
        public double TaxValue { get; set; }
        public double GrossPrice { get; set; }
        public string State { get; set; }
    }
}