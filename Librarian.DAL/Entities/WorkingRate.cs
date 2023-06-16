using Librarian.DAL.Entities.Base;

namespace Librarian.DAL.Entities
{
    public class WorkingRate : NamedEntity
    {
        public int HoursPerMonth { get; set; }

        public string? Description { get; set; }
    }
}
