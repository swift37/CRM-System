using FontAwesome.Sharp;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CRM.Views
{
    public partial class InfoCardSmallView : UserControl
    {
        public InfoCardSmallView() => InitializeComponent();

        #region TitleProperty
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(InfoCardSmallView),
                new PropertyMetadata(default(string)));
        public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
        #endregion

        #region TitleAddProperty
        public static readonly DependencyProperty TitleAddProperty =
            DependencyProperty.Register(
                "TitleAdd",
                typeof(string),
                typeof(InfoCardSmallView),
                new PropertyMetadata(default(string)));
        public string TitleAdd { get => (string)GetValue(TitleAddProperty); set => SetValue(TitleAddProperty, value); }
        #endregion

        #region NumberProperty
        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register(
                "Number",
                typeof(string),
                typeof(InfoCardSmallView),
                new PropertyMetadata(default(string)));
        public string Number { get => (string)GetValue(NumberProperty); set => SetValue(NumberProperty, value); }
        #endregion

        #region IconProperty
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                "Icon",
                typeof(IconChar),
                typeof(InfoCardSmallView),
                new PropertyMetadata(default(IconChar)));
        public IconChar Icon { get => (IconChar)GetValue(IconProperty); set => SetValue(IconProperty, value); }
        #endregion

        #region Background_1Property
        public static readonly DependencyProperty Background_1Property =
            DependencyProperty.Register(
                "Background_1",
                typeof(Color),
                typeof(InfoCardSmallView),
                new PropertyMetadata(default(Color)));
        public Color Background_1 { get => (Color)GetValue(Background_1Property); set => SetValue(Background_1Property, value); }
        #endregion

        #region Background_2Property
        public static readonly DependencyProperty Background_2Property =
            DependencyProperty.Register(
                "Background_2",
                typeof(Color),
                typeof(InfoCardSmallView),
                new PropertyMetadata(default(Color)));
        public Color Background_2 { get => (Color)GetValue(Background_2Property); set => SetValue(Background_2Property, value); }
        #endregion

        #region EllipseBackground_1Property
        public static readonly DependencyProperty EllipseBackground_1Property =
            DependencyProperty.Register(
                "EllipseBackground_1",
                typeof(Color),
                typeof(InfoCardSmallView),
                new PropertyMetadata(default(Color)));
        public Color EllipseBackground_1 { get => (Color)GetValue(EllipseBackground_1Property); set => SetValue(EllipseBackground_1Property, value); }
        #endregion

        #region EllipseBackground_2Property
        public static readonly DependencyProperty EllipseBackground_2Property =
            DependencyProperty.Register(
                "EllipseBackground_2",
                typeof(Color),
                typeof(InfoCardSmallView),
                new PropertyMetadata(default(Color)));
        public Color EllipseBackground_2 { get => (Color)GetValue(EllipseBackground_2Property); set => SetValue(EllipseBackground_2Property, value); }
        #endregion
    }
}
