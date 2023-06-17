using Librarian.DAL.Entities.Base;
using Librarian.Interfaces;

namespace Librarian.DAL.Entities
{
    public class WorkingRate : NamedEntity, IArchivable
    {
        public int HoursPerMonth { get; set; }

        public string? Description { get; set; }

        public bool IsActual { get; set; } = true;
    }
}
