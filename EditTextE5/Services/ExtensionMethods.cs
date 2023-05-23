using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditTextE5.Services
{
    public static class ExtensionMethods
    {
        public static void Replace<T>(this List<T> list, T oldItem, T newItem)
        {
            var oldItemIndex = list.IndexOf(oldItem);
            list[oldItemIndex] = newItem;
        }
    }
}
