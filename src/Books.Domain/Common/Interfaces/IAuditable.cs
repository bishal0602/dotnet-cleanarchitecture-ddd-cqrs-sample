using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.Common.Interfaces
{
    public interface IAuditable
    {
        string? CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string? LastModifiedBy { get; set; }
        DateTime? LastModifiedOn { get; set; }
    }
}
