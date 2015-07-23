using System;
using System.Reflection;

namespace Common
{
    public static class PropertyInfoEx
    {
        public static bool IsVirtual(this PropertyInfo self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            bool? found = null;

            foreach (MethodInfo method in self.GetAccessors())
            {
                if (found.HasValue)
                {
                    if (found.Value != method.IsVirtual)
                    {
                        found = null;
                        break;
                    }
                }
                else
                {
                    found = method.IsVirtual;
                }
            }

            if (found == null)
                return false;

            return found.Value;
        }
    }
}
