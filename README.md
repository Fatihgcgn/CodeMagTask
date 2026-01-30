# GS1 L3 Serialization & Aggregation – Technical Case

Bu repository, **Senior Yazılım Geliştirici Teknik Case** kapsamında  
GS1 standartlarına uygun **Level 3 (L3) serilizasyon ve agregasyon** sürecini
simüle eden bir uygulamayı içermektedir.

Bu çalışma, GS1 kavramlarını teorik olarak anlatmayı değil;  
**case’te istenen maddelerin doğru mimari ve doğru iş akışıyla
uygulanabildiğini** göstermeyi amaçlar.

---

## Projenin Kapsamı

Uygulama aşağıdaki senaryoyu uçtan uca kapsar:

- Müşteri tanımlama ve listeleme
- Müşteriye bağlı ürün tanımlama (GTIN)
- Ürün bazlı iş emri (Work Order) oluşturma
- İş emrine bağlı seri numarası üretimi
- SSCC üretimi
- Ürün → Koli → Palet agregasyonu
- Oluşan yapının API üzerinden tek response olarak sunulması
- Donanım olmadan otomasyon sürecinin simülasyonu (WinForms)

---

## Mimari Yaklaşım

Uygulama, case’te beklenen şekilde **katmanlı mimari** kullanılarak geliştirilmiştir.

Solution
│
├── WebApi → REST API katmanı
├── Application → İş kuralları ve L3 mantığı
├── Data → DbContext, Entity’ler, Migration’lar
└── WinForm → Simülasyon ve istemci uygulama



- Controller’lar yalnızca HTTP sorumluluğu taşır
- Serilizasyon ve agregasyon mantığı Application katmanındadır
- Veritabanı işlemleri Data katmanında yönetilir
- WinForms uygulaması iş kurallarından ayrıdır
- Mimari, gerçek L3 sistemlerinde servis bazlı ayrıştırmaya uygundur


---


## Kullanılan Teknolojiler


- .NET 6
- ASP.NET Core Web API
- C#
- Entity Framework Core
- Microsoft SQL Server (LocalDB veya normal instance)
- Windows Forms (.NET)
- Serilog (file-based logging)


---


## Case Gereksinimleri ile Uyum


### Müşteri Yönetimi
- Müşteri oluşturma
- Müşteri listeleme
- GLN bazlı müşteri modeli


---


### Ürün Yönetimi
- Müşteriye bağlı ürün tanımlama
- GTIN bazlı ürün yapısı


---


### İş Emri (Work Order)
- Ürün bazlı iş emri oluşturma
- İş emri üzerinde:
  - Üretim adedi
  - Lot / Batch No (10)
  - Son Kullanma Tarihi (17)
  - Seri numarası başlangıç değeri
  - İş emri durumu


---


### GS1 Identifier Üretimi
İş emrine bağlı olarak aşağıdaki GS1 Application Identifier’lar otomatik üretilir:


- (01) GTIN
- (21) Seri Numarası
- (17) Son Kullanma Tarihi
- (10) Batch / Lot No


Fiziksel karekod basımı yerine **GS1 string çıktısı** üretilmektedir.


---


### Seri Numarası ve SSCC Yönetimi
- Seri numaraları çakışmasız üretilir
- SSCC (00) oluşturulur
- Aşağıdaki agregasyon yapısı desteklenir:



Palet (SSCC)
└── Koli (SSCC)
└── Ürün Serileri

Agregasyon ilişkileri parent–child mantığıyla tutulur.

---

### İş Emri Detay API
Case’te zorunlu olarak belirtilen endpoint uygulanmıştır.

Bu endpoint tek response içinde:
- İş emri bilgileri
- Ürün bilgileri
- Üretilmiş seri numaraları
- SSCC ve agregasyon yapısı

döndürmektedir.

---

## Otomasyon ve Simülasyon (WinForms)

WinForms uygulaması, donanım bağımlılığı olmadan otomasyon sürecini simüle eder.

- Auto Generate seçeneği ile seri ve SSCC üretimi otomatik yapılır
- Agregasyon yapısı simülasyon olarak oluşturulur
- Yazıcı / kamera / PLC entegrasyonları mock servisler ile temsil edilir

---

## Logging

### WebApi
- Domain seviyesinde fırlatılan hatalar HTTP status code’lara map edilir
- Beklenmeyen hatalar merkezi olarak ele alınır

### WinForms
- UI thread exception’ları otomatik loglanır
- Unhandled / background exception’lar otomatik loglanır
- API çağrılarında oluşan hatalar ApiClient içerisinde loglanır

Loglar günlük olarak aşağıdaki klasöre yazılır:
WinForm/Logs/winform-log-YYYY-MM-DD.txt

## Kurulum (Setup)

Bu proje **.NET 6** ile geliştirilmiştir ve  
**Visual Studio 2019 (16.11+) veya Visual Studio 2022** ile açılabilir.

Migration işlemleri **manuel olarak çalıştırılmak zorunda değildir**.  
Uygulama ilk ayağa kalkarken mevcut migration’ları otomatik olarak uygular.

### Gereksinimler
- .NET 6 SDK
- Visual Studio 2019 (16.11+) veya Visual Studio 2022
- Microsoft SQL Server (LocalDB veya normal instance)

---

### 1) Projeyi klonlayın
```bash
git clone https://github.com/Fatihgcgn/CodeMagTask

Solution dosyasını Visual Studio ile açın.

2) Connection String ayarlayın

WebApi/appsettings.json dosyasında:

{
  "ConnectionStrings": {
    "connString": "Server=.;Database=CodeMagDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}

3) WebApi’yi çalıştırın

WebApi projesini Startup Project yapın

Uygulamayı çalıştırın (F5)

Uygulama başlarken:

Migration’lar otomatik uygulanır

Veritabanı yoksa oluşturulur

Kısa süreli bağlantı sorunları için retry mekanizması vardır

Swagger ekranının açılması backend’in hazır olduğunu gösterir.

4) WinForms uygulamasını çalıştırın

WinForm projesini çalıştırın

ApiClient içindeki baseUrl değerinin WebApi adresi ile aynı olduğundan emin olun
(Örn: https://localhost:7267/)

---

### Uygulamayı Test Etme (Önerilen Sıra)

Müşteri oluşturun

Ürün oluşturun

İş emri oluşturun

İş emirleri için Logistic Unit sekmesinden Paket ve Palet tanımlayın. (Paket için 1 tipli,Palet için 2 tipli Unit tanımlaması yapınız)

Simülasyon ekranında Auto Generate’i açın (varsayılan "1" yazın)

Simülasyonu başlatın (otomatik şekilde GS1 standartına uygun serial oluşturucaktır.)

İş emri detay endpoint’ini kontrol edin

Tanımlamaları yaptıktan sonra Agrasyon yapabilirsiniz .

Agrasyon işlemini gerçekleştirmek için Agrasyon sekmesine gidin

İş emrini seçin ve Serial-Package (seri ürün ile paket bağlama işlemi) yapınız.

İş emrini seçin ve Package-Palet (paket ile Palet bağlama işlemi ) yapınız.

Agrasyon işlemleri aynı sekmede Tree olarak gözükmektedir. Aynı şekilde Work Order sekmesinde Detaya tıklarsanız bütün detaylarıyla Snapshot verilerini görebilirsiniz.

WinForms log klasörünü kontrol ederek logging’i doğrulayın

###Varsayımlar

API ve WinForms aynı makinede çalıştırılır (localhost)

Otomasyon entegrasyonları simülasyon olarak ele alınmıştır

Logging WinForms tarafında file-based olarak yapılmıştır


###Son Not

Bu proje, GS1’i tanımlamaktan ziyade
GS1 L3 serilizasyon ve agregasyon sürecini doğru şekilde
modelleyip uygulayabildiğimi göstermek amacıyla hazırlanmıştır.
