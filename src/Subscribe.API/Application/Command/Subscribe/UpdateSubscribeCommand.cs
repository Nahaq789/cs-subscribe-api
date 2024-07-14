using System.Runtime.Serialization;
using MediatR;

namespace Subscribe.API.Application.Command.Subscribe;

public class UpdateSubscribeCommand : IRequest<bool>
{
    [DataMember]
    public Guid SubscribeAggregateId { get; set; }

    [DataMember]
    public DateTime PaymentDay { get; set; }

    [DataMember]
    public DateTime StartDay { get; set; }

    [DataMember]
    public DateTime? ExpectedDateOfCancellation { get; set; }

    [DataMember]
    public string ColorCode { get; set; }

    [DataMember]
    public bool IsYear { get; set; }

    [DataMember]
    public bool IsActive { get; set; }

    [DataMember]
    public Guid _categoryAggregateId { get; set; }

    [DataMember]
    public Guid _userAggregateId { get; set; }

    [DataMember]
    public string SubscribeName { get; set; }

    [DataMember]
    public decimal Amount { get; set; }

    public UpdateSubscribeCommand(
        Guid subscribeAggregateId,
        DateTime paymentDay,
        DateTime startDay,
        string colorCode,
        bool isYear,
        bool isActive,
        Guid categoryAggregateId,
        Guid userAggregateId,
        string subscribeName,
        decimal amount,
        DateTime? expectedDateOfCancellation = null)
    {
        SubscribeAggregateId = subscribeAggregateId;
        PaymentDay = paymentDay;
        StartDay = startDay;
        ExpectedDateOfCancellation = expectedDateOfCancellation;
        ColorCode = colorCode;
        IsYear = isYear;
        IsActive = isActive;
        _categoryAggregateId = categoryAggregateId;
        _userAggregateId = userAggregateId;
        SubscribeName = subscribeName;
        Amount = amount;
    }
}