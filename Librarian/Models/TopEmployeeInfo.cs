using Librarian.DAL.Entities;

namespace Librarian.Models
{
    public class TopEmployeeInfo
    {
        public Employee? Employee { get; set; }

        public int SalesCount { get; set; }

        public decimal SalesAmount { get; set; }
    }
}
