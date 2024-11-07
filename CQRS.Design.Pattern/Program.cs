using CQRS.Design.Pattern.Contexts;
using CQRS.Design.Pattern.Manual_CQRS.Handlers.CommandHandlers;
using CQRS.Design.Pattern.Manual_CQRS.Handlers.QueryHandlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLServer")));

#region Manual CQRS
builder.Services.AddTransient<CreateProductCommandHandler>()
                .AddTransient<DeleteProductCommandHandler>()
                .AddTransient<GetAllProductQueryHandler>()
                .AddTransient<GetByIdProductQueryHandler>();
#endregion

#region MediatR CQRS
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProductDbContext).Assembly));
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
