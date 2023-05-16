namespace Books.Domain.Common.Interfaces
{
    public interface IAggregateRoot<TId> : IEntity<TId> where TId : notnull
    {
    }
}
