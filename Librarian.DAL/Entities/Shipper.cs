using Librarian.DAL.Entities.Base;

namespace Librarian.DAL.Entities
{
    public class Shipper : NamedEntity
    {
        public string? ContactNumber { get; set; }

        public bool IsActual { get; set; } = true;
    }
}
