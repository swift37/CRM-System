using Librarian.DAL.Entities.Base;

namespace Librarian.DAL.Entities
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public virtual Employee? Employee { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual IEnumerable<OrderDetails>? OrderDetails { get; set; }

        public decimal Amount { get; set; }

        public Shipper? ShipVia { get; set; }

        public decimal ShippingCost  { get; set; }

        public string? ShipName { get; set; }

        public string? ShipAddress { get; set; }

        public bool IsActual { get; set; } = true;

        public override string ToString() =>
            $"[{OrderDate}]: Transaction for the sale of the product: {Employee} - {Customer}, {ShipAddress}";
    }
}
