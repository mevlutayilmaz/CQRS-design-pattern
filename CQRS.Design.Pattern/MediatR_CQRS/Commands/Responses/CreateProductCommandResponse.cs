﻿namespace CQRS.Design.Pattern.MediatR_CQRS.Commands.Responses
{
    public class CreateProductCommandResponse
    {
        public bool IsSuccess { get; set; }
        public Guid ProductId { get; set; }
    }
}
