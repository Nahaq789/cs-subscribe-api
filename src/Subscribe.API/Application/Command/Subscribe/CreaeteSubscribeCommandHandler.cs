using MediatR;

namespace Subscribe.API.Application.Command;

public class CreateSubscribeCommandHandler : IRequestHandler<CreateSubscribeCommand, bool>
{
    private readonly ISubscribeRepository _subscribeRepository;

    public CreateSubscribeCommandHandler(ISubscribeRepository subscribeRepository)
    {
        _subscribeRepository = subscribeRepository;
    }

    public async Task<bool> Handle(CreateSubscribeCommand command, CancellationToken cancellationToken)
    {
        var aggregateId = Guid.NewGuid();
        var subscribe = new SubscribeAggregate(
                aggregateId,
                command.PaymentDay,
                command.StartDay,
                command.ColorCode,
                command.IsYear,
                command._categoryAggregateId,
                command._userAggregateId
            );
        subscribe.SetSubscribeItem(command.SubscribeName, command.Amount, aggregateId);

        await _subscribeRepository.AddAsync(subscribe);

        return await _subscribeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}