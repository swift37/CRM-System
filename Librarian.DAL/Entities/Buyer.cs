using Librarian.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librarian.DAL.Entities
{
    public class Buyer : Person
    {
        public string? ContactNumber { get; set; }

        public string? ContactMail { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CashbackBalance { get; set; }

        public override string ToString() => $"{Name} {Surname}";
    }
}
