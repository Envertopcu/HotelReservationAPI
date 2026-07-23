# Hafta 4: API Sağlamlaştırma, Veri Bütünlüğü ve Sorgu Geliştirmeleri

Bu hafta rezervasyon API'sinin işlevleri geliştirilmiş; istemciden gelen verinin daha güvenli işlenmesi, listeleme uç noktalarının esnekleştirilmesi ve veritabanı düzeyinde veri bütünlüğünün korunması üzerine çalışılmıştır.

### 1. DTO Kullanımı ve İstemci Verisinin Sınırlandırılması

* Rezervasyon oluşturma işleminde `CreateReservationDto`, güncelleme işleminde `UpdateReservationDto` kullanılacak şekilde API sözleşmesi düzenlendi.
* DTO'lara müşteri, oda ve tarih alanları için doğrulama kuralları eklendi; `TotalPrice` alanı istemciden kabul edilmemektedir.
* Toplam ücret, konaklama süresi ve odanın güncel gecelik fiyatı kullanılarak yalnızca servis katmanında hesaplanmaya devam edildi. Böylece istemcinin fiyat bilgisini değiştirmesi engellendi.

### 2. Rezervasyon Güncelleme İş Kuralları

* Güncelleme işleminde rezervasyonun varlığı kontrol edilerek bulunamama durumu için `404 Not Found` yanıtı sağlandı.
* Yeni müşteri, oda ve tarih bilgileri doğrulanmakta; uygunluk kontrolünde güncellenen rezervasyon kendi tarih çakışması sorgusunun dışında tutulmaktadır.
* Tarih aralığı, müşteri ve oda doğrulamalarından sonra toplam ücret yeniden hesaplanarak güncel rezervasyona uygulanmaktadır.

### 3. Filtreleme ve Sayfalama

* Rezervasyon listesine `roomId` ve `customerId` parametreleriyle isteğe bağlı filtreleme eklendi.
* Büyük veri kümelerinde kontrollü sonuç döndürmek için `pageNumber` ve `pageSize` parametreleriyle sayfalama uygulandı.
* Geçersiz sayfa numarası için anlamlı hata yanıtı döndürülmekte, sayfa boyutu ise 1–100 aralığında sınırlandırılmaktadır.

### 4. Veri Bütünlüğü ve Eşzamanlılık Güvencesi

* Oda numarası için veritabanında benzersiz indeks tanımlandı; aynı oda numarasının tekrar kaydedilmesi engellendi.
* Oda veya müşteri silinmek istendiğinde ilişkili rezervasyonların yanlışlıkla silinmesini önlemek için foreign key silme davranışı `Restrict` olarak yapılandırıldı.
* Aynı odaya kesişen tarih aralıklarında eşzamanlı rezervasyon oluşturulmasını veritabanı düzeyinde engellemek üzere PostgreSQL exclusion constraint migration'ı eklendi.
* Bu kısıt ihlal edildiğinde API'nin `409 Conflict` yanıtı üretmesi için PostgreSQL hata kodu yönetimi eklendi.

### 5. Test ve Doğrulama

* Proje `dotnet build` komutuyla derlendi ve derleme işlemi hata veya uyarı olmadan tamamlandı.
* Swagger üzerinden rezervasyon oluşturma/güncelleme, uygun oda sorgulama, filtreleme ve sayfalama senaryolarının test edilmesi için uç noktalar hazır hale getirildi ve test edildi.

### Sonuç

Dördüncü hafta sonunda proje; yalnızca temel CRUD işlemleri yapan bir API olmaktan çıkarılarak, istemci verisini sınırlandıran, güncelleme süreçlerinde iş kurallarını koruyan, daha verimli sorgulama yapabilen ve veritabanı seviyesinde rezervasyon çakışmalarına karşı koruma sağlayan daha sağlam bir yapıya ulaştırılmıştır.
