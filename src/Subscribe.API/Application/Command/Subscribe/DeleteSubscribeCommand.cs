using System.Runtime.Serialization;
using MediatR;

namespace Subscribe.API.Application.Command;

[DataContract]
public class DeleteSubscribeCommand : IRequest<bool>
{
    [DataMember]
    public Guid SubscribeAggregateId { get; set; }

    [DataMember]
    public Guid UserAggregateId { get; set; }

    public DeleteSubscribeCommand(Guid subscribeAggregateId, Guid userAggregateId)
    {
        SubscribeAggregateId = subscribeAggregateId;
        UserAggregateId = userAggregateId;
    }
}
