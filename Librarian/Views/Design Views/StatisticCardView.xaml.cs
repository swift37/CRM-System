using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Librarian.Views
{    
    public partial class StatisticCardView : UserControl
    {
        public StatisticCardView() => InitializeComponent();

        #region ChartDataSourceProperty
        /// <summary>
        /// Chart Data Source
        /// </summary>
        public static readonly DependencyProperty ChartDataSourceProperty =
            DependencyProperty.Register(
                "ChartDataSource",
                typeof(double[]),
                typeof(InfoCardView),
                new PropertyMetadata(default(double[])));

        /// <summary>
        /// Chart Data Source
        /// </summary>
        public double[] ChartDataSource { get => (double[])GetValue(ChartDataSourceProperty); set => SetValue(ChartDataSourceProperty, value); }
        #endregion

        #region AxisXLabelsProperty
        /// <summary>
        /// Axis X Labels (Max. 12 values, to specify more values, modifications to the view template are required)
        /// </summary>
        public static readonly DependencyProperty AxisXLabelsProperty =
            DependencyProperty.Register(
                "AxisXLabels",
                typeof(double[]),
                typeof(InfoCardView),
                new PropertyMetadata(default(double[])));

        /// <summary>
        /// Axis X Labels (Max. 12 values, to specify more values, modifications to the view template are required)
        /// </summary>
        public double[] AxisXLabels { get => (double[])GetValue(AxisXLabelsProperty); set => SetValue(AxisXLabelsProperty, value); }
        #endregion

        #region AxisYLabelsProperty
        /// <summary>
        /// Axis Y Labels (Max. 7 values, to specify more values, modifications to the view template are required)
        /// </summary>
        public static readonly DependencyProperty AxisYLabelsProperty =
            DependencyProperty.Register(
                "AxisYLabels",
                typeof(double[]),
                typeof(InfoCardView),
                new PropertyMetadata(default(double[])));

        /// <summary>
        /// Axis Y Labels (Max. 7 values, to specify more values, modifications to the view template are required)
        /// </summary>
        public double[] AxisYLabels { get => (double[])GetValue(AxisYLabelsProperty); set => SetValue(AxisYLabelsProperty, value); }
        #endregion

        #region AxisYMaxValueProperty
        /// <summary>
        /// Axis Y MaxValue
        /// </summary>
        public static readonly DependencyProperty AxisYMaxValueProperty =
            DependencyProperty.Register(
                "AxisYMaxValue",
                typeof(double),
                typeof(InfoCardView),
                new PropertyMetadata(default(double)));

        /// <summary>
        /// Axis Y MaxValue
        /// </summary>
        public double AxisYMaxValue { get => (double)GetValue(AxisYMaxValueProperty); set => SetValue(AxisYMaxValueProperty, value); }
        #endregion

    }
}
