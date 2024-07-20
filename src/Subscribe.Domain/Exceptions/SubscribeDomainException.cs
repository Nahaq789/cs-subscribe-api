namespace Subscribe.Domain.Exceptions;

public class SubscribeDomainException : Exception
{
    public SubscribeDomainException()
    { }

    public SubscribeDomainException(string message) : base(message)
    {
    }

    public SubscribeDomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}