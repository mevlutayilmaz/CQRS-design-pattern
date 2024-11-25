## CQRS (Command Query Responsibility Segregation) Desenine Kapsamlı Bir Kılavuz

Bu README, CQRS desenini uygulamaya yönelik kapsamlı bir kılavuz sunar; temel kavramları, avantajlarını, uygun senaryoları, uygulama yaklaşımlarını (manuel ve MediatR kullanarak) ve en iyi uygulamaları kapsar.

**1. Temel Kavramlar**

CQRS, bir uygulama içinde okuma ve yazma işlemlerini ayıran bir desendir. Bu ayrım, ölçeklenebilirliği, sürdürülebilirliği ve performansı iyileştirir.

* **Komutlar (Commands):** Uygulamanın durumunu değiştiren eylemleri temsil eder. Örnekler arasında veri oluşturma, güncelleme veya silme yer alır. Genellikle asenkron oldukları ve doğrudan veri döndürmedikleri için kullanılırlar.

* **Komut İşleyicileri (Command Handlers):** Komutları işlemekten sorumludur. İstenen eylemi yürütmek ve değişiklikleri bir veri deposuna kalıcı hale getirmek için iş mantığını içerirler.

* **Sorgular (Queries):** Uygulamadan veri okuma isteklerini temsil eder. Örnekler arasında tek bir varlığı alım veya bir varlık koleksiyonu için sorgulama yer alır. Genellikle senkron oldukları ve veri döndürdükleri için kullanılırlar.

* **Sorgu İşleyicileri (Query Handlers):** Sorguları işlemekten sorumludur. Verileri bir veri deposundan veya diğer kaynaklardan alır ve istenen bilgileri döndürür. Uygulamanın durumunu değiştirmezler.


**2. CQRS'nin Avantajları**

* **Gelişmiş Ölçeklenebilirlik:** Komutlar ve sorgular, ilgili iş yüklerine göre bağımsız olarak ölçeklendirilebilir. Okuma yoğun bir uygulama, komut tarafına göre sorgu tarafını önemli ölçüde daha fazla ölçeklendirmenin avantajlarından yararlanabilir.

* **Artan Performans:** Her işlem türü (okuma veya yazma) için optimize edilmiş veri yapıları ve erişim yöntemleri kullanılabilir. Örneğin, sorgular MongoDB veya Redis gibi okuma için optimize edilmiş bir veritabanı kullanabilirken, komutlar ilişkisel bir veritabanı kullanabilir.

* **Artan Bakım Sürdürülebilirliği:** Okuma ve yazma işlemlerinin ayrılması, daha temiz ve daha sürdürülebilir koda yol açar. Her işlemin mantığı izole edilmiş ve anlaşılması ve test edilmesi daha kolaydır.

* **Gelişmiş Eşzamanlılık:** Okuma ve yazma işlemlerinin ayrılması, eşzamanlılık kontrolünü basitleştirir. Okuma işlemlerinin veritabanı kilitleri için yazma işlemleriyle rekabet etmesi gerekmez.

* **Daha İyi Test Edilebilirlik:** Ayrım, komutların ve sorguların birim test edilmesini kolaylaştırır. Her bileşen bağımsız olarak test edilebilir.

* **Esneklik:** Her işlem için en iyi veritabanı teknolojisinin seçilmesine olanak tanır (örneğin, komutlar için ilişkisel veritabanı, sorgular için NoSQL).


**3. Uygun Senaryolar**

* **Yüksek Trafikli Uygulamalar:** CQRS, yazma işlemlerine kıyasla önemli miktarda okuma işlemi olan uygulamalarda mükemmel performans gösterir.

* **Karmaşık İş Mantığı:** Ayrım, karmaşık iş kurallarının ele alınmasını basitleştirir.

* **Farklı Veri Erişim Gereksinimleri:** Okuma ve yazma işlemlerinin farklı veri modelleri veya erişim kalıpları gerektirdiği durumlarda.

* **Performans Optimizasyonu:** CQRS, hem okuma hem de yazma işlemleri için özel optimizasyon stratejilerinin uygulanmasına olanak tanır.

* **Gerçek Zamanlı Uygulamalar:** Ayrım, gerçek zamanlı okuma istekleri için veri erişimini optimize ederek yanıt vermeyi iyileştirir.


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
CQRSProjesi/
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
