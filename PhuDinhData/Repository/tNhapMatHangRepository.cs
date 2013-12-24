using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tNhapMatHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tNhapMatHang, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<tNhapMatHang> GetData(PhuDinhEntities context,
            Expression<Func<tNhapMatHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<tNhapMatHang>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<tNhapMatHang> GetData(PhuDinhEntities context, Expression<Func<tNhapMatHang, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<tNhapMatHang>.ChangedItemData> Save(PhuDinhEntities context, List<tNhapMatHang> data, List<tNhapMatHang> origData)
        {
            return Repository<tNhapMatHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<tNhapMatHang> GetDataQuery(PhuDinhEntities context
            , Expression<Func<tNhapMatHang, bool>> filter)
        {
            return Repository<tNhapMatHang>.GetData(context, filter).OrderByDescending(p => p.Ngay);
        }
    }
}
