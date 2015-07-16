using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PhuDinhEFClientContext.Repository
{
    public class RepositoryLocator<T> where T : class
    {
        private const string Pattern = "PhuDinhEFClientContext.Repository.{0}Repository";

        public static IQueryable<T> GetDataQuery(IQueryable<T> query)
        {
            var types = new Type[] { typeof(IQueryable<T>) };
            var method = GetMethod(MethodBase.GetCurrentMethod().Name, types);
            return method.Invoke(null, new object[] { query }) as IQueryable<T>;
        }

        public static IQueryable<T> GetDataQueryAndRelatedTables(IQueryable<T> query, ref List<string> relatedTables)
        {
            var types = new Type[] { typeof(IQueryable<T>), typeof(List<string>).MakeByRefType() };
            var method = GetMethod(MethodBase.GetCurrentMethod().Name, types);
            return method.Invoke(null, new object[] { query, relatedTables }) as IQueryable<T>;
        }

        private static MethodInfo GetMethod(string methodName, Type[] types)
        {
            var t = Type.GetType(string.Format(Pattern, typeof(T).Name));
            var method = t.GetMethod(methodName, types);
            return method;
        }
    }
}
