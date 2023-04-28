using Librarian.DAL.Entities;

namespace Librarian.Services.Interfaces
{
    public interface IUserDialogService
    {
        bool EditBook(Book book);

        bool Confirmation(string message, string caption);

        void Warning(string message, string caption);

        void Error(string message, string caption);
    }
}
