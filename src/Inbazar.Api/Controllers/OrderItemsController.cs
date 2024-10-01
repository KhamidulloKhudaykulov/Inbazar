using Inbazar.Application.OrderItems.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inbazar.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : ControllerBase
{
    private readonly ISender _sender;

    public OrderItemsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Post(OrderItemCreateCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Put(ChangeProductAmountCommand command)
    {
        return Ok(await _sender.Send(command));
    }
}
