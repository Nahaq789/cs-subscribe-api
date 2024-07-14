using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Subscribe.API.Application.Command;
using Subscribe.API.Application.Command.Subscribe;

namespace Subscribe.API.Presentation;

[ApiController]
[Route("/api/v1/subscribe")]
public class SubscribeController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger<SubscribeController> _logger;

    public SubscribeController(IMediator mediator, ILogger<SubscribeController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> CreateSubscribeAsync(
        [FromHeader(Name = "x-requestId")] Guid requestId,
        [FromBody] CreateSubscribeCommand command)
    {
        try
        {
            if (requestId == Guid.Empty)
            {
                _logger.LogWarning("RequestId is empty");
                return TypedResults.BadRequest("RequestId is empty");
            }

            using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                var requestCommand = new IdentifiedCommand<CreateSubscribeCommand, bool>(command, requestId);
                var result = await _mediator.Send(requestCommand.Command);

                if (result)
                {
                    _logger.LogInformation("CreateSubscribeCommand Success - RequestId: {}", requestId);
                    return TypedResults.Ok();
                }
                else
                {
                    _logger.LogInformation("CreateSubscribeCommand Failed - RequestId: {}", requestId);
                    return TypedResults.Problem("CreateSubscribeCommand Failed - RequestId: {}", requestId.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Error Message: {}", ex.Message);
            return TypedResults.Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> UpdateSubscribeAsync(
        [FromHeader(Name = "x-requestId")] Guid requestId,
        [FromBody] UpdateSubscribeCommand command
    )
    {
        try
        {
            if (requestId == Guid.Empty)
            {
                _logger.LogWarning("RequestId is empty");
                return TypedResults.BadRequest("RequestId is empty");
            }

            using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                var requestCommand = new IdentifiedCommand<UpdateSubscribeCommand, bool>(command, requestId);
                var result = await _mediator.Send(requestCommand.Command);

                if (result)
                {
                    _logger.LogInformation("CreateSubscribeCommand Success - RequestId: {}", requestId);
                    return TypedResults.Ok();
                }
                else
                {
                    _logger.LogInformation("CreateSubscribeCommand Failed - RequestId: {}", requestId);
                    return TypedResults.Problem("CreateSubscribeCommand Failed - RequestId: {}", requestId.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Error Message: {}", ex.Message);
            return TypedResults.Problem(detail: ex.Message, statusCode: 500);
        }
    }
}