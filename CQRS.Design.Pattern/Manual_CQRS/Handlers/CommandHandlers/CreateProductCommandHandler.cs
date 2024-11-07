using CQRS.Design.Pattern.Contexts;
using CQRS.Design.Pattern.Manual_CQRS.Commands.Requests;
using CQRS.Design.Pattern.Manual_CQRS.Commands.Responses;

namespace CQRS.Design.Pattern.Manual_CQRS.Handlers.CommandHandlers
{
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
}
