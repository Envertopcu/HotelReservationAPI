# 1. Hafta Raporu


Bu hafta projenin temel iskeleti ve veritabaný altyapýsý baþarýyla kurulmuþtur. Tamamlanan teknik adýmlar aþaðýda özetlenmiþtir:

* **Proje Kurulumu:** .NET Core Web API projesi oluþturuldu ve katmanlý mimariye (Models, Repository, Service, Controllers) uygun klasör yapýsý düzenlendi.
* **Veri Modelleme (Domain Layer):** `Room`, `Customer` ve `Reservation` varlýklarý (entity) Code-First yaklaþýmýna uygun olarak oluþturuldu. Veri bütünlüðünü saðlamak için Data Annotations (Required, Range, StringLength, EmailAddress vb.) kullanýldý.
* **Ýliþkisel Veritabaný Tasarýmý:** Rezervasyon modelinde `CustomerId` ve `RoomId` üzerinden Foreign Key (Yabancý Anahtar) iliþkileri kurularak tablolar arasý baðlantýlar saðlandý.
* **Veritabaný Konfigürasyonu:** Entity Framework Core kullanýlarak SQLite veritabaný entegrasyonu yapýldý. `AppDbContext` sýnýfý yazýldý ve uygulamanýn Dependency Injection (DI) ayarlarý tamamlandý.
* **Migration Ýþlemleri:** EF Core CLI araçlarý ile `InitialCreate` göçü oluþturuldu ve `database update` komutuyla `HotelReservation.db` SQLite veritabaný fiziksel olarak ayaða kaldýrýldý.
* **Repository Pattern (Depo Kalýbý):** Veri eriþim katmanýný iþ mantýðýndan soyutlamak adýna Repository Design Pattern projeye entegre edilmeye baþlandý. Bu kapsamda `IRoomRepository` arayüzü ve veritabaný ile doðrudan asenkron iletiþim kuran `RoomRepository` sýnýfý geliþtirilip DI kapsayýcýsýna kaydedildi.