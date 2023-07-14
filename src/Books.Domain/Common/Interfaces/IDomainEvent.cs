
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.Common.Interfaces
{
    public interface IDomainEvent : INotification
    {
    }
}
