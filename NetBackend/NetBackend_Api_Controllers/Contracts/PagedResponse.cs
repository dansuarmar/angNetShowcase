using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBackend_Api_Controllers.Contracts
{
    internal record PagedResponse<T>(List<T> items, int page, int size, int total, bool hasNext, bool hasPrev) where T : class
    {
    }
}
