using CQRS.Design.Pattern.Contexts;
using CQRS.Design.Pattern.MediatR_CQRS.Queries.Requests;
using CQRS.Design.Pattern.MediatR_CQRS.Queries.Responses;
using MediatR;

namespace CQRS.Design.Pattern.MediatR_CQRS.Handlers.QueryHandlers
{
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
}
