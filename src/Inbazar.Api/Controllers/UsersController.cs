using Inbazar.Api.Contracts;
using Inbazar.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inbazar.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IResult> Post(UserCreateCommand command)
    {
        var result = await _mediator.Send(command);

        return result.IsSuccess ? Results.Ok(new Response
        {
            Code = StatusCodes.Status200OK.ToString(),
            Message = "Ok",
            Data = result.Value
        }) :
        Results.Problem();
    }
}
