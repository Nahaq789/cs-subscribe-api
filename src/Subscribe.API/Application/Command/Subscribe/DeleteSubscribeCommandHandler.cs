using System.Runtime.CompilerServices;
using MediatR;
using Subscribe.Domain.Model.SubscribeAggregate;

namespace Subscribe.API.Application.Command;

public class DeleteSubscribeCommandHandler : IRequestHandler<DeleteSubscribeCommand, bool>
{
    private readonly ISubscribeRepository _subscribeRepository;

    public DeleteSubscribeCommandHandler(ISubscribeRepository subscribeRepository)
    {
        _subscribeRepository = subscribeRepository;
    }

    public async Task<bool> Handle(DeleteSubscribeCommand command, CancellationToken cancellationToken)
    {
        var target = await _subscribeRepository.FindBySubscribeAgIdAndUserAgId(command.SubscribeAggregateId, command.UserAggregateId);

        target.SetDeleteDay();

        return await _subscribeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
