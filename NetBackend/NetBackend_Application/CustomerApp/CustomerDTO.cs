using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBackend_Application.CustomerApp
{
    public class CustomerDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(200)]
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [StringLength(100)]
        public string Phone { get; set; } = string.Empty;
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
    }
}
