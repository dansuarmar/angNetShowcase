using System.ComponentModel.DataAnnotations;

namespace NetBackend_Database.Model
{
    public class DBEntity
    {
        [StringLength(320)]
        public DateTimeOffset Created { get; set; } = DateTime.Now;

        [StringLength(320)]
        public DateTimeOffset? LastUpdated { get; set; }
    }
}
