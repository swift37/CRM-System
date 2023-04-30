using Librarian.DAL.Entities.Base;

namespace Librarian.DAL.Entities
{
    public class Category : NamedEntity
    {
        public virtual ICollection<Book?>? Books { get; set; } = new HashSet<Book?>();

        public override string ToString() => $"{Name}";
    }
}
