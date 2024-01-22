using MediatR;
using System.ComponentModel.DataAnnotations;

namespace NetBackend_Application.CustomerApp
{
    public class PatchCustomerCommand : IRequest<CustomerResult>
    {
        public Guid Id { get; set; }
        [StringLength(200, MinimumLength = 1)]
        public string? FirstName { get; set; }
        [StringLength(200)]
        public string? LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [StringLength(100)]
        public string? Phone { get; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
    }
}
