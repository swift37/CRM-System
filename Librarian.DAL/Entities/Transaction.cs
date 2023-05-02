using Librarian.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librarian.DAL.Entities
{
    public class Transaction : Entity
    {
        public DateTime? TransactionDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public decimal Discount { get; set; }

        public virtual Book? Book { get; set; }

        public virtual Seller? Seller { get; set; }

        public virtual Buyer? Buyer { get; set; }

        public override string ToString() => 
            $"[{TransactionDate}]: Transaction for the sale of the {Book?.Category} {Book}: {Seller}, {Buyer}, {Amount:C}";
    }
}
