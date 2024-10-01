using Inbazar.Application.Categories.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inbazar.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CategoriesController : ControllerBase
{
    private readonly ISender _sender;

    public CategoriesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromQuery] CategoryCreateCommand command)
    {
        return Ok(await _sender.Send(command));
    }
}
