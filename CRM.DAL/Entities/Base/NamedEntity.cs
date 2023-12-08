using System.ComponentModel.DataAnnotations;

namespace CRM.DAL.Entities.Base
{
    public class NamedEntity : Entity
    {
        [Required]
        public string? Name { get; set; }
    }
}
