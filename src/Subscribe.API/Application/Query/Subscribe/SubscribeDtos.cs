namespace Subscribe.API.Application.Query.Subscribe;

public record SubscribeAggregateDto
{
    public Guid SubscribeAggregateId { get; set; }
    public DateTime PaymentDay { get; set; }
    public DateTime StartDay { get; set; }
    public DateTime? ExpectedDateOfCancellation { get; set; }
    public string ColorCode { get; set; }
    public bool IsYear { get; set; }
    public bool IsActive  { get; set; }
    public Guid CategoryAggregeteId { get; set; }
    public Guid UserAggregaeteId { get; set; }
    public DateTime? DeleteDay { get; set; }
    public SubscribeItemDto SubscribeItem { get; set; }
}

public record SubscribeItemDto
{
    public string SubscribeName { get; set; }
    public decimal Amount { get; set; }
}