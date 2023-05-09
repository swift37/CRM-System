using Librarian.DAL.Entities.Base;
using System.Reflection.Metadata;

namespace Librarian.DAL.Entities
{
    public class Seller : Person
    {
        public DateTime DeteOfBirth { get; set; }

        public string? ContactNumber { get; set; }

        public string? ContactMail { get; set; }

        public string? IndeidentityDocumentNumber { get; set; }

        public string? WorkingRate { get; set; }

        public override string ToString() => $"Seller {Name} {Surname} {Patronymic}";
    }
}
