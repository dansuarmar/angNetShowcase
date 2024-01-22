using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBackend_Api_Controllers.Contracts
{ 
    internal record CustomerResponse(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string phone,
        string description,
        DateTimeOffset created,
        DateTimeOffset? lastUpdated
        )
    {
    }
}
