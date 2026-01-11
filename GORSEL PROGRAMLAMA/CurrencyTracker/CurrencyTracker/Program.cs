using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CurrencyTracker.Models;

namespace CurrencyTracker
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static List<Currency> currencyList = new List<Currency>();

        static async Task Main(string[] args)
        {
            Console.WriteLine("Veriler API'den çekiliyor, lütfen bekleyin...");
            await FetchDataAsync();

            bool showMenu = true;
            while (showMenu)
            {
                Console.WriteLine("\n===== CurrencyTracker =====");
                Console.WriteLine("1. Tüm dövizleri listele");
                Console.WriteLine("2. Koda göre döviz ara");
                Console.WriteLine("3. Belirli bir değerden büyük dövizleri listele");
                Console.WriteLine("4. Dövizleri değere göre sırala");
                Console.WriteLine("5. İstatistiksel özet göster");
                Console.WriteLine("0. Çıkış");
                Console.Write("Seçiminiz: ");

                // Null check eklendi
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": ListAll(); break;
                    case "2": SearchByCode(); break;
                    case "3": ListGreater(); break;
                    case "4": SortData(); break;
                    case "5": ShowStats(); break;
                    case "0": showMenu = false; break;
                    default: Console.WriteLine("Hatalı seçim!"); break;
                }
            }
        }

        static async Task FetchDataAsync()
        {
            try
            {
                string url = "https://api.frankfurter.app/latest?from=TRY";
                var response = await client.GetStringAsync(url);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<CurrencyResponse>(response, options);

                // API verisi ve Rates dictionary'si null değilse listeye çevir
                if (result?.Rates != null)
                {
                    // LINQ Select kullanımı
                    currencyList = result.Rates.Select(r => new Currency
                    {
                        Code = r.Key,
                        Rate = r.Value
                    }).ToList();
                }
                else
                {
                    Console.WriteLine("API'den veri alınamadı.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }

        static void ListAll()
        {
            // LINQ Select ile listeleme
            var list = currencyList.Select(c => $"{c.Code}: {c.Rate}");
            foreach (var item in list)
                Console.WriteLine(item);
        }

        static void SearchByCode()
        {
            Console.Write("Kod girin (Örn: USD): ");
            // Kullanıcı girişinde null kontrolü ve büyük harfe çevirme
            string code = (Console.ReadLine() ?? "").ToUpper();

            // LINQ Where kullanımı
            var found = currencyList.FirstOrDefault(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(found != null ? $"{found.Code}: {found.Rate}" : "Bulunamadı.");
        }

        static void ListGreater()
        {
            Console.Write("Alt sınır değerini girin: ");
            string input = Console.ReadLine() ?? "0";

            if (decimal.TryParse(input, out decimal limit))
            {
                // LINQ Where kullanımı
                var filtered = currencyList.Where(c => c.Rate > limit).ToList();
                foreach (var c in filtered)
                    Console.WriteLine($"{c.Code}: {c.Rate}");
            }
            else
            {
                Console.WriteLine("Geçersiz sayısal değer.");
            }
        }

        static void SortData()
        {
            // LINQ OrderBy kullanımı (Değere göre küçükten büyüğe)
            var sorted = currencyList.OrderBy(c => c.Rate).ToList();
            foreach (var c in sorted)
                Console.WriteLine($"{c.Code}: {c.Rate}");
        }

        static void ShowStats()
        {
            if (!currencyList.Any())
            {
                Console.WriteLine("Veri listesi boş.");
                return;
            }

            // LINQ Count, Max, Min, Average kullanımı
            int totalCount = currencyList.Count();
            decimal maxRate = currencyList.Max(c => c.Rate);
            decimal minRate = currencyList.Min(c => c.Rate);
            double avgRate = (double)currencyList.Average(c => c.Rate);

            Console.WriteLine($"--- İstatistiksel Özet ---");
            Console.WriteLine($"Toplam Döviz Sayısı: {totalCount}");
            Console.WriteLine($"En Yüksek Kur: {maxRate}");
            Console.WriteLine($"En Düşük Kur: {minRate}");
            Console.WriteLine($"Ortalama Kur: {avgRate:F4}");
        }
    }
}