using Librarian.DAL.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librarian.DAL.Entities
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public OrderDetails? OrderDetails { get; set; }

        public virtual Product? Product { get; set; }

        public virtual Employee? Employee { get; set; }

        public virtual Customer? Customer { get; set; }

        public Shipper? ShipVia { get; set; }

        public decimal ShippingCost  { get; set; }

        public string? ShipName { get; set; }

        public string? ShipAddress { get; set; }

        public bool IsActual { get; set; }

        public override string ToString() =>
            $"[{OrderDate}]: Transaction for the sale of the {Product?.Category} {Product}: {Employee}, {Customer}";
    }
}
