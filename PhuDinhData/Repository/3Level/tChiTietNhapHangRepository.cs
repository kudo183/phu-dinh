using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChiTietNhapHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tChiTietNhapHang, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<tChiTietNhapHang> GetData(PhuDinhEntities context,
            Expression<Func<tChiTietNhapHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<tChiTietNhapHang>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<tChiTietNhapHang> GetData(PhuDinhEntities context, Expression<Func<tChiTietNhapHang, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<tChiTietNhapHang>.ChangedItemData> Save(PhuDinhEntities context, List<tChiTietNhapHang> data, List<tChiTietNhapHang> origData)
        {
            var result = Repository<tChiTietNhapHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));

            TonKhoManager.UpdateByChiTietNhapHang(result);

            return result;
        }

        private static IQueryable<tChiTietNhapHang> GetDataQuery(PhuDinhEntities context
            , Expression<Func<tChiTietNhapHang, bool>> filter)
        {
            return Repository<tChiTietNhapHang>.GetData(context, filter).OrderByDescending(p => p.tNhapHang.Ngay);
        }
    }
}
