using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rNuocRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rNuoc, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<rNuoc> GetData(PhuDinhEntities context,
            Expression<Func<rNuoc, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<rNuoc>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<rNuoc> GetData(PhuDinhEntities context, Expression<Func<rNuoc, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<rNuoc>.ChangedItemData> Save(PhuDinhEntities context, List<rNuoc> data, List<rNuoc> origData)
        {
            return Repository<rNuoc>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<rNuoc> GetDataQuery(PhuDinhEntities context
            , Expression<Func<rNuoc, bool>> filter)
        {
            return Repository<rNuoc>.GetData(context, filter).OrderBy(p => p.TenNuoc);
        }
    }
}
