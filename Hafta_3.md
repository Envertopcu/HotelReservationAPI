# Hafta 3: Ýþ Kurallarý (Business Logic), Güvenlik ve Hata Yönetimi

Bu hafta, otel rezervasyon API'mizi sadece veri tabanýna kayýt (CRUD) yapan basit bir yapýdan çýkarýp; iþ kurallarýný denetleyen, tarih çakýþmalarýný engelleyen ve finansal verileri güvenle hesaplayan "zeki" bir sisteme dönüþtürdük.


### 1. Mimarinin Derinleþtirilmesi ve Dependency Injection (DI)
* Controller, Service ve Repository katmanlarý arasýndaki "Sözleþme (Interface)" mantýðý tam olarak oturtuldu. 
* Gelen HTTP isteklerinin `[Route]` ve `app.MapControllers()` trafik yönlendirmesiyle doðru Controller'a nasýl ulaþtýðý analiz edildi.
* Katmanlarýn birbirini `new` anahtar kelimesiyle oluþturmasý (Tight Coupling) engellendi; bunun yerine Constructor Injection kullanýlarak sýnýflar arasý baðlar esnekleþtirildi.

### 2. Tarih Çakýþma Algoritmasý (Room Availability)
* Bir odanýn ayný tarihlerde iki farklý müþteriye satýlmasýný engellemek için **Kesiþim Kümesi (Date Overlap)** algoritmasý geliþtirildi.
* `IReservationRepository` içerisine `IsRoomAvailableAsync` metodu eklendi.
* Performansý artýrmak için bütün verileri C# tarafýna çekmek yerine, Entity Framework Core'un `AnyAsync` metodu kullanýlarak sorgunun doðrudan PostgreSQL motoru üzerinde çalýþmasý saðlandý.

### 3. Hata Yönetimi (Exception Handling)
* Hatalý iþlemlerin veritabanýna ulaþmasýný engellemek için Service katmanýna filtreler (if bloklarý) eklendi (Örn: Çýkýþ tarihinin giriþ tarihinden önce seçilmesi).
* Hata durumlarýnda Service katmanýndan fýrlatýlan özel `Exception`'lar, Controller katmanýnda global bir `try-catch` bloðu ile yakalandý.
* Sistemin çökmesi (HTTP 500) engellenerek, dýþ dünyaya (Ýstemciye/Swagger'a) açýklayýcý mesajlar içeren `400 Bad Request` yanýtlarý dönülmesi saðlandý.

### 4. Otomatik Fiyat Hesaplama ve Payload Güvenliði
* **"Ýstemciye Asla Güvenme" (Never Trust the Client)** güvenlik prensibi uygulandý. Frontend veya API üzerinden gelen manipüle edilmiþ `TotalPrice` verileri yok sayýldý.
* `ReservationService` içerisine `IRoomRepository` enjekte edilerek, ayný anda iki farklý veritabaný tablosuyla iletiþim kurulmasý saðlandý.
* Müþterinin konaklayacaðý gece sayýsý (Tarih farký) hesaplanýp, odanýn güncel `PricePerNight` (Gecelik Fiyat) bilgisi veritabanýndan çekilerek toplam tutar backend tarafýnda güvenli bir þekilde hesaplandý.