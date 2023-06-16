using Librarian.DAL.Entities.Base;

namespace Librarian.DAL.Entities
{
    public class Supplier : NamedEntity
    {
        public string? ContactName { get; set; }

        public string? ContactTitle { get; set; }

        public string? ContactNumber { get; set; }

        public string? ContactMail { get; set; }

        public string? Address { get; set; }

        public bool IsActual { get; set; } = true;
    }
}
