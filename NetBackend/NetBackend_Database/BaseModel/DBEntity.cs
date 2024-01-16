using System.ComponentModel.DataAnnotations;

namespace NetBackend_Database.Model
{
    public class DBEntity
    {
        public DateTimeOffset Created { get; set; } = DateTime.Now;
        public DateTimeOffset? LastUpdated { get; set; }
    }
}
