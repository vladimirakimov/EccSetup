using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.EccSetup.Application.Utils
{
    public static class Utils
    {
        public static bool IsEnumerableNullOrEmpty<T>(this IEnumerable<T> data)
        {
            return data == null || !data.Any();
        }
    }
}
