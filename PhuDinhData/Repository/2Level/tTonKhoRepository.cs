using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tTonKhoRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tTonKho, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<tTonKho> GetData(PhuDinhEntities context,
            Expression<Func<tTonKho, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<tTonKho>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<tTonKho> GetData(PhuDinhEntities context, Expression<Func<tTonKho, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<tTonKho>.ChangedItemData> Save(PhuDinhEntities context, List<tTonKho> data, List<tTonKho> origData)
        {
            return Repository<tTonKho>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<tTonKho> GetDataQuery(PhuDinhEntities context
            , Expression<Func<tTonKho, bool>> filter)
        {
            return Repository<tTonKho>.GetData(context, filter).OrderByDescending(p => p.Ngay);
        }
    }
}
