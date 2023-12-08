using CRM.DAL.Entities.Base;
using CRM.Interfaces;

namespace CRM.DAL.Entities
{
    public class WorkingRate : NamedEntity, IArchivable
    {
        public int HoursPerMonth { get; set; }

        public string? Description { get; set; }

        public bool IsActual { get; set; } = true;
    }
}
