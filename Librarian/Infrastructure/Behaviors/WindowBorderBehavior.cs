using Microsoft.Xaml.Behaviors;
using System.Windows.Input;
using System.Windows;
using Librarian.Tools;

namespace Librarian.Infrastructure.Behaviors
{
    class WindowTitleBarBehavior : Behavior<UIElement>
    {
        protected override void OnAttached() => AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;

        protected override void OnDetaching() => AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                (AssociatedObject.FindVisualRoot() as Window)?.DragMove();
            }
            else
            {
                if (!(AssociatedObject.FindVisualRoot() is Window window)) return;

                window.WindowState = window.WindowState switch
                {
                    WindowState.Normal => WindowState.Maximized,
                    WindowState.Maximized => WindowState.Normal,
                    _ => window.WindowState
                };
            }

        }
    }
}
