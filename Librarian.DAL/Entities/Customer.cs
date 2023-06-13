using Librarian.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librarian.DAL.Entities
{
    public class Customer : Person
    {
        public string? ContactName { get; set; }

        public string? ContactTitle { get; set; }

        public string? ContactNumber { get; set; }

        public string? ContactMail { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CashbackBalance { get; set; }

        public string? Address { get; set; }

        public bool IsActual { get; set; }

        public override string ToString() => $"{Name} {Surname}";
    }
}
