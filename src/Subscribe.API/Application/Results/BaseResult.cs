namespace Subscribe.API.Results;

public abstract class BaseResult
{
    public bool Success { get; set; }
    public long? Id { get; set; }
    public string? ErrorMessage { get; set; }
}