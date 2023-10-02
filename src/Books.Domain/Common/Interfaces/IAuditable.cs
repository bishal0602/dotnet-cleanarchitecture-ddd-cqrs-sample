using Books.Domain.UserAggregate;
using Books.Domain.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.Common.Interfaces
{
    public interface IAuditable
    {
        User? CreatedBy { get; set; }
        UserId? CreatedByUserId { get; set; }
        DateTime CreatedOn { get; set; }
        User? LastModifiedBy { get; set; }
        UserId? LastModifiedByUserId { get; set; }
        DateTime? LastModifiedOn { get; set; }
    }
}
