using FontAwesome.Sharp;
using System.Windows;
using System.Windows.Controls;

namespace CRM.Views
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

        #region Desc_1Property
        public static readonly DependencyProperty Desc_1Property =
            DependencyProperty.Register(
                "Desc_1",
                typeof(string),
                typeof(OrderView),
                new PropertyMetadata(default(string)));

        public string Desc_1 { get => (string)GetValue(Desc_1Property); set => SetValue(Desc_1Property, value); }
        #endregion

        #region Desc_2Property
        public static readonly DependencyProperty Desc_2Property =
            DependencyProperty.Register(
                "Desc_2",
                typeof(string),
                typeof(OrderView),
                new PropertyMetadata(default(string)));

        public string Desc_2 { get => (string)GetValue(Desc_2Property); set => SetValue(Desc_2Property, value); }
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
