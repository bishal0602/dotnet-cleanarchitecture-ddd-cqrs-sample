using Books.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.BookAggregate.Events
{
    public record BookCreatedEvent(Book Book) : IDomainEvent
    {
    }
}
