using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tMatHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tMatHang, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<tMatHang> GetData(PhuDinhEntities context,
            Expression<Func<tMatHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<tMatHang>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<tMatHang> GetData(PhuDinhEntities context, Expression<Func<tMatHang, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<tMatHang>.ChangedItemData> Save(PhuDinhEntities context, List<tMatHang> data, List<tMatHang> origData)
        {
            return Repository<tMatHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<tMatHang> GetDataQuery(PhuDinhEntities context
            , Expression<Func<tMatHang, bool>> filter)
        {
            return Repository<tMatHang>.GetData(context, filter).OrderBy(p => p.TenMatHang);
        }
    }
}
