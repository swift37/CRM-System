using Librarian.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librarian.DAL.Entities
{
    public class Book : NamedEntity
    {
        public virtual Category? Category { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public override string ToString() => $"{Name}";
    }
}
