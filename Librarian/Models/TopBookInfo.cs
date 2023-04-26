using Librarian.DAL.Entities;

namespace Librarian.Models
{
    public class TopBookInfo
    {
        public Book? Book { get; set; }

        public int TransactionsCount { get; set; }
    }
}
