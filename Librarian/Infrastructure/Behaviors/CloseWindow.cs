using Librarian.Tools;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Librarian.Infrastructure.Behaviors
{
    public class CloseWindow : Behavior<RadioButton>
    {
        protected override void OnAttached() => AssociatedObject.Click += OnButtonClick;

        protected override void OnDetaching() => AssociatedObject.Click -= OnButtonClick;

        private void OnButtonClick(object sender, RoutedEventArgs e) => 
            (AssociatedObject.FindVisualRoot() as Window)?.Close();

    }
}
