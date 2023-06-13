using Librarian.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librarian.DAL.Entities
{
    public class Product : NamedEntity
    {
        public virtual Category? Category { get; set; }

        public virtual Supplier? Supplier { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public int UnitsInEnterprise { get; set; }

        public int UnitsOnOrder { get; set; }

        public bool IsActual { get; set; }

        public override string ToString() => $"{Name}";
    }
}
