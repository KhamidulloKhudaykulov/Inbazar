using Inbazar.Api.Contracts;
using Inbazar.Application.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inbazar.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ISender _sender;

    public OrdersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Post(OrderCreateCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsSuccess)
        {
            return Ok(new Response
            {
                Code = StatusCodes.Status200OK.ToString(),
                Message = "Ok",
                Data = result
            });
        }
        return BadRequest(new Response
        {
            Code = result.Error.Code,
            Message = result.Error.Message
        });
    }
}
