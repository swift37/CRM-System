using Librarian.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.DAL.Entities
{
    public class OrderDetails : Entity
    {
        public virtual Order? Order { get; set; }
        
        public virtual Product? Product { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal Discount { get; set; }

        public decimal Amount { get; set; }

        public bool IsActual { get; set; }
    }
}
