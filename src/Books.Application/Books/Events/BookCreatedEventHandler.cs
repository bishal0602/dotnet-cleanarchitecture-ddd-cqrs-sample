using Books.Domain.BookAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Books.Events
{
    public class BookCreatedEventHandler : INotificationHandler<BookCreatedEvent>
    {
        public Task Handle(BookCreatedEvent notification, CancellationToken cancellationToken)
        {
            // do stuff
            Console.WriteLine($"Book Created Event Recieved for Book {notification.Book.Id} - {notification.Book.Title}");
            return Task.CompletedTask;
        }
    }
}
