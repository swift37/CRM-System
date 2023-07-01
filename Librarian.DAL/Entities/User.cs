using Librarian.DAL.Entities.Base;
using Librarian.Interfaces;

namespace Librarian.DAL.Entities
{
    public class User : Entity, IArchivable
    {
        public string? Login { get; set; }

        public string? Password { get; set; }

        public int PermissionLevel { get; set; }

        public bool IsActual { get; set; } = true;
    }
}
