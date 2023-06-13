using Librarian.DAL.Entities;

namespace Librarian.Models
{
    public class TopBookInfo
    {
        public Product? Book { get; set; }

        public int TransactionsCount { get; set; }

        public decimal TransactionsAmount { get; set; }
    }
}
