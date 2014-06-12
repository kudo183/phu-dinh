using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChiTietDonHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tChiTietDonHang, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<tChiTietDonHang> GetData(PhuDinhEntities context,
            Expression<Func<tChiTietDonHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<tChiTietDonHang>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<tChiTietDonHang> GetData(PhuDinhEntities context, Expression<Func<tChiTietDonHang, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<tChiTietDonHang>.ChangedItemData> Save(PhuDinhEntities context, List<tChiTietDonHang> data, List<tChiTietDonHang> origData)
        {
            var changed = Repository<tChiTietDonHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));

            TonKhoManager.UpdateByChiTietDonHang(changed);

            return changed;
        }

        private static IQueryable<tChiTietDonHang> GetDataQuery(PhuDinhEntities context
            , Expression<Func<tChiTietDonHang, bool>> filter)
        {
            return Repository<tChiTietDonHang>.GetData(context, filter).OrderByDescending(p => p.tDonHang.Ngay);
        }
    }
}
