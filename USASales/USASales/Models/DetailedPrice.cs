namespace USASales.Models
{
    public class DetailedPrice
    {
        public double PurchaseAmount { get; set; }
        public double Margin { get; set; }
        public double NetPrice => PurchaseAmount + Margin;
        public double TaxPercentage { get; set; }
        public double TaxValue { get; set; }
        public double GrossPrice { get; set; }
    }
}