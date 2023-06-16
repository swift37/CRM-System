using Librarian.DAL.Entities;

namespace Librarian.Models
{
    public class TopProductsInfo
    {
        public Product? Product { get; set; }

        public int OrdersCount { get; set; }

        public decimal OrdersAmount { get; set; }
    }
}
