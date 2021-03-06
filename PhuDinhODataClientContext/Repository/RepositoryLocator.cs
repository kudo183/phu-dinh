﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PhuDinhODataClientContext.Repository
{
    public class RepositoryLocator<T> where T : class
    {
        private const string Pattern = "PhuDinhODataClientContext.Repository.{0}Repository";

        public static IQueryable<T> GetDataQuery(IQueryable<T> query)
        {
            var types = new Type[] { typeof(IQueryable<T>) };
            var method = GetMethod(MethodBase.GetCurrentMethod().Name, types);
            return method.Invoke(null, new object[] { query }) as IQueryable<T>;
        }

        //public static IQueryable<T> GetReferenceDataQuery(IQueryable<T> query)
        //{
        //    var types = new Type[] { typeof(IQueryable<T>) };
        //    var method = GetMethod(MethodBase.GetCurrentMethod().Name, types);
        //    if (method == null)
        //        return query;
        //    return method.Invoke(null, new object[] { query }) as IQueryable<T>;
        //}

        private static MethodInfo GetMethod(string methodName, Type[] types)
        {
            var t = Type.GetType(string.Format(Pattern, typeof(T).Name));
            var method = t.GetMethod(methodName, types);
            return method;
        }
    }
}
