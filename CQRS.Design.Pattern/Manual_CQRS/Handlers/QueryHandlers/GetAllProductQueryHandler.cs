using CQRS.Design.Pattern.Contexts;
using CQRS.Design.Pattern.Manual_CQRS.Queries.Requests;
using CQRS.Design.Pattern.Manual_CQRS.Queries.Responses;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Design.Pattern.Manual_CQRS.Handlers.QueryHandlers
{
    public class GetAllProductQueryHandler(ProductDbContext context)
    {
        public async Task<List<GetAllProductQueryResponse>> GetAllProductAsync(GetAllProductQueryRequest request)
        {
            var products = await context.Products.ToListAsync();
            return products.Select(p => new GetAllProductQueryResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                CreatedDate = p.CreatedDate,
            }).ToList();
        }
    }
}
