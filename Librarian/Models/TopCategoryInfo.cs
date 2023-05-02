using Librarian.DAL.Entities;

namespace Librarian.Models
{
    public class TopCategoryInfo
    {
        public Category? Category { get; set; }

        public int TransactionsCount { get; set; }

        public decimal TransactionsAmount { get; set; }
    }
}
