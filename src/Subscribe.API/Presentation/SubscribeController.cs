using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Subscribe.API.Application.Command;
using Subscribe.API.Application.Command.Subscribe;
using Subscribe.API.Application.Query.Subscribe;

namespace Subscribe.API.Presentation;

[ApiController]
[Route("/api/v1/subscribe")]
public class SubscribeController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger<SubscribeController> _logger;
    private readonly ISubscribeQueries _subscribeQueries;

    public SubscribeController(IMediator mediator, ILogger<SubscribeController> logger, ISubscribeQueries subscribeQueries)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _subscribeQueries = subscribeQueries ?? throw  new ArgumentNullException(nameof(subscribeQueries));
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
                    _logger.LogInformation("UpdateSubscribeCommand Success - RequestId: {}", requestId);
                    return TypedResults.Ok();
                }
                else
                {
                    _logger.LogInformation("UpdateSubscribeCommand Failed - RequestId: {}", requestId);
                    return TypedResults.Problem("UpdateSubscribeCommand Failed - RequestId: {}", requestId.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Error Message: {}", ex.Message);
            return TypedResults.Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpPut("delete")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> DeleteSubscribeAsync(
        [FromHeader(Name = "x-requestId")] Guid requestId,
        [FromBody] DeleteSubscribeCommand command
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
                var requestCommand = new IdentifiedCommand<DeleteSubscribeCommand, bool>(command, requestId);
                var result = await _mediator.Send(requestCommand.Command);

                if (result)
                {
                    _logger.LogInformation("DeleteSubscribeCommand Success - RequestId: {}", requestId);
                    return TypedResults.Ok();
                }
                else
                {
                    _logger.LogInformation("DeleteSubscribeCommand Failed - RequestId: {}", requestId);
                    return TypedResults.Problem("DeleteSubscribeCommand Failed - RequestId: {}", requestId.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Error Message: {}", ex.Message);
            return TypedResults.Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpGet]
    [Route("/api/v1/subscribe/find")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok<IEnumerable<SubscribeAggregateDto>>, BadRequest<string>>> GetSubscribeByUserAsync(
        [FromHeader(Name = "x-requestId")] Guid requestId, [FromQuery] Guid userid)
    {
        if (requestId == Guid.Empty)
        {
            _logger.LogWarning("RequestId is empty");
            return TypedResults.BadRequest("RequestId is empty");
        }
        var result = await _subscribeQueries.GetSubscribeByUserAsync(userid);
        return TypedResults.Ok(result);
    }

    [HttpGet]
    [Route("/api/v1/subscribe/findbyid")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok<SubscribeAggregateDto>, BadRequest<string>, ProblemHttpResult>> GetSubscribeByUserAndAggregateAsync (
            [FromHeader(Name = "x-requestId")] Guid requestId,
            [FromQuery] Guid userid,
            [FromQuery] Guid subscribeid
            )
    {
        if (requestId == Guid.Empty)
        {
            _logger.LogWarning("RequestId is empty");
            return TypedResults.BadRequest("RequestId is empty");
        }
        var result = await _subscribeQueries.GetSubscribeByUserAndAggregateAsync(subscribeid, userid);

        if (result == null)
        {
            return TypedResults.Problem(detail: "Failed get subscribe", statusCode: 500);
        }
        return  TypedResults.Ok(result);
    }
}