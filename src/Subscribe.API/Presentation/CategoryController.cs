using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Subscribe.API.Application.Command;
using Subscribe.API.Application.Query.Category;

namespace Subscribe.API.Presentation;

[ApiController]
[Route("/api/v1/category")]
public class CategoryController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryQueries _categoryQueries;

    public CategoryController(IMediator mediator, ILogger<CategoryController> logger, ICategoryQueries categoryQueries)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _categoryQueries = categoryQueries ?? throw new ArgumentNullException(nameof(categoryQueries));
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> CreateCategoryAsync(
        [FromHeader(Name = "x-requestId")] Guid requestId,
        [FromBody] CreateCategoryCommand command
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
                var requestCommand = new IdentifiedCommand<CreateCategoryCommand, bool>(command, requestId);
                var result = await _mediator.Send(requestCommand.Command);

                if (result)
                {
                    _logger.LogInformation("CreateCategoryCommand Success - RequestId: {}", requestId);
                    return TypedResults.Ok();
                }
                else
                {
                    _logger.LogInformation("CreateCategoryCommand Failed - RequestId: {}", requestId);
                    return TypedResults.Problem("CreateCategoryCommand Failed - RequestId: {}", requestId.ToString());
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
    public async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> UpdateCategoryAsync(
        [FromHeader(Name = "x-requestId")] Guid requestId,
        [FromBody] UpdateCategoryCommand command
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
                var requestCommand = new IdentifiedCommand<UpdateCategoryCommand, bool>(command, requestId);
                var result = await _mediator.Send(requestCommand.Command);

                if (result)
                {
                    _logger.LogInformation("UpdateCategoryCommand Success - RequestId: {}", requestId);
                    return TypedResults.Ok();
                }
                else
                {
                    _logger.LogInformation("UpdateCategoryCommand Failed - RequestId: {}", requestId);
                    return TypedResults.Problem("UpdateCategoryCommand Failed - RequestId: {}", requestId.ToString());
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
