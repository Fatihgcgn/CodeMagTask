# GS1 L3 Serialization & Aggregation – Technical Case

Bu repository, gönderilen **Senior Yazılım Geliştirici Teknik Case** kapsamında
GS1 standartlarına uygun **Level 3 (L3) serilizasyon ve agregasyon** sürecini
simüle eden bir uygulamayı içermektedir.

Bu çalışma bir “örnek GS1 anlatımı” değil;  
**case’te istenen maddeleri gerçekten yapabildiğimi göstermek** amacıyla hazırlanmıştır.

---

## Uygulamanın Kapsamı

Uygulama aşağıdaki senaryoyu uçtan uca kapsar:

- Müşteri ve müşteri bazlı ürün tanımı
- Ürün için iş emri oluşturma
- İş emrine bağlı seri numarası üretimi
- SSCC üretimi
- Ürün → Koli → Palet agregasyonu
- Oluşan yapının API üzerinden tek response olarak sunulması
- Donanım olmadan otomasyon sürecinin simülasyonu

---

## Mimari Yaklaşım

Uygulama, case’te beklenen şekilde **katmanlı mimari** kullanılarak geliştirilmiştir.

Solution
│
├── WebApi → REST API katmanı
├── Application → İş kuralları ve L3 mantığı
├── Domain → Entity ve çekirdek modeller
├── Infrastructure → EF Core / MSSQL
└── WinForms → Simülasyon ve istemci uygulama


- Controller’lar yalnızca API sorumluluğu taşır
- Serilizasyon ve agregasyon mantığı Application katmanındadır
- UI katmanı (WinForms) iş kuralı içermez
- Yapı, gerçek bir L3 sisteminde servis bazlı ayrıştırmaya uygundur

---

## Kullanılan Teknolojiler

- .NET (ASP.NET Core Web API)
- C#
- MS SQL Server
- Entity Framework Core
- Windows Forms (.NET)
- Dependency Injection

---

## Case Maddeleri ile Uyum

### Müşteri Yönetimi
- Müşteri oluşturma
- Müşteri listeleme
- GLN bilgisiyle müşteri tanımı

---

### Ürün Yönetimi
- Müşteriye bağlı ürün tanımlama
- GTIN bazlı ürün yapısı

---

### İş Emri Yönetimi
- Ürün bazlı iş emri oluşturma
- İş emri üzerinde:
  - Üretim adedi
  - Lot / Batch No (10)
  - Son Kullanma Tarihi (17)
  - Seri numarası başlangıç değeri
  - İş emri durumu

---

### GS1 Identifier Üretimi
İş emrine bağlı olarak aşağıdaki AI’lar otomatik üretilmektedir:

- (01) GTIN
- (21) Seri Numarası
- (17) Son Kullanma Tarihi
- (10) Batch / Lot No

GS1 string çıktısı üretilmekte, fiziksel baskı yapılmamaktadır.

---

### Seri Numarası ve SSCC Yönetimi
- Seri numaraları çakışmasız üretilir
- SSCC (00) oluşturulur
- Aşağıdaki agregasyon yapısı desteklenir:

Palet (SSCC)
└── Koli (SSCC)
└── Ürün Serileri


Parent–child ilişkileri sistemde açık şekilde tutulur.

---

### İş Emri Detay API
Case’te istenen zorunlu endpoint uygulanmıştır.

Bu endpoint tek response içinde:
- İş emri bilgilerini
- Ürün bilgilerini
- Üretilmiş seri numaralarını
- SSCC ve agregasyon yapısını

döndürmektedir.

---

## Otomasyon ve Simülasyon

WinForms uygulaması üzerinden:

- Auto Generate seçildiğinde
- Seriler ve SSCC’ler otomatik üretilir
- Agregasyon yapısı simülasyon olarak oluşturulur

Bu yapı, yazıcı, kamera ve PLC entegrasyonlarının
donanım olmadan simüle edilmesini sağlar.

---

## Logging (WinForms)

WinForms uygulamasında oluşan hatalar ve exception’lar Serilog ile dosyaya kaydedilir.

- UI thread exception’ları `Application.ThreadException` ile yakalanır
- Unhandled / background exception’lar `AppDomain.CurrentDomain.UnhandledException` ile yakalanır
- API çağrılarında oluşan hatalar ApiClient içerisinde loglanır

Log dosyaları günlük olarak (rolling) aşağıdaki klasöre yazılır:

- `WinForm/Logs/winform-log-YYYY-MM-DD.txt`


## Teknik Notlar

- Katmanlı mimari prensiplerine uyulmuştur
- Dependency Injection kullanılmıştır
- Clean Code yaklaşımı benimsenmiştir
- Veritabanında:
  - Foreign Key ilişkileri
  - Unique constraint’ler
  uygulanmıştır
- Hata yönetimi ve loglama genişletilebilir yapıdadır

---

## Teslim

- Kaynak kod
- EF Core migration
- Bu README

---

## Son Not

Bu proje, GS1’i anlatmaktan çok  
**GS1 L3 serilizasyon ve agregasyon sürecini doğru şekilde
modelleyip uygulayabildiğimi** göstermek amacıyla hazırlanmıştır.
