using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rMatHangNguyenLieuRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rMatHangNguyenLieu, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<rMatHangNguyenLieu> GetData(PhuDinhEntities context,
            Expression<Func<rMatHangNguyenLieu, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<rMatHangNguyenLieu>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<rMatHangNguyenLieu> GetData(PhuDinhEntities context, Expression<Func<rMatHangNguyenLieu, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<rMatHangNguyenLieu>.ChangedItemData> Save(PhuDinhEntities context, List<rMatHangNguyenLieu> data, List<rMatHangNguyenLieu> origData)
        {
            return Repository<rMatHangNguyenLieu>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<rMatHangNguyenLieu> GetDataQuery(PhuDinhEntities context
            , Expression<Func<rMatHangNguyenLieu, bool>> filter)
        {
            return Repository<rMatHangNguyenLieu>.GetData(context, filter).OrderByDescending(p => p.MaMatHang);
        }
    }
}
