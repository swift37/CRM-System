using Librarian.DAL.Entities.Base;
using Librarian.Interfaces;

namespace Librarian.DAL.Entities
{
    public class Supply : Entity, IArchivable
    {
        public DateTime SupplyDate { get; set; }

        public int SupplierId { get; set; }

        public Supplier? Supplier { get; set; }

        public ICollection<SupplyDetails>? SupplyDetails { get; set; }

        public decimal SupplyCost { get; set; }

        public int ProductsQuantity { get; set; }

        public bool IsActual { get; set; } = true;
    }
}
