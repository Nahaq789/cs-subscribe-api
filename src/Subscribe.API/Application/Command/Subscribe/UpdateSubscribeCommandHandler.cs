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
        var subscribe = new SubscribeAggregate(
            subscribeAggregateId: command.SubscribeAggregateId,
            paymentDay: command.PaymentDay,
            startDay: command.StartDay,
            colorCode: command.ColorCode,
            isYear: command.IsYear,
            isActive: command.IsActive,
            categoryAggregateId: command._categoryAggregateId,
            userAggregateId: command._userAggregateId,
            expectedDateOfCancellation: command.ExpectedDateOfCancellation
        );

        _subscribeRepository.UpdateAsync(subscribe);
        return await _subscribeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
