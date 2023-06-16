using Librarian.DAL.Entities;

namespace Librarian.Models
{
    public class TopProductsInfo
    {
        public Product? Product { get; set; }

        public int SalesCount { get; set; }

        public decimal SalesAmount { get; set; }
    }
}
