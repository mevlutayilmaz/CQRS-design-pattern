using CQRS.Design.Pattern.Contexts;
using CQRS.Design.Pattern.Manual_CQRS.Queries.Requests;
using CQRS.Design.Pattern.Manual_CQRS.Queries.Responses;

namespace CQRS.Design.Pattern.Manual_CQRS.Handlers.QueryHandlers
{
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
}
