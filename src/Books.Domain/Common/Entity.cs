using Books.Domain.Common.Interfaces;

namespace Books.Domain.Common
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents where TId : notnull
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public TId Id { get; protected set; }
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        protected Entity(TId id)
        {
            Id = id;
        }
        protected Entity() { }
        public override bool Equals(object? obj)
        {
            return obj is Entity<TId> entity && Id.Equals(entity.Id);
        }

        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
