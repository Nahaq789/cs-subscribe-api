using MediatR;

namespace Subscribe.API.Application.Command;

public abstract class IdentifiedCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R> where T : IRequest<R>
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    protected IdentifiedCommandHandler(IMediator mediator, ILogger logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<R> Handle(IdentifiedCommand<T, R> command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Sending command: {CommandName}", command.GetType().Name);

            var result = await _mediator.Send(command);

            _logger.LogInformation("Command result: {@Result} - {CommandName}", result, command.GetType().Name);

            return result;
        }
        catch
        {
            return default;
        }
    }
}