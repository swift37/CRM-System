using Librarian.DAL.Entities.Base;

namespace Librarian.DAL.Entities
{
    public class Product : NamedEntity
    {
        public virtual Category? Category { get; set; }

        public virtual Supplier? Supplier { get; set; }

        public virtual IEnumerable<OrderDetails>? OrderDetails { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public int UnitsInEnterprise { get; set; }

        public bool IsActual { get; set; } = true;

        public override string ToString() => $"{Name}";
    }
}
