using Swftx.Wpf.ViewModels;

namespace Librarian.ViewModels
{
    public class PasswordCreatorViewModel : ViewModel
    {
        #region Password
        private string? _Password;

        /// <summary>
        /// Password
        /// </summary>
        public string? Password { get => _Password; set => Set(ref _Password, value); }
        #endregion

        public PasswordCreatorViewModel() 
        {
            if (App.IsDesignMode)
               InitProps("password");
        }

        public void InitProps(string? password)
        {
            Password = password;
        }
    }
}
