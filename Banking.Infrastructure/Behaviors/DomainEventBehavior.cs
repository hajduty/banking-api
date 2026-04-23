using Banking.Domain;
using Banking.Infrastructure.Data;
using MediatR;

namespace Banking.Infrastructure.Behaviors;

public class DomainEventBehavior<TRequest, TResponse>(
    AppDbContext dbContext,
    IPublisher publisher)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        var aggregates = dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Any())
            .ToList();

        var events = new List<IDomainEvent>();

        foreach (var aggregate in aggregates)
        {
            events.AddRange(aggregate.Entity.DomainEvents);
        }

        foreach (var domainEvent in events)
            await publisher.Publish(domainEvent, cancellationToken);

        foreach (var aggregate in aggregates)
        {
            aggregate.Entity.ClearDomainEvents();
        }

        return response;
    }
}