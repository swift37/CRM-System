using Librarian.Interfaces;

namespace Librarian.DAL.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
