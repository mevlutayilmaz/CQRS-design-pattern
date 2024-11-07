using CQRS.Design.Pattern.Contexts;
using CQRS.Design.Pattern.Entities;
using CQRS.Design.Pattern.MediatR_CQRS.Commands.Requests;
using CQRS.Design.Pattern.MediatR_CQRS.Commands.Responses;
using MediatR;

namespace CQRS.Design.Pattern.MediatR_CQRS.Handlers.CommandHandlers
{
    public class DeleteProductCommandHandler(ProductDbContext context) : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product product = await context.Products.FindAsync(Guid.Parse(request.Id));
            if (product != null)
            {
                context.Products.Remove(product);

                await context.SaveChangesAsync();

                return new() { IsSuccess = true };
            }
            return new() { IsSuccess = false };
        }
    }
}
