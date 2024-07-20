namespace Subscribe.API;

public record CategoryAggregateDto
{
    public Guid CategoryAggregateId { get; set; }
    public string ColorCode { get; set; }
    public string IconFilePath { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
    public Guid UserAggregateId { get; set; }
    public CategoryItemDto CategoryItem { get; set; }
}

public record CategoryItemDto
{
    public string CategoryName { get; set; }
}
