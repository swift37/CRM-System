using CRM.DAL.Entities.Base;
using CRM.Interfaces;

namespace CRM.DAL.Entities
{
    public class SupplyDetails : Entity, IArchivable
    {
        public int SupplyId { get; set; }

        public virtual Supply? Supply { get; set; }

        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public bool IsActual { get; set; } = true;
    }
}
