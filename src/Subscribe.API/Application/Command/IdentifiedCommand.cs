using MediatR;

namespace Subscribe.API.Application.Command;

public class IdentifiedCommand<T, R> : IRequest<R> where T : IRequest<R>
{
    public T Command { get; set; }
    public Guid Id { get; set; }

    public IdentifiedCommand(T command, Guid id)
    {
        Command = command;
        Id = id;
    }
}