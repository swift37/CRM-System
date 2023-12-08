using System.Windows.Media;
using System.Windows;

namespace CRM.Tools
{
    public static class DependencyObjectExtensions
    {
        public static DependencyObject FindVisualRoot(this DependencyObject dependencyObject)
        {
            do
            {
                var parent = VisualTreeHelper.GetParent(dependencyObject);
                if (parent is null) return dependencyObject;
                dependencyObject = parent;
            }
            while (true);
        }

        public static DependencyObject FindLogicalRoot(this DependencyObject dependencyObject)
        {
            do
            {
                var parent = LogicalTreeHelper.GetParent(dependencyObject);
                if (parent is null) return dependencyObject;
                dependencyObject = parent;
            }
            while (true);
        }

        public static T? FindVisualParent<T>(this DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject is null) return null;
            var parent = dependencyObject;
            do
            {
                parent = VisualTreeHelper.GetParent(dependencyObject);

            } while (parent != null && !(parent is T));
            return parent as T;
        }

        public static T? FindLogicalParent<T>(this DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject is null) return null;
            var parent = dependencyObject;
            do
            {
                parent = LogicalTreeHelper.GetParent(dependencyObject);

            } while (parent != null && !(parent is T));
            return parent as T;
        }
    }
}
