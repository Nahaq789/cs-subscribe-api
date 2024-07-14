using MediatR;
using Subscribe.Domain.Model.SubscribeAggregate;

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
        var subscribeAggregate = new SubscribeAggregate(
                Guid.NewGuid(),
                command.PaymentDay,
                command.StartDay,
                command.ColorCode,
                command.IsYear,
                command.CategoryAggregateId,
                command.UserAggregateId
            );
        subscribeAggregate.SetSubscribeItem(command.SubscribeName, command.Amount, subscribeAggregate.SubscribeAggregateId);

        await _subscribeRepository.AddAsync(subscribeAggregate);

        return await _subscribeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}