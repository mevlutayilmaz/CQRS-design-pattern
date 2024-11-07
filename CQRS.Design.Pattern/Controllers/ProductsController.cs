using CQRS.Design.Pattern.MediatR_CQRS.Commands.Requests;
using CQRS.Design.Pattern.MediatR_CQRS.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Design.Pattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    #region Manual CQRS
    //public class ProductsController(CreateProductCommandHandler createProductCommandHandler,
    //    DeleteProductCommandHandler deleteProductCommandHandler,
    //    GetAllProductQueryHandler getAllProductQueryHandler,
    //    GetByIdProductQueryHandler getByIdProductQueryHandler) : ControllerBase
    //{
    //    [HttpGet]
    //    public async Task<IActionResult> GetAllProduct([FromQuery] GetAllProductQueryRequest request)
    //        => Ok(await getAllProductQueryHandler.GetAllProductAsync(request));

    //    [HttpGet("{Id}")]
    //    public async Task<IActionResult> GetByIdProduct([FromRoute] GetByIdProductQueryRequest request)
    //        => Ok(await getByIdProductQueryHandler.GetByIdProductAsync(request));

    //    [HttpPut]
    //    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommandRequest request)
    //        => Ok(await createProductCommandHandler.CreateProductAsync(request));

    //    [HttpDelete("{Id}")]
    //    public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductCommandRequest request)
    //        => Ok(await deleteProductCommandHandler.DeleteProductAsync(request));
    //}
    #endregion

    #region MediatR CQRS
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] GetAllProductQueryRequest request)
            => Ok(await mediator.Send(request));

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdProduct([FromRoute] GetByIdProductQueryRequest request)
            => Ok(await mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommandRequest request)
            => Ok(await mediator.Send(request));

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductCommandRequest request)
            => Ok(await mediator.Send(request));
    }
    #endregion
}
