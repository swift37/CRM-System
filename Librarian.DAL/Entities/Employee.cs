using Librarian.DAL.Entities.Base;
using Librarian.Interfaces;

namespace Librarian.DAL.Entities
{
    public class Employee : Person, IArchivable
    {
        public string? Login { get; set; }

        public string? Password { get; set; }

        public int PermissionLevel { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime HireDate { get; set; }

        public string? Title { get; set; }

        public string? ContactNumber { get; set; }

        public string? ContactMail { get; set; }

        public string? Address { get; set; }

        public string? IdentityDocumentNumber { get; set; }

        public DateTime? Extension { get; set; }

        public WorkingRate? WorkingRate { get; set; }

        public bool IsActual { get; set; } = true;

        public override string ToString() => $"{Name} {Surname}";
    }
}
