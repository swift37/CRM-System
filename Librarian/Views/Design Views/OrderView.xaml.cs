using FontAwesome.Sharp;
using System.Windows;
using System.Windows.Controls;

namespace Librarian.Views
{
    public partial class OrderView : UserControl
    {
        public OrderView() => InitializeComponent();

        #region TitleProperty
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(OrderView),
                new PropertyMetadata(default(string)));

        public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
        #endregion

        #region DescProperty
        public static readonly DependencyProperty DescProperty =
            DependencyProperty.Register(
                "Desc",
                typeof(string),
                typeof(OrderView),
                new PropertyMetadata(default(string)));

        public string Desc { get => (string)GetValue(DescProperty); set => SetValue(DescProperty, value); }
        #endregion

        #region IconProperty
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                "Icon",
                typeof(IconChar),
                typeof(OrderView),
                new PropertyMetadata(default(IconChar)));

        public IconChar Icon { get => (IconChar)GetValue(IconProperty); set => SetValue(IconProperty, value); }
        #endregion
    }
}
