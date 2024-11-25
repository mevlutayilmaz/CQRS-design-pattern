# CQRS (Command Query Responsibility Segregation) Design Pattern

CQRS (Command Query Responsibility Segregation) deseni, bir uygulamanın Komutlar (Commands) ve Sorgular (Queries) için farklı süreçler ve modeller kullanmasını sağlayarak veri manipülasyonunu ve veri okuma işlemlerini optimize eden bir yazılım mimarisi desenidir.

Bu belge, CQRS'nin temel kavramlarını, faydalarını ve nasıl uygulanabileceğini açıklarken aynı zamanda örnek bir proje yapısını da içermektedir.

## 📚 Temel Kavramlar

* **Komutlar (Commands):** Veritabanındaki state’in değişikliğini temsil eden davranışlardır. Kayıt ekleme, mevcut olan bir kaydı güncelleme yahut silme gibi eylemlerin hepsi komut olarak değerlendirilebilir.

* **Komut Modeli (Command Model):** Komutları işlemek için kullanılan modeller veya sınıflardır. Bu yapılar sayesinde veri değişiklikleri gerçekleştirilebilmektedir.

* **Sorgular (Queries):** Veri okuma işlemlerinin hepsi sorgu olarak nitelendirilebilir. Veritabanından veri almak veya belirli bir kaydı sorgulamak gibi eylemlerin tümüdür.

* **Sorgu Modeli (Query Model):** Sorguları işlemek için kullanılan model veya sınıflardır. Veri okuma işlemleri için optimize edilmiş veri yapısını temsil ederler.

## 🎯 CQRS'in Amacı

**1. Performans ve Ölçeklenebilirlik**

CQRS, sistemdeki yoğunluğa göre farklı bölümleri bağımsız olarak ölçeklendirme imkânı sunar. 
Örneğin:
* E-ticaret uygulamalarında ürünler sürekli okunarak listelenir. Bu, sistemde yüksek okuma talebi oluşturur.
* Yeni ürün ekleme ve ürün güncelleme işlemleri ise daha seyrek gerçekleşir.

CQRS ile okuma işlemleri için ayrı bir veritabanı kullanılabilir ve bu veritabanı okuma işlemleri için optimize edilebilir. Benzer şekilde, yazma işlemleri başka bir veritabanında yürütülerek farklı bir şekilde ölçeklendirilebilir. Böylece her iki tür işlem için uygun çözümler sunulabilir ve maliyet düşürülür.

**2. Karmaşıklığın Azaltılması**

Komut ve sorguların birbirinden ayrılması, karmaşık iş süreçlerini daha yönetilebilir parçalara bölmeyi sağlar. Bu yapı, geliştiricilere iş akışlarını sadeleştirme ve daha net bir şekilde yönetme imkânı verir.

**3. Esneklik**

CQRS deseni, yeni gereksinimlerin eklenmesini veya mevcut gereksinimlerin değiştirilmesini kolaylaştırır. Her iki operasyon tipi (okuma ve yazma) için farklı teknolojiler veya mimariler kullanılabilir.


## ⚙️ Uygun Senaryolar

* **Yüksek Trafikli Uygulamalar:** Yüksek trafikli uygulamalarda CQRS pattern'ı optimizasyon açısından oldukça faydalı olabilir. Özellikle performans ve uygulama ölçeklenebilirliği açısından bu tarz uygulamalarda, uygun noktalarda düşünülüp, tasarlanmalıdır.

* **Karmaşık İş Mantığı:** Uygulamanın karmaşık iş mantığına sahip olduğu senaryolarda CQRS pattern'ı sayesinde komutlar ve sorgular arasında ayrım yaparak karmaşıklık azaltılabilir ve kodun daha yönetilebilir olması sağlanabilir.

* **Farklı Veri Erişim Gereksinimleri:** Uygulama, veri yazma ve okuma işlemleri için farklı gereksinimlere sahipse eğer CQRS pattern bu konuda esnek yaklaşım sergilenmesini sağlayabilir. Özellikle yoğun sorgulama gereksiniminin söz konusu olduğu çalışmalarda sorguların ve komutların ayrılması elzemdir.

* **Performans Optimizasyonu:** Uygulamanın performansını artırmak veya veri erişimini optimize etmek istediğinizde CQRS pattern'ini uygulayabilirsiniz. Hem bu pattern sayesinde farklı optimizasyon stratejilerini uygulama şansı ve esnekliği de söz konusu olmaktadır. Misal olarak, sorgulama tarafında caching veya NoSQL gibi hızlı çözümler fark yaratacaktır.

* **Gerçek Zamanlı Uygulamalar:** Etkileşimli ve gerçek zamanlı uygulamalarda kullanıcıların hızlı yanıt alması asli unsurdur. CQRS deseni, veri okuma işlemlerini optimize ederek kullanıcı deneyimini iyileştirebilir.


**4. Uygulama Yaklaşımları**

**a) Manuel Uygulama**

Bu yaklaşım, komutlar, sorgular ve işleyiciler için sınıfları manuel olarak oluşturmayı içerir. İnce taneli kontrol sağlar, ancak daha ayrıntılı olabilir.

* **Komut Örneği:**

```csharp
public class UrunOlusturKomutu
{
    public string Adi { get; set; }
    public decimal Fiyat { get; set; }
}
```

* **Komut İşleyicisi Örneği:**

```csharp
public class UrunOlusturKomutIsleyicisi : ICommandHandler<UrunOlusturKomutu>
{
    private readonly UrunContext _context; // Veritabanı bağlamı

    public UrunOlusturKomutIsleyicisi(UrunContext context)
    {
        _context = context;
    }

    public async Task Handle(UrunOlusturKomutu request, CancellationToken cancellationToken)
    {
        var urun = new Urun { Adi = request.Adi, Fiyat = request.Fiyat };
        _context.Urunler.Add(urun);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
```

* **Sorgu Örneği:**

```csharp
public class UrunuIdyeGoreAlSorgusu : IRequest<Urun>
{
    public int Id { get; set; }
}
```

* **Sorgu İşleyicisi Örneği:**

```csharp
public class UrunuIdyeGoreAlSorguIsleyicisi : IRequestHandler<UrunuIdyeGoreAlSorgusu, Urun>
{
    private readonly UrunContext _context; // Veritabanı bağlamı

    public UrunuIdyeGoreAlSorguIsleyicisi(UrunContext context)
    {
        _context = context;
    }

    public async Task<Urun> Handle(UrunuIdyeGoreAlSorgusu request, CancellationToken cancellationToken)
    {
        return await _context.Urunler.FindAsync(request.Id, cancellationToken);
    }
}
```

**b) MediatR Uygulaması**

MediatR, hafif bir aracı desen sağlayarak CQRS uygulamasını basitleştirir. Komutların ve sorguların ilgili işleyicilerine yönlendirilmesini işler.


```csharp
//Install-Package MediatR

//MediatR'ı bağımlılık enjeksiyon kabınızda kaydedin
services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

//Kullanım Örneği
var sonuc = await mediator.Send(new UrunuIdyeGoreAlSorgusu { Id = 1 });
await mediator.Send(new UrunOlusturKomutu { Adi = "Yeni Ürün", Fiyat = 25.99 });
```

**5. Dizin Yapısı (Örnek)**

Aşağıdaki dizin yapısı, CQRS bileşenlerini mantıklı bir şekilde düzenler:

```
CQRS/
├── Komutlar/
│   ├── UrunOlusturKomutu.cs
│   ├── UrunGuncelleKomutu.cs
│   └── ...
├── KomutIsleyiciler/
│   ├──  📂 UrunOlusturKomutIsleyicisi.cs
│   ├── UrunGuncelleKomutIsleyicisi.cs
│   └── ...
├── Sorgular/
│   ├── UrunuIdyeGoreAlSorgusu.cs
│   ├── TumUrunleriAlSorgusu.cs
│   └── ...
├── SorguIsleyiciler/
│   ├── UrunuIdyeGoreAlSorguIsleyicisi.cs
│   ├── TumUrunleriAlSorguIsleyicisi.cs
│   └── ...
└── ... diğer proje klasörleri ...

```


**6. Olay Kaynakçılığı (İsteğe Bağlı)**

Olay Kaynakçılığı genellikle CQRS ile birlikte kullanılır. Uygulamanın mevcut durumunu depolamak yerine, meydana gelen olayların bir dizisini depolar. Bu yaklaşım, denetlenebilirlik, daha basit eşzamanlılık yönetimi ve geçmiş durumları yeniden oluşturma yeteneği gibi avantajlar sağlar.


**7. Diğer Hususlar:**

* **Hata Yönetimi:** Hem komutlar hem de sorgular için sağlam hata yönetimi mekanizmaları uygulayın.
* **Veri Doğrulama:** Komutları işlemeden önce giriş verilerini doğrulayın.
* **İşlem Yönetimi:** Gerektiğinde işlemler kullanarak veri tutarlılığını sağlayın.
* **Günlük Kaydı:** Hata ayıklama ve denetim amacıyla komut ve sorgu yürütmelerini günlüğe kaydedin.
* **Güvenlik:** Verileri korumak için uygun güvenlik önlemleri uygulayın.


Bu kapsamlı kılavuz, CQRS desenini uygulamaya yönelik sağlam bir temel sağlar. Manuel veya MediatR uygulaması arasında seçim yapmak, proje gereksinimlerine ve ekip tercihlerine bağlıdır. Anahtar, temel prensipleri anlamak ve uygulamanızın mimarisini ve performansını iyileştirmek için bunları etkili bir şekilde uygulamaktadır.
