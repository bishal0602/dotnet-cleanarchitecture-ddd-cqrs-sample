using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.Common.Interfaces
{
    public interface IHasDomainEvents
    {
        public IReadOnlyList<IDomainEvent> DomainEvents { get; }
        public void AddDomainEvent(IDomainEvent domainEvent);
        public void ClearDomainEvents();
    }
}
