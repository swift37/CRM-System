using Librarian.DAL.Entities.Base;

namespace Librarian.DAL.Entities
{
    public class OrderDetails : Entity
    {
        public int OrderId { get; set; }
        
        public virtual Order? Order { get; set; }

        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal Discount { get; set; }

        public bool IsActual { get; set; } = true;
    }
}
