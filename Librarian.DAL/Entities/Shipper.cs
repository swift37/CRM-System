using Librarian.DAL.Entities.Base;
using Librarian.Interfaces;

namespace Librarian.DAL.Entities
{
    public class Shipper : NamedEntity, IArchivable
    {
        public string? ContactNumber { get; set; }

        public bool IsActual { get; set; } = true;

        public override string ToString() => $"{Name}";
    }
}
