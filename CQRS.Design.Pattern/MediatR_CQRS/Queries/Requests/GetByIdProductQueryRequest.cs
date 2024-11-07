using CQRS.Design.Pattern.MediatR_CQRS.Queries.Responses;
using MediatR;

namespace CQRS.Design.Pattern.MediatR_CQRS.Queries.Requests
{
    public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>
    {
        public string Id { get; set; }
    }
}
