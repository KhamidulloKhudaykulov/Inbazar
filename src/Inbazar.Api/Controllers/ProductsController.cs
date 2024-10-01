using Inbazar.Api.Contracts;
using Inbazar.Application.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inbazar.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductCreateCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(new ProductCreatedDTO
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price
            });
        }

        return BadRequest(result.Error);
    }

    [HttpPut]
    public async Task<IActionResult> Put(ProductUpdateCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(new Response
        {
            Code = "200",
            Message = "Ok",
        });
    }
}
