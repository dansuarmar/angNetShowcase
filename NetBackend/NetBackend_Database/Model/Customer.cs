using System.ComponentModel.DataAnnotations;

namespace NetBackend_Database.Model
{
    public class Customer : DBEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(200)]
        public string LastName { get; set; } = string.Empty;
        [StringLength(320)]
        public string Email { get; set; } = string.Empty;
        [StringLength(100)]
        public string Phone { get; set; } = string.Empty;
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
    }
}
