using Librarian.DAL.Entities.Base;

namespace Librarian.DAL.Entities
{
    public class Category : NamedEntity
    {
        public virtual ICollection<Product?>? Products { get; set; } = new HashSet<Product?>();

        public override string ToString() => $"{Name}";
    }
}
