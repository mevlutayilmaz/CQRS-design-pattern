using CQRS.Design.Pattern.MediatR_CQRS.Commands.Responses;
using MediatR;

namespace CQRS.Design.Pattern.MediatR_CQRS.Commands.Requests
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
