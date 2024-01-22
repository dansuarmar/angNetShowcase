using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetBackend_Application.CustomerApp
{
    public class CustomerResult //TODO Cambiar a CustomerResultResult
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset Created { get; set; } = DateTime.Now;
        public DateTimeOffset? LastUpdated { get; set; }
    }
}
