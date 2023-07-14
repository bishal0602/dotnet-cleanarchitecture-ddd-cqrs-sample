using Books.Domain.Common.Interfaces;

namespace Books.Domain.Common
{
    public abstract class AuditableAggregate<TId> : AggregateRoot<TId>, IAuditable where TId : notnull
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

    }
}
