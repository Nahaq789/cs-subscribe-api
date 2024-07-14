using MediatR;
using System.Runtime.Serialization;

[DataContract]
public class CreateSubscribeCommand : IRequest<bool>
{
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
    public Guid _categoryAggregateId { get; set; }

    [DataMember]
    public Guid _userAggregateId { get; set; }

    [DataMember]
    public string SubscribeName { get; set; }

    [DataMember]
    public decimal Amount { get; set; }

    public CreateSubscribeCommand(
        DateTime paymentDay,
        DateTime startDay,
        string colorCode,
        bool isYear,
        Guid categoryAggregateId,
        Guid userAggregateId,
        string subscribeName,
        decimal amount,
        DateTime? expectedDateOfCancellation = null)
    {
        PaymentDay = paymentDay;
        StartDay = startDay;
        ExpectedDateOfCancellation = expectedDateOfCancellation;
        ColorCode = colorCode;
        IsYear = isYear;
        _categoryAggregateId = categoryAggregateId;
        _userAggregateId = userAggregateId;
        SubscribeName = subscribeName;
        Amount = amount;
    }
}