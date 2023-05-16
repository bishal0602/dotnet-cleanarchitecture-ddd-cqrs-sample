namespace Books.Domain.Common.Interfaces
{
    public interface IEntity<TId> where TId : notnull
    {
        TId Id { get; }
    }
}
