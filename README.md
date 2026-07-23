# Hotel Reservation API

Otel odaları, müşteriler ve rezervasyonların yönetimi için geliştirilen .NET 8 tabanlı REST API projesidir. Proje, staj süresince katmanlı mimari, veri erişimi, iş kuralları ve API geliştirme pratiklerini uygulamak amacıyla hazırlanmıştır.

## Kullanılan Teknolojiler

* .NET 8 / ASP.NET Core Web API
* Entity Framework Core 8
* PostgreSQL ve Npgsql Entity Framework sağlayıcısı
* Swagger / OpenAPI
* Code First migrations

## Mimari

Uygulama sorumlulukların ayrılması için katmanlı bir yapıda tasarlanmıştır:

* **Models:** `Room`, `Customer` ve `Reservation` alan modelleri.
* **DTOs:** Rezervasyon oluşturma, güncelleme ve listeleme veri taşıma nesneleri.
* **Data:** `AppDbContext` ve Entity Framework Core yapılandırması.
* **Repository:** Veritabanı erişim işlemlerini soyutlayan repository arayüzleri ve sınıfları.
* **Service:** Fiyat hesaplama, tarih doğrulama ve oda uygunluğu gibi iş kurallarının yer aldığı katman.
* **Controllers:** HTTP isteklerini karşılayan REST API uç noktaları.

## Başlıca Özellikler

* Oda, müşteri ve rezervasyon için CRUD işlemleri
* Rezervasyon tarih çakışması kontrolü
* Konaklama süresi ve oda gecelik fiyatına göre sunucu tarafında toplam ücret hesaplama
* Rezervasyon listelemede oda/müşteri filtresi ve sayfalama
* Uygun odaların tarih aralığına göre sorgulanması
* Oda numarasının benzersizliği ve ilişkisel veriler için silme kısıtları
* Swagger arayüzü ile API dokümantasyonu ve test imkânı

## API Uç Noktaları

| Kaynak | Uç noktalar |
| --- | --- |
| Oda | `GET`, `POST /api/Room`; `GET`, `PUT`, `DELETE /api/Room/{id}`; `GET /api/Room/available` |
| Müşteri | `GET`, `POST /api/Customer`; `GET`, `PUT`, `DELETE /api/Customer/{id}` |
| Rezervasyon | `GET`, `POST /api/Reservation`; `GET`, `PUT`, `DELETE /api/Reservation/{id}` |

`GET /api/Reservation` uç noktası `roomId`, `customerId`, `pageNumber` ve `pageSize` sorgu parametrelerini destekler. `GET /api/Room/available` için `checkInDate` ve `checkOutDate` parametreleri kullanılır.

## Çalıştırma

1. .NET 8 SDK ve PostgreSQL'i kurun.
2. Bağlantı dizesini kullanıcı gizli ayarlarına veya ortam değişkenine ekleyin:

   ```powershell
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=HotelReservationDb;Username=<kullanici>;Password=<parola>" --project .\HotelReservationAPI\HotelReservationAPI.csproj
   ```

3. Veritabanı migration'larını uygulayın:

   ```powershell
   dotnet ef database update --project .\HotelReservationAPI\HotelReservationAPI.csproj
   ```

4. Uygulamayı başlatın:

   ```powershell
   dotnet run --project .\HotelReservationAPI\HotelReservationAPI.csproj
   ```

Geliştirme ortamında Swagger arayüzü varsayılan olarak `/swagger` yolunda açılır.


