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

## ✨ CQRS Nasıl Uygulanır

CQRS pattern'ı biri manuel bir diğeri de MediatR kütüphanesiyle olmak üzere iki türlü uygulanabilmektedir.

Hangi yöntemle uygularsak uygulayalım temelde CQRS davranışının kavramlarına hakim olmamız gerekmektedir. Bu kavramlar, Commands, Queries ve Handlers'dır.

* **Commands**: Uygulamada yapılacak tüm command'leri temsil edecek olan sınıflardır. İçerisinde komutla ilgili verileri barındırır.

* **Queries**: Uygulamada yapılacak tüm query'leri temsil edecek olan sınıflardır. İçerisinde sorgulama neticesinde gelen verilerin alanlarını barındırır.

* **Handlers**: Command ve Query'lerin işlenmesini gerçekleştirecek olan operasyonel sınıflardır. Gelen bir Command yahut Query isteğinin karşılığında yapılacak iş/operasyon bu sınıfta gerçekleştirilir.

## 📂 Proje Yapısı

CQRS'nin uygulanması için önerilen klasör yapısı şu şekildedir:

```
📂 CQRS
├── 📂 Commands
│   ├── 📂 Requests
│   ├── 📂 Responses
├── 📂 Handlers
│   ├── 📂 CommandHandlers
│   ├── 📂 QueryHandlers
├── 📂 Queries
│   ├── 📂 Requests
│   └── 📂 Responses
```

## 🚀 Adım Adım Uygulama (Manuel)

**1. Command ve Query Sınıfları Tanımlama**

*Command Örneği*
```csharp
public class CreateProductCommandRequest
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class CreateProductCommandResponse
{
    public bool IsSuccess { get; set; }
    public Guid ProductId { get; set; }
}
```

*Query Örneği*
```csharp
public class GetByIdProductQueryRequest
{
    public string Id { get; set; }
}

public class GetByIdProductQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
}
```

**2. Handler'ları Uygulama**

*Command Örneği*
```csharp
public class CreateProductCommandHandler(ProductDbContext context)
{
    public async Task<CreateProductCommandResponse> CreateProductAsync(CreateProductCommandRequest request)
    {
        var result = await context.Products.AddAsync(new()
        {
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity,
            CreatedDate = DateTime.UtcNow,
        });

        await context.SaveChangesAsync();
            
        return new()
        {
            IsSuccess = true,
            ProductId = result.Entity.Id
        };
    }
}
```

*Query Örneği*
```csharp
public class GetByIdProductQueryHandler(ProductDbContext context)
{
    public async Task<GetByIdProductQueryResponse> GetByIdProductAsync(GetByIdProductQueryRequest request)
    {
        var product = await context.Products.FindAsync(Guid.Parse(request.Id));

        if (product == null)
            throw new KeyNotFoundException("Product not found.");

        return new GetByIdProductQueryResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            CreatedDate = product.CreatedDate,
        };
    }
}
```

**3. Servislerin Entegrasyonu**

`Program.cs` dosyasına şu kod eklenir:

```csharp
builder.Services.AddTransient<CreateProductCommandHandler>()
                .AddTransient<DeleteProductCommandHandler>()
                .AddTransient<GetAllProductQueryHandler>()
                .AddTransient<GetByIdProductQueryHandler>();
```

**4. Controller Sınıfı**

```csharp
public class ProductsController(CreateProductCommandHandler createProductCommandHandler,
    DeleteProductCommandHandler deleteProductCommandHandler,
    GetAllProductQueryHandler getAllProductQueryHandler,
    GetByIdProductQueryHandler getByIdProductQueryHandler) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProduct([FromQuery] GetAllProductQueryRequest request)
        => Ok(await getAllProductQueryHandler.GetAllProductAsync(request));

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetByIdProduct([FromRoute] GetByIdProductQueryRequest request)
        => Ok(await getByIdProductQueryHandler.GetByIdProductAsync(request));

    [HttpPut]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommandRequest request)
        => Ok(await createProductCommandHandler.CreateProductAsync(request));

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductCommandRequest request)
        => Ok(await deleteProductCommandHandler.DeleteProductAsync(request));
}

```


## 🚀 Adım Adım Uygulama (MediatR)

**1. Command ve Query Sınıfları Tanımlama**

`MediatR` kütüphanesini uygulamaya eklemeyi unutmayın.

*Command Örneği*
```csharp
public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class CreateProductCommandResponse
{
    public bool IsSuccess { get; set; }
    public Guid ProductId { get; set; }
}
```

*Query Örneği*
```csharp
public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>
{
    public string Id { get; set; }
}

public class GetByIdProductQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
}
```

**2. Handler'ları Uygulama**

*Command Örneği*
```csharp
public class CreateProductCommandHandler(ProductDbContext context) : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await context.Products.AddAsync(new()
        {
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity,
            CreatedDate = DateTime.UtcNow,
        });

        await context.SaveChangesAsync();
            
        return new()
        {
            IsSuccess = true,
            ProductId = result.Entity.Id
        };
    }
}
```

*Query Örneği*
```csharp
public class GetByIdProductQueryHandler(ProductDbContext context) : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
{
    public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await context.Products.FindAsync(Guid.Parse(request.Id));

        if (product == null)
            throw new KeyNotFoundException("Product not found.");

        return new GetByIdProductQueryResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            CreatedDate = product.CreatedDate,
        };
    }
}
```

**3. MediatR Entegrasyonu**

`Program.cs` dosyasına şu kod eklenir:

```csharp
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProductDbContext).Assembly));
```

**4. Controller Sınıfı**

```csharp
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProduct([FromQuery] GetAllProductQueryRequest request)
        => Ok(await mediator.Send(request));

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetByIdProduct([FromRoute] GetByIdProductQueryRequest request)
        => Ok(await mediator.Send(request));

    [HttpPut]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommandRequest request)
        => Ok(await mediator.Send(request));

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductCommandRequest request)
        => Ok(await mediator.Send(request));
}
```


