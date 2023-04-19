using Librarian.DAL.Entities.Base;

namespace Librarian.DAL.Entities
{
    public class Book : NamedEntity
    {
        public virtual Category? Category { get; set; }
    }
}
