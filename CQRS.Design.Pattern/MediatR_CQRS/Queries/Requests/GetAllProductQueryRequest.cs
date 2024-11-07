using CQRS.Design.Pattern.MediatR_CQRS.Queries.Responses;
using MediatR;

namespace CQRS.Design.Pattern.MediatR_CQRS.Queries.Requests
{
    public class GetAllProductQueryRequest : IRequest<List<GetAllProductQueryResponse>>
    {
    }
}
