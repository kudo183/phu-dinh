using System;
using System.Linq;
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

        public static bool IsEntityProperty(this PropertyInfo self)
        {
            if (self.CanRead == false || self.CanWrite == false)
                return false;

            if (self.IsVirtual() == true)
                return true;

            if (PrimitiveTypes.Test(self.PropertyType) == true)
                return true;

            return false;
        }

        private static class PrimitiveTypes
        {
            private static readonly Type[] List;

            static PrimitiveTypes()
            {
                var types = new[]
                          {
                              typeof (Enum),
                              typeof (String),
                              typeof (Char),
                              typeof (Guid),

                              typeof (Boolean),
                              typeof (Byte),
                              typeof (Int16),
                              typeof (Int32),
                              typeof (Int64),
                              typeof (Single),
                              typeof (Double),
                              typeof (Decimal),

                              typeof (SByte),
                              typeof (UInt16),
                              typeof (UInt32),
                              typeof (UInt64),

                              typeof (DateTime),
                              typeof (DateTimeOffset),
                              typeof (TimeSpan),
                          };


                var nullTypes = from t in types
                                where t.IsValueType
                                select typeof(Nullable<>).MakeGenericType(t);

                List = types.Concat(nullTypes).ToArray();
            }

            public static bool Test(Type type)
            {
                if (List.Any(x => x.IsAssignableFrom(type)))
                    return true;

                var nut = Nullable.GetUnderlyingType(type);
                return nut != null && nut.IsEnum;
            }
        }
    }
}
