using Librarian.DAL.Entities.Base;
using Librarian.Interfaces;

namespace Librarian.DAL.Entities
{
    public class Product : NamedEntity, IArchivable
    {
        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        public int SupplierId { get; set; }

        public virtual Supplier? Supplier { get; set; }

        public virtual IEnumerable<OrderDetails>? OrderDetails { get; set; }

        public virtual IEnumerable<SupplyDetails>? SupplyDetails { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public int UnitsOnOrder { get; set; }

        public bool IsActual { get; set; } = true;

        public override string ToString() => $"{Name}";
    }
}
