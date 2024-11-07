using CQRS.Design.Pattern.Contexts;
using CQRS.Design.Pattern.MediatR_CQRS.Queries.Requests;
using CQRS.Design.Pattern.MediatR_CQRS.Queries.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Design.Pattern.MediatR_CQRS.Handlers.QueryHandlers
{
    public class GetAllProductQueryHandler(ProductDbContext context) : IRequestHandler<GetAllProductQueryRequest, List<GetAllProductQueryResponse>>
    {
        public async Task<List<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
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
