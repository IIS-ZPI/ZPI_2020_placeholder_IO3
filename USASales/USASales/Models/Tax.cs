namespace USASales.Models
{
    public class Tax
    {
        public long Id { get; set; }
        public string Category { get; set; }
        public string State { get; set; }
        public float TaxPercentage { get; set; }
        public double ThresholdUsd { get; set; }
    }
}