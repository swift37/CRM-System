using Librarian.DAL.Entities;

namespace Librarian.Models
{
    public class TopCategoryInfo
    {
        public Category? Category { get; set; }

        public int SalesCount { get; set; }

        public decimal SalesAmount { get; set; }
    }
}
