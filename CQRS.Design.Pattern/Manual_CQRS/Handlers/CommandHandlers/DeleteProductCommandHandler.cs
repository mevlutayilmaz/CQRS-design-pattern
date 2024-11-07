 using CQRS.Design.Pattern.Contexts;
using CQRS.Design.Pattern.Entities;
using CQRS.Design.Pattern.Manual_CQRS.Commands.Requests;
using CQRS.Design.Pattern.Manual_CQRS.Commands.Responses;

namespace CQRS.Design.Pattern.Manual_CQRS.Handlers.CommandHandlers
{
    public class DeleteProductCommandHandler(ProductDbContext context)
    {
        public async Task<DeleteProductCommandResponse> DeleteProductAsync(DeleteProductCommandRequest request)
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
