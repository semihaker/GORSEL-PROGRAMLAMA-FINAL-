using System.Collections.Generic;

namespace CurrencyTracker.Models
{
    public class CurrencyResponse
    {
        public string Base { get; set; } = string.Empty;
        // Yeni bir dictionary örneği atayarak null uyarısını siliyoruz
        public Dictionary<string, decimal> Rates { get; set; } = new Dictionary<string, decimal>();
    }
}