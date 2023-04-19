using Librarian.DAL.Entities.Base;

namespace Librarian.DAL.Entities
{
    public class Category : NamedEntity
    {
        public virtual IEnumerable<Book?>? Books { get; set; }
    }
}
