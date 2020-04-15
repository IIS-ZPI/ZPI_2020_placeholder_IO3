namespace USASales.Models
{
    public class Tax
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public float TaxPercentage { get; set; }
        public float ThresholdUsd { get; set; }
    }
}