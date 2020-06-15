using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Utils
{
    public class Coppier<TSource, TDest> where TSource : class
                                             where TDest : class
    {
        public static void Copy(TSource source, TDest dest)
        {
            var parentProperties = source.GetType().GetProperties();
            var childProperties = dest.GetType().GetProperties();

            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                    {
                        childProperty.SetValue(dest, parentProperty.GetValue(source));
                        break;
                    }
                }
            }
        }
    }
}
