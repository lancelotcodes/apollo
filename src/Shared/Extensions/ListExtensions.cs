using System.Collections.Generic;
using System.Linq;

namespace Shared.Extensions
{
    public static class ListExtensions
    {
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }

        public static bool IsAny<T>(this IList<T> data)
        {
            return data != null && data.Any();
        }
    }
}
