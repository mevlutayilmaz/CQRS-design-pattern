using CQRS.Design.Pattern.MediatR_CQRS.Commands.Responses;
using MediatR;

namespace CQRS.Design.Pattern.MediatR_CQRS.Commands.Requests
{
    public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>
    {
        public string Id { get; set; }
    }
}
