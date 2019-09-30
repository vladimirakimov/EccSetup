using System.Collections;
using System.Linq;

namespace ITG.Brix.EccSetup.Application.Extensions
{
    public static class ObjectExtension
    {
        public static string[] ToStringArray(this object arg)
        {
            var collection = arg as IEnumerable;
            if (collection != null)
            {
                return collection.Cast<object>().Select(x => x.ToString()).ToArray();
            }

            if (arg == null)
            {
                return new string[] { };
            }

            return new string[] { arg.ToString() };
        }
    }
}
