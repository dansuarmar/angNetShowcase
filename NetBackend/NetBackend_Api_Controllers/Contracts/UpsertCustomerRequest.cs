using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBackend_Api_Controllers.Contracts
{
    public class UpsertCustomerRequest
    {
        [StringLength(200, MinimumLength = 2)]
        public required string firstName { get; set; }

        [StringLength(200)]
        public string lastName { get; set; } = string.Empty;

        [StringLength(100)]
        public string phone { get; set; } = string.Empty;

        [EmailAddress]
        public required string email { get; set; }

        [StringLength(10000)]
        public string description { get; set; } = string.Empty;
    }
}
