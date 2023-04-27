using System.Collections.ObjectModel;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
        {
            return new ObservableCollection<T>(items);
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> items)
        {
            return new ObservableCollection<T>(items);
        }
    }
}
