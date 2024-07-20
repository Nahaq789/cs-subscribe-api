namespace Subscribe.Domain.Exceptions;

public class CategoryDomainException : Exception
{
    public CategoryDomainException() : base() { }
    public CategoryDomainException(string message) : base(message) { }
    public CategoryDomainException(string message, Exception innerException) : base(message, innerException) { }
}
