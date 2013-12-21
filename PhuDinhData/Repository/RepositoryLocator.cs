using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace PhuDinhData.Repository
{
    public class RepositoryLocator<T> where T : class
    {
        private const string pattern = "PhuDinhData.Repository.{0}Repository";

        public static int GetDataCount(PhuDinhEntities context, Expression<Func<T, bool>> filter)
        {
            var types = new Type[] { typeof(PhuDinhEntities), typeof(Expression<Func<T, bool>>) };
            var method = GetMethod(MethodBase.GetCurrentMethod().Name, types);
            return Convert.ToInt32(method.Invoke(null, new object[] { context, filter }));
        }

        public static List<T> GetData(PhuDinhEntities context, Expression<Func<T, bool>> filter)
        {
            var types = new Type[] { typeof(PhuDinhEntities), typeof(Expression<Func<T, bool>>) };
            var method = GetMethod(MethodBase.GetCurrentMethod().Name, types);
            return method.Invoke(null, new object[] { context, filter }) as List<T>;
        }

        public static List<Repository<T>.ChangedItemData> Save(
            PhuDinhEntities context, List<T> data, List<T> origData)
        {
            var types = new Type[] { typeof(PhuDinhEntities), typeof(List<T>), typeof(List<T>) };
            var method = GetMethod(MethodBase.GetCurrentMethod().Name, types);
            return method.Invoke(null, new object[] { context, data, origData }) as List<Repository<T>.ChangedItemData>;
        }

        private static MethodInfo GetMethod(string methodName, Type[] types)
        {
            var t = Type.GetType(string.Format(pattern, typeof(T).Name));
            var method = t.GetMethod(methodName, types);
            return method;
        }
    }
}
