using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
    //todo: Перенести ObservableCollectionExtensions в пакет
    public static class ObservableCollectionExtensions
    {
        public static void Add<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
                collection.Add(item);
        }

        public static void ClearAdd<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            collection.Clear();
            collection.Add(items);
        }
    }
}
