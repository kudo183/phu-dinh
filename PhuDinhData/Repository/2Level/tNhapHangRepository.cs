using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tNhapHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tNhapHang, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<tNhapHang> GetData(PhuDinhEntities context,
            Expression<Func<tNhapHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<tNhapHang>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<tNhapHang> GetData(PhuDinhEntities context, Expression<Func<tNhapHang, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<tNhapHang>.ChangedItemData> Save(PhuDinhEntities context, List<tNhapHang> data, List<tNhapHang> origData)
        {
            return Repository<tNhapHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<tNhapHang> GetDataQuery(PhuDinhEntities context
            , Expression<Func<tNhapHang, bool>> filter)
        {
            return Repository<tNhapHang>.GetData(context, filter).OrderByDescending(p => p.Ngay);
        }
    }
}
