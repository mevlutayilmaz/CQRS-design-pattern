using CQRS.Design.Pattern.Contexts;
using CQRS.Design.Pattern.MediatR_CQRS.Commands.Requests;
using CQRS.Design.Pattern.MediatR_CQRS.Commands.Responses;
using MediatR;

namespace CQRS.Design.Pattern.MediatR_CQRS.Handlers.CommandHandlers
{
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
}
