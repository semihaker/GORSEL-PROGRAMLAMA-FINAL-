# CurrencyTracker – Final Ödevi

Merhaba Hocam,

Bu projede, istenilen kriterler doğrultusunda Frankfurter API'yi kullanarak Türk Lirası bazlı döviz kurlarını takip eden bir konsol uygulaması geliştirdim. 

## Proje Hakkında
Uygulama başladığında verileri canlı olarak API'den çekiyor ve hafızada (List içerisinde) tutuyor. Menü üzerinden yapmak istediğiniz işlemi seçtiğinizde arka planda LINQ sorguları çalışarak sonuçları ekrana getiriyor.

## Ödevde Yaptığım İşlemler
- **Veri Çekme:** `HttpClient` kullanarak asenkron bir şekilde verileri aldım ve JSON verisini verdiğiniz model sınıflarına (CurrencyResponse ve Currency) parse ettim.
- **LINQ Sorguları:**
    - Tüm listeleme işlemlerinde `.Select` kullandım.
    - Koda göre arama ve filtrelemede `.Where` ve `.FirstOrDefault` kullandım (Büyük/küçük harf duyarlılığını `StringComparison` ile çözdüm).
    - Sıralama menüsünde `.OrderBy` kullandım.
    - İstatistik kısmında ise `.Count`, `.Max`, `.Min` ve `.Average` fonksiyonlarından yararlandım.
- **Hata Yönetimi:** İnternet bağlantısı veya API kaynaklı hatalar için basit `try-catch` blokları ekledim.

## Dikkat Edilen Yasaklar
- Kodun hiçbir yerinde elle yazılmış (hard-coded) döviz verisi yoktur.
- Tüm veri işleme süreçleri döngüler yerine LINQ ile yapılmıştır.
- Proje tamamen konsol tabanlıdır (GUI kullanılmamıştır).

## Bilgilerim
- **Ad Soyad:** [SEMİH AKER]
- **Öğrenci Numarası:** [20230108033]
- **Bölüm:** [BİLGİSAYAR PROGRAMCILIĞI]

Saygılarımla.
