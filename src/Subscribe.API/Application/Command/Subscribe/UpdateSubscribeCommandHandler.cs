using MediatR;
using Subscribe.Domain.Model.SubscribeAggregate;

namespace Subscribe.API.Application.Command.Subscribe;

public class UpdateSubscribeCommandHandler : IRequestHandler<UpdateSubscribeCommand, bool>
{
    private readonly ISubscribeRepository _subscribeRepository;

    public UpdateSubscribeCommandHandler(ISubscribeRepository subscribeRepository)
    {
        _subscribeRepository = subscribeRepository;
    }
    public async Task<bool> Handle(UpdateSubscribeCommand command, CancellationToken cancellationToken)
    {
        var baseSubscribe = await _subscribeRepository.FindBySubscribeAggregateId(command.SubscribeAggregateId);

        baseSubscribe.UpdateSubscribeAggregate(
            paymentDay: command.PaymentDay,
            startDay: command.StartDay,
            colorCode: command.ColorCode,
            isYear: command.IsYear,
            isActive: command.IsActive,
            categoryAggregateId: command.CategoryAggregateId,
            userAggregateId: command.UserAggregateId,
            expectedDateOfCancellation: command.ExpectedDateOfCancellation);

        baseSubscribe.UpdateSubscribeItem(command.SubscribeName, command.Amount, command.SubscribeAggregateId);

        return await _subscribeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
