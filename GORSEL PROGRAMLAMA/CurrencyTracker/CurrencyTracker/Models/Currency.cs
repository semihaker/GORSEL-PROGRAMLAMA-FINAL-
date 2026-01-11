namespace CurrencyTracker.Models
{
    public class Currency
    {
        // = string.Empty; ekleyerek null uyarısını siliyoruz
        public string Code { get; set; } = string.Empty;
        public decimal Rate { get; set; }
    }
}