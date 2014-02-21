using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tDonHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tDonHang, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<tDonHang> GetData(PhuDinhEntities context,
            Expression<Func<tDonHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<tDonHang>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<tDonHang> GetData(PhuDinhEntities context, Expression<Func<tDonHang, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<tDonHang>.ChangedItemData> Save(PhuDinhEntities context, List<tDonHang> data, List<tDonHang> origData)
        {
            var result = Repository<tDonHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));

            TonKhoManager.UpdateByDonHang(result);

            return result;
        }

        private static IQueryable<tDonHang> GetDataQuery(PhuDinhEntities context
            , Expression<Func<tDonHang, bool>> filter)
        {
            return Repository<tDonHang>.GetData(context, filter).OrderByDescending(p => p.Ngay);
        }
    }
}
