using System.Windows.Input;

namespace Librarian
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
