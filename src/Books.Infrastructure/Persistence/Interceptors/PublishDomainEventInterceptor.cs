using Books.Domain.Common.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Persistence.Interceptors
{
    public class PublishDomainEventInterceptor : SaveChangesInterceptor
    {
        private readonly IPublisher _publisher;

        public PublishDomainEventInterceptor(IPublisher publisher)
        {
            _publisher = publisher;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await PublishDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        private async Task PublishDomainEvents(DbContext? context)
        {
            if (context is null) return;

            List<IHasDomainEvents> entityWithDomainEvents = context.ChangeTracker.Entries<IHasDomainEvents>().Where(e => e.Entity.DomainEvents.Any()).Select(e => e.Entity).ToList();
            List<IDomainEvent> domainEvents = entityWithDomainEvents.SelectMany(e => e.DomainEvents).ToList();
            entityWithDomainEvents.ForEach(e => e.ClearDomainEvents());
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}
